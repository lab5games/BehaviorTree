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

        public void Reset()
        {
            foreach(var n in nodes)
            {
                n.started = false;
                n.state = State.RUNNING;
            }
        }

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

        private List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            RootNode rootNode = parent as RootNode;
            if (rootNode && rootNode.child)
            {
                children.Add(rootNode.child);
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.child)
            {
                children.Add(decorator.child);
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                return composite.children;
            }

            return children;
        }

        private void Traverse(Node node, System.Action<Node> visiter)
        {
            if (node)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        public void Binding(GameObject srcGameObject)
        {
            foreach(var n in nodes)
            {
                n.sourceGameObject = srcGameObject;
            }
        }

        public BehaviorTree Clone()
        {
            BehaviorTree cloneTree = Instantiate(this);
            cloneTree.rootNode = rootNode.Clone() as RootNode;
            cloneTree.nodes = new List<Node>();
            Traverse(cloneTree.rootNode, (n) =>
            {
                cloneTree.nodes.Add(n);
            });

            return cloneTree;
        }
    }
}
