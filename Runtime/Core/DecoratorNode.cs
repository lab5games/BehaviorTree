
namespace Lab5Games.AI
{
    public abstract class DecoratorNode : BehaviorTreeNode
    {
        public BehaviorTreeNode child;

        public override BehaviorTreeNode Clone()
        {
            DecoratorNode cloneNode = Instantiate(this);
            cloneNode.child = child.Clone();

            return cloneNode;
        }
    }
}
