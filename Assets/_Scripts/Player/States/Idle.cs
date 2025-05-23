using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class Idle : GroundState
    {
        public Idle(EPlayerState key, StateDataSO data, Animator animator, PlayerControl bodyPart, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, bodyPart, inputProvider, groundSensor)
        {
        }

        public override void Do()
        {
            base.Do();

            _bodyPart.RigidBody.linearVelocity = Vector2.zero;
        }

        public override EPlayerState GetNextState()
        {
            if (base.GetNextState() == EPlayerState.None)
            {
                if (_inputProvider.MoveDir != 0)
                {
                    return EPlayerState.Run;
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
