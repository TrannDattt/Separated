using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public abstract class GroundState : EnemyBaseState
    {
        protected EnemyControl _enemy;
        protected UnitNavigator _navigator;

        protected GroundState(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy, UnitNavigator navigator) : base(key, data, animator)
        {
            _enemy = enemy;
            _navigator = navigator;
        }

        public override void Do()
        {
            base.Do();

            ChangeFaceDir();
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish && _navigator.CheckInAttackRange(_enemy.Player.transform, _enemy.transform))
            {
                return EBehaviorState.Skill1;
            }

            // TODO: Change to HURT when get take light damage
            // TODO: Change to THROWN_AWAY when get heavy damage
            // TODO: Change to DIE when run out of HP

            return EBehaviorState.None;
        }

        private void ChangeFaceDir()
        {
            if (_navigator.GetMoveDirection(_enemy.Player.transform, _enemy.transform).x > 0)
            {
                _enemy.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_navigator.GetMoveDirection(_enemy.Player.transform, _enemy.transform).x < 0)
            {
                _enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}