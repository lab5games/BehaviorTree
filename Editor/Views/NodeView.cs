using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace Lab5Games.AI.Editor
{ 
    public class NodeView : Node
    {
        public BehaviorTreeNode treeNode;

        public NodeView(BehaviorTreeNode treeNode)
        {
            this.treeNode = treeNode;
        }
    }
}
