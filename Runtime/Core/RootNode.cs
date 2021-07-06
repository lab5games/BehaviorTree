
namespace Lab5Games.AI
{
    public class RootNode : BehaviorTreeNode
    {
        [UnityEngine.HideInInspector] public BehaviorTreeNode child;

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            return child.Update();
        }
    }
}
