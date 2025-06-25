using System.Collections;
using Separated.Data;
using Separated.Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.SummonedBeasts
{
    public class BeastSkillManager : MonoBehaviour
    {
        private StateDataSO _passiveSkillData;
        private SkillStateData _activeSkillData;

        [SerializeField] private UnityEvent<EBehaviorState> OnPassivePassiveEquiped;
        [SerializeField] private UnityEvent<EBehaviorState> OnActiveSkillEquiped;

        public void EquipBeastSkill()
        {
            OnPassivePassiveEquiped?.Invoke(_passiveSkillData.StateKey);
            OnActiveSkillEquiped?.Invoke(_activeSkillData.StateKey);
        }
    }
}
