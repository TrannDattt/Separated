using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Enemies.EnemyStateMachine;

namespace Separated.Enemies
{
    public abstract class EnemyBaseState : BaseState<EEnemyState>
    {
        public float PlayedTime => Time.time - _startTime;
        
        protected StateDataSO _curStateData;
        protected StateDataSO[] _stateDataList;
        protected Animator _animator;

        protected float _startTime;
        protected bool _isFinish;

        protected EnemyBaseState(EEnemyState key, StateDataSO data, Animator animator) : base(key)
        {
            _curStateData = data;
            _animator = animator;
        }

        protected EnemyBaseState(EEnemyState key, StateDataSO[] datas, StateDataSO data, Animator animator) : base(key)
        {
            _stateDataList = datas;
            _curStateData = data;
            _animator = animator;
        }

        protected virtual void PlayAnim()
        {
            if (_curStateData.Clip != null)
            {
                _animator.speed = _curStateData.Clip.length / _curStateData.PeriodTime;
                _animator.Play(_curStateData.Clip.name);
            }
        }

        public override void Enter()
        {
            _isFinish = false;
            _startTime = Time.time;

            PlayAnim();
        }

        public override void Exit()
        {
            // throw new System.NotImplementedException();
        }

        public override void Do()
        {
            // throw new System.NotImplementedException();
        }

        public override void FixedDo()
        {
            // throw new System.NotImplementedException();
        }

        public override EEnemyState GetNextState()
        {
            return Key;
        }
    }
}