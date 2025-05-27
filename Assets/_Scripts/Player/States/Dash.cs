using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Dash : PlayerBaseState
    {
        private PlayerControl _player;
        private PlayerInput _inputProvider;
        private GroundSensor _groundSensor;

        private float _velocityX;
        private float _velocityXMult;

        public Dash(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _player = player;
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
        }

        public override void Enter()
        {
            base.Enter();

            // _inputProvider.UseInput(PlayerInput.EInputType.Dash);
            

            _velocityX = (_curStateData as DashStateData).Speed * _inputProvider.FaceDir;
            _velocityXMult = _velocityX / _player.RigidBody.linearVelocityX;
        }

        public override void Do()
        {
            base.Do();

            _player.RigidBody.linearVelocity = new Vector2(_velocityX, 0);

            if (PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override void Exit()
        {
            base.Exit();

            _inputProvider.UseInput(PlayerInput.EInputType.Dash);
            _player.RigidBody.linearVelocity = new(_player.RigidBody.linearVelocityX / _velocityXMult, 0);
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish)
            {
                if(_groundSensor.CheckSensor(GroundSensor.EDirection.Down))
                {
                    return EBehaviorState.Land;
                }
                else
                {
                    return EBehaviorState.Fall;
                }
            }
            return Key;
        }
    }
}
