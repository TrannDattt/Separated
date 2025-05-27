using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;

namespace Separated.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : BaseUnit, IDamageble
    {
        public static PlayerControl Instance { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }

        public PlayerInput InputProvider { get;  private set; }

        private void ChangeFaceDir()
        {
            if (InputProvider.FaceDir > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (InputProvider.FaceDir < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

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

            RigidBody = GetComponent<Rigidbody2D>();
            InputProvider = GetComponent<PlayerInput>();
        }

        protected virtual void Start()
        {
        }

        void Update()
        {
            ChangeFaceDir();
        }

        // void OnTriggerEnter2D(Collider2D collision)
        // {
        //     var canDoDamageUnit = collision.GetComponent<ICanDoDamage>();
        //     var obj = canDoDamageUnit?.GetGameObject();
        //     if (canDoDamageUnit != null && !CompareTag(obj.tag))
        //     {
        //         Debug.Log($"{obj}, {collision.gameObject}");
        //         canDoDamageUnit.Do(this);
        //     }
        // }

        public void TakeDamage(float damage)
        {
            // Debug.Log($"Enemy {gameObject.name} took {damage} damage.");
        }

        public void TakePoiseDamage(float poiseDamage)
        {
            // Debug.Log($"Enemy {gameObject.name} took {poiseDamage} poise damage.");
        }

        public void Knockback(Vector2 knockbackDir, float knockbackForce)
        {
            RigidBody.AddForce(knockbackForce * knockbackDir, ForceMode2D.Impulse);
        }
    }
}
