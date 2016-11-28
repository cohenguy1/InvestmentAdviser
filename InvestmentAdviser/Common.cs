using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentAdviser
{
    public static class Common
    {
        public const int NumOfTurns = 20;

        public const int NumOfTurnsInTable = 10;

        public static int GetTotalPrizePoints(IEnumerable<ScenarioTurn> scenarioTurns)
        {
            int totalPrizePoints = scenarioTurns.Where(turn => turn.Played).
                Sum(tur => 110 - (int)tur.Profit * 10);

            // with one decimal precision
            return totalPrizePoints;
        }
    }
}