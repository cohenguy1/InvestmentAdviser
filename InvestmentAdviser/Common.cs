﻿using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentAdviser
{
    public static class Common
    {
        public const int TotalInvestmentsTurns = 20;

        public const int NumOfTurnsInTable = 10;

        public const int NumOfChanges = 100;

        public static int GetTotalPrizePoints(IEnumerable<ScenarioTurn> scenarioTurns)
        {
            int totalPrizePoints = scenarioTurns.Where(turn => turn.Played).
                Sum(tur => tur.PrizePoints);

            // with one decimal precision
            return totalPrizePoints;
        }
    }
}