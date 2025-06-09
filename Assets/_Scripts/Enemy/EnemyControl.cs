using Separated.Data;
using Separated.Interfaces;
using Separated.Player;
using Separated.Unit;
using UnityEngine;

namespace Separated.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyControl : BaseUnit
    {
        public Rigidbody2D RigidBody { get; private set; }
        public PlayerControl Player { get; private set; }

        private UnitNavigator _navigator;

        public override void Init()
        {
            base.Init();

            CurStatData = ScriptableObject.CreateInstance<EnemyStatDataSO>();
            CurStatData.CopyData(StatData);

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

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            // Debug.Log($"Enemy {gameObject.name} took {damage} damage.");

            (this as IDamageble).CanTakeDamage = false;
            IsTakingDamage = true;
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