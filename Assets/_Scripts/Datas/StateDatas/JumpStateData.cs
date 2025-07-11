using UnityEngine;

namespace Separated.Data
{
    [CreateAssetMenu(menuName = "Scriptable Object/State Data/Jump")]
    public class JumpStateData : StateDataSO{
        public float JumpSpeedX;
        public float JumpSpeedY;
    }
}
