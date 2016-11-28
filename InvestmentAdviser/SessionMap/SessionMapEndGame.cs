using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace InvestmentAdviser
{
    public partial class EndGame : System.Web.UI.Page
    {
        
        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }
        
        public List<ScenarioTurn> ScenarioTurns
        {
            get { return (List<ScenarioTurn>)Session[SessionMap.ScenarioTurnsStr]; }
            set { Session[SessionMap.ScenarioTurnsStr] = value; }
        }

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[SessionMap.AskPositionStr]; }
            set { Session[SessionMap.AskPositionStr] = value; }
        }

        public Stopwatch GameStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.GameStopwatchStr]; }
            set { Session[SessionMap.GameStopwatchStr] = value; }
        }

        
        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}