using System.Collections.Generic;

namespace Lab5Games.AI
{
    public abstract class CompositeNode : BehaviorTreeNode
    {
        public List<BehaviorTreeNode> children = new List<BehaviorTreeNode>();
    }
}
