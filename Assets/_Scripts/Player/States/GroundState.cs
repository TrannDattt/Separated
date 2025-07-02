using System;
using System.Collections;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public abstract class GroundState : PlayerBaseState
    {
        protected PlayerInputManager _inputProvider;
        protected GroundSensor _groundSensor;
        protected PlayerControl _player;

        private float _coyoteTime = .2f;
        private bool _inCoyoteTime = true;
        private bool _isCountdownCoyote = false;

        public GroundState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();

            if (!_groundSensor.CheckSensor(GroundSensor.EDirection.Down))
            {
                _inCoyoteTime = false;
            }
        }

        public override void Do()
        {
            base.Do();

            _player.ChangeFaceDir();

            if (!_groundSensor.CheckSensor(GroundSensor.EDirection.Down) && !_isCountdownCoyote)
            {
                _isCountdownCoyote = true;
                ResetCoyoteTime();
            }
        }

        public override void Exit()
        {
            base.Exit();

            _inCoyoteTime = true;
            _isCountdownCoyote = false;
        }

        public override EBehaviorState GetNextState()
        {
            if (_inputProvider.JumpInput && (_groundSensor.CheckSensor(GroundSensor.EDirection.Down) || _inCoyoteTime))
            {
                return EBehaviorState.Jump;
            }
            if (!_groundSensor.CheckSensor(GroundSensor.EDirection.Down) && !_inCoyoteTime)
            {
                return EBehaviorState.Fall;
            }

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

        private void ResetCoyoteTime()
        {
            IEnumerator ResetCoyoteTimeCoroutine()
            {
                yield return new WaitForSeconds(_coyoteTime);
                _inCoyoteTime = false;
                _isCountdownCoyote = false;
            }

            RuntimeCoroutine.Instance.StartRuntimeCoroutine(ResetCoyoteTimeCoroutine());
        }
    }
}
