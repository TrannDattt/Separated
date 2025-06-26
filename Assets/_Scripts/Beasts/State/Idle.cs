using Separated.Data;
using Separated.Enums;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class Idle : GroundState
    {
        private IdleStateData _idleData => CurStateData as IdleStateData;

        public Idle(EBehaviorState key, StateDataSO data, Animator animator, BeastControl beast, UnitNavigator navigator)
            : base(key, data, animator, beast, navigator)
        {

        }

        public override void Enter()
        {
            base.Enter();

            _beast.Rigigbody.linearVelocity = Vector2.zero;
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= _idleData.IdleTime)
            {
                IsFinish = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
            // Debug.Log(1);
                if (IsFinish && _navigator.CheckInTriggerRange() && !_navigator.CheckInAttackRange())
                {
                    return EBehaviorState.Run;
                }

                return Key;
            }

            return base.GetNextState();
        }
    }
}