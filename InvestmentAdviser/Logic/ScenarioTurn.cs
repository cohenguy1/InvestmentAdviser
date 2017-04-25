using InvestmentAdviser.Enums;
using System.Text.RegularExpressions;
using System;

namespace InvestmentAdviser.Logic
{
    public class ScenarioTurn
    {
        public ScenarioTurnEnum TurnEnum;

        public int Profit;

        public int TurnNumber { get; private set; }

        public bool Played { get; private set; }

        public const string TurnTitlePrefix = "Turn ";

        public ScenarioTurn(int turnNumber, int profit)
        {
            TurnEnum = (ScenarioTurnEnum)turnNumber;
            TurnNumber = turnNumber;
            Profit = profit;
            Played = false;
        }

        public static string GetTurnTitle(int turnNumber)
        {
            return TurnTitlePrefix + turnNumber;
        }

        public void SetPlayed()
        {
            Played = true;
        }
    }
}