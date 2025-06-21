using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class AttackState : PlayerBaseState
    {
        protected AttackData _attackData => _curStateData as AttackData;

        protected PlayerInputManager _inputProvider;
        protected PlayerControl _player;
        protected PlayerStateMachine _stateMachine;
        protected UnitHitbox _hitbox;

        public AttackState(EBehaviorState key, StateDataSO[] datas, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, PlayerStateMachine stateMachine, UnitHitbox hitbox) : base(key, datas, data, animator)
        {
            _player = player;
            _inputProvider = inputProvider;
            _stateMachine = stateMachine;
            _hitbox = hitbox;
        }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInputManager.EActionInputType.Attack);

            base.Enter();

            _player.RigidBody.linearVelocityX = 0;
            _hitbox.SetAttackData(_attackData);
            // _hitbox.EnableHitbox();
        }

        public override void Exit()
        {
            base.Exit();

            // _hitbox.DisableHitbox();
            // _hitbox.ResetAttackData();
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish)
            {
                return EBehaviorState.Idle;
            }

            return EBehaviorState.None;
        }
    }
}
