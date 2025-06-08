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
        private float _acceleration;

        public Fall(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _isFinish = true;
            _firstVelocityX = _fallData.FallSpeedX;
            _acceleration = _fallData.MaxFallSpeedY / _fallData.PeriodTime;
        }

        public override void Do()
        {
            base.Do();
            
            var velocityY = Mathf.Min(_acceleration * PlayedTime, _fallData.MaxFallSpeedY);
            _player.RigidBody.linearVelocityY = velocityY;
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
