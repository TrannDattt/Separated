using System;
using System.Collections.Generic;
using Separated.Data;
using Separated.GameManager;
using Separated.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Player
{
    public class PlayerInventoryManager : MonoBehaviour, IEventListener<LootDropData>
    {
        public int SoulHeld { get; private set; }
        public Dictionary<GameObject, int> ItemsHeld { get; private set; }

        public void Initialize()
        {
            SoulHeld = 0;
            ItemsHeld = new Dictionary<GameObject, int>();

            var lootDropEvent = EventManager.GetEvent<LootDropData>();
            lootDropEvent.AddListener(this);
        }

        public void OnEventNotify(LootDropData eventData)
        {
            ChangeSoulHeld(eventData.SoulDrop);
            ChangeItemHeld(eventData.ItemsDrop);
        }

        public void ChangeSoulHeld(int amount)
        {
            SoulHeld += amount;
            EventManager.GetEvent<int>().Notify(amount);
        }

        public void ChangeItemHeld(ItemsDrop[] drops)
        {
            foreach (var drop in drops)
            {
                if (!ItemsHeld.ContainsKey(drop.Item))
                {
                    if (drop.Amount > 0)
                    {
                        ItemsHeld.Add(drop.Item, drop.Amount);
                    }
                    else
                    {
                        Debug.Log($"Not enough {drop.Item}");
                    }
                    continue;
                }

                int currentAmount = ItemsHeld[drop.Item];
                int newAmount = currentAmount + drop.Amount;

                if (newAmount == 0)
                {
                    ItemsHeld.Remove(drop.Item);
                    continue;
                }

                if (drop.Amount > 0)
                {
                    ItemsHeld[drop.Item] = newAmount;
                }
                else
                {
                    if (newAmount >= 0)
                    {
                        ItemsHeld[drop.Item] = newAmount;
                    }
                    else
                    {
                        Debug.Log($"Not enough {drop.Item}");
                    }
                }
            }

            EventManager.GetEvent<ItemsDrop[]>().Notify(drops);
        }

        private void Start()
        {
            Initialize();
        }
    }

    [Serializable]
    public class ItemsDrop {
        public GameObject Item;
        public int Amount;
    }
}
