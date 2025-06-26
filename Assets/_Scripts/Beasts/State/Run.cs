using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class Run : GroundState
    {
        private RunStateData _runData => CurStateData as RunStateData;

        public Run(EBehaviorState key, StateDataSO data, Animator animator, BeastControl beast, UnitNavigator navigator)
            : base(key, data, animator, beast, navigator)
        {
        }

        public override void Enter()
        {
            base.Enter();

            IsFinish = true;
        }

        public override void Do()
        {
            base.Do();

            var moveDir = _navigator.GetMoveDirection();
            _beast.Rigigbody.linearVelocity = new Vector2(moveDir.x * _runData.Speed, _beast.Rigigbody.linearVelocity.y);
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