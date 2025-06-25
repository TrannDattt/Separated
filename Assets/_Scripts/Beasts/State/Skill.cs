using System.Collections;
using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class Skill : BeastBaseState
    {
        private SkillStateData _skillData => CurStateData as SkillStateData;
        private BeastControl _beast;
        private BeastStateMachine _stateMachine;
        private UnitHitbox _hitbox;
        private UnitNavigator _navigator;

        private Queue<SkillPhase> _phaseQueue = new();
        private SkillPhase _curPhase;
        private float _phaseStartTime;

        public bool IsInCoolDown;

        // TODO: Use 1 animation for whole skill instead of 1 for each phase
        public Skill(EBehaviorState key, StateDataSO data, Animator animator, BeastControl beast, BeastStateMachine stateMachine, UnitNavigator navigator, UnitHitbox hitbox) : base(key, data, animator)
        {
            _beast = beast;
            _stateMachine = stateMachine;
            _hitbox = hitbox;
            _navigator = navigator;
        }

        public override void Enter()
        {
            if (IsInCoolDown)
            {
                // Debug.Log("In cooldown");
                return;
            }

            _phaseQueue.Clear();
            _skillData.PeriodTime = 0;
            foreach (var phase in _skillData.SkillPhases)
            {
                _phaseQueue.Enqueue(phase);
                _skillData.PeriodTime += phase.PhaseDuration;
            }

            base.Enter();

            _phaseStartTime = 0;
            ChangeToNextPhase();
        }

        public override void Do()
        {
            base.Do();

            _curPhase.Do();

            if (PlayedTime > _phaseStartTime + _curPhase.PhaseDuration)
            {
                _phaseStartTime = PlayedTime;
                ChangeToNextPhase();
            }

            if (PlayedTime >= _skillData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override void Exit()
        {
            if (IsInCoolDown)
            {
                var replacedSkill = _stateMachine.GetRandomSkill();
                _navigator.SetSkillData(CurStateData as SkillStateData);
                _stateMachine.UpdateState(replacedSkill.Key, replacedSkill, true);
                return;
            }

            base.Exit();

            IsInCoolDown = true;
            RuntimeCoroutine.Instance.StartRuntimeCoroutine(CooldownSkillCoroutine());

            var nextSkill = _stateMachine.GetRandomSkill();
            _navigator.SetSkillData(nextSkill.CurStateData as SkillStateData);
            _stateMachine.UpdateState(nextSkill.Key, nextSkill);
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish || IsInCoolDown)
            {
                // Debug.Log("idle");
                return EBehaviorState.Idle;
            }

            return Key;
        }

        private void ChangeToNextPhase()
        {
            _curPhase?.Exit();
            if (_phaseQueue.Count == 0)
            {
                return;
            }

            _curPhase = _phaseQueue.Dequeue();
            InitPhase();
        }

        private void InitPhase()
        {
            switch (_curPhase.SkillType)
            {
                case ESkillPhaseType.AttackSkill:
                    (_curPhase as AttackPhase).Init(_hitbox);
                    break;

                case ESkillPhaseType.BuffSkill:
                    break;

                case ESkillPhaseType.SummonSkill:
                    break;

                case ESkillPhaseType.MovingSkill:
                    var moveDirX = _navigator.GetMoveDirection().x < 0 ? -1 : 1;
                    (_curPhase as MovePhase).Init(_beast.RigidBody, moveDirX);
                    break;

                default:
                    break;
            }
        }

        private IEnumerator CooldownSkillCoroutine()
        {
            yield return new WaitForSeconds(_skillData.Cooldown);
            IsInCoolDown = false;
        }
    }
}