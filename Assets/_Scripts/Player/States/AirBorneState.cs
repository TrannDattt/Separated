using System.Timers;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Unity.VisualScripting;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public abstract class AirBorneState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerControl _player;

        protected float _firstVelocityX;

        public AirBorneState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _player = player;
        }

        public override void Do()
        {
            base.Do();

            _player.RigidBody.linearVelocityX = _inputProvider.MoveDir * Mathf.Abs(_firstVelocityX);
        }

        public override void Exit()
        {
            base.Exit();

            _inputProvider.UseInput(PlayerInput.EInputType.Jump);
        }

        public override EBehaviorState GetNextState()
        {
            // TODO: Change to AIR_ATTACK when get MOUSE_0 input
            // TODO: Change to THROWN_AWAY when get heavy damage
            if (_inputProvider.DashInput)
            {
                return EBehaviorState.Dash;
            }

            // if(_inputProvider.JumpInput){
            //     return EBehaviorState.Jump;
            // }

            if (_isFinish && _groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                return EBehaviorState.Land;
            }

            return EBehaviorState.None;
        }
    }
}
