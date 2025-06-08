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

        bool IDamageble.CanTakeDamage { get; set; }

        private UnitNavigator _navigator;

        public override void Init()
        {
            base.Init();

            RigidBody = GetComponent<Rigidbody2D>();
            _navigator = new UnitNavigator();
        }

        private void ChangeFaceDir()
        {
            if (_navigator.GetMoveDirection(Player.transform, transform).x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_navigator.GetMoveDirection(Player.transform, transform).x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        void Awake()
        {
            Init();
        }

        void Start()
        {
            Player = PlayerControl.Instance;
            Debug.Log(Player);
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