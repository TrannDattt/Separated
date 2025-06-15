using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    public class Die : EnemyBaseState
    {
        private EnemyControl _enemy;
        private DropContainer _dropContainer;
        private bool _isDeath;

        public Die(EBehaviorState key, StateDataSO data, Animator animator, EnemyControl enemy, DropContainer dropContainer) : base(key, data, animator)
        {
            _enemy = enemy;
            _dropContainer = dropContainer;
        }

        public override void Enter()
        {
            base.Enter();

            _isDeath = false;
            _dropContainer.DropValue();
            Debug.Log("Die");
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime > CurStateData.PeriodTime)
            {
                _isFinish = true;
                if (!_isDeath)
                {
                    Debug.Log("Drop");
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