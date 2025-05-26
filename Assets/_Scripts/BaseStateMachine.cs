using System;
using System.Collections.Generic;
using UnityEngine;

namespace Separated.Helpers
{
    public abstract class BaseStateMachine<T> : MonoBehaviour where T : Enum
    {
        public BaseState<T> CurState { get; protected set; }

        protected Dictionary<T, BaseState<T>> _stateDict = new();

        protected virtual void ChangeState(T nextKey)
        {
            CurState?.Exit();
            CurState = _stateDict[nextKey];
            CurState.Enter();
        }

        protected virtual void Update()
        {
            if(CurState != null)
            {
                CurState.Do();

                var nextStateKey = CurState.GetNextState();
                if(!nextStateKey.Equals(CurState.Key)){
                    ChangeState(nextStateKey);
                    // Debug.Log(nextStateKey);
                }

            }
        }

        void FixedUpdate()
        {
            CurState?.FixedDo();
        }
    }
}
