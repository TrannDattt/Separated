using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/UI Data/Skill Node")]
    public class SkillNodeData : ScriptableObject
    {
        public Sprite SkillIcon;
        public string SkillName;
        public string SkillDescription;

        public SkillNodeData[] ParentNodes;
        public StateDataSO NodeSkillData;

        public bool IsActive;
    }
}