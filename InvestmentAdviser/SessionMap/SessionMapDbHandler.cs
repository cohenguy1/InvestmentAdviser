using System.Diagnostics;

namespace InvestmentAdviser
{
    public partial class DbHandler : System.Web.UI.Page
    {
        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }
        
        public int RandomHuristicAskPosition
        {
            get { return (int)Session[SessionMap.RandomHuristicAskPositionStr]; }
            set { Session[SessionMap.RandomHuristicAskPositionStr] = value; }
        }

        public int? VectorNum
        {
            get { return (int?)Session[SessionMap.VectorNumStr]; }
            set { Session[SessionMap.VectorNumStr] = value; }
        }
        
        public Stopwatch GameStateStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.GameStateStopwatchStr]; }
            set { Session[SessionMap.GameStateStopwatchStr] = value; }
        }
    }
}