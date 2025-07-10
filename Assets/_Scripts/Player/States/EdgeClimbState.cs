using System.Collections;
using Separated.Data;
using Separated.Enums;
using Separated.Helpers;
using UnityEngine;
using static Separated.Helpers.GroundSensor;

namespace Separated.Player
{
    public class EdgeClimbState : PlayerBaseState
    {
        private ClimbStateData _climbData => _curStateData as ClimbStateData;
        private PlayerControl _player;
        private GroundSensor _sensor;

        private bool _isMovingY;
        private float _moveYTime;
        private float _finsishClimbTime;

        public EdgeClimbState(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, GroundSensor sensor) : base(key, data, animator)
        {
            _player = player;
            _sensor = sensor;
        }

        protected override void PlayAnim()
        {
        }

        private void PlayClimbAnim()
        {
            var clipName = _climbData.Clip.name;

            var normalizedTime = PlayedTime / (_moveYTime * 2);
            _animator.Play(clipName, 0, normalizedTime);
        }

        private void PlayEndClimbAnim()
        {
            var clipName = _climbData.Clip.name;

            var normalizedTime = (PlayedTime - _finsishClimbTime) / (_climbData.PeriodTime * .5f);
            _animator.Play(clipName, 0, Mathf.Min(normalizedTime + .5f, .9f));
        }

        public override void Enter()
        {
            _isMovingY = true;
            var climbDistanceY = _sensor.CheckSensor(EDirection.MidRight) ?
                                    _sensor.GetSensorsDistance(EDirection.TopRight, EDirection.BotRight).y :
                                    _sensor.GetSensorsDistance(EDirection.MidRight, EDirection.BotRight).y;

            _moveYTime = climbDistanceY / _climbData.SpeedY;

            var runtimeCoroutine = RuntimeCoroutine.Instance;

            IEnumerator ClimbCoroutine()
            {
                yield return runtimeCoroutine.StartRuntimeCoroutine(DOTween.UnitLerpCoroutine(
                    _player.RigidBody,
                    _moveYTime,
                    new Vector2(0, climbDistanceY)
                ));
            }

            runtimeCoroutine.StartRuntimeCoroutine(ClimbCoroutine());

            base.Enter();
        }

        public override void Do()
        {
            base.Do();

            if (_isMovingY && (_sensor.CheckSensor(EDirection.MidRight) || _sensor.CheckSensor(EDirection.BotRight)))
            {
                PlayClimbAnim();
            }

            if (!_sensor.CheckSensor(EDirection.BotRight))
            {
                if (_isMovingY)
                {
                    _isMovingY = false;
                    _finsishClimbTime = PlayedTime;
                    RuntimeCoroutine.Instance.StartRuntimeCoroutine(WaitAnimEnd());

                    IEnumerator WaitAnimEnd()
                    {
                        yield return new WaitForSeconds(_climbData.PeriodTime / 2);
                        _isFinish = true;
                    }
                }

                _player.RigidBody.linearVelocity = new(_climbData.SpeedX, 0);
                PlayEndClimbAnim();
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
