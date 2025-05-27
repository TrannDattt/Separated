using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public class AttackState : EnemyBaseState
    {
        protected AttackSkillData _attackData => _curStateData as AttackSkillData;

        private EnemyControl _enemy;
        private EnemyStateMachine _stateMachine;
        private UnitHitbox _hitbox;
        private UnitNavigator _navigator;

        public AttackState(EBehaviorState key, StateDataSO[] datas, StateDataSO data, Animator animator, EnemyControl enemy, EnemyStateMachine stateMachine, UnitHitbox hitbox, UnitNavigator navigator) : base(key, datas, data, animator)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
            _hitbox = hitbox;
            _navigator = navigator;
        }

        public override void Enter()
        {
            base.Enter();

            _enemy.RigidBody.linearVelocityX = 0;
            _hitbox.SetAttackData(_attackData);
            _hitbox.EnableHitbox();
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override void Exit()
        {
            base.Exit();

            _hitbox.DisableHitbox();
            _hitbox.ResetAttackData();
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish)
            {
                var nextAttackData = _stateMachine.GetRandomData(_stateDataList);
                var nextAttackState = new AttackState(Key, _stateDataList, nextAttackData, _animator, _enemy, _stateMachine, _hitbox, _navigator);
                _stateMachine.UpdateState(Key, nextAttackState);
                _navigator.SetAttackData(nextAttackData as AttackSkillData);

                return EBehaviorState.Idle;
            }

            return Key;
        }
    }
}