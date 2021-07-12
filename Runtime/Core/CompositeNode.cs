using System.Collections.Generic;
using UnityEngine;

namespace Lab5Games.AI
{
    public abstract class CompositeNode : BehaviorTreeNode
    {
        [HideInInspector] public List<BehaviorTreeNode> children = new List<BehaviorTreeNode>();

        public override BehaviorTreeNode Clone()
        {
            CompositeNode cloneNode = Instantiate(this);
            cloneNode.children = children.ConvertAll(n => n.Clone());

            return cloneNode;
        }
    }
}
