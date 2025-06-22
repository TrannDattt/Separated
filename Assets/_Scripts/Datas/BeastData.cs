using System;
using Separated.Enums;
using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Data/BeastData")]
    public class BeastData : ScriptableObject
    {
        [Header("Basic Info")]
        public EBeastType Type;
        public string Name;

        [Header("Image")]
        public Sprite Portrait;
        public Sprite InactiveSkullIcon;
        public Sprite ActiveSkullIcon;

        [Header("Behavior States")]
        public BehaviorState[] BehaviorStates;

        [Header("Skill tree")]
        public SkillNodeData[] SkillNodes;

        [Header("Overrided Skills")]
        public SkillNodeData DefaultActiveSkill;
        public SkillNodeData DefaultPassiveSkill;
    }

    [Serializable]
    public class BehaviorState
    {
        public EBehaviorState StateType;
        public StateDataSO Data;
    }
}