using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class AttackState : PlayerBaseState, ICanDoDamage
    {
        protected AttackData _attackData => _curStateData as AttackData;

        public float Damage => _attackData.Damage;
        public float PoiseDamage => _attackData.PoiseDamage;
        public Vector2 KnockbackDir => _attackData.KnockbackDir;
        public float KnockbackForce => _attackData.KnockbackForce;

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
            _hitbox.SetHitboxData(this);
        }

        public override void Exit()
        {
            base.Exit();

            // _hitbox.DisableHitbox();
            // _hitbox.ResetAttackData();
        }

        public override EBehaviorState GetNextState()
        {
            if (IsFinished)
            {
                return EBehaviorState.Idle;
            }

            return EBehaviorState.None;
        }

        public GameObject GetGameObject()
        {
            return _player.gameObject;
        }
    }
}
