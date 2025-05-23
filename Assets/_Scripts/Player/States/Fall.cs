using Separated.Data;
using Separated.Helpers;
using UnityEditor.Overlays;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class Fall : AirBorneState
    {
        private FallStateData _fallData => _stateData as FallStateData;
        private float _firstVelocityX;
        private float _firstVelocityY;

        public Fall(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, bodyPart, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _isFinish = true;
            _firstVelocityX = _bodyPart.RigidBody.linearVelocityX;
            _firstVelocityY = _bodyPart.RigidBody.linearVelocityY;
        }

        public override void Do()
        {
            base.Do();

            var velocityX = Mathf.Abs(_firstVelocityX) * _inputProvider.MoveDir;
            var velocityY = Mathf.Min(_firstVelocityY + _fallData.Acceleration * PlayedTime, _fallData.MaxFallSpeed);
            // _bodyPart.RigidBody.linearVelocity = new Vector2(velocityX, -1 * velocityY);
        }

        public override EPlayerState GetNextState()
        {
            if (base.GetNextState() == EPlayerState.None)
            {
                // TODO: Change to DIE when get fall for too long

                return Key;
            }
            else
            {
                return base.GetNextState();
            }
        }
    }
}
