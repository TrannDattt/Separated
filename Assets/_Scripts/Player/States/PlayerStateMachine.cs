using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using UnityEngine.Events;
using static Separated.Player.PlayerSkillManager;

namespace Separated.Player{
    public class PlayerStateMachine : BaseStateMachine<EBehaviorState>, IEventListener<Tuple<ESkillSlot, BeastData>>
    {
        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        [SerializeField] private JumpStateData _jumpData;
        [SerializeField] private FallStateData _fallData;
        [SerializeField] private LandStateData _landData;
        [SerializeField] private DashStateData _dashData;
        [SerializeField] private AttackData[] _meleeAttackDatas;
        // [SerializeField] private List<MeleeAttackStateData> _meleeAttackDatas;
        // [SerializeField] private SkillStateData[] _skillDatas;
        [SerializeField] private HurtStateData _hurtData;
        [SerializeField] private DieStateData _dieData;

        private PlayerControl _player;
        private PlayerInputManager _inputProvider;
        private GroundSensor _groundSensor;
        private Animator _animator;
        private UnitHitbox _hitbox;
        private PlayerSkillManager _skillManager;

        public void InitSM()
        {
            _stateDict.Clear();

            _stateDict.Add(EBehaviorState.Idle, new Idle(EBehaviorState.Idle, _idleData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EBehaviorState.Run, new Run(EBehaviorState.Run, _runData, _animator, _player, _inputProvider, _groundSensor));

            _stateDict.Add(EBehaviorState.Jump, new Jump(EBehaviorState.Jump, _jumpData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EBehaviorState.Fall, new Fall(EBehaviorState.Fall, _fallData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EBehaviorState.Land, new Land(EBehaviorState.Land, _landData, _animator, _player));
            _stateDict.Add(EBehaviorState.Dash, new Dash(EBehaviorState.Dash, _dashData, _animator, _player, _inputProvider, _groundSensor));
            
            _stateDict.Add(EBehaviorState.Attack, new MeleeAttack(EBehaviorState.Attack, _meleeAttackDatas, _meleeAttackDatas[0], _animator, _player, _inputProvider, this, _hitbox));
            // _stateDict.Add(EBehaviorState.Skill1, new SkillState(EBehaviorState.Skill1, _skillDatas[0], _animator, _inputProvider, _player, _hitbox));
            // _stateDict.Add(EBehaviorState.Skill2, new SkillState(EBehaviorState.Skill2, _skillDatas[1], _animator, _inputProvider, _player, _hitbox));
            // _stateDict.Add(EBehaviorState.Skill3, new SkillState(EBehaviorState.Skill3, _skillDatas[2], _animator, _inputProvider, _player, _hitbox));
            // _stateDict.Add(EBehaviorState.Skill4, new SkillState(EBehaviorState.Skill4, _skillDatas[3], _animator, _inputProvider, _player, _hitbox));
            // _stateDict.Add(EBehaviorState.Ultimate, new SkillState(EBehaviorState.Ultimate, _skillDatas[4], _animator, _inputProvider, _player, _hitbox));

            _stateDict.Add(EBehaviorState.Hurt, new Hurt(EBehaviorState.Hurt, _hurtData, _animator, _player));
            _stateDict.Add(EBehaviorState.Die, new Die(EBehaviorState.Die, _dieData, _animator));

            ChangeState(EBehaviorState.Idle);
        }

        public void UpdateState(EBehaviorState key, PlayerBaseState newState, bool forceChange = false)
        {
            if (_stateDict.ContainsKey(key))
            {
                _stateDict[key] = newState;
            }
            else
            {
                Debug.LogError($"State {key} not found in state dictionary.");
            }

            if (forceChange)
            {
                ChangeState(key);
            }
        }

        public T GetNextData<T>(T curData, T[] dataList) where T : StateDataSO
        {
            if (curData == null)
            {
                Debug.LogError("Current data is null.");
                return null;
            }

            var index = dataList.ToList().IndexOf(curData);
            var nextIndex = (index + 1) % dataList.Length;
            return dataList[nextIndex];
        }

        public EBehaviorState GetSkillKey(ESkillSlot slot)
        {
            return slot switch
            {
                ESkillSlot.Skill1 => EBehaviorState.Skill1,
                ESkillSlot.Skill2 => EBehaviorState.Skill2,
                ESkillSlot.Skill3 => EBehaviorState.Skill3,
                ESkillSlot.Skill4 => EBehaviorState.Skill4,
                ESkillSlot.Ultimate => EBehaviorState.Ultimate,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            };
        }

        public void OnEventNotify(Tuple<ESkillSlot, BeastData> eventData)
        {
            var skillKey = GetSkillKey(eventData.Item1);
            var skillData = eventData.Item2?.DefaultActiveSkill.NodeSkillData as SkillStateData;
            var newSkillState = new SkillState(skillKey, skillData, _animator, _inputProvider, _player, _hitbox);

            if (!_stateDict.ContainsKey(skillKey))
            {
                _stateDict.Add(skillKey, newSkillState);
                return;
            }

            UpdateState(skillKey, newSkillState);
        }

        void Awake()
        {
            _player = GetComponent<PlayerControl>();
            _inputProvider = GetComponent<PlayerInputManager>();
            _groundSensor = GetComponentInChildren<GroundSensor>();
            _animator = GetComponentInChildren<Animator>();
            _hitbox = GetComponentInChildren<UnitHitbox>();
            
            _skillManager = GetComponentInChildren<PlayerSkillManager>();
            _skillManager.Initialize();

            InitSM();
        }

        protected override void Update()
        {
            if (_player.Stat.CurHp == 0)
            {
                // ChangeState(EBehaviorState.Die);
            }

            if (_player.Stat.CurPoise == 0)
            {
                // ChangeState(EBehaviorState.KnockOut);
            }

            if (_player.IsTakingDamage)
            {
                _player.IsTakingDamage = false;
                ChangeState(EBehaviorState.Hurt);
            }

            base.Update();
        }
    }
}