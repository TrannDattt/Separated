using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Jump : AirBorneState
    {
        private JumpStateData _jumpData => _curStateData as JumpStateData;

        private float _firstVelocityX;
        private float _firstVelocityY;

        public Jump(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, bodyPart, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            // TODO: Use this input here when there are multiple jump
            // _inputProvider.UseInput(PlayerInput.EInputType.Jump);

            base.Enter();

            _firstVelocityX = _bodyPart.RigidBody.linearVelocityX;
            _firstVelocityY = (_jumpData.JumpDistance - .5f * _jumpData.Acceleration * Mathf.Pow(_jumpData.PeriodTime, 2)) / _jumpData.PeriodTime;
        }

        public override void Do()
        {
            base.Do();
            
            var velocityX = Mathf.Abs(_firstVelocityX) * _inputProvider.MoveDir;
            var velocityY = _firstVelocityY + _jumpData.Acceleration * PlayedTime;
            _bodyPart.RigidBody.linearVelocity = new Vector2(velocityX, velocityY);
            // _bodyPart.RigidBody.AddForce(Vector2.up);

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EPlayerState GetNextState()
        {
            if (base.GetNextState() == EPlayerState.None)
            {
                if (_isFinish)
                {
                    return EPlayerState.Fall;
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
