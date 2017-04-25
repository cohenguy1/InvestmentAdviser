using InvestmentAdviser.Enums;
using System;
using System.Diagnostics;

namespace InvestmentAdviser
{
    public partial class InstructionsPage : System.Web.UI.Page
    {
        protected void btnNextInstruction_Click(object sender, EventArgs e)
        {
            Response.Redirect("Quiz.aspx");
        }
    }
}