using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using Separated.Helpers;
using Separated.Skills;
using Separated.Unit;
using UnityEngine;

namespace Separated.Player{
    public class PlayerStateMachine : BaseStateMachine<PlayerStateMachine.EPlayerState>
    {
        public enum EPlayerState
        {
            None,
            Idle,
            Run,
            Dash,
            Jump,
            Fall,
            Land,
            Attack,
            // Attack2,
            // Attack3,
            Skill1,
            Skill2,
            Skill3,
            Skill4,
            Ultimate,
            Hurt,
            ThrownAway,
            GetUp,
            Die,
        }

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

            _stateDict.Add(EPlayerState.Idle, new Idle(EPlayerState.Idle, _idleData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EPlayerState.Run, new Run(EPlayerState.Run, _runData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EPlayerState.Jump, new Jump(EPlayerState.Jump, _jumpData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EPlayerState.Fall, new Fall(EPlayerState.Fall, _fallData, _animator, _player, _inputProvider, _groundSensor));
            _stateDict.Add(EPlayerState.Land, new Land(EPlayerState.Land, _landData, _animator, _player));
            _stateDict.Add(EPlayerState.Dash, new Dash(EPlayerState.Dash, _dashData, _animator, _player, _inputProvider, _groundSensor));

            // var attack3 = new MeleeAttack(EPlayerState.Attack3, _meleeAttackDatas[2], _animator, _player, _inputProvider, null);
            // _stateDict.Add(EPlayerState.Attack3, attack3);
            // var attack2 = new MeleeAttack(EPlayerState.Attack2, _meleeAttackDatas[1], _animator, _player, _inputProvider, attack3);
            // _stateDict.Add(EPlayerState.Attack2, attack2);
            // var attack1 = new MeleeAttack(EPlayerState.Attack1, _meleeAttackDatas[0], _animator, _player, _inputProvider, attack2);
            // _stateDict.Add(EPlayerState.Attack1, attack1);
            _stateDict.Add(EPlayerState.Attack, new MeleeAttack(EPlayerState.Attack, _meleeAttackDatas, _meleeAttackDatas[0], _animator, _player, _inputProvider, this, _hitbox));

            _stateDict.Add(EPlayerState.Skill1, new SkillState(EPlayerState.Skill1, _skillDatas[0], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EPlayerState.Skill2, new SkillState(EPlayerState.Skill2, _skillDatas[1], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EPlayerState.Skill3, new SkillState(EPlayerState.Skill3, _skillDatas[2], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EPlayerState.Skill4, new SkillState(EPlayerState.Skill4, _skillDatas[3], _animator, _inputProvider, _skillManager));
            _stateDict.Add(EPlayerState.Ultimate, new SkillState(EPlayerState.Ultimate, _skillDatas[4], _animator, _inputProvider, _skillManager));

            ChangeState(EPlayerState.Idle);

            _hitbox.DisableHitbox();
        }

        public void UpdateState(EPlayerState key, PlayerBaseState newState, bool forceChange = false)
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

        // public T GetNextData<T>(T curData, List<T> dataList) where T : StateDataSO
        // {
        //     if (curData == null)
        //     {
        //         Debug.LogError("Current data is null.");
        //         return null;
        //     }

        //     var index = dataList.IndexOf(curData);
        //     var nextIndex = (index + 1) % dataList.Count;
        //     return dataList[nextIndex];
        // }

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

        public T GetRandomData<T>(T[] dataList) where T : StateDataSO
        {
            if (dataList == null || dataList.Length == 0)
            {
                Debug.LogError("Data list is null or empty.");
                return null;
            }

            var randomIndex = Random.Range(0, dataList.Length);
            return dataList[randomIndex];
        }

        // public T GetRandomData<T>(List<T> dataList) where T : StateDataSO
        // {
        //     if (dataList == null || dataList.Count == 0)
        //     {
        //         Debug.LogError("Data list is null or empty.");
        //         return null;
        //     }

        //     var randomIndex = Random.Range(0, dataList.Count);
        //     return dataList[randomIndex];
        // }

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
    }
}