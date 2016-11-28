using InvestmentAdviser.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {
        private bool NeedToAskRating()
        {
            if (AlreadyAskedForRating || GameMode != GameMode.Advisor)
            {
                return false;
            }

            if (CurrentTurnNumber == Common.NumOfTurns)
            {
                return true;
            }

            if (AskPosition == AskPositionHeuristic.First)
            {
                return (CurrentTurnNumber == 1);
            }

            if (AskPosition == AskPositionHeuristic.Last)
            {
                return (CurrentTurnNumber == Common.NumOfTurns);
            }

            if (AskPosition == AskPositionHeuristic.Random)
            {
                return (CurrentTurnNumber == RandomHuristicAskPosition);
            }

            if (AskPosition == AskPositionHeuristic.Optimal)
            {
                return Optimal.ShouldAsk(CurrentTurnNumber, GetCurrentTurn());
            }
            
            if (AskPosition == AskPositionHeuristic.MonteCarlo)
            {
                int[] accepted = new int[10];
                var stoppingPosition = -1;

                for (var index = 0; index < ScenarioTurns.Count; index++)
                {
                    stoppingPosition = CurrentTurnNumber;    
                }

                // calculated offline whether to stop for stopping position 0 & 1
                if (stoppingPosition == 0)
                {
                    return accepted[0] <= 3;
                }
                else if (stoppingPosition == 1)
                {
                    if (accepted[0] == 4)
                    {
                        return accepted[1] <= 3;
                    }
                    else if (accepted[0] == 5)
                    {
                        return accepted[1] <= 2;
                    }

                    // accepted[0] >= 6
                    return false;
                }

                bool shouldAsk = ShouldAsk(accepted, stoppingPosition, new Random());

                return shouldAsk;
            }

            return false;
        }

        protected void RateAdvisor()
        {
            TimerGame.Enabled = false;

            MultiView2.ActiveViewIndex = 1;

            dbHandler.UpdateTimesTable(GameState.Rate);
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            string userRating = ratingHdnValue.Value;

            if (string.IsNullOrEmpty(userRating) || userRating == "0")
            {
                return;
            }

            string reason = reasonTxtBox.Text;
            if (string.IsNullOrEmpty(reason.Trim()))
            {
                Alert.Show("Please explain your rating selection in the text box.");
                return;
            }

            int agentRating = int.Parse(userRating);

            SaveRatingToDB(agentRating);

            MultiView2.ActiveViewIndex = 0;

            dbHandler.UpdateTimesTable(GameState.AfterRate);

            TimerGame.Enabled = true;
        }

        private void SaveRatingToDB(int adviserRating)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                sqlConnection1.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserRatings (UserId, AdviserRating, Position1Rank, Position2Rank, " +
                    "Position3Rank, Position4Rank, Position5Rank, Position6Rank, Position7Rank, Position8Rank, Position9Rank, Position10Rank, TotalPrizePoints, " +
                    " InstructionsTime, AskPosition, VectorNum, Reason) " +
                    " VALUES (@UserId, @AdviserRating, @Position1Rank, @Position2Rank, @Position3Rank, @Position4Rank, " +
                    "@Position5Rank, @Position6Rank, @Position7Rank, @Position8Rank, @Position9Rank, @Position10Rank, @TotalPrizePoints, " +
                    "@InstructionsTime, @AskPosition, @VectorNum, @Reason)"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AdviserRating", adviserRating.ToString());
                    cmd.Parameters.AddWithValue("@Position1Rank", GetTurnProfitToInsertToDb(1));
                    cmd.Parameters.AddWithValue("@Position2Rank", GetTurnProfitToInsertToDb(2));
                    cmd.Parameters.AddWithValue("@Position3Rank", GetTurnProfitToInsertToDb(3));
                    cmd.Parameters.AddWithValue("@Position4Rank", GetTurnProfitToInsertToDb(4));
                    cmd.Parameters.AddWithValue("@Position5Rank", GetTurnProfitToInsertToDb(5));
                    cmd.Parameters.AddWithValue("@Position6Rank", GetTurnProfitToInsertToDb(6));
                    cmd.Parameters.AddWithValue("@Position7Rank", GetTurnProfitToInsertToDb(7));
                    cmd.Parameters.AddWithValue("@Position8Rank", GetTurnProfitToInsertToDb(8));
                    cmd.Parameters.AddWithValue("@Position9Rank", GetTurnProfitToInsertToDb(9));
                    cmd.Parameters.AddWithValue("@Position10Rank", GetTurnProfitToInsertToDb(10));
                    cmd.Parameters.AddWithValue("@TotalPrizePoints", Common.GetTotalPrizePoints(ScenarioTurns));
                    cmd.Parameters.AddWithValue("@InstructionsTime", Math.Round(InstructionsStopwatch.Elapsed.TotalMinutes, 2));
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    cmd.Parameters.AddWithValue("@VectorNum", VectorNum);
                    cmd.Parameters.AddWithValue("@Reason", reasonTxtBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetTurnProfitToInsertToDb(int turnIndex)
        {
            var turn = GetScenarioTurn(turnIndex);

            return turn.Profit == null ? null : turn.Profit.ToString();
        }
    }
}
