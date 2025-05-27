using System;
using Separated.Data;
using Separated.Helpers;
using Separated.Interfaces;
using UnityEngine;

namespace Separated.Unit
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class UnitHitbox : MonoBehaviour, ICanDoDamage
    {
        public float Damage => _damage;
        public float PoiseDamage => _poiseDamage;
        public Vector2 KnockbackDir => _knockbackDir;
        public float KnockbackForce => _knockbackForce;

        private float _damage;
        private float _poiseDamage;
        private Vector2 _knockbackDir;
        private float _knockbackForce;

        private BoxCollider2D _collider;

        public event Action OnDoingDamage;

        public void SetAttackData(AttackSkillData data)
        {
            _damage = data.Damage;
            _poiseDamage = data.PoiseDamage;
            _knockbackDir = data.KnockbackDir;
            _knockbackForce = data.KnockbackForce;
        }

        public void ResetAttackData()
        {
            _damage = 0f;
            _poiseDamage = 0f;
            _knockbackDir = Vector2.zero;
            _knockbackForce = 0f;
        }

        public void EnableHitbox() => _collider.enabled = true;

        public void DisableHitbox() => _collider.enabled = false;

        void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            DisableHitbox();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            var damageableUnit = collision.GetComponentInParent<IDamageble>();
            if (damageableUnit != null && !CompareTag(collision.tag))
            {
                Debug.Log(collision.gameObject.name + " hit by " + GetGameObject().name);
                (this as ICanDoDamage).Do(damageableUnit);

                if (CompareTag("Player"))
                {
                    OnDoingDamage?.Invoke();
                    TimeEffect.Instance.SlowTime(.2f, 0.5f);
                    // CameraEffect.Instance.ShakeCamera(0.1f, 0.05f);
                }
            }
        }

        public GameObject GetGameObject() => transform.parent.parent.gameObject;
    }
}
