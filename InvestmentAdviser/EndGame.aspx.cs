using InvestmentAdviser.Enums;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace InvestmentAdviser
{
    public partial class EndGame : System.Web.UI.Page
    {
        public const double PointsPerCent = 25;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameStopwatch.Stop();
                
                int prizePoints = Common.GetTotalPrizePoints(ScenarioTurns);

                TotalPrizePointsLbl.Text = prizePoints.ToString("");

                BonusLbl.Text = Math.Round(prizePoints / PointsPerCent, 2).ToString("0.0") + " cents";

                dbHandler.UpdateTimesTable(GameState.EndGame);
            }
        }

        private double GetBonus()
        {
            var dollars = Common.GetTotalPrizePoints(ScenarioTurns) / PointsPerCent;
            var cents = dollars / 100;
            return Math.Round(cents, 2);
        }

        protected void rewardBtn_Click(object sender, EventArgs e)
        {
            string workerId = UserId;

            dbHandler.UpdateTimesTable(GameState.CollectedPrize);

            string assignmentId = (string)Session["turkAss"];

            NameValueCollection data = new NameValueCollection();
            data.Add("assignmentId", assignmentId);
            data.Add("workerId", workerId);
            data.Add("hitId", (string)Session["hitId"]);

            double bonusAmount = GetBonus();
            decimal bonusDecimal = Convert.ToDecimal(bonusAmount);

            SendFeedback(bonusAmount);

            rewardBtn.Enabled = false;

            if (workerId != "friend")
            {
                IncreaseAskPositionCount();

                DisposeSession();

                Alert.RedirectAndPOST(this.Page, "https://www.mturk.com/mturk/externalSubmit", data);
            }
        }

        public void IncreaseAskPositionCount()
        {
            string nextAskPosition = null;

            switch (AskPosition)
            {
                case AskPositionHeuristic.First:
                    nextAskPosition = AskPositionHeuristic.Optimal.ToString();
                    break;
                case AskPositionHeuristic.Optimal:
                    nextAskPosition = AskPositionHeuristic.MonteCarlo.ToString();
                    break;
                case AskPositionHeuristic.MonteCarlo:
                    nextAskPosition = AskPositionHeuristic.Last.ToString();
                    break;
                case AskPositionHeuristic.Last:
                    nextAskPosition = AskPositionHeuristic.Random.ToString();
                    break;
                case AskPositionHeuristic.Random:
                    nextAskPosition = "Done";
                    break;
            }

            DbHandler dbHandler = new DbHandler();
            dbHandler.SetVectorNextAskPosition(nextAskPosition);
        }

        private void SendFeedback(double bonus)
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string feedback = feedbackTxtBox.Text;

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserFeedback (UserId, Feedback, TotalTime, TotalPrizePoints, Bonus) " + 
                        "VALUES (@UserId, @Feedback, @TotalTime, @TotalPrizePoints, @Bonus)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Feedback", feedback);
                        cmd.Parameters.AddWithValue("@TotalTime", Math.Round(GameStopwatch.Elapsed.TotalMinutes, 1));
                        cmd.Parameters.AddWithValue("@TotalPrizePoints", Common.GetTotalPrizePoints(ScenarioTurns));
                        cmd.Parameters.AddWithValue("@Bonus", bonus);
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();

                        sqlConnection1.Close();
                    }
                }
            }
            catch (SQLiteException)
            {
            }
        }

        private void DisposeSession()
        {
            if (dbHandler != null)
            {
                dbHandler.Dispose();
            }
            
            if (ScenarioTurns != null)
            {
                ScenarioTurns = null;
            }
        }
    }
}