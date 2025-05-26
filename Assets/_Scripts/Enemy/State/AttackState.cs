using Separated.Data;
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

        public AttackState(EEnemyState key, StateDataSO[] datas, StateDataSO data, Animator animator, EnemyControl enemy, EnemyStateMachine stateMachine, UnitHitbox hitbox) : base(key, data, animator)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
            _hitbox = hitbox;
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

        public override EEnemyState GetNextState()
        {
            if (_isFinish)
            {
                var nextAttackData = _stateMachine.GetRandomData(_stateDataList);
                var nextAttackState = new AttackState(Key, _stateDataList, nextAttackData, _animator, _enemy, _stateMachine, _hitbox);
                _stateMachine.UpdateState(Key, nextAttackState);

                return EEnemyState.Idle;
            }

            return Key;
        }
    }
}