namespace Separated.Enums
{
    public enum ESkillPhaseType
    {
        PassiveSkill, // Skill that is override one of player states
        AttackSkill, // Skill that deals damage
        BuffSkill, // Skill that changes stats of the unit
        SummonSkill, // Skill that summons objects like units or projectiles
        MovingSkill, // Skill that change unit's position
    }
}