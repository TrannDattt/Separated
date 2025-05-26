using Separated.Interfaces;
using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyControl : BaseUnit, IDamageble
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

        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"Enemy {gameObject.name} collided with {collision.gameObject.name}.");
            var obj = collision.transform.parent.gameObject;
            if (collision.TryGetComponent(out ICanDoDamage canDoDamageUnit) && !CompareTag(collision.tag))
            {
                canDoDamageUnit.Do(this);
            }
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"Enemy {gameObject.name} took {damage} damage.");
        }

        public void TakePoiseDamage(float poiseDamage)
        {
            Debug.Log($"Enemy {gameObject.name} took {poiseDamage} poise damage.");
        }

        public void Knockback(Vector2 knockbackDir, float knockbackForce)
        {
            var faceDir = _player.InputProvider.FaceDir;
            RigidBody.AddForce(faceDir * knockbackForce * knockbackDir, ForceMode2D.Impulse);
        }
    }
}