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
            // TODO: Use this input here when there are multiple jump
            // _inputProvider.UseInput(PlayerInput.EInputType.Jump);

            base.Enter();

            _firstVelocityX = _jumpData.JumpSpeedX;
            _acceleration = -_jumpData.JumpSpeedY / _jumpData.PeriodTime;
            // _firstVelocityX = _player.RigidBody.linearVelocityX;
            // _firstVelocityY = (_jumpData.JumpDistance - .5f * _jumpData.Acceleration * Mathf.Pow(_jumpData.PeriodTime, 2)) / _jumpData.PeriodTime;
        }

        public override void Do()
        {
            base.Do();
            
            var velocityY = _jumpData.JumpSpeedY + _acceleration * PlayedTime;
            _player.RigidBody.linearVelocityY = velocityY;
            // _bodyPart.RigidBody.AddForce(Vector2.up);

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (_isFinish || _groundSensor.CheckSensor(GroundSensor.EDirection.Up))
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
