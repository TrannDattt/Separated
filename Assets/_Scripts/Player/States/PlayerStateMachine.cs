using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Skills;
using Separated.Unit;
using UnityEngine;

namespace Separated.Player{
    public class PlayerStateMachine : BaseStateMachine<EBehaviorState>
    {
        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        [SerializeField] private JumpStateData _jumpData;
        [SerializeField] private FallStateData _fallData;
        [SerializeField] private LandStateData _landData;
        [SerializeField] private DashStateData _dashData;
        [SerializeField] private AttackSkillData[] _meleeAttackDatas;
        // [SerializeField] private List<MeleeAttackStateData> _meleeAttackDatas;
        [SerializeField] private SkillStateData[] _skillDatas;

        private PlayerControl _player;
        private PlayerInput _inputProvider;
        private GroundSensor _groundSensor;
        private SkillManager _skillManager;
        private Animator _animator;
        private UnitHitbox _hitbox;

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
            _stateDict.Add(EBehaviorState.Skill1, new SkillState(EBehaviorState.Skill1, _skillDatas[0], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EBehaviorState.Skill2, new SkillState(EBehaviorState.Skill2, _skillDatas[1], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EBehaviorState.Skill3, new SkillState(EBehaviorState.Skill3, _skillDatas[2], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EBehaviorState.Skill4, new SkillState(EBehaviorState.Skill4, _skillDatas[3], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EBehaviorState.Ultimate, new SkillState(EBehaviorState.Ultimate, _skillDatas[4], _animator, _inputProvider, _skillManager));

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

        void Awake()
        {
            _player = GetComponent<PlayerControl>();
            _inputProvider = GetComponent<PlayerInput>();
            _groundSensor = GetComponentInChildren<GroundSensor>();
            _skillManager = GetComponentInChildren<SkillManager>();
            _animator = GetComponentInChildren<Animator>();
            _hitbox = GetComponentInChildren<UnitHitbox>();

            InitSM();
        }

        // void Start()
        // {
        //     _hitbox.DisableHitbox();
        // }
    }
}