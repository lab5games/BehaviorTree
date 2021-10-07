
namespace Lab5Games.AI
{
    public class DebugLogNode : ActionNode
    {
        public string message = "DEBUG LOG NODE";

        protected override void OnStart()
        {
            UnityEngine.Debug.Log(message);
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
