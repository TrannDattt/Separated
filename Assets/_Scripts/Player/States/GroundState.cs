using System;
using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class GroundState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerControl _bodyPart;

        public GroundState(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
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

            if(_inputProvider.Skill1Input)
            {
                return EPlayerState.Skill1;
            }

            if (_inputProvider.Skill2Input)
            {
                return EPlayerState.Skill2;
            }

            if (_inputProvider.Skill3Input)
            {
                return EPlayerState.Skill3;
            }

            if (_inputProvider.Skill4Input)
            {
                return EPlayerState.Skill4;
            }

            if (_inputProvider.UltimateInput)
            {
                return EPlayerState.Ultimate;
            }
            
            if (_inputProvider.AttackInput)
            {
                return EPlayerState.Attack;
            }

            if (_inputProvider.DashInput)
            {
                return EPlayerState.Dash;
            }

            return EPlayerState.None;
        }
    }
}
