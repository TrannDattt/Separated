using Separated.Data;
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
        private BeastData _beastData;

        public void Initialize(BeastData beastData)
        {
            _beastData = beastData?? throw new System.ArgumentNullException(nameof(beastData), "BeastData cannot be null");
        }

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
