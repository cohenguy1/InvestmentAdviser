using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System;
using System.Linq;
using System.Text;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {
        public const int StartTimerInterval = 2500;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimerGame.Interval = StartTimerInterval;
                TimerGame.Enabled = false;

                AlreadyAskedForRating = false;
                AskForRating = false;

                MultiView2.ActiveViewIndex = 0;

                ClearTurnTable();

                ImageInterview.Visible = true;
                LabelInterviewing.Visible = true;

                CurrentTurnNumber = 1;

                StartInterviewsForPosition(0);
            }
        }

        protected void btnNextToQuiz_Click(object sender, EventArgs e)
        {
            Response.Redirect("Quiz.aspx");
        }

        private void StartInterviewsForPosition(int position)
        {
            TimerGame.Enabled = false;

            StatusLabel.Text = "";

            SetTitle();

            CleanCurrentScenarioTurn();

            CurrentTurnStatus = TurnStatus.Initial;

            TimerGame.Enabled = true;
        }


        private void SetTitle()
        {
            string turnTitle = GetCurrentJobTitle();
            var currentTurnNumber = CurrentTurnNumber;

            PositionHeader.Text = turnTitle;

            if (currentTurnNumber >= 1)
            {
                MovingToNextPositionLabel.Text = "Moving on to the next turn:" + "<br />" + "<br />";
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = turnTitle + "<br />" + "<br />" + "<br />";
                MovingJobTitleLabel.Visible = true;

                if (currentTurnNumber > 1 && currentTurnNumber <= Common.NumOfTurnsInTable)
                {
                    SetSeenTableRowStyle(currentTurnNumber - 1);
                }
            }

            // next turn
            if (currentTurnNumber <= Common.NumOfTurnsInTable)
            {
                SetTableRowStyle(currentTurnNumber);
            }
            else if (currentTurnNumber <= Common.NumOfTurns)
            {
                ShiftCells();

                UpdateNewRow(CurrentTurnNumber);
            }
        }


        protected void TimerGame_Tick(object sender, EventArgs e)
        {
            if (AskForRating)
            {
                RateAdvisor();

                AskForRating = false;
                AlreadyAskedForRating = true;

                return;
            }
            
            if (CurrentTurnStatus == TurnStatus.Initial)
            {
                CurrentTurnStatus = TurnStatus.Processing;
                LabelInterviewing.Visible = true;
                MovingToNextPositionLabel.Visible = false;
                MovingJobTitleLabel.Visible = false;
            }
            else if (CurrentTurnStatus == TurnStatus.Processing)
            {
                ProcessCandidate();
            }
        }

        private void FillNextPosition()
        {
            IncreaseCurrentPosition();

            StartInterviewsForPosition(CurrentTurnNumber);
        }

        private void CleanCurrentScenarioTurn()
        {
            TimerGame.Enabled = false;

            ClearInterviewImages();
        }

        private void ProcessCandidate()
        {
            if (!TimerGame.Enabled)
            {
                TimerGame.Interval = 1000;
                TimerGame.Enabled = true;
            }

            Random r = new Random();
            GetCurrentTurn().SetProfit(r.Next(100));

            UpdatePositionToAcceptedCandidate();
            TurnSummary();
        }

        private void TurnSummary()
        {
            TimerGame.Enabled = false;
            CurrentTurnStatus = TurnStatus.Initial;
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
            TurnSummaryLbl1.Visible = true;
            TurnSummaryLbl2.Visible = true;
            TurnSummaryLbl3.Visible = true;
            PrizePointsLbl1.Visible = true;
            PrizePointsLbl2.Visible = true;
            PrizePointsLbl3.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextTurn.Visible = true;

            TurnSummaryLbl2.Text = GetCurrentTurn().Gain.ToString();
            PrizePointsLbl2.Text = (110 - 1 * 10).ToString();
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to proceed to the next turn.<br />";
        }

        private void UpdatePositionToAcceptedCandidate()
        {
            var currentTurn = GetCurrentTurn();

            int totalPrizePoints = Common.GetTotalPrizePoints(ScenarioTurns);
            UpdateTurnsTable(currentTurn, totalPrizePoints);
        }

        protected void btnNextTurn_Click(object sender, EventArgs e)
        {
            TurnSummaryLbl1.Visible = false;
            TurnSummaryLbl2.Visible = false;
            TurnSummaryLbl3.Visible = false;
            PrizePointsLbl1.Visible = false;
            PrizePointsLbl2.Visible = false;
            PrizePointsLbl3.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextTurn.Visible = false;

            if (NeedToAskRating())
            {
                AskForRating = true;
            }

            CurrentTurnStatus = TurnStatus.MoveToNextTurn;

            if (CurrentTurnNumber < Common.NumOfTurns)
            {
                FillNextPosition();
            }
            else
            {
                TimerGame.Enabled = false;
                Response.Redirect("EndGame.aspx");
            }
        }
    }
}