
namespace Lab5Games.AI
{
    public class SequenceNode : CompositeNode
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

            switch (child.Update())
            {
                case EState.RUNNING:
                    return EState.RUNNING;
                case EState.FAILURE:
                    return EState.FAILURE;
                case EState.SUCCESS:
                    ++current;
                    break;
            }

            return current >= children.Count ? EState.SUCCESS : EState.RUNNING;
        }
    }
}
