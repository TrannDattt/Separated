using Separated.Data;
using Separated.GameManager;
using Separated.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Unit
{
    public class UnitStat
    {
        private BaseStatDataSO _curData;

        public float MaxHp => _curData.Hp;
        public float CurHp { get; private set; }

        public float MaxPoise => _curData.Poise;
        public float CurPoise { get; private set; }

        public Vector2 TriggerRange => _curData.TriggerRange;

        public bool IsDead { get; set; }

        public UnityEvent<float> OnHpChanged;

        public UnitStat(BaseStatDataSO statData)
        {
            _curData = statData;
            OnHpChanged = new();

            CurHp = MaxHp;
            CurPoise = MaxPoise;
        }

        public void ChangeMaxHp(float amount)
        {
            _curData.Hp = Mathf.Max(0, _curData.Hp + amount);
            CurHp = Mathf.Clamp(CurHp, 0, MaxHp);
        }

        public void ChangeCurHp(float amount)
        {
            CurHp = Mathf.Clamp(CurHp + amount, 0, MaxHp);
            // var hpChangedEvent = EventManager.GetEvent<float>(EventManager.EEventType.HealthChanged);
            // hpChangedEvent.Notify(amount);
            OnHpChanged?.Invoke(amount);
        }
    }
}
