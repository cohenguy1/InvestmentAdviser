using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {

        private void IncreaseCurrentPosition()
        {
            CurrentTurnNumber++;
        }

        private void UpdateTurnsTable(ScenarioTurn currentTurn, int totalPrizePoints)
        {
            if (currentTurn.TurnNumber <= Common.NumOfTurns)
            {
                int turnRow = Math.Min(currentTurn.TurnNumber, Common.NumOfTurnsInTable);

                UpdateTurnRow(currentTurn, turnRow);
            }

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: " + totalPrizePoints.ToString();
        }

        private void UpdateNewRow(int turnNumber)
        {
            ClearTableRowStyle(Common.NumOfTurnsInTable);

            ScenarioTurn newTurn = GetScenarioTurn(turnNumber);

            UpdateTurnCellTitle(newTurn, Common.NumOfTurnsInTable);

            SetTableRowStyle(Common.NumOfTurnsInTable);
        }

        private void UpdateTurnCellTitle(ScenarioTurn scenarioTurn, int turnRow)
        {
            TableCell tableCell = GetTableCell(turnRow, TableColumnType.Turn);

            tableCell.Text = "&nbsp;" + scenarioTurn.GetTurnTitle();
        }

        private void ShiftCells()
        {
            var currentRow = Common.NumOfTurnsInTable - 1;

            var turnsToDisplay = ScenarioTurns.Where(turn => turn.Played).OrderByDescending(turn => turn.TurnNumber);

            foreach (var scenarioTurn in turnsToDisplay)
            {
                if (currentRow < 1)
                {
                    return;
                }

                UpdateTurnRow(scenarioTurn, currentRow);

                UpdateTurnCellTitle(scenarioTurn, currentRow);

                currentRow--;
            }
        }

        private void UpdateTurnRow(ScenarioTurn currentTurn, int turnRow)
        {
            var positionPrizeCell = GetPrizePointsCell(turnRow);
            positionPrizeCell.Text = "&nbsp;" + (110 - currentTurn.Profit * 10).ToString();

            var rankCell = GetRankCell(turnRow);
            rankCell.Text = "&nbsp;" + currentTurn.Profit;
        }

        private void ClearTurnTable()
        {
            for (int turnIndex = 1; turnIndex <= Common.NumOfTurnsInTable; turnIndex++)
            {
                ClearTableRowStyle(turnIndex);
            }

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: ";
        }

        private void ClearTableRowStyle(int turnIndex)
        {
            ClearCellStyle(turnIndex, TableColumnType.Turn);
            ClearCellStyle(turnIndex, TableColumnType.Gain);
            ClearCellStyle(turnIndex, TableColumnType.PrizePoints);
        }

        private void SetSeenTableRowStyle(int turnIndex)
        {
            SetSeenCellStyle(turnIndex, TableColumnType.Turn);
            SetSeenCellStyle(turnIndex, TableColumnType.Gain);
            SetSeenCellStyle(turnIndex, TableColumnType.PrizePoints);
        }

        private void SetTableRowStyle(int turnIndex)
        {
            SetTableRowStyle(turnIndex, TableColumnType.Turn);
            SetTableRowStyle(turnIndex, TableColumnType.Gain);
            SetTableRowStyle(turnIndex, TableColumnType.PrizePoints);
        }

        private void ClearCellStyle(int turnRow, TableColumnType position)
        {
            var tableCell = GetTableCell(turnRow, position);

            if (position != TableColumnType.Turn)
            {
                tableCell.Text = "";
            }
            tableCell.ForeColor = System.Drawing.Color.Black;
            tableCell.Font.Italic = false;
            tableCell.Font.Bold = false;
        }

        private void SetSeenCellStyle(int turnRow, TableColumnType columnType)
        {
            var tableCell = GetTableCell(turnRow, columnType);
            tableCell.ForeColor = System.Drawing.Color.Blue;
            tableCell.Font.Bold = false;
            tableCell.Font.Italic = true;
        }

        private void SetTableRowStyle(int turnRow, TableColumnType columnType)
        {
            var tableCell = GetTableCell(turnRow, columnType);
            tableCell.ForeColor = System.Drawing.Color.Green;
            tableCell.Font.Bold = true;
        }

        private TableCell GetTableCell(int turnRow, TableColumnType columnType)
        {
            switch (columnType)
            {
                case TableColumnType.Turn:
                    return GetTurnCell(turnRow);
                case TableColumnType.Gain:
                    return GetRankCell(turnRow);
                case TableColumnType.PrizePoints:
                    return GetPrizePointsCell(turnRow);
            }

            return null;
        }

        private ScenarioTurn GetScenarioTurn(int indexPosition)
        {
            return ScenarioTurns[indexPosition - 1];
        }

        private ScenarioTurn GetCurrentTurn()
        {
            return GetScenarioTurn(CurrentTurnNumber);
        }

        private string GetCurrentJobTitle()
        {
            var currentTurn = GetCurrentTurn();
            return currentTurn.GetTurnTitle();
        }

        private TableCell GetTurnCell(int row)
        {
            switch (row)
            {
                case 1:
                    return ScenarioTurn1Cell;
                case 2:
                    return ScenarioTurn2Cell;
                case 3:
                    return ScenarioTurn3Cell;
                case 4:
                    return ScenarioTurn4Cell;
                case 5:
                    return ScenarioTurn5Cell;
                case 6:
                    return ScenarioTurn6Cell;
                case 7:
                    return ScenarioTurn7Cell;
                case 8:
                    return ScenarioTurn8Cell;
                case 9:
                    return ScenarioTurn9Cell;
                case 10:
                    return ScenarioTurn10Cell;
                default:
                    return null;
            }
        }

        private TableCell GetPrizePointsCell(int row)
        {
            switch (row)
            {
                case 1:
                    return ScenarioTurn1PrizeCell;
                case 2:
                    return ScenarioTurn2PrizeCell;
                case 3:
                    return ScenarioTurn3PrizeCell;
                case 4:
                    return ScenarioTurn4PrizeCell;
                case 5:
                    return ScenarioTurn5PrizeCell;
                case 6:
                    return ScenarioTurn6PrizeCell;
                case 7:
                    return ScenarioTurn7PrizeCell;
                case 8:
                    return ScenarioTurn8PrizeCell;
                case 9:
                    return ScenarioTurn9PrizeCell;
                case 10:
                    return ScenarioTurn10PrizeCell;
                default:
                    return null;
            }
        }

        private TableCell GetRankCell(int row)
        {
            switch (row)
            {
                case 1:
                    return ScenarioTurn1RankCell;
                case 2:
                    return ScenarioTurn2RankCell;
                case 3:
                    return ScenarioTurn3RankCell;
                case 4:
                    return ScenarioTurn4RankCell;
                case 5:
                    return ScenarioTurn5RankCell;
                case 6:
                    return ScenarioTurn6RankCell;
                case 7:
                    return ScenarioTurn7RankCell;
                case 8:
                    return ScenarioTurn8RankCell;
                case 9:
                    return ScenarioTurn9RankCell;
                case 10:
                    return ScenarioTurn10RankCell;
                default:
                    return null;
            }
        }
    }
}