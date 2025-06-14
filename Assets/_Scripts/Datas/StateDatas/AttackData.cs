using Separated.Enums;
using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Attack")]
    public class AttackData : StateDataSO
    {
        public float Damage;
        public float PoiseDamage;

        public Vector2 KnockbackDir;
        public float KnockbackForce;

        public Vector2 Range;
        public float Cooldown;
    }
}
