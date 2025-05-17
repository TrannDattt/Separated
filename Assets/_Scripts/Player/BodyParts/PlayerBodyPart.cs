using Separated.Helpers;
using UnityEngine;

namespace Separated.PlayerControl
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlayerBodyPart : BaseUnit
    {
        public Rigidbody2D RigidBody { get; private set; }

        protected virtual void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();
        }
    }
}
