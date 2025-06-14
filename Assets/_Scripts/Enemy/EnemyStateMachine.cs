using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    public class EnemyStateMachine : BaseStateMachine<EBehaviorState>
    {
        [SerializeField] private IdleStateData _idleData;
        [SerializeField] private RunStateData _runData;
        [SerializeField] private AttackData[] _attackDatas;
        [SerializeField] private HurtStateData _hurtData;
        [SerializeField] private DieStateData _dieData;

        private EnemyControl _enemy;
        private Animator _animator;
        private UnitNavigator _navigator;
        private UnitHitbox _hitbox;
        private DropContainer _dropContainer;

        public void InitSM()
        {
            _stateDict.Clear();

            _stateDict.Add(EBehaviorState.Idle, new Idle(EBehaviorState.Idle, _idleData, _animator, _enemy, _navigator));
            _stateDict.Add(EBehaviorState.Run, new Run(EBehaviorState.Run, _runData, _animator, _enemy, _navigator));

            var curAttackData = GetRandomData(_attackDatas);
            _stateDict.Add(EBehaviorState.Attack, new AttackState(EBehaviorState.Attack, _attackDatas, curAttackData, _animator, _enemy, this, _hitbox, _navigator));

            _stateDict.Add(EBehaviorState.Hurt, new Hurt(EBehaviorState.Hurt, _hurtData, _animator, _enemy));
            _stateDict.Add(EBehaviorState.Die, new Die(EBehaviorState.Die, _dieData, _animator, _enemy, _dropContainer));

            _navigator.SetAttackData(curAttackData);
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
            _dropContainer = GetComponentInChildren<DropContainer>();

            InitSM();
        }

        protected override void Update()
        {
            if (_enemy.CurStatData.Hp == 0)
            {
                _enemy.CurStatData.Hp = -1;
                // Debug.Log("Change state");
                ChangeState(EBehaviorState.Die);
            }

            if (_enemy.IsTakingDamage && _enemy.CurStatData.Hp > 0)
            {
                _enemy.IsTakingDamage = false;
                ChangeState(EBehaviorState.Hurt);
            }

            base.Update();
        }
    }
}