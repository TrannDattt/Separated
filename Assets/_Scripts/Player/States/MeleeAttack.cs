using Separated.Data;
using UnityEngine;
using static Separated.PlayerControl.PlayerStateMachine;

namespace Separated.PlayerControl
{
    public class MeleeAttack : AttackState
    {
        private MeleeAttack _nextAttack;
        private bool _canDoNextAttack;

        public MeleeAttack(EPlayerState key, StateDataSO data, Animator animator, PlayerInput inputProvider, MeleeAttack nextAttack) : base(key, data, animator, inputProvider)
        {
            _nextAttack = nextAttack;
        }

        public override void Enter()
        {
            base.Enter();

            _canDoNextAttack = false;
        }

        public override void Do()
        {
            base.Do();

            if (PlayedTime >= .8f * _stateData.PeriodTime)
            {
                _canDoNextAttack = true;
            }

            if (PlayedTime >= _stateData.PeriodTime)
            {
                _isFinish = true;
                _canDoNextAttack = false;
            }
        }

        public override EPlayerState GetNextState()
        {
            if (base.GetNextState() == EPlayerState.None)
            {
                if (_canDoNextAttack && _inputProvider.AttackInput && _nextAttack != null)
                {
                    return _nextAttack.Key;
                }
                
                return Key;
            }

            return base.GetNextState();
        }
    }
}
