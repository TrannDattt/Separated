using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Jump")]
    public class JumpStateData : StateDataSO{
        // public float JumpSpeed;
        public float JumpDistance;
        public float Acceleration;
        public int JumpCount; // Number of jumps allowed in the air
    }
}
