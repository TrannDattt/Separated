using System.Collections.Generic;
using Separated.Enums;
using Separated.GameManager;
using Separated.Interfaces;
using Separated.Poolings;
using Separated.Skills;
using UnityEngine;

namespace Separated.Views
{
    public class BeastDictionaryView : GameMenu, IEventListener<EBeastType>
    {
        [SerializeField] private SkullIcon _iconPrefab;

        private BeastDictionary _beastDictionary;
        private Dictionary<EBeastType, SkullIcon> _iconDict = new();
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
                    _iconDict.Add(beastData.Type, skullIcon);
                });
            }

            var beastUnlockedEvent = EventManager.GetEvent<EBeastType>();
            beastUnlockedEvent.AddListener(this);
        }

        public void EnableIcon(EBeastType type)
        {
            var data = _beastDictionary.GetBeastData(type);
            _iconDict[type].EnableIcon(data);
        }

        public void OnEventNotify(EBeastType eventData)
        {
            EnableIcon(eventData);
        }
    }
}