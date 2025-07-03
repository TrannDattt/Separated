using System.Collections.Generic;
using UnityEngine;

namespace Separated.Helpers
{
    public class GroundSensor : MonoBehaviour {
        public enum EDirection
        {
            Up,

            Down,

            TopLeft,
            MidLeft,
            BotLeft,

            TopRight,
            MidRight,
            BotRight,
        }

        [SerializeField] private Transform _upSensor;
        [SerializeField] private Transform _downSensor;

        [SerializeField] private Transform _topLeftSensor;
        [SerializeField] private Transform _midLeftSensor;
        [SerializeField] private Transform _botLeftSensor;

        [SerializeField] private Transform _topRightSensor;
        [SerializeField] private Transform _midRightSensor;
        [SerializeField] private Transform _botRightSensor;

        private Dictionary<EDirection, Transform> _sensorDict = new();

        private Transform GetSensor(EDirection dir) => _sensorDict[dir];
        
        public bool CheckSensor(EDirection dir)
        {
            var direction = dir switch
            {
                EDirection.Up => Vector2.up,
                EDirection.Down => Vector2.down,
                EDirection.TopLeft or EDirection.MidLeft or EDirection.BotLeft => Vector2.left,
                EDirection.TopRight or EDirection.MidRight or EDirection.BotRight => Vector2.right,
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
                { EDirection.TopLeft, _topLeftSensor },
                { EDirection.MidLeft, _midLeftSensor },
                { EDirection.BotLeft, _botLeftSensor },
                { EDirection.TopRight, _topRightSensor },
                { EDirection.MidRight, _midRightSensor },
                { EDirection.BotRight, _botRightSensor },
            };
        }
    }
}
