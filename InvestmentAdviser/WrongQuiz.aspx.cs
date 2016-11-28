using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvestmentAdviser
{
    public partial class WrongQuiz : System.Web.UI.Page
    {
        public int MaxNumOfWrongQuizAnswers = Quiz.MaxNumOfWrongQuizAnswers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QuizWrongLbl.Text = "&nbsp;You have been wrong for " + MaxNumOfWrongQuizAnswers + " times.";

                dbHandler.SetVectorAssignmentNull();
            }
        }
    }
}