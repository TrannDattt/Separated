using System.ComponentModel.Design;
using System.Timers;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Unity.VisualScripting;
using UnityEngine;
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

        public AirBorneState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _player = player;
        }

        public override void Do()
        {
            base.Do();

            _player.RigidBody.linearVelocityX = _inputProvider.MoveDir * Mathf.Abs(_firstVelocityX);
            _player.ChangeFaceDir();
        }

        public override void Exit()
        {
            base.Exit();

            _inputProvider.UseInput(PlayerInputManager.EActionInputType.Jump);
        }

        public override EBehaviorState GetNextState()
        {
            if (!_groundSensor.CheckSensor(EDirection.TopRight) && _groundSensor.CheckSensor(EDirection.MidRight))
            {
                Debug.Log("To climb state");
                return EBehaviorState.EdgeClimb;
            }

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
