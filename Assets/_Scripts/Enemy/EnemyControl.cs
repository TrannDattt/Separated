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
        public PlayerControl Player { get; private set; }

        private UnitNavigator _navigator;

        private void ChangeFaceDir()
        {
            if (_navigator.GetMoveDirection(Player.transform, transform).x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_navigator.GetMoveDirection(Player.transform, transform).x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        void OnEnable()
        {
            Player = PlayerControl.Instance;
        }

        void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            _navigator = new UnitNavigator();
        }

        void Update()
        {
            ChangeFaceDir();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
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
            var faceDir = Player.InputProvider.FaceDir;
            RigidBody.AddForce(faceDir * knockbackForce * knockbackDir, ForceMode2D.Impulse);
        }
    }
}