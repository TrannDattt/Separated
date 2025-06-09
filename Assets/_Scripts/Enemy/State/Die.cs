using Separated.Data;
using Separated.Enums;
using UnityEngine;

namespace Separated.Enemies
{
    public class Die : EnemyBaseState
    {
        public Die(EBehaviorState key, StateDataSO data, Animator animator) : base(key, data, animator)
        {
        }
    }
}