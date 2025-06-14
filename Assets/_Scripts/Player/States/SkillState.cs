using System.Collections;
using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Player
{
    public class SkillState : PlayerBaseState
    {
        private SkillStateData _skillData => _curStateData as SkillStateData;
        private PlayerInput _inputProvider;
        private PlayerControl _player;
        private UnitHitbox _hitbox;
        private UnityEvent<SkillStateData> _onSkillUsed;

        private Queue<SkillPhase> _phaseQueue = new();
        private SkillPhase _curPhase;
        private float _phaseStartTime;

        public bool IsInCoolDown;

        // TODO: Use 1 animation for whole skill instead of 1 for each phase
        public SkillState(EBehaviorState key, StateDataSO data, Animator animator, PlayerInput inputProvider, PlayerControl player, UnitHitbox hitbox, UnityEvent<SkillStateData> onSkillUsed) : base(key, data, animator)
        {
            _inputProvider = inputProvider;
            _player = player;
            _hitbox = hitbox;
            _onSkillUsed = onSkillUsed;
        }

        public override void Enter()
        {
            _inputProvider.UseInput(PlayerInput.EInputType.Skill);

            if (IsInCoolDown)
            {
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
                return;
            }

            base.Exit();

            IsInCoolDown = true;
            RuntimeCoroutine.Instance.StartRuntimeCoroutine(CooldownSkillCoroutine());
            _onSkillUsed?.Invoke(_skillData);
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish || IsInCoolDown)
            {
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
            _skillData.Clip = _curPhase.Clip;
            PlayAnim();
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
                    (_curPhase as MovePhase).Init(_player.RigidBody, _inputProvider.FaceDir);
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
