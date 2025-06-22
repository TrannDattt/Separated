using Separated.Player;
using UnityEngine;

namespace Separated.Data
{

    [CreateAssetMenu(menuName = "Scriptable Object/Drop Data")]
    public class LootDropData : ScriptableObject
    {
        public int SoulDrop;
        public ItemsDrop[] ItemsDrop;
    }
}