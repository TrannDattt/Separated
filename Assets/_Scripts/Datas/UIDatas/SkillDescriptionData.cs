using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/UI Data/Skill Description")]
    public class SkillDescriptionData : ScriptableObject
    {
        public Sprite SkillIcon;
        public string SkillName;
        public string SkillDescription;
    }
}