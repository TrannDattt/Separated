using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class MeleeAttack : AttackState
    {
        // private List<MeleeAttackStateData> _attackDataList => _stateDataList.ConvertAll(x => x as MeleeAttackStateData);
        private AttackSkillData _attackData => _curStateData as AttackSkillData;
        public override float Damage => _attackData.Damage;
        public override float PoiseDamage => _attackData.PoiseDamage;
        public override Vector2 KnockbackDir => _attackData.KnockbackDir;
        public override float KnockbackForce => _attackData.KnockbackForce;

        // private MeleeAttack _nextAttack;
        private bool _canDoNextAttack;

        // public MeleeAttack(EPlayerState key, List<StateDataSO> datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine) : base(key, datas, data, animator, player, inputProvider, stateMachine)
        // {

        // }

        public MeleeAttack(EPlayerState key, StateDataSO[] datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine) : base(key, datas, data, animator, player, inputProvider, stateMachine)
        {

        }

        // public MeleeAttack(EPlayerState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, MeleeAttack nextAttack) : base(key, data, animator, player, inputProvider)
        // {
        //     _nextAttack = nextAttack;
        // }

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

        public override EPlayerState GetNextState()
        {
            if (base.GetNextState() == EPlayerState.None)
            {
                if (_canDoNextAttack && _inputProvider.AttackInput)
                {
                    Debug.Log(_stateDataList.ToList().IndexOf(_attackData));
                    var nextAttackData = _stateMachine.GetNextData(_attackData, _stateDataList);
                    // var nextAttackData = _stateMachine.GetNextData(_attackData, _attackDataList);
                    var newAttackState = new MeleeAttack(Key, _stateDataList, nextAttackData, _animator, _player, _inputProvider, _stateMachine);
                    _stateMachine.UpdateState(Key, newAttackState, true);
                    return newAttackState.Key;
                }
                
                return Key;
            }

            return base.GetNextState();
        }
    }
}
