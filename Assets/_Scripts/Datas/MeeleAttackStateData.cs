using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/MeleeAttack")]
    public class MeleeAttackStateData : StateDataSO
    {
        public float Damage;
        public float PoiseDamage;
        public Vector2 KnockbackDir;
        public float KnockbackForce;
    }
}
