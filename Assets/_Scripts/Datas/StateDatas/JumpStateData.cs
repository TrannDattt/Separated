using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Jump")]
    public class JumpStateData : StateDataSO{
        public float JumpSpeedX;
        public float JumpSpeedY;
        public int JumpCount; // Number of jumps allowed in the air
    }
}
