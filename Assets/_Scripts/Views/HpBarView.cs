using System;
using System.Collections;
using Separated.GameManager;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Poolings;
using Separated.Unit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Separated.Views
{
    public class HpBarView : GameUI
    {
        // TODO: Add event listener
        [SerializeField] private Image _fillImg;

        [SerializeField] private ImagePopup _hpLost;
        [SerializeField] private bool _doAnim;

        private UnitStat _stat;
        private UnityEvent<Color> _onColorChanged = new();

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public void Initialize(UnitStat stat)
        {
            _stat = stat;
        }
        
        public void UpdateHpBar(float amount)
        {
            if (_stat == null)
            {
                Debug.LogWarning("Unit or its stat data is not set.");
                return;
            }

            float newHpPercentage = _stat.CurHp / _stat.MaxHp;
            float hpLostPercentage = _fillImg.fillAmount - newHpPercentage;
            _fillImg.fillAmount = newHpPercentage;

            if (_doAnim)
            {
                if (hpLostPercentage > 0)
                {
                    var hpTransform = _fillImg.GetComponent<RectTransform>();
                    var hpLostPos = hpTransform.anchoredPosition + new Vector2(hpTransform.rect.width * newHpPercentage, 0);

                    UIPooling.GetFromPool(_hpLost, hpLostPos, GetComponent<RectTransform>(), (hpLost) =>
                    {
                        var newHpLost = hpLost as ImagePopup;

                        newHpLost.Initialize(hpLostPercentage);
                        newHpLost.Pop(false);
                    });

                    StartCoroutine(LostHpCoroutine(hpLostPercentage));
                }
            }
            // Debug.Log($"Player's health: {_unit.CurStatData.Hp} / {_unit.StatData.Hp}");
        }

        private IEnumerator LostHpCoroutine(float lostPercentage)
        {
            _onColorChanged.AddListener(ChangeFillColor);

            yield return StartCoroutine(DOTween.UIChangeColorCoroutine(_fillImg.color, .5f, new Color(1f, .5f, .5f), _onColorChanged, .5f));

            _onColorChanged.RemoveListener(ChangeFillColor);
            ChangeFillColor(Color.white);
        }

        private void ChangeFillColor(Color color) => _fillImg.color = color;
    }
}