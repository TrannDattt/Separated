using NUnit.Framework;
using Separated.Enums;
using UnityEngine;

namespace Separated.Data
{
    public abstract class SkillStateData : StateDataSO
    {
        public ESkillType SkillType;
        public float Cooldown;
    }
}
