using System.Collections;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using UnityEngine;

namespace Separated.Enemies
{
    public class Hurt : VulnerableState
    {
        private HurtStateData _hurtData => _curStateData as HurtStateData;

        public Hurt(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy) : base(key, data, animator, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            RuntimeCoroutine.Instance.StartRuntimeCoroutine(RecoveryCoroutine());
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime > _hurtData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() != EBehaviorState.None)
            {
                return base.GetNextState();
            }

            if (_isFinish)
            {
                return EBehaviorState.Idle;
            }

            return Key;
        }

        private IEnumerator RecoveryCoroutine()
        {
            yield return new WaitForSeconds(_hurtData.RecoveryCooldownTime);
            (_enemy as IDamageble).CanTakeDamage = true;
        }
    }
}