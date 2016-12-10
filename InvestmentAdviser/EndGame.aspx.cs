﻿using InvestmentAdviser.Enums;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace InvestmentAdviser
{
    public partial class EndGame : System.Web.UI.Page
    {
        public const double DollarsPerCent = 20;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameStopwatch.Stop();
                
                int totalProfit = Common.GetTotalProfit(ScenarioTurns);

                TotalProfitLbl.Text = totalProfit.ToString("") + " $";

                BonusLbl.Text = GetBonus().ToString("") + " cents";

                dbHandler.UpdateTimesTable(GameState.EndGame);
            }
        }

        private int GetBonus()
        {
            var cents = Common.GetTotalProfit(ScenarioTurns) / DollarsPerCent;
            return (int)Math.Round(cents, 0);
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

            int bonusAmount = GetBonus();

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

        private void SendFeedback(int bonus)
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string feedback = feedbackTxtBox.Text;

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserFeedback (UserId, Feedback, TotalTime, TotalProfit, Bonus) " +
                        "VALUES (@UserId, @Feedback, @TotalTime, @TotalProfit, @Bonus)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Feedback", feedback);
                        cmd.Parameters.AddWithValue("@TotalTime", Math.Round(GameStopwatch.Elapsed.TotalMinutes, 1));
                        cmd.Parameters.AddWithValue("@TotalProfit", Common.GetTotalProfit(ScenarioTurns));
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