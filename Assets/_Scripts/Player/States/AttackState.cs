using System.Collections.Generic;
using Separated.Data;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class AttackState : PlayerBaseState
    {
        protected AttackSkillData _attackData => _curStateData as AttackSkillData;

        protected PlayerInput _inputProvider;
        protected PlayerControl _player;
        protected PlayerStateMachine _stateMachine;
        protected UnitHitbox _hitbox;

        // public AttackState(EPlayerState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider) : base(key, data, animator)
        // {
        //     _player = player;
        //     _inputProvider = inputProvider;
        // }

        public AttackState(EPlayerState key, StateDataSO[] datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInput inputProvider, PlayerStateMachine stateMachine, UnitHitbox hitbox) : base(key, datas, data, animator)
        {
            _player = player;
            _inputProvider = inputProvider;
            _stateMachine = stateMachine;
            _hitbox = hitbox;
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
            _hitbox.SetAttackData(_attackData);
            _hitbox.EnableHitbox();
        }

        public override void Exit()
        {
            base.Exit();

            _hitbox.DisableHitbox();
            _hitbox.ResetAttackData();
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
