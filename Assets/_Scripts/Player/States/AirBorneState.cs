using System.ComponentModel.Design;
using System.Timers;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using static Separated.GameManager.EventManager;
using static Separated.Helpers.GroundSensor;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public abstract class AirBorneState : PlayerBaseState
    {
        protected PlayerInputManager _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerControl _player;

        protected float _firstVelocityX;

        private Event<EEventType> _touchGroundEvent = new();

        public AirBorneState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _player = player;
            _touchGroundEvent = GetEvent(EEventType.TouchedGround);
        }

        public override void Do()
        {
            base.Do();

            _player.RigidBody.linearVelocityX = _inputProvider.MoveDir * Mathf.Abs(_firstVelocityX);
            _player.ChangeFaceDir();
        }

        public override EBehaviorState GetNextState()
        {
            if (!_groundSensor.CheckSensor(EDirection.TopRight, 1) && _groundSensor.CheckSensor(EDirection.MidRight))
            {
                return EBehaviorState.EdgeClimb;
            }

            // TODO: Change to AIR_ATTACK when get MOUSE_0 input
            // TODO: Change to THROWN_AWAY when get heavy damage
            if (_inputProvider.DashInput)
            {
                return EBehaviorState.Dash;
            }

            if (_inputProvider.JumpInput)
            {
                return EBehaviorState.AddtionalJump;
            }
            
            if (IsFinished && _groundSensor.CheckSensor(EDirection.Down))
            {
                _touchGroundEvent.Notify();
                return EBehaviorState.Land;
            }

            return EBehaviorState.None;
        }
    }
}
