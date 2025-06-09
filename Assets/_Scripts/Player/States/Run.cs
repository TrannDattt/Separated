using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class Run : GroundState
    {
        private RunStateData _runData => _curStateData as RunStateData;

        public Run(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
        }

        public override void Do()
        {
            base.Do();

            _player.RigidBody.linearVelocity = new(_runData.Speed * _inputProvider.MoveDir, _player.RigidBody.linearVelocityY);
        }

        public override EBehaviorState GetNextState()
        {
            if (base.GetNextState() == EBehaviorState.None)
            {
                if (_inputProvider.MoveDir == 0)
                {
                    return EBehaviorState.Idle;
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
