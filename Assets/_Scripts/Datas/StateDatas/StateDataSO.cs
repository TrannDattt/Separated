using Separated.Enums;
using Separated.Player;
using UnityEngine;

namespace Separated.Data
{
    public abstract class StateDataSO : ScriptableObject{
        public EBehaviorState StateKey;
        public AnimationClip Clip;
        public float PeriodTime = 1; // Time to play a full loop
    } 
}
