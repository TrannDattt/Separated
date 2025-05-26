using UnityEngine;

namespace Separated.Interfaces
{
    public interface ICanDoDamage
    {
        public float Damage { get; }
        public float PoiseDamage { get; }
        public Vector2 KnockbackDir { get; }
        public float KnockbackForce { get; }

        public void DoDamage(IDamageble target) => target.TakeDamage(Damage);
        public void DoPoiseDamage(IDamageble target) => target.TakePoiseDamage(PoiseDamage);
        public void DoKnockback(IDamageble target) => target.Knockback(KnockbackDir, KnockbackForce);

        public void Do(IDamageble target)
        {
            DoDamage(target);
            DoPoiseDamage(target);
            DoKnockback(target);
        }
    }

    public interface IDamageble
    {
        public void TakeDamage(float damage);
        public void TakePoiseDamage(float poiseDamage);
        public void Knockback(Vector2 knockbackDir, float knockbackForce);
    }
}