using Separated.Data;
using Separated.Enums;
using Separated.Interfaces;
using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyControl : BaseUnit
    {
        [field: SerializeField] public EBeastType EnemyType { get; private set; }

        public Rigidbody2D RigidBody { get; private set; }
        public PlayerControl Player { get; private set; }

        public override void Init()
        {
            base.Init();

            Player = PlayerControl.Instance;
            RigidBody = GetComponent<Rigidbody2D>();
        }

        void Awake()
        {
            Init();
        }

        void Start()
        {
            Player = PlayerControl.Instance;
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            // Debug.Log($"Enemy {gameObject.name} took {damage} damage.");
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