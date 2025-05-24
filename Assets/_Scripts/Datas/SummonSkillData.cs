using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Skill/Summon")]
    public class SummonSkillData : SkillStateData
    {
        public GameObject[] SummonedObjectPrefabs;
    }
}
