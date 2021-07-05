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

        public bool started = false;
        public EState state = EState.RUNNING;

        public string guid;
        public Vector2 position;

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

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract EState OnUpdate();
    }
}
