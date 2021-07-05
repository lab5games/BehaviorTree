using System;
using System.Collections.Generic;

namespace Lab5Games.AI
{
    using Node = BehaviorTreeNode;

    public static class BehaviorTreeUtils
    {
        public static List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            RootNode rootNode = parent as RootNode;
            if(rootNode && rootNode.child)
            {
                children.Add(rootNode.child);
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if(decorator && decorator.child)
            {
                children.Add(decorator.child);
            }

            CompositeNode composite = parent as CompositeNode;
            if(composite)
            {
                return composite.children;
            }

            return children;
        }
    }
}
