using Separated.Data;
using Separated.Enums;
using UnityEngine;

namespace Separated.Player
{
    public class Die : PlayerBaseState
    {
        private PlayerControl _player;

        public Die(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player) : base(key, data, animator)
        {
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();

            _player.RigidBody.linearVelocity = Vector2.zero;
        }
    }
}
