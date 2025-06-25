using Separated.Data;
using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BeastControl : MonoBehaviour
    {
        public Rigidbody2D RigidBody { get; private set; }
        public BeastData Data { get; private set; }

        private PlayerControl _player;

        public void Initialize(BeastData beastData)
        {
            RigidBody = GetComponent<Rigidbody2D>();

            _player = PlayerControl.Instance;
            Data = beastData != null ? beastData : throw new System.ArgumentNullException(nameof(beastData), "BeastData cannot be null");
        }

        void Start()
        {
            _player = PlayerControl.Instance;
            // Initialize();
        }
    }
}
