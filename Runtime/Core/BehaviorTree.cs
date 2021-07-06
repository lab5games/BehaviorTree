using UnityEngine;
using System.Collections.Generic;

namespace Lab5Games.AI
{
    using Node = BehaviorTreeNode;
    using State = BehaviorTreeNode.EState;

    [CreateAssetMenu(fileName ="New Behavior Tree", menuName ="Lab5Games/AI/Behavior Tree")]
    public class BehaviorTree : ScriptableObject
    {
        public RootNode rootNode = null;
        [HideInInspector] public State treeState = State.RUNNING;
        public List<Node> nodes = new List<Node>();

        public State Update()
        {
            if(treeState == State.RUNNING)
            {
                treeState = rootNode.Update();
            }

            return treeState;
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
        }

        public void RemoveNode(Node node)
        {
            nodes.Remove(node);
        }

        public BehaviorTree Clone()
        {
            BehaviorTree cloneTree = Instantiate(this);
            cloneTree.rootNode = rootNode.Clone() as RootNode;
            cloneTree.nodes = new List<Node>();
            BehaviorTreeUtils.Traverse(cloneTree.rootNode, (n) =>
            {
                cloneTree.nodes.Add(n);
            });

            return cloneTree;
        }
    }
}
