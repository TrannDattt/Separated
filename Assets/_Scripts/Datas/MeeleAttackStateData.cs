using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/MeleeAttack")]
    public class MeleeAttackStateData : StateDataSO
    {
        public float Damage;
        public float PoiseDamage;
        public float Cooldown;
    }
}
