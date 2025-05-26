using Separated.Player;
using UnityEngine;

namespace Separated.Data
{
    public abstract class StateDataSO : ScriptableObject{
        public PlayerStateMachine.EPlayerState StateKey;
        public AnimationClip Clip;
        public float PeriodTime = 1; // Time to play a full loop
    } 
}
