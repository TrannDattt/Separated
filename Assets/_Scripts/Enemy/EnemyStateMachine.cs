using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Enemies
{
    public class EnemyStateMachine : BaseStateMachine<EBehaviorState>
    {
        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        // [SerializeField] private AttackData[] _attackDatas;
        [SerializeField] private SkillStateData[] _skillDatas;
        [SerializeField] private HurtStateData _hurtData;
        [SerializeField] private DieStateData _dieData;

        [SerializeField] private UnityEvent<SkillStateData> _onSkillUsed;

        private SkillState[] _skillStateArr;

        private EnemyControl _enemy;
        private Animator _animator;
        private UnitNavigator _navigator;
        private UnitHitbox _hitbox;

        public void InitSM()
        {
            _stateDict.Clear();

            _stateDict.Add(EBehaviorState.Idle, new Idle(EBehaviorState.Idle, _idleData, _animator, _enemy, _navigator));
            _stateDict.Add(EBehaviorState.Run, new Run(EBehaviorState.Run, _runData, _animator, _enemy, _navigator));

            // var curAttackData = GetRandomData(_attackDatas);
            // _stateDict.Add(EBehaviorState.Attack, new AttackState(EBehaviorState.Attack, _attackDatas, curAttackData, _animator, _enemy, this, _hitbox, _navigator));

            _skillStateArr = new SkillState[_skillDatas.Length];
            for (int i = 0; i < _skillDatas.Length; i++)
            {
                var newSkillState = new SkillState(EBehaviorState.Skill1, _skillDatas[i], _animator, _enemy, this, _navigator, _hitbox, _onSkillUsed);
                _skillStateArr[i] = newSkillState;
            }
            var curSkillState = _skillStateArr[0];
            _stateDict.Add(curSkillState.Key, curSkillState);

            _stateDict.Add(EBehaviorState.Hurt, new Hurt(EBehaviorState.Hurt, _hurtData, _animator, _enemy));
            _stateDict.Add(EBehaviorState.Die, new Die(EBehaviorState.Die, _dieData, _animator, _enemy));

            _navigator.SetSkillData(curSkillState.CurStateData as SkillStateData);
            ChangeState(EBehaviorState.Idle);
        }

        public void UpdateState(EBehaviorState key, EnemyBaseState newState, bool forceChange = false)
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

        public SkillState GetRandomSkill()
        {
            if (_skillStateArr == null || _skillStateArr.Length == 0)
            {
                Debug.LogError("Data list is null or empty.");
                return null;
            }
            var readySkills = _skillStateArr.Where(s => !s.IsInCoolDown).ToArray();
            // Debug.Log(readySkills.Length);

            var randomIndex = Random.Range(0, readySkills.Length);
            return readySkills[randomIndex];
        }

        public override void ChangeState(EBehaviorState nextKey)
        {
            base.ChangeState(nextKey);
            // Debug.Log($"Enemy State Changed to: {nextKey}");
        }

        void Start()
        {
            _enemy = GetComponent<EnemyControl>();
            _animator = GetComponentInChildren<Animator>();
            _hitbox = GetComponentInChildren<UnitHitbox>();
            _navigator = new UnitNavigator();

            InitSM();
        }

        protected override void Update()
        {
            if (_enemy.Stat.CurHp == 0 && !_enemy.Stat.IsDead)
            {
                _enemy.Stat.IsDead = true;
                // Debug.Log("Die");
                ChangeState(EBehaviorState.Die);
            }

            if (_enemy.IsTakingDamage && _enemy.Stat.CurHp > 0)
            {
                _enemy.IsTakingDamage = false;
                ChangeState(EBehaviorState.Hurt);
            }

            base.Update();
        }
    }
}