using Separated.GameManager;
using Separated.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;
using static Separated.GameManager.EventManager;

namespace Separated.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        public enum EActionInputType
        {
            Jump,
            Dash,
            Attack,
            Skill,
            // Ultimate,
        }

        public enum EInputUIType
        {

        }

        public float MoveDir { get; private set; }
        public int FaceDir { get; private set; } = 1;
        public bool JumpInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool AttackInput { get; private set; }
        public bool Skill1Input { get; private set; }
        public bool Skill2Input { get; private set; }
        public bool Skill3Input { get; private set; }
        public bool Skill4Input { get; private set; }
        public bool UltimateInput { get; private set; }

        private bool _canDoAction => !MenuControl.Instance.HasOpenedMenu;

        public void OnMove(InputAction.CallbackContext context)
        {
            if (_canDoAction)
            {
                MoveDir = context.ReadValue<float>();
                FaceDir = MoveDir != 0 ? (int)Mathf.Sign(MoveDir) : FaceDir;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                JumpInput = true;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                DashInput = true;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                AttackInput = true;
            }
        }

        public void OnUsingSkill1(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                Skill1Input = true;
            }
        }

        public void OnUsingSkill2(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                Skill2Input = true;
            }
        }

        public void OnUsingSkill3(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                Skill3Input = true;
            }
        }

        public void OnUsingSkill4(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                Skill4Input = true;
            }
        }

        public void OnUsingUltimate(InputAction.CallbackContext context)
        {
            if (_canDoAction && context.started)
            {
                UltimateInput = true;
            }
        }

        public void UseInput(EActionInputType inputType)
        {
            switch (inputType)
            {
                case EActionInputType.Jump:
                    JumpInput = false;
                    break;

                case EActionInputType.Dash:
                    DashInput = false;
                    break;

                case EActionInputType.Attack:
                    AttackInput = false;
                    break;

                case EActionInputType.Skill:
                    Skill1Input = false;
                    Skill2Input = false;
                    Skill3Input = false;
                    Skill4Input = false;
                    UltimateInput = false;
                    break;

                default:
                    break;
            }
        }
    }
}
