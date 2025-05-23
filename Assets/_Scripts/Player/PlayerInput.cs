using Separated.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Separated.Player
{
    [RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput), typeof(PlayerControl))]
    public class PlayerInput : MonoBehaviour
    {
        public enum EInputType
        {
            Jump,
            Dash,
            Attack,
            Skill,
            // Ultimate,
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

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<float>();
            FaceDir = MoveDir != 0 ? (int)Mathf.Sign(MoveDir) : FaceDir;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                JumpInput = true;
            }
        }

        public void OnDash(InputAction.CallbackContext context){
            if(context.started)
            {
                DashInput = true;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackInput = true;
            }
        }

        public void OnUsingSkill1(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Skill1Input = true;
            }
        }

        public void OnUsingSkill2(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Skill2Input = true;
            }
        }

        public void OnUsingSkill3(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Skill3Input = true;
            }
        }

        public void OnUsingSkill4(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Skill4Input = true;
            }
        }

        public void OnUsingUltimate(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                UltimateInput = true;
            }
        }

        public void UseInput(EInputType inputType)
        {
            switch (inputType)
            {
                case EInputType.Jump:
                    JumpInput = false;
                    break;

                case EInputType.Dash:
                    DashInput = false;
                    break;

                case EInputType.Attack:
                    AttackInput = false;
                    break;

                case EInputType.Skill:
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
