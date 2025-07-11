using System.Collections;
using Separated.Data;
using Separated.Enums;
using Separated.GameManager;
using Separated.Helpers;
using Separated.Interfaces;
using UnityEngine;
using static Separated.GameManager.EventManager;

namespace Separated.Player
{
    public class AirJump : Jump, IEventListener
    {
        private AirJumpData _airJumpData => _curStateData as AirJumpData;

        private int _remainingJumpCount;
        private Event<EEventType> _touchGroundEvent = new();

        public AirJump(EBehaviorState key, StateDataSO data, Animator animator, PlayerControl player, PlayerInputManager inputProvider, GroundSensor groundSensor) : base(key, data, animator, player, inputProvider, groundSensor)
        {
            _remainingJumpCount = _airJumpData.JumpCount;

            _touchGroundEvent = GetEvent(EEventType.TouchedGround);
            _touchGroundEvent.AddListener(this);
        }

        public override void Enter()
        {
            if (_remainingJumpCount == 0)
            {
                IsFinished = true;
                return;
            }
            _remainingJumpCount--;

            base.Enter();
        }

        public override void Do()
        {
            if (IsFinished)
            {
                _inputProvider.UseInput(PlayerInputManager.EActionInputType.Jump);
                return;
            }

            base.Do();
        }

        private void ResetJump() => _remainingJumpCount = _airJumpData.JumpCount;

        public void OnEventNotify()
        {
            ResetJump();
        }
    }
}
