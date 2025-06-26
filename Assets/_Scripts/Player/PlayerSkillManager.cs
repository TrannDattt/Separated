using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Separated.Data;
using Separated.Interfaces;
using Separated.Views;
using UnityEngine;
using static Separated.GameManager.EventManager;
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
                // { ESkillSlot.Ultimate, new() } // Ultimate is always summon all the equipped beasts
            };

            if (skillStates != null)
            {
                for (int i = 0; i < _skillDict.Keys.Count - 1; i++)
                {
                    var key = _skillDict.Keys.ElementAt(i);
                    _skillDict[key] = i >= skillStates.Length ? new() : new(skillStates[i]);
                }

                // TODO: Init ultimate skill
            }

            _playerSkillView.Initialize(_skillDict.Values.ToArray());

            // Debug.Log("Init listener");

            var skillUpdatedEvent = GetEvent<Tuple<ESkillSlot, BeastData>>(EEventType.PlayerSkillChanged);
            skillUpdatedEvent.AddListener(this);

            var skillUsedEvent = GetEvent<ESkillSlot>(EEventType.PlayerSkillUsed);
            skillUsedEvent.AddListener(this);
        }

        public void UpdateSkillDict(ESkillSlot slot, BeastData beastData)
        {
            if (beastData == null)
            {
                _skillDict[slot] = null;
                // TODO: Update ultimate skill
                return;
            }

            var skillData = beastData.DefaultActionSkill; // TODO: Get current skill from beast skill manager => skill tree
            Debug.Log(skillData);
            _skillDict[slot] = new(skillData);

            _playerSkillView.UpdateSkillView(slot, skillData);
        }

        public void OnEventNotify(Tuple<ESkillSlot, BeastData> eventData)
        {
            // Debug.Log("Update skill 2");
            UpdateSkillDict(eventData.Item1, eventData.Item2);
        }

        public void UseSkill(ESkillSlot slot)
        {
            _playerSkillView.CooldownSkill(slot);
        }

        public void OnEventNotify(ESkillSlot eventData)
        {
            // Debug.Log("Use skill 2");
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
