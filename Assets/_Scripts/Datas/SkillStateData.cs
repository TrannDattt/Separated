using NUnit.Framework;
using Separated.Enums;
using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Skill")]
    public abstract class SkillStateData : StateDataSO
    {
        public float Damage;
        public float PoiseDamage;
        public Vector2 KnockbackDir;
        public float KnockbackForce;

        public ESkillType SkillType;
        // public float Cooldown;

        public string SkillName;
        public string SkillDescription;
    }

    public class PassiveSkillData : SkillStateData
    {
        // TODO: Add state data to override
    }

    public class ActiveSkillData : SkillStateData
    {
        public float Cooldown;
        public bool IsFinishedCooldown;

        public void DoSkill() => IsFinishedCooldown = false;
    }

    public class CombatSkillData : SkillStateData
    {
        public float Cooldown;
        public bool IsFinishedCooldown;
        
        public void DoSkill() => IsFinishedCooldown = false;
    }

    public class UltimateSkillData : SkillStateData
    {

    }
}
