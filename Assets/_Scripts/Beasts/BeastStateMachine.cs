using System.Linq;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class BeastStateMachine : BaseStateMachine<EBehaviorState>
    {

        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        [SerializeField] private SkillStateData[] _skillDatas;

        private Skill[] _skillStates;

        private BeastControl _beast;
        private Animator _animator;
        private UnitNavigator _navigator;
        private UnitHitbox _hitbox;

        public void InitSM()
        {
            _hitbox = GetComponentInChildren<UnitHitbox>();
            _navigator = new UnitNavigator(_beast.gameObject, _beast.Data.TriggerRange, EUnitType.Enemy);

            _stateDict.Clear();

            _stateDict.Add(EBehaviorState.Idle, new Idle(EBehaviorState.Idle, _idleData, _animator, _beast, _navigator));
            _stateDict.Add(EBehaviorState.Run, new Run(EBehaviorState.Run, _runData, _animator, _beast, _navigator));

            _skillStates = new Skill[_skillDatas.Length];
            for (int i = 0; i < _skillDatas.Length; i++)
            {
                var newSkillState = new Skill(EBehaviorState.Skill1, _skillDatas[i], _animator, _beast, this, _navigator, _hitbox);
                _skillStates[i] = newSkillState;
            }
            var curSkillState = _skillStates[0];
            _stateDict.Add(curSkillState.Key, curSkillState);
            
            _navigator.SetSkillData(curSkillState.CurStateData as SkillStateData);
            ChangeState(EBehaviorState.Idle);
        }

        public void UpdateState(EBehaviorState key, BeastBaseState newState, bool forceChange = false)
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

        public Skill GetRandomSkill()
        {
            if (_skillStates == null || _skillStates.Length == 0)
            {
                Debug.LogError("Data list is null or empty.");
                return null;
            }
            var readySkills = _skillStates.Where(s => !s.IsInCoolDown).ToArray();
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
            InitSM();
        }

        protected override void Update()
        {
            // if (_beast.Stat.CurHp == 0 && !_beast.Stat.IsDead)
            // {
            //     _beast.Stat.IsDead = true;
            //     // Debug.Log("Die");
            //     ChangeState(EBehaviorState.Die);
            // }

            // if (_beast.IsTakingDamage && _beast.Stat.CurHp > 0)
            // {
            //     _beast.IsTakingDamage = false;
            //     ChangeState(EBehaviorState.Hurt);
            // }

            base.Update();
        }
    }
}
