using InvestmentAdviser.Enums;
using InvestmentAdviser.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace InvestmentAdviser
{
    public partial class Game : System.Web.UI.Page
    {
        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }

        public bool AlreadyAskedForRating
        {
            get { return (bool)Session[SessionMap.AlreadyAskedForRatingStr]; }
            set { Session[SessionMap.AlreadyAskedForRatingStr] = value; }
        }

        public bool AskForRating
        {
            get { return (bool)Session[SessionMap.AskForRatingStr]; }
            set { Session[SessionMap.AskForRatingStr] = value; }
        }

        public int CurrentTurnNumber
        {
            get { return (int)Session[SessionMap.CurrentTurnNumberStr]; }
            set { Session[SessionMap.CurrentTurnNumberStr] = value; }
        }

        public List<ScenarioTurn> ScenarioTurns
        {
            get { return (List<ScenarioTurn>)Session[SessionMap.ScenarioTurnsStr]; }
            set { Session[SessionMap.ScenarioTurnsStr] = value; }
        }

        public TurnStatus CurrentTurnStatus
        {
            get { return (TurnStatus)Session[SessionMap.CurrentTurnStatusStr]; }
            set { Session[SessionMap.CurrentTurnStatusStr] = value; }
        }

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[SessionMap.AskPositionStr]; }
            set { Session[SessionMap.AskPositionStr] = value; }
        }

        public int RandomHuristicAskPosition
        {
            get { return (int)Session[SessionMap.RandomHuristicAskPositionStr]; }
            set { Session[SessionMap.RandomHuristicAskPositionStr] = value; }
        }

        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }

        public int? VectorNum
        {
            get { return (int?)Session[SessionMap.VectorNumStr]; }
            set { Session[SessionMap.VectorNumStr] = value; }
        }
    }
}