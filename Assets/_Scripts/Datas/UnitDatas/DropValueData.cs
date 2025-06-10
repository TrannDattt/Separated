using Separated.Player;
using UnityEngine;

namespace Separated.Data
{

    [CreateAssetMenu(menuName = "Scriptable Object/Stat Data/Loot Drop")]
    public class LootDropData : ScriptableObject
    {
        public int SoulDrop;
        public ItemsDrop[] ItemsDrop;
    }
}