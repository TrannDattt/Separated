using Separated.Data;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class AttackState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;

        public AttackState(EPlayerState key, StateDataSO data, Animator animator, PlayerInput inputProvider) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
        }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInput.EInputType.Attack);

            base.Enter();
        }

        public override EPlayerState GetNextState()
        {
            if (_isFinish)
            {
                return EPlayerState.Idle;
            }

            return EPlayerState.None;
        }
    }
}
