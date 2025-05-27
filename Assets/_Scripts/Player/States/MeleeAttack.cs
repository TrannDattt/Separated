using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class MeleeAttack : AttackState
    {
        private bool _canDoNextAttack;

        public MeleeAttack(EBehaviorState key, StateDataSO[] datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine, UnitHitbox hitbox) : base(key, datas, data, animator, player, inputProvider, stateMachine, hitbox)
        {

        }

        public override void Enter()
        {
            base.Enter();

            _canDoNextAttack = false;
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= .8f * _curStateData.PeriodTime)
            {
                _canDoNextAttack = true;
            }

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
                _canDoNextAttack = false;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (_canDoNextAttack && _inputProvider.AttackInput)
                {
                    var nextAttackData = _stateMachine.GetNextData(_attackData, _stateDataList);
                    var newAttackState = new MeleeAttack(Key, _stateDataList, nextAttackData, _animator, _player, _inputProvider, _stateMachine, _hitbox);
                    _stateMachine.UpdateState(Key, newAttackState, true);
                    return newAttackState.Key;
                }

                return Key;
            }
            
            var firstAttackData = _stateDataList[0];
            var fisrtAttackState = new MeleeAttack(Key, _stateDataList, firstAttackData, _animator, _player, _inputProvider, _stateMachine, _hitbox);
            _stateMachine.UpdateState(Key, fisrtAttackState);

            return base.GetNextState();
        }
    }
}
