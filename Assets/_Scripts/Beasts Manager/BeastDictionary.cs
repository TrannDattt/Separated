using System.Collections.Generic;
using System.Linq;
using Separated.Data;
using Separated.Enums;
using Separated.GameManager;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Views;
using UnityEngine;

namespace Separated.Skills
{
    public class BeastDictionary : Singleton<BeastDictionary>, IEventListener<EBeastType>
    {
        [SerializeField] private BeastData[] _beastDatas;
        [SerializeField] private BeastDictionaryView _beastDictionaryView;

        private Dictionary<EBeastType, BeastData> _beastDict = new();
        private Dictionary<EBeastType, bool> _availableBeastDict = new();

        private void InitDict()
        {
            _beastDict.Clear();

            foreach (var beastData in _beastDatas)
            {
                _beastDict.Add(beastData.Type, beastData);
            }

            if (_availableBeastDict.Count == _beastDict.Count)
                return;

            foreach (var beastData in _beastDatas)
            {
                if (_availableBeastDict.ContainsKey(beastData.Type))
                    continue;

                _availableBeastDict.Add(beastData.Type, false);
            }

            var enemyDieEvent = EventManager.GetEvent<EBeastType>(EventManager.EEventType.EnemyDied);
            enemyDieEvent.AddListener(this);
        }

        public void OnEventNotify(EBeastType eventData)
        {
            UpdateDict(eventData);
        }

        public bool IsBeastAvailable(EBeastType type)
        {
            return _availableBeastDict.TryGetValue(type, out var isAvailable) && isAvailable;
        }

        public BeastData GetBeastData(EBeastType type)
        {
            if (!IsBeastAvailable(type))
            {
                return null;
            }

            return _beastDict.TryGetValue(type, out var beastData) ? beastData : null;
        }

        public BeastData[] GetAllBeastDatas()
        {
            //TODO: Try: Check contain listener this => Remove listener => Notify => Add Listener
            // var beastDatas = new BeastData[_beastDict.Count];
            // foreach (var key in _beastDict.Keys) {
            //     beastDatas.Append(_beastDict[key]);
            // }
            // return beastDatas;
            return _beastDict.Values.ToArray();
        }

        private void UpdateDict(EBeastType type)
        {
            if (_availableBeastDict.ContainsKey(type))
            {
                _availableBeastDict[type] = true;
                // var beastUpdatedEvent = EventManager.GetEvent<EBeastType>(EventManager.EEventType.InventoryUpdated);
                // beastUpdatedEvent.Notify(type);
                _beastDictionaryView.EnableIcon(type);
            }
            else
            {
                Debug.LogWarning($"Beast type {type} not found in available beast dictionary.");
            }
        }

        void Start()
        {
            InitDict();
        }
    }
}