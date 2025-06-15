using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public class Idle : GroundState
    {
        private IdleStateData _idleData => CurStateData as IdleStateData;

        public Idle(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy, UnitNavigator navigator)
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

            if(PlayedTime >= _idleData.IdleTime)
            {
                _isFinish = true;
            }
        }
        
        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (_isFinish && _navigator.CheckInTriggerRange(_enemy.Player.transform, _enemy.transform, new(10f, .5f)) && !_navigator.CheckInAttackRange(_enemy.Player.transform, _enemy.transform))
                {
                    return EBehaviorState.Run;
                }

                return Key;
            }

            return base.GetNextState();
        }
    }
}