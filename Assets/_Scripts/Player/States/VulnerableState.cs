using Separated.Data;
using Separated.Enums;
using UnityEngine;

namespace Separated.Player
{
    public class VulnerableState : PlayerBaseState
    {
        protected PlayerControl _player;

        public VulnerableState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player) : base(key, data, animator)
        {
            _player = player;
        }

        public override EBehaviorState GetNextState()
        {
            return EBehaviorState.None;
        }
    }
}
