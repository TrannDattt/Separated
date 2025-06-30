using Separated.Data;
using Separated.Enums;
using Separated.Poolings;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class Die : BeastBaseState
    {
        private DieStateData _dieData => CurStateData as DieStateData;

        private BeastControl _beast;
        private bool _isDeath;

        public Die(EBehaviorState key, StateDataSO data, Animator animator, BeastControl beast) : base(key, data, animator)
        {
            _beast = beast;
        }

        public override void Enter()
        {
            base.Enter();

            _isDeath = false;
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime > CurStateData.PeriodTime)
            {
                IsFinish = true;
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

            //TODO: Return beast to pool
            // Debug.Log(PlayedTime);
            SummonedObjectPooling.ReturnObject(_beast);
            _beast.gameObject.SetActive(false);
        }

        public override EBehaviorState GetNextState()
        {
            return Key;
        }
    }
}