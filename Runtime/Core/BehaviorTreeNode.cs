using UnityEngine;

namespace Lab5Games.AI
{
    public abstract class BehaviorTreeNode : ScriptableObject
    {
        public enum EState
        {
            RUNNING,
            FAILURE,
            SUCCESS
        }

        [HideInInspector] public bool started = false;
        [HideInInspector] public EState state = EState.RUNNING;

        [HideInInspector] public string GUID;
        [HideInInspector] public Vector2 position;

        [TextArea] public string description;

        public GameObject sourceGameObject { get; internal set; }

        public EState Update()
        {
            if(!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if(state == EState.SUCCESS || state == EState.FAILURE)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual BehaviorTreeNode Clone()
        {
            return Instantiate(this);
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract EState OnUpdate();
    }
}
