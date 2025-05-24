using System.Collections;
using Separated.Data;
using Separated.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Separated.SummonedBeasts
{
    public class SummonedBeastSkillManager : MonoBehaviour
    {

        // private PassiveSkillData _passiveSkillData;
        // private BuffSkillData _activeSkillData;
        // private CombatSkillData _combatSkillData;

        public bool CheckCanUseSkill(SkillStateData data)
        {
            return data.SkillType switch
            {
                // ESkillType.PassiveSkill => true,// Passive skills are always available
                // ESkillType.ActiveSkill => _activeSkillData.IsFinishedCooldown,
                // ESkillType.CombatSkill => _combatSkillData.IsFinishedCooldown,
                _ => false,// Debug.LogError("Invalid skill type");
            };
        }

        private void StartCooldown(ESkillType skillType, float cooldownTime)
        {
            StartCoroutine(Cooldown());

            IEnumerator Cooldown()
            {
                yield return new WaitForSeconds(cooldownTime);
                // if (skillType == ESkillType.ActiveSkill)
                // {
                //     _activeSkillData.IsFinishedCooldown = true;
                // }
                // else if (skillType == ESkillType.CombatSkill)
                // {
                //     _combatSkillData.IsFinishedCooldown = true;
                // }
            }
        }

        public void ApplyPassiveSkill()
        {
            // Apply passive skill effects
        }

        public void DoActiveSkill()
        {
            // _activeSkillData.DoSkill();
            // StartCooldown(ESkillType.ActiveSkill, _activeSkillData.Cooldown);

            // Apply active skill effects
        }

        public void DoCombatSkill()
        {
            // _combatSkillData.DoSkill();
            // StartCooldown(ESkillType.CombatSkill, _combatSkillData.Cooldown);

            // Apply combat skill effects
        }

        public void UpdateSkill(ESkillType skillType, SkillStateData newSkillData)
        {
            switch (skillType)
            {
                // case ESkillType.PassiveSkill:
                //     _passiveSkillData = newSkillData as PassiveSkillData;
                //     break;
                // case ESkillType.ActiveSkill:
                //     _activeSkillData = newSkillData as ActiveSkillData;
                //     break;
                // case ESkillType.CombatSkill:
                //     _combatSkillData = newSkillData as CombatSkillData;
                //     break;
                default:
                    // Debug.LogError("Invalid skill type");
                    break;
            }
        }
    }
}
