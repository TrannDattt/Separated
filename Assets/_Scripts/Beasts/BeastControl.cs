using System.Collections;
using System.Linq;
using System.Timers;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BeastControl : BaseStateMachine<EBehaviorState>, ISummonable
    {
        private BeastData _data;
        public EBeastType Type => _data ? _data.Type : EBeastType.Null;

        private IdleStateData _idleData => _data.IdleData;
        private RunStateData _runData => _data.RunData;
        private SkillStateData[] _skillDatas => _data.SkillDatas;
        private DieStateData _dieData => _data.DieData;

        private Skill[] _skillStates;

        private Rigidbody2D _body;
        private Animator _animator;
        private UnitNavigator _navigator;
        private UnitHitbox _hitbox;

        public Rigidbody2D Rigigbody => _body;
        private PlayerControl _player => PlayerControl.Instance;

        public void Initialize(BeastData data)
        {
            _data = data;

            _hitbox = GetComponentInChildren<UnitHitbox>();
            _navigator = new UnitNavigator(gameObject, _data.TriggerRange, EUnitType.Enemy);
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();

            _animator.runtimeAnimatorController = _data.AnimControl;
            _stateDict.Clear();

            _stateDict.Add(EBehaviorState.Idle, new Idle(EBehaviorState.Idle, _idleData, _animator, this, _navigator));
            _stateDict.Add(EBehaviorState.Run, new Run(EBehaviorState.Run, _runData, _animator, this, _navigator));

            _skillStates = new Skill[_skillDatas.Length];
            for (int i = 0; i < _skillDatas.Length; i++)
            {
                var newSkillState = new Skill(EBehaviorState.Attack, _skillDatas[i], _animator, this, _navigator, _hitbox);
                _skillStates[i] = newSkillState;
            }
            var curSkillState = _skillStates[0];
            _stateDict.Add(curSkillState.Key, curSkillState);

            _stateDict.Add(EBehaviorState.Die, new Die(EBehaviorState.Die, _dieData, _animator, this));

            _navigator.SetSkillData(curSkillState.CurStateData as SkillStateData);
            ChangeState(EBehaviorState.Idle);

            StartCoroutine(CooldownExistCoroutine(_data.ExistTime));
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
            // Debug.Log(randomIndex);
            return readySkills.Length > 0 ? readySkills[randomIndex] : _skillStates[0];
        }

        public override void ChangeState(EBehaviorState nextKey, bool wait = false)
        {
            base.ChangeState(nextKey);
            Debug.Log($"Beast State Changed to: {nextKey}");
        }

        private IEnumerator CooldownExistCoroutine(float existTime)
        {
            yield return new WaitForSeconds(existTime);
            ChangeState(EBehaviorState.Die);
        }
    }
}
