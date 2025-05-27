using System;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public abstract class GroundState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerControl _player;

        public GroundState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _player = player;
        }

        public override EBehaviorState GetNextState()
        {
            if (_inputProvider.JumpInput && _groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                return EBehaviorState.Jump;
            }
            if(!_groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                return EBehaviorState.Fall;
            }
            
            // TODO: Change to HURT when get take light damage
            // TODO: Change to THROWN_AWAY when get heavy damage
            // TODO: Change to DIE when run out of HP

            if (_inputProvider.Skill1Input)
            {
                return EBehaviorState.Skill1;
            }

            if (_inputProvider.Skill2Input)
            {
                return EBehaviorState.Skill2;
            }

            if (_inputProvider.Skill3Input)
            {
                return EBehaviorState.Skill3;
            }

            if (_inputProvider.Skill4Input)
            {
                return EBehaviorState.Skill4;
            }

            if (_inputProvider.UltimateInput)
            {
                return EBehaviorState.Ultimate;
            }
            
            if (_inputProvider.AttackInput)
            {
                return EBehaviorState.Attack;
            }

            if (_inputProvider.DashInput)
            {
                return EBehaviorState.Dash;
            }

            return EBehaviorState.None;
        }
    }
}
