using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Views;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Skills
{
    public class SummonedBeastManager : MonoBehaviour
    {
        private Dictionary<EBehaviorState, SkillStateData> _skillSlotDict = new Dictionary<EBehaviorState, SkillStateData>
        {
            { EBehaviorState.Skill1, null },
            { EBehaviorState.Skill2, null },
            { EBehaviorState.Skill3, null },
            { EBehaviorState.Skill4, null },
        };

        [SerializeField] private UnityEvent<EBehaviorState> OnSkillChanged;

        public void ChangeSkill(EBehaviorState slot, SkillStateData newData)
        {

        }
    }
}