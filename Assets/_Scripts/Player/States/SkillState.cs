using Separated.Data;
using Separated.Interfaces;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class SkillState : PlayerBaseState, ICanDoDamage
    {
        public virtual float Damage { get; }
        public virtual float PoiseDamage { get; }
        public virtual Vector2 KnockbackDir { get; }
        public virtual float KnockbackForce { get; }

        protected PlayerInput _inputProvider;

        // TODO: Make a skill acxercuter class to handle skill logic
        public SkillState(EPlayerState key, StateDataSO data, Animator animator, PlayerInput inputProvider) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
        }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInput.EInputType.Skill);

            base.Enter();
        }

        public override void Do()
        {
            base.Do();

            if(PlayedTime >= _stateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EPlayerState GetNextState()
        {
            if (_isFinish)
            {
                return EPlayerState.Idle;
            }

            return Key;
        }
    }
}
