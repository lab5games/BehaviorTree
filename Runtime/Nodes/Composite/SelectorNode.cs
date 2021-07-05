
namespace Lab5Games.AI
{
    public class SelectorNode : CompositeNode
    {
        int current = 0;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            var child = children[current];

            switch(child.Update())
            {
                case EState.RUNNING:
                    return EState.RUNNING;
                case EState.FAILURE:
                    ++current;
                    break;
                case EState.SUCCESS:
                    return EState.SUCCESS;
            }

            return current >= children.Count ? EState.FAILURE : EState.RUNNING;
        }
    }
}
