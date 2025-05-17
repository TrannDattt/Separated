using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Dash")]
    public class DashStateData : StateDataSO{
        public float Speed;
        public int DashCount; // Number of dashs allowed in the air
    }
}
