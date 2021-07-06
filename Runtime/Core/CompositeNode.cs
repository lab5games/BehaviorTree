using System.Collections.Generic;

namespace Lab5Games.AI
{
    public abstract class CompositeNode : BehaviorTreeNode
    {
        [UnityEngine.HideInInspector] public List<BehaviorTreeNode> children = new List<BehaviorTreeNode>();
    }
}
