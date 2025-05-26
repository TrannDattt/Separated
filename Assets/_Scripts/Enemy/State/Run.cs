using Separated.Data;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public class Run : GroundState
    {
        private RunStateData _runData => _curStateData as RunStateData;

        public Run(EEnemyState key, StateDataSO data, Animator animator, EnemyControl enemy, UnitNavigator navigator)
            : base(key, data, animator, enemy, navigator)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _isFinish = true;
        }

        public override void Do()
        {
            base.Do();

            var moveDir = _navigator.GetMoveDirection(_enemy.Player.transform, _enemy.transform);
            _enemy.RigidBody.linearVelocity = new Vector2(moveDir.x * _runData.Speed, _enemy.RigidBody.linearVelocity.y);
        }
        
        public override EEnemyState GetNextState()
        {
            if (base.GetNextState() == EEnemyState.None)
            {
                if (!_navigator.CheckInTriggerRange(_enemy.Player.transform, _enemy.transform, new(3f, .5f)))
                {
                    return Key;
                }

                return EEnemyState.Idle;
            }
            
            return base.GetNextState();
        }
    }
}