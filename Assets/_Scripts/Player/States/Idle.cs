using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Idle : GroundState
    {
        public Idle(EPlayerState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.RigidBody.linearVelocity = Vector2.zero;
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
