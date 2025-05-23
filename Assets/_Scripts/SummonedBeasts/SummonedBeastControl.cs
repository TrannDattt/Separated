using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SummonedBeastControl : BaseUnit
    {
        public Rigidbody2D RigidBody { get; private set; }

        private PlayerControl _player;

        void OnEnable()
        {
            _player = PlayerControl.Instance;
        }

        void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();
        }
    }
}
