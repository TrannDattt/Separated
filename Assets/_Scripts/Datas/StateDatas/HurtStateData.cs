using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Hurt")]
    public class HurtStateData : StateDataSO
    {
        public float RecoveryCooldownTime;
    }
}
