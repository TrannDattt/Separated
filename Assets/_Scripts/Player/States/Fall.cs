using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEditor.Overlays;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Fall : AirBorneState
    {
        private FallStateData _fallData => _curStateData as FallStateData;
        private float _firstVelocityX;
        private float _firstVelocityY;

        public Fall(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _isFinish = true;
            _firstVelocityX = _player.RigidBody.linearVelocityX;
            _firstVelocityY = _player.RigidBody.linearVelocityY;
        }

        public override void Do()
        {
            base.Do();

            var velocityX = Mathf.Abs(_firstVelocityX) * _inputProvider.MoveDir;
            var velocityY = Mathf.Min(_firstVelocityY + _fallData.Acceleration * PlayedTime, _fallData.MaxFallSpeed);
            _player.RigidBody.linearVelocity = new Vector2(velocityX, -1 * velocityY);
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
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
