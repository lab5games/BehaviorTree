
namespace Lab5Games.AI
{
    public class InverterNode : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            switch(child.Update())
            {
                case EState.FAILURE:
                    return EState.SUCCESS;
                case EState.SUCCESS:
                    return EState.FAILURE;
            }

            return EState.RUNNING;
        }
    }
}
