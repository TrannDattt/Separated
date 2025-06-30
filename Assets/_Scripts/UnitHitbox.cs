using System;
using Separated.Data;
using Separated.Helpers;
using Separated.Interfaces;
using UnityEngine;

namespace Separated.Unit
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class UnitHitbox : MonoBehaviour
    {
        private ICanDoDamage _canDoDamage;

        public float Damage => _canDoDamage.Damage;
        public float PoiseDamage => _canDoDamage.PoiseDamage;
        public Vector2 KnockbackDir => _canDoDamage.KnockbackDir;
        public float KnockbackForce => _canDoDamage.KnockbackForce;

        private BoxCollider2D _collider;

        public event Action OnDoingDamage;

        public void SetHitboxData(ICanDoDamage data)
        {
            _canDoDamage = data;
        }

        // public void ResetAttackData()
        // {
        //     _damage = 0f;
        //     _poiseDamage = 0f;
        //     _knockbackDir = Vector2.zero;
        //     _knockbackForce = 0f;
        // }

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
                // Debug.Log(collision.gameObject.name + " hit by " + GetGameObject().name);
                _canDoDamage.Do(damageableUnit);

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
