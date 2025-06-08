using System;
using Separated.Data;
using Separated.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Separated.Unit
{
    public abstract class BaseUnit : MonoBehaviour, IDamageble
    {
        [field: SerializeField] public BaseStatDataSO StatData { get; protected set; }

        public BaseStatDataSO CurStatData { get; protected set; }
        bool IDamageble.CanTakeDamage { get; set; }

        public UnityEvent OnHealthChanged;

        public virtual void Init()
        {
            (this as IDamageble).CanTakeDamage = true;
        }

        public virtual void Knockback(Vector2 knockbackDir, float knockbackForce)
        {
            
        }

        public virtual void TakeDamage(float damage)
        {
            CurStatData.Hp = Mathf.Max(0, CurStatData.Hp - damage);
            OnHealthChanged?.Invoke();
        }

        public virtual void TakePoiseDamage(float poiseDamage)
        {
            
        }
    }
}
