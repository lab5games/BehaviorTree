
namespace Lab5Games.AI
{
    public class SucceederNode : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            child.Update();
            return EState.SUCCESS;
        }
    }
}
