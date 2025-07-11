using System;
using System.Collections.Generic;
using Separated.Enemies;
using Separated.Enums;
using UnityEngine;

namespace Separated.Helpers
{
    public abstract class BaseStateMachine<T> : MonoBehaviour where T : Enum
    {
        public BaseState<T> CurState { get; protected set; }

        protected Dictionary<T, BaseState<T>> _stateDict = new();

        public virtual void ChangeState(T nextKey, bool wait = false)
        {
            if (!_stateDict.ContainsKey(nextKey))
            {
                Debug.Log($"{gameObject.tag} dont have state: {nextKey}");
            }

            if (CurState == null || !wait || (wait && CurState != null && CurState.IsFinished))
            // if (CurState == null || wait || (CurState != null && !CurState.IsFinished))
            {
                CurState?.Exit();
                CurState = _stateDict[nextKey];
                CurState.Enter();
            }
        }

        protected virtual void Update()
        {
            if (CurState != null)
            {
                var nextStateKey = CurState.GetNextState();
                if (!nextStateKey.Equals(CurState.Key))
                {
                    // if (CurState is EnemyBaseState)
                    // {
                    //     Debug.Log(CurState.Key);
                        // Debug.Log(nextStateKey);
                    // }
                    ChangeState(nextStateKey);
                }

                CurState.Do();
            }
        }

        void FixedUpdate()
        {
            CurState?.FixedDo();
        }
    }
}
