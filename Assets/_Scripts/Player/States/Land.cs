using Separated.Data;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
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

            if (PlayedTime >= _curStateData.PeriodTime)
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
