using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public class Run : GroundState
    {
        private RunStateData _runData => CurStateData as RunStateData;

        public Run(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy, UnitNavigator navigator)
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

            var moveDir = _navigator.GetMoveDirection();
            _enemy.RigidBody.linearVelocity = new Vector2(moveDir.x * _runData.Speed, _enemy.RigidBody.linearVelocity.y);
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (!_navigator.CheckInTriggerRange())
                {
                    return EBehaviorState.Idle;
                }

                return Key;
            }

            return base.GetNextState();
        }
    }
}