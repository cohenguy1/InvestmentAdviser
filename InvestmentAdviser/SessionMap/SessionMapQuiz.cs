namespace InvestmentAdviser
{
    public partial class Quiz : System.Web.UI.Page
    {
        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }

        public int NumOfWrongQuizAnswers
        {
            get { return (int)Session[SessionMap.NumOfWrongQuizAnswersStr]; }
            set { Session[SessionMap.NumOfWrongQuizAnswersStr] = value; }
        }

        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}