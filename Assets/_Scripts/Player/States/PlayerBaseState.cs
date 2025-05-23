using Separated.Data;
using Separated.Helpers;
using UnityEngine;
using static Separated.Player.PlayerStateMachine;

namespace Separated.Player
{
    public class PlayerBaseState : BaseState<EPlayerState>
    {
        public float PlayedTime => Time.time - _startTime;

        protected StateDataSO _stateData;
        protected Animator _animator;

        protected float _startTime;
        protected bool _isFinish;

        public PlayerBaseState(EPlayerState key, StateDataSO data, Animator animator) : base(key)
        {
            _stateData = data;
            _animator = animator;
        }

        // public PlayerBaseState(EPlayerState key, StateDataSO[] datas, Animator animator) : base(key)
        // {
        //     _stateData = data;
        //     _animator = animator;
        // }

        protected virtual void PlayAnim(){

        }

        public override void Do()
        {
            // throw new System.NotImplementedException();
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

        public override void FixedDo()
        {
            // throw new System.NotImplementedException();
        }

        public override EPlayerState GetNextState()
        {
            return Key;
        }
    }
}
