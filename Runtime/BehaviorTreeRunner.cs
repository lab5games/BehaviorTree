using UnityEngine;

namespace Lab5Games.AI
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        public BehaviorTree tree;

        public void Setup(BehaviorTree tree)
        {
            if(tree)
            {
                this.tree = tree.Clone();
                
                foreach(var node in this.tree.nodes)
                {
                    node.sourceGameObject = gameObject;
                }
            }
        }

        private void Start()
        {
            Setup(tree);
        }

        private void Update()
        {
            if(tree)
            {
                tree.Update();
            }
        }
    }
}
