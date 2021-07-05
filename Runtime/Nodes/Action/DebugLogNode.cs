
namespace Lab5Games.AI
{
    public class DebugLogNode : ActionNode
    {
        public ELogType logType = ELogType.Log;
        public string message = "DEBUG LOG NODE";

        protected override void OnStart()
        {
            DebugEx.Log(logType, $"[DebugLogNode]: {message}");
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            return EState.SUCCESS;
        }
    }
}
