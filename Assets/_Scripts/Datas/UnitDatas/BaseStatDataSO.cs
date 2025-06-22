using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/Stat Data")]
    public class BaseStatDataSO : ScriptableObject
    {
        public float Hp;
        public float Poise;
    }
}