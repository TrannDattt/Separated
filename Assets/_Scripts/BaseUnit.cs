using Separated.Data;
using Separated.Interfaces;
using Separated.Views;
using UnityEngine;

namespace Separated.Unit
{
    public abstract class BaseUnit : MonoBehaviour, IDamageble
    {
        [SerializeField] private BaseStatDataSO _statData;
        [SerializeField] private HpBarView _hpBarView;

        public UnitStat Stat { get; private set; }
        public bool IsTakingDamage { get; set; }
        public bool CanTakeDamage { get; set; }

        public virtual void Init()
        {
            Stat = new UnitStat(_statData);

            (this as IDamageble).CanTakeDamage = true;
            IsTakingDamage = false;

            _hpBarView?.Initialize(Stat);
        }

        public virtual void Knockback(Vector2 knockbackDir, float knockbackForce)
        {

        }

        public virtual void TakeDamage(float damage)
        {
            Stat.ChangeCurHp(-damage);

            (this as IDamageble).CanTakeDamage = false;
            IsTakingDamage = true;
        }

        public virtual void TakePoiseDamage(float poiseDamage)
        {

        }

        void OnEnable()
        {
            if (_hpBarView != null)
            {
                Stat.OnHpChanged.AddListener(_hpBarView.UpdateHpBar);
            }
        }

        void OnDisable()
        {
            if (_hpBarView != null)
            {
                Stat.OnHpChanged.RemoveListener(_hpBarView.UpdateHpBar);
            }
        }
    }
}
