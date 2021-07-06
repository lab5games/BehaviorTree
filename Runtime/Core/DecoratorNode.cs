
namespace Lab5Games.AI
{
    public abstract class DecoratorNode : BehaviorTreeNode
    {
        [UnityEngine.HideInInspector] public BehaviorTreeNode child;
    }
}
