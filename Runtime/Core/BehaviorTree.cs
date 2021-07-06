using UnityEngine;
using System.Collections.Generic;

namespace Lab5Games.AI
{
    using Node = BehaviorTreeNode;
    using State = BehaviorTreeNode.EState;

    [CreateAssetMenu(fileName ="New Behavior Tree", menuName ="Lab5Games/AI/Behavior Tree")]
    public class BehaviorTree : ScriptableObject
    {
        [HideInInspector] public RootNode rootNode = null;
        [HideInInspector] public State treeState = State.RUNNING;
        [HideInInspector] public List<Node> nodes = new List<Node>();

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
    }
}
