using System;
using Separated.Data;
using Separated.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Unit
{
    public class DropContainer : MonoBehaviour
    {
        [SerializeField] private LootDropData _dropData;

        public UnityEvent<int> OnDropSoul;
        public UnityEvent<ItemsDrop[]> OnDropItem;

        public void DropValue()
        {
            if (_dropData.SoulDrop > 0)
            {
                //TODO: Collect soul logic
                OnDropSoul?.Invoke(_dropData.SoulDrop);
            }

            if (_dropData.ItemsDrop.Length > 0)
            {
                //TODO: Collect items logic
                OnDropItem?.Invoke(_dropData.ItemsDrop);
            }
        }
    }
}
