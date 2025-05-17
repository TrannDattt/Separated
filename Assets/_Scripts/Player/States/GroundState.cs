using System;
using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class GroundState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerBodyPart _bodyPart;

        public GroundState(EPlayerState key, StateDataSO data, Animator animator, PlayerBodyPart bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _bodyPart = bodyPart;
        }

        public override EPlayerState GetNextState()
        {
            if (_inputProvider.JumpInput && _groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                return EPlayerState.Jump;
            }
            if(!_groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                return EPlayerState.Fall;
            }
            // TODO: Change to HURT when get take light damage
            // TODO: Change to THROWN_AWAY when get heavy damage
            // TODO: Change to DIE when run out of HP
            
            if (_inputProvider.AttackInput)
            {
                return EPlayerState.Attack1;
            }

            if (_inputProvider.DashInput)
            {
                return EPlayerState.Dash;
            }

            return EPlayerState.None;
        }
    }
}
