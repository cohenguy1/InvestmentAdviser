﻿using InvestmentAdviser.Enums;
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

        private void UpdateTurnsTable(ScenarioTurn currentTurn, int totalProfit)
        {
            if (currentTurn.TurnNumber <= Common.TotalInvestmentsTurns)
            {
                int turnRow = Math.Min(currentTurn.TurnNumber, Common.NumOfTurnsInTable);

                UpdateTurnRow(currentTurn, turnRow);
            }

            TotalProfitCell.Text = "&nbsp;Total Profit: $ " + totalProfit.ToString() + " (virtual) &nbsp;";
        }

        private void UpdateNewRow(int turnNumber)
        {
            ClearTableRowStyle(Common.NumOfTurnsInTable);

            UpdateTurnCellTitle(turnNumber, Common.NumOfTurnsInTable);

            SetTableRowStyle(Common.NumOfTurnsInTable);
        }

        private void UpdateTurnCellTitle(int turnNumber, int turnRow)
        {
            TableCell tableCell = GetTableCell(turnRow, TableColumnType.Turn);

            tableCell.Text = "&nbsp;" + ScenarioTurn.GetTurnTitle(turnNumber);
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

                UpdateTurnCellTitle(scenarioTurn.TurnNumber, currentRow);

                currentRow--;
            }
        }

        private void UpdateTurnRow(ScenarioTurn currentTurn, int turnRow)
        {
            var profitCell = GetProfitCell(turnRow);
            profitCell.Text = "&nbsp;$ " + currentTurn.Profit;
        }

        private void ClearTurnTable()
        {
            for (int turnIndex = 1; turnIndex <= Common.NumOfTurnsInTable; turnIndex++)
            {
                ClearTableRowStyle(turnIndex);
            }

            TotalProfitCell.Text = "&nbsp;Total Profit: ";
        }

        private void ClearTableRowStyle(int turnIndex)
        {
            ClearCellStyle(turnIndex, TableColumnType.Turn);
            ClearCellStyle(turnIndex, TableColumnType.Profit);
        }

        private void SetSeenTableRowStyle(int turnIndex)
        {
            SetSeenCellStyle(turnIndex, TableColumnType.Turn);
            SetSeenCellStyle(turnIndex, TableColumnType.Profit);
        }

        private void SetTableRowStyle(int turnIndex)
        {
            SetTableRowStyle(turnIndex, TableColumnType.Turn);
            SetTableRowStyle(turnIndex, TableColumnType.Profit);
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
                case TableColumnType.Profit:
                    return GetProfitCell(turnRow);
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

        private string GetJobTitle(int currentTurnNumber)
        {
            var turnTitle = ScenarioTurn.GetTurnTitle(currentTurnNumber) + "/" + Common.TotalInvestmentsTurns;
            return turnTitle;
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

        private TableCell GetProfitCell(int row)
        {
            switch (row)
            {
                case 1:
                    return ScenarioTurn1ProfitCell;
                case 2:
                    return ScenarioTurn2ProfitCell;
                case 3:
                    return ScenarioTurn3ProfitCell;
                case 4:
                    return ScenarioTurn4ProfitCell;
                case 5:
                    return ScenarioTurn5ProfitCell;
                case 6:
                    return ScenarioTurn6ProfitCell;
                case 7:
                    return ScenarioTurn7ProfitCell;
                case 8:
                    return ScenarioTurn8ProfitCell;
                case 9:
                    return ScenarioTurn9ProfitCell;
                case 10:
                    return ScenarioTurn10ProfitCell;
                default:
                    return null;
            }
        }
    }
}