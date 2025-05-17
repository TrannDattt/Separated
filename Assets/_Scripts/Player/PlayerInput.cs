using Separated.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Separated.PlayerControl
{
    [RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput), typeof(PlayerBodyPart))]
    public class PlayerInput : MonoBehaviour
    {
        public enum EInputType
        {
            Jump,
            Dash,
            Attack,
        }

        public float MoveDir { get; private set; }
        public int FaceDir { get; private set; } = 1;
        public bool JumpInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool AttackInput { get; private set; }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<float>();
            FaceDir = MoveDir != 0 ? (int)Mathf.Sign(MoveDir) : FaceDir;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                // if(GroundSensor.CheckSensor(GroundSensor.EDirection.Down))
                // {
                    JumpInput = true;
                // }
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

                default:
                    break;
            }
        }
    }
}
