using Separated.Data;
using Separated.Unit;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public abstract class GroundState : EnemyBaseState
    {
        protected EnemyControl _enemy;
        protected UnitNavigator _navigator;

        protected GroundState(EEnemyState key, StateDataSO data, Animator animator, EnemyControl enemy, UnitNavigator navigator) : base(key, data, animator)
        {
            _enemy = enemy;
            _navigator = navigator;
        }

        public override EEnemyState GetNextState()
        {
            if (_isFinish && _navigator.CheckInAttackRange(_enemy.Player.transform, _enemy.transform))
            {
                return EEnemyState.Attack;
            }

            // TODO: Change to HURT when get take light damage
            // TODO: Change to THROWN_AWAY when get heavy damage
            // TODO: Change to DIE when run out of HP

            return EEnemyState.None;
        }
    }
}