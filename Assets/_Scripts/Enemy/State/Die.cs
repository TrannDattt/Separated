using Separated.Data;
using Separated.Enums;
using Separated.GameManager;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    public class Die : EnemyBaseState
    {
        private DieStateData _dieData => CurStateData as DieStateData;

        private EnemyControl _enemy;
        private bool _isDeath;

        public Die(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy) : base(key, data, animator)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            _isDeath = false;
            var enemyDieEvent = EventManager.GetGenericEvent<EBeastType>(EventManager.EEventType.EnemyDied);
            enemyDieEvent.Notify(_enemy.EnemyType);

            var enemyDropEvent = EventManager.GetGenericEvent<LootDropData>(EventManager.EEventType.EnemyDied);
            enemyDropEvent.Notify(_dieData.DropData);
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime > CurStateData.PeriodTime)
            {
                _isFinish = true;
                if (!_isDeath)
                {
                    // Debug.Log("Drop");
                    _isDeath = true;
                    Exit();
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            //TODO: Return enemy to pool
            // Debug.Log(PlayedTime);
            _enemy.gameObject.SetActive(false);
        }

        public override EBehaviorState GetNextState()
        {
            return Key;
        }
    }
}