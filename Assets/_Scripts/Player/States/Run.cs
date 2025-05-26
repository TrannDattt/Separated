using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Run : GroundState
    {
        private RunStateData _runData => _curStateData as RunStateData;

        public Run(EPlayerState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Do()
        {
            base.Do();

            _player.RigidBody.linearVelocity = new (_runData.Speed * _inputProvider.MoveDir, _player.RigidBody.linearVelocityY);
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
