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

        [Header("Stat")]
        public Vector2 TriggerRange;
        public float ExistTime;

        [Header("Behavior States")]
        public RuntimeAnimatorController AnimControl;
        public IdleStateData IdleData;
        public RunStateData RunData;
        public SkillStateData[] SkillDatas;
        public DieStateData DieData;

        [Header("Skill tree")]
        public SkillNodeData[] SkillNodes;

        [Header("Overrided Skills")]
        public SkillNodeData DefaultActionSkill;
        public SkillNodeData DefaultPassiveSkill;
    }

    [Serializable]
    public class BehaviorState
    {
        public EBehaviorState StateType;
        public StateDataSO Data;
    }
}