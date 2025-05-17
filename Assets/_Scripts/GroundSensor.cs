using UnityEngine;

namespace Separated.Helpers
{
    public class GroundSensor : MonoBehaviour{
        public enum EDirection{
            Up,
            Down,
            Left,
            Right,
        }

        [SerializeField] private GameObject _downSensor;

        public bool CheckSensor(EDirection dir){
            switch(dir){
                case EDirection.Down:
                // Debug.Log(1);
                    return CheckSensor(_downSensor, dir);

                default:
                    return false;
            }
        }

        private bool CheckSensor(GameObject sensor, EDirection dir){
            var direction = dir switch{
                EDirection.Up => Vector2.up,
                EDirection.Down => Vector2.down,
                EDirection.Left => Vector2.left,
                EDirection.Right => Vector2.right,
                _ => Vector2.zero
            };

            Debug.DrawRay(sensor.transform.position, direction, Color.red, 0.1f);
            // Debug.Log(Physics2D.Raycast(sensor.transform.position, direction, .1f, LayerMask.GetMask("Ground")) == true);
            return Physics2D.Raycast(sensor.transform.position, direction, .1f, LayerMask.GetMask("Ground"));
        }
    }
}
