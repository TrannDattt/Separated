using Separated.Data;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class Land : PlayerBaseState
    {
        private PlayerControl _bodyPart;

        public Land(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart) : base(key, data, animator)
        {
            _bodyPart = bodyPart;
        }

        public override void Enter()
        {
            base.Enter();

            _bodyPart.RigidBody.linearVelocity = Vector2.zero;
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= _stateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EPlayerState GetNextState()
        {
            if (_isFinish)
            {
                return EPlayerState.Idle;
            }
            return Key;
        }
    }
}
