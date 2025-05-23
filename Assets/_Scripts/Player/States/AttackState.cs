using Separated.Data;
using Separated.Interfaces;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class AttackState : PlayerBaseState, ICanDoDamage
    {
        public virtual float Damage { get; }
        public virtual float PoiseDamage { get; }
        public virtual Vector2 KnockbackDir { get; }
        public virtual float KnockbackForce { get; }

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
