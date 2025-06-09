using Separated.Data;
using Separated.Enums;
using UnityEngine;

namespace Separated.Enemies
{
    public class VulnerableState : EnemyBaseState
    {
        protected EnemyControl _enemy;

        public VulnerableState(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy) : base(key, data, animator)
        {
            _enemy = enemy;
        }

        public override EBehaviorState GetNextState()
        {
            return EBehaviorState.None;
        }
    }
}