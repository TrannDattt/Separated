using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Separated.Data;
using Separated.Interfaces;
using Separated.Views;
using UnityEngine;
using static Separated.Player.PlayerSkillManager;

namespace Separated.Player
{
    public class PlayerSkillManager : MonoBehaviour, IEventListener<Tuple<ESkillSlot, BeastData>>, IEventListener<ESkillSlot>
    {
        public enum ESkillSlot
        {
            Skill1,
            Skill2,
            Skill3,
            Skill4,
            Ultimate,
        }

        [SerializeField] private PlayerSkillView _playerSkillView;

        private Dictionary<ESkillSlot, PlayerSkill> _skillDict;

        public void Initialize(SkillNodeData[] skillStates = null)
        {
            _skillDict = new Dictionary<ESkillSlot, PlayerSkill>
            {
                { ESkillSlot.Skill1, new() },
                { ESkillSlot.Skill2, new() },
                { ESkillSlot.Skill3, new() },
                { ESkillSlot.Skill4, new() },
                { ESkillSlot.Ultimate, new() } // Ultimate is always summon all the equipped beasts
            };

            if (skillStates == null)
            {
                return;
            }

            for (int i = 0; i < _skillDict.Keys.Count - 1; i++)
            {
                var key = _skillDict.Keys.ElementAt(i);
                _skillDict[key] = i >= skillStates.Length ? new() : new(skillStates[i]);
            }

            // TODO: Init ultimate skill

            _playerSkillView.Initialize(_skillDict.Values.ToArray());
        }

        public void UpdateSkillDict(ESkillSlot slot, BeastData beastData)
        {
            if (beastData == null)
            {
                _skillDict[slot] = null;
                // TODO: Update ultimate skill
                return;
            }

            var skillData = beastData.DefaultActiveSkill; // TODO: Get current skill from beast skill manager => skill tree
            _skillDict[slot] = new(skillData);

            _playerSkillView.UpdateSkillView(slot, skillData);
        }

        public void OnEventNotify(Tuple<ESkillSlot, BeastData> eventData)
        {
            UpdateSkillDict(eventData.Item1, eventData.Item2);
        }

        public void UseSkill(ESkillSlot slot)
        {
            _playerSkillView.CooldownSkill(slot);
        }

        public void OnEventNotify(ESkillSlot eventData)
        {
            UseSkill(eventData);
        }
    }

    public class PlayerSkill
    {
        public SkillNodeData SkillData { get; private set; }

        public PlayerSkill(SkillNodeData skillData = null)
        {
            SkillData = skillData;
        }
    }
}
