using InvestmentAdviser.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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

            if (CurrentTurnNumber == Common.TotalInvestmentsTurns)
            {
                return true;
            }

            if (AskPosition == AskPositionHeuristic.First)
            {
                return (CurrentTurnNumber == 1);
            }

            if (AskPosition == AskPositionHeuristic.Last)
            {
                return (CurrentTurnNumber == Common.TotalInvestmentsTurns);
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
                int[] changes = new int[Common.TotalInvestmentsTurns];
                var stoppingPosition = -1;

                for (var turnIndex = 0; turnIndex < Common.TotalInvestmentsTurns; turnIndex++)
                {
                    if (ScenarioTurns[turnIndex].Played)
                    {
                        changes[turnIndex] = ScenarioTurns[turnIndex].Profit;
                        stoppingPosition++;
                    }
                    else
                    {
                        break;
                    }
                }

                var shouldAsk = AskMonteCarlo(changes, stoppingPosition);

                return shouldAsk;
            }

            return false;
        }

        private bool AskMonteCarlo(int[] changes, int stoppingPosition)
        {
            if (AlreadyPerformingMonteCarlo)
            {
                return false;
            }

            AlreadyPerformingMonteCarlo = true;

            bool shouldAsk = MonteCarlo.ShouldAsk(changes, stoppingPosition, new Random());

            AlreadyPerformingMonteCarlo = false;

            return shouldAsk;
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

                StringBuilder command = new StringBuilder();
                command.Append("INSERT INTO UserRatings (UserId, AdviserRating, ");

                for (int i = 1; i <= Common.TotalInvestmentsTurns; i++)
                {
                    command.Append("Turn" + i + ", ");
                }

                command.Append("TotalProfit, InstructionsTime, AskPosition, VectorNum, Reason) ");
                command.Append("VALUES (@UserId, @AdviserRating,");

                for (int i = 1; i <= Common.TotalInvestmentsTurns; i++)
                {
                    command.Append("@Turn" + i + ", ");
                }

                command.Append("@TotalProfit, @InstructionsTime, @AskPosition, @VectorNum, @Reason) ");

                using (SQLiteCommand cmd = new SQLiteCommand(command.ToString()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AdviserRating", adviserRating.ToString());

                    for (int i = 1; i <= Common.TotalInvestmentsTurns; i++)
                    {
                        cmd.Parameters.AddWithValue("@Turn" + i, GetTurnEarningToInsertToDb(i));
                    }

                    cmd.Parameters.AddWithValue("@TotalProfit", Common.GetTotalProfit(ScenarioTurns));

                    var instructionTime = GetInstructionsTime();

                    cmd.Parameters.AddWithValue("@InstructionsTime", instructionTime);
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    cmd.Parameters.AddWithValue("@VectorNum", VectorNum);
                    cmd.Parameters.AddWithValue("@Reason", reasonTxtBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetInstructionsTime()
        {
            if (InstructionsStopwatch == null)
            {
                return string.Empty;
            }

            return InstructionsStopwatch.Elapsed.TotalMinutes.ToString("0.00");
        }

        private string GetTurnEarningToInsertToDb(int turnIndex)
        {
            var turn = GetScenarioTurn(turnIndex);

            if (!turn.Played)
            {
                return string.Empty;
            }

            return turn.Profit.ToString();
        }
    }
}
