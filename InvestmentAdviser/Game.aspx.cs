﻿using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
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

                FillScenarioTurns();

                ShowInterviewImages();

                CurrentTurnNumber = 1;

                StartInterviewsForPosition(0);
            }
        }

        private void FillScenarioTurns()
        {
            var profits = dbHandler.GetScenariosProfits();

            var scenarioTurns = new List<ScenarioTurn>();

            for (int i = 1; i <= Common.TotalInvestmentsTurns; i++)
            {
                scenarioTurns.Add(new ScenarioTurn(i, profits[i - 1]));
            }

            ScenarioTurns = scenarioTurns;
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
            var currentTurnNumber = CurrentTurnNumber;
            string turnTitle = GetJobTitle(currentTurnNumber);

            PositionHeader.Text = turnTitle;

            if (currentTurnNumber >= 1)
            {
                MovingToNextPositionLabel.Text = Common.MovingToNextString;
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = turnTitle + "<br /><br /><br />";
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
            else if (currentTurnNumber <= Common.TotalInvestmentsTurns)
            {
                ShiftCells();

                UpdateNewRow(currentTurnNumber);
            }
        }


        protected void TimerGame_Tick(object sender, EventArgs e)
        {
            if (AskForRating)
            {
                RateAdvisor();
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
            StartInterviewsForPosition(CurrentTurnNumber);
        }

        private void CleanCurrentScenarioTurn()
        {
            ClearInterviewImages();
        }

        private void ProcessCandidate()
        {
            if (!TimerGame.Enabled)
            {
                TimerGame.Interval = 1000;
                TimerGame.Enabled = true;
            }

            GetCurrentTurn().SetPlayed();

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
            ProfitLbl.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextTurn.Visible = true;

            ProfitLbl.Text = "$ " + GetCurrentTurn().Profit.ToString();
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to proceed to the next turn.<br />";
        }

        private void UpdatePositionToAcceptedCandidate()
        {
            var currentTurn = GetCurrentTurn();

            int totalProfit = Common.GetTotalProfit(ScenarioTurns);
            UpdateTurnsTable(currentTurn, totalProfit);
        }

        protected void btnNextTurn_Click(object sender, EventArgs e)
        {
            TurnSummaryLbl1.Visible = false;
            TurnSummaryLbl2.Visible = false;
            ProfitLbl.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextTurn.Visible = false;

            if (NeedToAskRating())
            {
                AskForRating = true;
                AlreadyAskedForRating = true;
            }

            CurrentTurnStatus = TurnStatus.MoveToNextTurn;

            IncreaseCurrentPosition();

            if (CurrentTurnNumber <= Common.TotalInvestmentsTurns)
            {
                FillNextPosition();
            }
            else
            {
                if (AskForRating)
                {
                    RateAdvisor();
                }
                else
                {
                    TimerGame.Enabled = false;
                    Response.Redirect("EndGame.aspx");
                }
            }
        }
    }
}