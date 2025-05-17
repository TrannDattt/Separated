using System.Timers;
using Separated.Data;
using Separated.Helpers;
using Unity.VisualScripting;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class AirBorneState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerBodyPart _bodyPart;

        public AirBorneState(EPlayerState key, StateDataSO data, Animator animator, PlayerBodyPart bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _bodyPart = bodyPart;
        }

        public override void Exit()
        {
            base.Exit();

            _inputProvider.UseInput(PlayerInput.EInputType.Jump);
        }

        public override EPlayerState GetNextState()
        {
            // TODO: Change to AIR_ATTACK when get MOUSE_0 input
            // TODO: Change to THROWN_AWAY when get heavy damage
            if (_inputProvider.DashInput)
            {
                return EPlayerState.Dash;
            }

            // if(_inputProvider.JumpInput){
            //     return EPlayerState.Jump;
            // }

            if (_isFinish && _groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                return EPlayerState.Land;
            }

            return EPlayerState.None;
        }
    }
}
