using System.Collections.Generic;
using UnityEngine;

namespace Separated.Helpers
{
    public class GroundSensor : MonoBehaviour {
        public enum EDirection {
            Up,
            Down,
            Left,
            Right,
        }

        [SerializeField] private Transform _upSensor;
        [SerializeField] private Transform _downSensor;
        [SerializeField] private Transform _leftSensor;
        [SerializeField] private Transform _rightSensor;

        private Dictionary<EDirection, Transform> _sensorDict = new();

        private Transform GetSensor(EDirection dir) => _sensorDict[dir];
        
        public bool CheckSensor(EDirection dir)
        {
            var direction = dir switch
            {
                EDirection.Up => Vector2.up,
                EDirection.Down => Vector2.down,
                EDirection.Left => Vector2.left,
                EDirection.Right => Vector2.right,
                _ => Vector2.zero
            };

            var sensor = GetSensor(dir);

            Debug.DrawRay(sensor.position, direction, Color.red, 0.1f);
            return Physics2D.Raycast(sensor.position, direction, .1f, LayerMask.GetMask("Ground"));
        }

        void Awake()
        {
            _sensorDict = new()
            {
                { EDirection.Up, _upSensor },
                { EDirection.Down, _downSensor },
                { EDirection.Left, _leftSensor },
                { EDirection.Right, _rightSensor },
            };
        }
    }
}
