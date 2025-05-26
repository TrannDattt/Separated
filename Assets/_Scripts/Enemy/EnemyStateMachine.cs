using Separated.Data;
using Separated.Helpers;
using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    public class EnemyStateMachine : BaseStateMachine<EnemyStateMachine.EEnemyState>
    {
        public enum EEnemyState
        {
            None,
            Idle,
            Run,
            Attack,
            Hurt,
            Die,
        }

        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        [SerializeField] private AttackSkillData[] _attackDatas;
        // [SerializeField] private HurtStateData _hurtData;
        // [SerializeField] private DieStateData _dieData;

        private EnemyControl _enemy;
        private Animator _animator;
        private UnitNavigator _navigator;
        private UnitHitbox _hitbox;

        public void InitSM()
        {
            _navigator = new UnitNavigator();
            _stateDict.Clear();

            _stateDict.Add(EEnemyState.Idle, new Idle(EEnemyState.Idle, _idleData, _animator, _enemy, _navigator));
            _stateDict.Add(EEnemyState.Run, new Run(EEnemyState.Run, _runData, _animator, _enemy, _navigator));

            var curAttackData = GetRandomData(_attackDatas);
            _stateDict.Add(EEnemyState.Attack, new AttackState(EEnemyState.Attack, _attackDatas, curAttackData, _animator, _enemy, this, _hitbox));
            // _stateDict.Add(EEnemyState.Hurt, new Hurt(EEnemyState.Hurt, _hurtData));
            // _stateDict.Add(EEnemyState.Die, new Die(EEnemyState.Die, _dieData));

            _navigator.SetAttackData(curAttackData);
        }

        public void UpdateState(EEnemyState key, EnemyBaseState newState, bool forceChange = false)
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

        void Start()
        {
            _enemy = GetComponent<EnemyControl>();
            _animator = GetComponentInChildren<Animator>();
            _hitbox = GetComponentInChildren<UnitHitbox>();
        }
    }
}