using Separated.Data;
using Separated.Enums;
using Separated.Interfaces;
using Separated.Skills;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class SkillState : PlayerBaseState
    {
        protected PlayerInput _inputProvider;
        protected SkillManager _skillManager;

        // TODO: Make a skill acxercuter class to handle skill logic
        public SkillState(EBehaviorState key, StateDataSO data, Animator animator, PlayerInput inputProvider, SkillManager skillManager) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _skillManager = skillManager;
        }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInput.EInputType.Skill);

            base.Enter();
        }

        public override void Do()
        {
            base.Do();

            if(PlayedTime >= _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish)
            {
                return EBehaviorState.Idle;
            }

            return Key;
        }
    }
}
