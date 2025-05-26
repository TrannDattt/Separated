using Separated.Data;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public class Idle : GroundState
    {
        private IdleStateData _idleData => _curStateData as IdleStateData;

        public Idle(EEnemyState key, StateDataSO data, Animator animator, EnemyControl enemy, UnitNavigator navigator)
            : base(key, data, animator, enemy, navigator)
        {

        }

        public override void Enter()
        {
            base.Enter();

            _enemy.RigidBody.linearVelocity = Vector2.zero;
        }

        public override void Do()
        {
            base.Do();

            if(_idleData.IdleTime > 0 && PlayedTime >= _idleData.IdleTime)
            {
                _isFinish = true;
            }
        }
        
        public override EEnemyState GetNextState()
        {
            if (base.GetNextState() == EEnemyState.None)
            {
                if (_navigator.CheckInTriggerRange(_enemy.Player.transform, _enemy.transform, new(3f, .5f)) && !_navigator.CheckInAttackRange(_enemy.Player.transform, _enemy.transform))
                {
                    return EEnemyState.Run;
                }

                return Key;
            }

            return base.GetNextState();
        }
    }
}