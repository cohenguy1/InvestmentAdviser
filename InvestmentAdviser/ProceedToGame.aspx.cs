using InvestmentAdviser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvestmentAdviser
{
    public partial class ProceedToGame : System.Web.UI.Page
    {
        protected void btnNextToGame_Click(object sender, EventArgs e)
        {
            Response.Redirect("Quiz.aspx");
        }
		
        protected void btnPrevToInstructions_Click(object sender, EventArgs e)
        {
            Response.Redirect("InstructionsPage.aspx");
        }
    }
}