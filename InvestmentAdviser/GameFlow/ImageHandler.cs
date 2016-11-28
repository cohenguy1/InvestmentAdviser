using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {
        private void ClearInterviewImages()
        {
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
        }
    }
}