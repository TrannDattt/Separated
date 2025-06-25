using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public abstract class BeastBaseState : BaseState<EBehaviorState>
    {
        public float PlayedTime => Time.time - _startTime;
        
        public StateDataSO CurStateData { get; protected set; }
        protected StateDataSO[] _stateDataList;
        protected Animator _animator;

        protected float _startTime;
        protected bool _isFinish;

        protected BeastBaseState(EBehaviorState key, StateDataSO data, Animator animator) : base(key)
        {
            CurStateData = data;
            _animator = animator;
        }

        protected BeastBaseState(EBehaviorState key, StateDataSO[] datas, StateDataSO data, Animator animator) : base(key)
        {
            _stateDataList = datas;
            CurStateData = data;
            _animator = animator;
        }

        protected virtual void PlayAnim()
        {
            if (CurStateData.Clip != null)
            {
                _animator.speed = CurStateData.Clip.length / CurStateData.PeriodTime;
                _animator.Play(CurStateData.Clip.name);
            }
        }

        public override void Enter()
        {
            _isFinish = false;
            _startTime = Time.time;

            PlayAnim();
        }

        public override void Do()
        {
            // throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            // throw new System.NotImplementedException();
        }

        public override void FixedDo()
        {
            // throw new System.NotImplementedException();
        }

        public override EBehaviorState GetNextState()
        {
            return Key;
        }
    }
}