using System.Collections.Generic;
using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/UI Data/Skill Node")]
    public class SkillNodeData : ScriptableObject
    {
        [HideInInspector]
        public string Id;
        public string Name;
        public Sprite Icon;
        public string Description;

        public SkillNodeData[] ParentDatas;
        public StateDataSO SkillData;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = System.Guid.NewGuid().ToString();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        public override bool Equals(object other)
        {
            return other is SkillNodeData data && Id == data.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}