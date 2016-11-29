﻿using InvestmentAdviser.Enums;
using System.Text.RegularExpressions;
using System;

namespace InvestmentAdviser.Logic
{
    public class ScenarioTurn
    {
        public ScenarioTurnEnum TurnEnum;

        public int PrizePoints;

        public double EarningPercentage { get; private set; }

        public int TurnNumber { get; private set; }

        public bool Played { get; private set; }

        public ScenarioTurn(int turnNumber)
        {
            TurnEnum = (ScenarioTurnEnum)turnNumber;
            TurnNumber = turnNumber;
            Played = false;
        }
        
        public string GetTurnTitle()
        {
            return "Turn " + TurnNumber;
        }

        public void SetProfit(int profit)
        {
            EarningPercentage = profit;
            PrizePoints = 3;
            Played = true;
        }
    }
}