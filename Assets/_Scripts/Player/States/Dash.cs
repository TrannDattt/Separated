using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class Dash : PlayerBaseState
    {
        private PlayerControl _bodyPart;
        private PlayerInput _inputProvider;
        private GroundSensor _groundSensor;

        private float _velocityX;
        private float _velocityXMult;

        public Dash(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator)
        {
            _bodyPart = bodyPart;
            _inputProvider = inputProvider;
            _groundSensor = groundSensor;
        }

        public override void Enter()
        {
            base.Enter();

            // _inputProvider.UseInput(PlayerInput.EInputType.Dash);
            

            _velocityX = (_stateData as DashStateData).Speed * _inputProvider.FaceDir;
            _velocityXMult = _velocityX / _bodyPart.RigidBody.linearVelocityX;
        }

        public override void Do()
        {
            base.Do();

            _bodyPart.RigidBody.linearVelocity = new Vector2(_velocityX, 0);

            if (PlayedTime >= _stateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override void Exit()
        {
            base.Exit();

            _inputProvider.UseInput(PlayerInput.EInputType.Dash);
            _bodyPart.RigidBody.linearVelocity = new(_bodyPart.RigidBody.linearVelocityX / _velocityXMult, 0);
        }

        public override EPlayerState GetNextState()
        {
            if (_isFinish)
            {
                if(_groundSensor.CheckSensor(GroundSensor.EDirection.Down))
                {
                    return EPlayerState.Land;
                }
                else
                {
                    return EPlayerState.Fall;
                }
            }
            return Key;
        }
    }
}
