using Separated.Enums;
using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Skill/Attack")]
    public class AttackSkillData : SkillStateData
    {
        public float Damage;
        public float PoiseDamage;

        public Vector2 KnockbackDir;
        public float KnockbackForce;

        public float Range;
    }
}
