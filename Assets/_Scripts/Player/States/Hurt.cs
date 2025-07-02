using System.Collections;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using UnityEngine;

namespace Separated.Player
{
    public class Hurt : VulnerableState
    {
        private HurtStateData _hurtData => _curStateData as HurtStateData;

        public Hurt(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player) : base(key, data, animator, player)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.RigidBody.linearVelocity = Vector2.zero;
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
            (_player as IDamageble).CanTakeDamage = true;
        }
    }
}
