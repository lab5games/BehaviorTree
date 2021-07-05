
namespace Lab5Games.AI
{
    public class WaitNode : ActionNode
    {
        public float duration;

        private float elpasedTime;

        protected override void OnStart()
        {
            elpasedTime = 0;
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            elpasedTime += UnityEngine.Time.deltaTime;

            return elpasedTime > duration ? EState.SUCCESS : EState.RUNNING;
        }
    }
}
