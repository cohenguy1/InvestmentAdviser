using InvestmentAdviser.Enums;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace InvestmentAdviser
{
    public partial class EndGame : System.Web.UI.Page
    {
        public const double VirtualDollarsPerCent = 30;

        public const double centToDollar = 1 / 100.0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var totalProfit = Common.GetTotalProfit(ScenarioTurns);

                TotalProfitLbl.Text = "$ " + totalProfit.ToString("");

                BonusLbl.Text = GetBonus(totalProfit).ToString("") + " cents";
            }
        }

        private int GetBonus(int totalProfit)
        {
            var cents = totalProfit / VirtualDollarsPerCent;
            return (int)Math.Round(cents, 0);
        }

        protected void rewardBtn_Click(object sender, EventArgs e)
        {
            string workerId = UserId;

            string assignmentId = (string)Session["turkAss"];

            NameValueCollection data = new NameValueCollection();
            data.Add("assignmentId", assignmentId);
            data.Add("workerId", workerId);
            data.Add("hitId", (string)Session["hitId"]);

            SendFeedback();

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

        private void SendFeedback()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string feedback = feedbackTxtBox.Text;

            var totalProfitCents = Common.GetTotalProfit(ScenarioTurns);

            var bonusDollars = GetBonus(totalProfitCents) * centToDollar;

            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserFeedback (UserId, Feedback, TotalProfit, Bonus) " +
                        "VALUES (@UserId, @Feedback, @TotalProfit, @Bonus)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Feedback", feedback);
                        cmd.Parameters.AddWithValue("@TotalProfit", totalProfitCents);
                        cmd.Parameters.AddWithValue("@Bonus", bonusDollars);
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