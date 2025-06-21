using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Idle : GroundState
    {
        public Idle(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.RigidBody.linearVelocity = Vector2.zero;
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (_inputProvider.MoveDir != 0)
                {
                    return EBehaviorState.Run;
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
