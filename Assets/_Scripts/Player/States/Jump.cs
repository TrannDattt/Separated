using Separated.Data;
using Separated.Enums;
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

        public Jump(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            // TODO: Use this input here when there are multiple jump
            // _inputProvider.UseInput(PlayerInput.EInputType.Jump);

            base.Enter();

            _firstVelocityX = _player.RigidBody.linearVelocityX;
            _firstVelocityY = (_jumpData.JumpDistance - .5f * _jumpData.Acceleration * Mathf.Pow(_jumpData.PeriodTime, 2)) / _jumpData.PeriodTime;
        }

        public override void Do()
        {
            base.Do();
            
            var velocityX = Mathf.Abs(_firstVelocityX) * _inputProvider.MoveDir;
            var velocityY = _firstVelocityY + _jumpData.Acceleration * PlayedTime;
            _player.RigidBody.linearVelocity = new Vector2(velocityX, velocityY);
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
                if (_isFinish)
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
