using Separated.Helpers;
using UnityEngine;

namespace Separated.PlayerControl
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlayerControl : BaseUnit
    {
        public Rigidbody2D RigidBody { get; private set; }

        private SpriteRenderer _spriteRenderer;

        private void ChangeFaceDir()
        {
            if (RigidBody.linearVelocityX != 0)
            {
                _spriteRenderer.flipX = RigidBody.linearVelocityX < 0;
            }
        }

        protected virtual void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            ChangeFaceDir();
        }
    }
}
