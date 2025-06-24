using System;
using System.Collections.Generic;
using Separated.Data;
using Separated.Enums;
using Separated.Poolings;
using Separated.Skills;
using UnityEngine;
using static Separated.GameManager.EventManager;
using static Separated.Player.PlayerSkillManager;

namespace Separated.Views
{
    public class BeastDictionaryView : GameMenu
    {
        [SerializeField] private SkullIcon _iconPrefab;

        private BeastDictionary _beastDictionary;
        private Dictionary<EBeastType, SkullIcon> _iconDict = new();
        private Dictionary<ESkillSlot, SkullIcon> _activeDict = new()
        {
            { ESkillSlot.Skill1, null },
            { ESkillSlot.Skill2, null },
            { ESkillSlot.Skill3, null },
            { ESkillSlot.Skill4, null },
            { ESkillSlot.Ultimate, null }
        };
        private RectTransform _rectTransform;

        public override void Initialize()
        {
            base.Initialize();

            _rectTransform = GetComponent<RectTransform>();
            _beastDictionary = BeastDictionary.Instance;
            if (_beastDictionary == null)
            {
                Debug.LogError("BeastDictionary instance is not initialized.");
                return;
            }

            _iconDict.Clear();
            foreach (var beastData in _beastDictionary.GetAllBeastDatas())
            {
                UIPooling.GetFromPool(_iconPrefab, parent: _rectTransform, onGet: (icon) =>
                {
                    var skullIcon = icon as SkullIcon;
                    var available = _beastDictionary.IsBeastAvailable(beastData.Type);
                    skullIcon.Initialize(available ? beastData : null);
                    skullIcon.OnClicked.AddListener(() =>
                    {
                        UpdateActiveDict(skullIcon);
                    });

                    _iconDict.Add(beastData.Type, skullIcon);
                });
            }

            var activeDictUpdateEvent = GetEvent<Tuple<ESkillSlot, BeastData>>(EEventType.PlayerSkillChanged);
            foreach (var key in _activeDict.Keys)
            {
                var activeSkull = _activeDict[key];
                activeDictUpdateEvent.Notify(new(key, activeSkull ? activeSkull.CurData : null));
            }

            // Debug.Log("Dict init");
        }

        public void EnableIcon(EBeastType type)
        {
            var data = _beastDictionary.GetBeastData(type);
            _iconDict[type].EnableIcon(data);
        }

        public void UpdateActiveDict(SkullIcon icon)
        {
            foreach (var key in _activeDict.Keys)
            {
                var activeDictUpdateEvent = GetEvent<Tuple<ESkillSlot, BeastData>>(EEventType.PlayerSkillChanged);

                if (_activeDict[key] == null)
                {
                    icon.TurnOn();
                    _activeDict[key] = icon;
                    activeDictUpdateEvent.Notify(new(key, icon.CurData));
            Debug.Log("Update skill 1");
                    return;
                }
                else if (_activeDict[key] == icon)
                {
                    icon.TurnOff();
                    _activeDict[key] = null;
                    activeDictUpdateEvent.Notify(new(key, null));
            Debug.Log("Update skill 1");
                    return;
                }
            }

            // TODO: Pop a dialog message to notify the user that all slots are occupied
        }
    }
}