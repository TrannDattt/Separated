using System.Collections;
using System.Linq;
using System.Timers;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Unit;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class MeleeAttack : AttackState
    {

        public MeleeAttack(EBehaviorState key, StateDataSO[] datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine, UnitHitbox hitbox) : base(key, datas, data, animator, player, inputProvider, stateMachine, hitbox)
        {

        }

        public override void Enter()
        {
            base.Enter();

            RuntimeCoroutine.Instance.StopRuntimeCoroutine(ResetAttackCoroutine());
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                return Key;
            }

            ChooseNextAttackState();
            RuntimeCoroutine.Instance.StartRuntimeCoroutine(ResetAttackCoroutine());
            return base.GetNextState();
        }

        private void ChooseNextAttackState()
        {
            var nextAttackData = _stateMachine.GetNextData(_attackData, _stateDataList);
            var newAttackState = new MeleeAttack(Key, _stateDataList, nextAttackData, _animator, _player, _inputProvider, _stateMachine, _hitbox);
            _stateMachine.UpdateState(Key, newAttackState);

        }

        private IEnumerator ResetAttackCoroutine()
        {
            yield return new WaitForSeconds(1.5f);

            var firstAttackData = _stateDataList[0];
            var fisrtAttackState = new MeleeAttack(Key, _stateDataList, firstAttackData, _animator, _player, _inputProvider, _stateMachine, _hitbox);
            _stateMachine.UpdateState(Key, fisrtAttackState);

        }
    }
}
