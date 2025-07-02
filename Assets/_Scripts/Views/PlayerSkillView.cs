using System;
using System.Collections.Generic;
using Separated.Data;
using Separated.Player;
using Separated.Views;
using UnityEngine;
using static Separated.Player.PlayerSkillManager;

namespace Separated.Views
{
    public class PlayerSkillView : GameUI
    {
        [SerializeField] private SkillSlot[] _skillSlots;

        private Dictionary<ESkillSlot, SkillSlot> _skillSlotDict;

        public override void Hide()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public void Initialize(PlayerSkill[] playerSkills)
        {
            _skillSlotDict = new Dictionary<ESkillSlot, SkillSlot>()
            {
                { ESkillSlot.Skill1, _skillSlots[0] },
                { ESkillSlot.Skill2, _skillSlots[1] },
                { ESkillSlot.Skill3, _skillSlots[2] },
                { ESkillSlot.Skill4, _skillSlots[3] },
            };

            for (int i = 0; i < _skillSlots.Length; i++)
            {
                _skillSlots[i].Initialize(playerSkills[i].SkillData);
            }
        }

        public void UpdateSkillView(ESkillSlot slot, SkillNodeData skillData)
        {
            var skillSlot = _skillSlotDict[slot];
            skillSlot.UpdateSkillView(skillData);
        }

        public void CooldownSkill(ESkillSlot slot)
        {
            if (!_skillSlotDict.ContainsKey(slot))
            {
                return;
            }

            var skillSlot = _skillSlotDict[slot];
            skillSlot.CooldownSkill();
        }
    }
}
