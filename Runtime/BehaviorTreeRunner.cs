using UnityEngine;

namespace Lab5Games.AI
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        [SerializeField] BehaviorTree tree;

        public void Setup(BehaviorTree tree)
        {
            if(tree)
            {
                this.tree = tree.Clone();

                this.tree.Binding(gameObject);
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
