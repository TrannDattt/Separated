using JetBrains.Annotations;
using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class GroundState : BeastBaseState
    {
        protected BeastControl _beast;
        protected UnitNavigator _navigator;

        public GroundState(EBehaviorState key, StateDataSO data, Animator animator, BeastControl beast, UnitNavigator navigator)
        : base(key, data, animator)
        {
            _beast = beast;
            _navigator = navigator;
        }

        public override void Do()
        {
            base.Do();

            ChangeFaceDir();
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish && _navigator.CheckInAttackRange())
            {
                return EBehaviorState.Attack;
            }
            // TODO: Change to DIE when pass exist time

            return EBehaviorState.None;
        }

        private void ChangeFaceDir()
        {
            if (_navigator.GetMoveDirection().x > 0)
            {
                _beast.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_navigator.GetMoveDirection().x < 0)
            {
                _beast.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}