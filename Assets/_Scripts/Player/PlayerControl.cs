using System;
using Separated.Data;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;

namespace Separated.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : BaseUnit
    {
        public static PlayerControl Instance { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public PlayerInputManager InputProvider { get; private set; }

        public int FaceDir => InputProvider.FaceDir;

        public override void Init()
        {
            base.Init();

            RigidBody = GetComponent<Rigidbody2D>();
            InputProvider = GetComponent<PlayerInputManager>();
        }

        public void ChangeFaceDir() => transform.localScale = new Vector3(FaceDir, 1, 1);

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

            Init();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }

        public override void TakePoiseDamage(float poiseDamage)
        {
            base.TakePoiseDamage(poiseDamage);
            // Debug.Log($"Enemy {gameObject.name} took {poiseDamage} poise damage.");
        }

        public override void Knockback(Vector2 knockbackDir, float knockbackForce)
        {
            base.Knockback(knockbackDir, knockbackForce);

            RigidBody.AddForce(knockbackForce * knockbackDir, ForceMode2D.Impulse);
        }
    }
}
