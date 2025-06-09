using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Separated.UIElements
{
    public class HpBar : GameUI
    {
        [SerializeField] private Image _fill;

        private BaseUnit _unit;

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public override void Initialize()
        {
        }

        public void SetUnit(BaseUnit unit)
        {
            _unit = unit;
            UpdateHpBar();
        }

        public void UpdateHpBar()
        {
            if (_unit == null || _unit.CurStatData == null)
            {
                Debug.LogWarning("Unit or its stat data is not set.");
                return;
            }

            _fill.fillAmount = _unit.CurStatData.Hp / _unit.StatData.Hp;
            Debug.Log($"Player's health: {_unit.CurStatData.Hp} / {_unit.StatData.Hp}");
        }
    }
}