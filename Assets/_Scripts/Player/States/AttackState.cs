using System.Collections.Generic;
using Separated.Data;
using Separated.Interfaces;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class AttackState : PlayerBaseState, ICanDoDamage
    {
        public virtual float Damage { get; }
        public virtual float PoiseDamage { get; }
        public virtual Vector2 KnockbackDir { get; }
        public virtual float KnockbackForce { get; }

        protected PlayerInput _inputProvider;
        protected PlayerControl _player;
        protected PlayerStateMachine _stateMachine;

        // public AttackState(EPlayerState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider) : base(key, data, animator)
        // {
        //     _player = player;
        //     _inputProvider = inputProvider;
        // }

        public AttackState(EPlayerState key, StateDataSO[] datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine) : base(key, datas, data, animator)
        {
            _player = player;
            _inputProvider = inputProvider;
            _stateMachine = stateMachine;
        }

        // public AttackState(EPlayerState key, List<StateDataSO> datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine) : base(key, datas, data, animator)
        // {
        //     _player = player;
        //     _inputProvider = inputProvider;
        //     _stateMachine = stateMachine;
        // }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInput.EInputType.Attack);

            base.Enter();

            _player.RigidBody.linearVelocityX = 0;
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
