using UnityEngine;

namespace Lab5Games.AI
{
    public class RootNode : BehaviorTreeNode
    {
        [HideInInspector] public BehaviorTreeNode child;

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override EState OnUpdate()
        {
            return child.Update();
        }

        public override BehaviorTreeNode Clone()
        {
            RootNode cloneNode = Instantiate(this);
            cloneNode.child = child.Clone();

            return cloneNode;
        }
    }
}
