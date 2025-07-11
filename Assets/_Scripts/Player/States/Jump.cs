using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Unity.Properties;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Jump : AirBorneState
    {
        private JumpStateData _jumpData => _curStateData as JumpStateData;

        private float _acceleration;

        public Jump(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInputManager.EActionInputType.Jump);

            base.Enter();

            _firstVelocityX = _jumpData.JumpSpeedX;
            _acceleration = -_jumpData.JumpSpeedY / _jumpData.PeriodTime;
        }

        public override void Do()
        {
            base.Do();

            var velocityY = _jumpData.JumpSpeedY + _acceleration * PlayedTime;
            _player.RigidBody.linearVelocityY = velocityY;

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                IsFinished = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (IsFinished || _groundSensor.CheckSensor(GroundSensor.EDirection.Up))
                {
                    return EBehaviorState.Fall;
                }
                return Key;
            }
            else
            {
                return base.GetNextState();
            }
        }
    }
}
