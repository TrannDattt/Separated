using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Stat Data")] //TODO: Change to Serialized class
    public class BaseStatDataSO : ScriptableObject
    {
        public float Hp;
        public float Poise;
        public Vector2 TriggerRange;
    }
}