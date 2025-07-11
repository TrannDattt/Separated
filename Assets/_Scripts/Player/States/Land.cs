using Separated.Data;
using Separated.Enums;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Land : PlayerBaseState
    {
        private PlayerControl _bodyPart;

        public Land(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player) : base(key, data, animator)
        {
            _bodyPart = player;
        }

        public override void Enter()
        {
            base.Enter();

            _bodyPart.RigidBody.linearVelocity = Vector2.zero;
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                IsFinished = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (IsFinished)
            {
                return EBehaviorState.Idle;
            }
            return Key;
        }
    }
}
