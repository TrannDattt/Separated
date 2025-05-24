using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Run : GroundState
    {
        public Run(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, bodyPart, inputProvider, groundSensor)
        {
        }

        public override void Do()
        {
            base.Do();

            _bodyPart.RigidBody.linearVelocity = new ((_curStateData as RunStateData).Speed * _inputProvider.MoveDir, _bodyPart.RigidBody.linearVelocityY);
        }

        public override EPlayerState GetNextState()
        {
            if (base.GetNextState() == EPlayerState.None)
            {
                if (_inputProvider.MoveDir == 0)
                {
                    return EPlayerState.Idle;
                }
                return Key;
            }
            else
            {
                return base.GetNextState();
            }
        }
    }
}
