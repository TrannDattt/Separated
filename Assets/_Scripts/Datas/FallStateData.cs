using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Fall")]
    public class FallStateData : StateDataSO{
        public float MaxFallTime;
        public float Acceleration;
        public float MaxFallSpeed;
    }
}
