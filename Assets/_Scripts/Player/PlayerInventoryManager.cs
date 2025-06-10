using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Player
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        public int SoulHeld { get; private set; }
        public Dictionary<GameObject, int> ItemsHeld { get; private set; }
        public UnityEvent<int> OnSoulChanged;

        public void ChangeSoulHeld(int amount)
        {
            SoulHeld += amount;
            OnSoulChanged?.Invoke(amount);
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
                        OnSoulChanged?.Invoke(drop.Amount);
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
                    OnSoulChanged?.Invoke(drop.Amount);
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
        }
    }

    [Serializable]
    public class ItemsDrop {
        public GameObject Item;
        public int Amount;
    }
}
