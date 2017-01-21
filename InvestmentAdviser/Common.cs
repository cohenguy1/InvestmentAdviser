using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentAdviser
{
    public static class Common
    {
        public const int TotalInvestmentsTurns = 20;

        public const int NumOfTurnsInTable = 10;

        public const int NumOfChanges = 300;

        public static int GetTotalProfit(IEnumerable<ScenarioTurn> scenarioTurns)
        {
            int totalProfit = scenarioTurns.Where(turn => turn.Played).
                Sum(tur => tur.Profit);

            // with one decimal precision
            return totalProfit;
        }
    }
}