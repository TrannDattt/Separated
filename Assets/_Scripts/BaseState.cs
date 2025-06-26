using System;
using UnityEngine;

namespace Separated.Helpers
{
    public abstract class BaseState<T> where T : Enum
    {
        public T Key { get; private set; }
        public bool IsFinished { get; protected set; }

        public BaseState(T key)
        {
            Key = key;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Do();
        public abstract void FixedDo();
        public abstract T GetNextState();
    }
}
