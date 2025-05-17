using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class Jump : AirBorneState
    {
        private JumpStateData _jumpData => _stateData as JumpStateData;

        private float _firstVelocityX;
        private float _firstVelocityY;

        public Jump(EPlayerState key, StateDataSO data, Animator animator, PlayerBodyPart bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, bodyPart, inputProvider, groundSensor)
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

            if (PlayedTime >= _stateData.PeriodTime)
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
