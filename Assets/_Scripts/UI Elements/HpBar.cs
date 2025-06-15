using System;
using System.Collections;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Unit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Separated.UIElements
{
    public class HpBar : GameUI
    {
        [SerializeField] private Image _fillImg;

        [SerializeField] private CanvasGroup _hpLostCanvasGroup;
        [SerializeField] private Image _hpLostImg;
        [SerializeField] private float _offset;
        [SerializeField] private bool _doAnim;

        private BaseUnit _unit;
        private UnityEvent<Color> _onColorChanged = new();

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

            float newHpPercentage = _unit.CurStatData.Hp / _unit.StatData.Hp;
            float hpLostPercentage = _fillImg.fillAmount - newHpPercentage;
            _fillImg.fillAmount = newHpPercentage;

            if (_doAnim)
            {
                if (hpLostPercentage > 0)
                {
                    StartCoroutine(LostHpCoroutine(hpLostPercentage));
                }
            }
            // Debug.Log($"Player's health: {_unit.CurStatData.Hp} / {_unit.StatData.Hp}");
        }

        private IEnumerator LostHpCoroutine(float lostPercentage)
        {
            var hpTransform = _fillImg.GetComponent<RectTransform>();
            var hpLostPos = hpTransform.anchoredPosition + new Vector2(hpTransform.rect.width * _fillImg.fillAmount, 0);

            _hpLostCanvasGroup.GetComponent<RectTransform>().anchoredPosition = hpLostPos;
            _hpLostCanvasGroup.alpha = 1;
            _hpLostImg.fillAmount = lostPercentage;
            _onColorChanged.AddListener(ChangeFillColor);

            StartCoroutine(DOTweenUI.ChangeColorCoroutine(_fillImg.color, .5f, new Color(1f, .5f, .5f), _onColorChanged, .5f));
            yield return StartCoroutine(DOTweenUI.LerpCoroutine(_hpLostCanvasGroup, .5f, _offset * Vector2.down, .8f));

            _onColorChanged.RemoveListener(ChangeFillColor);
            ChangeFillColor(Color.white);
            yield return StartCoroutine(DOTweenUI.FadeCoroutine(_hpLostCanvasGroup, 1f, false, 5f));
        }

        private void ChangeFillColor(Color color) => _fillImg.color = color;
    }
}