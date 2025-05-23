using Separated.Unit;
using UnityEngine;

namespace Separated.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : BaseUnit
    {
        public static PlayerControl Instance { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();
        }
    }
}
