
namespace Lab5Games.AI
{
    public class RepeatUntilFailNode : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            if(child.Update() == EState.FAILURE)
            {
                return EState.SUCCESS;
            }

            return EState.RUNNING;
        }
    }
}
