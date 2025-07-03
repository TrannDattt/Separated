using System.Collections;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;

namespace Separated.Player
{
    public class EdgeClimbState : PlayerBaseState
    {
        private PlayerControl _player;

        private float _climbDistanceX = .7f;
        private float _climbDistanceY = 1.3f;
        private float _moveYTime => _curStateData.PeriodTime * .7f;
        private float _moveXTime => _curStateData.PeriodTime - _moveYTime;

        public EdgeClimbState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player) : base(key, data, animator)
        {
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Climb 1");

            var runtimeCoroutine = RuntimeCoroutine.Instance;

            IEnumerator ClimbCoroutine()
            {
                yield return runtimeCoroutine.StartRuntimeCoroutine(DOTween.UnitLerpCoroutine(
                    _player.RigidBody,
                    _moveYTime,
                    new Vector2(0, _climbDistanceY)
                ));

                yield return runtimeCoroutine.StartRuntimeCoroutine(DOTween.UnitLerpCoroutine(
                    _player.RigidBody,
                    _moveXTime,
                    new Vector2(_climbDistanceX, 0)
                ));
            }

            runtimeCoroutine.StartRuntimeCoroutine(ClimbCoroutine());
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime > _curStateData.PeriodTime)
            {
                _isFinish = true;
            }
        }

        public override EBehaviorState GetNextState()
        {
            if (_isFinish)
            {
                return EBehaviorState.Idle;
            }

            return Key;
        }
    }
}
