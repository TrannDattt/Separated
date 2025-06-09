using UnityEngine;
using UnityEngine.Events;

namespace Separated.Interfaces
{
    public interface ICanDoDamage
    {
        public float Damage { get; }
        public float PoiseDamage { get; }
        public Vector2 KnockbackDir { get; }
        public float KnockbackForce { get; }

        public GameObject GetGameObject();
        public void DoDamage(IDamageble target) => target.TakeDamage(Damage);
        public void DoPoiseDamage(IDamageble target) => target.TakePoiseDamage(PoiseDamage);
        public void DoKnockback(IDamageble target)
        {
            var self = GetGameObject();
            var faceDir = self.transform.localScale.x > 0 ? 1 : -1;
            var knockbackDir = new Vector2(faceDir * KnockbackDir.x, KnockbackDir.y);
            target.Knockback(knockbackDir, KnockbackForce);
        }

        public void Do(IDamageble target)
        {
            if (target.CanTakeDamage)
            {
                Debug.Log($"{target} takes {Damage} damage");
                Debug.Log($"{target} can take damage: {target.CanTakeDamage}");

                DoDamage(target);
                DoPoiseDamage(target);
                DoKnockback(target);
            }
            else
            {
                Debug.Log($"{target} cant take damage");
            }
        }
    }

    public interface IDamageble
    {
        public bool CanTakeDamage { get; set; }
        public void TakeDamage(float damage);
        public void TakePoiseDamage(float poiseDamage);
        public void Knockback(Vector2 knockbackDir, float knockbackForce);
    }
}