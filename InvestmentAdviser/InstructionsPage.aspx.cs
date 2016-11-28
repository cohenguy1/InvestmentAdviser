using InvestmentAdviser.Enums;
using System;
using System.Diagnostics;

namespace InvestmentAdviser
{
    public partial class InstructionsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InstructionsStopwatch = new Stopwatch();
                InstructionsStopwatch.Start();

                dbHandler.UpdateTimesTable(GameState.Instructions);
            }
        }

        protected void btnNextInstruction_Click(object sender, EventArgs e)
        {
            // training start
            InstructionsStopwatch.Stop();
            dbHandler.UpdateTimesTable(GameState.TrainingStart);

            Response.Redirect("Quiz.aspx");
        }
    }
}