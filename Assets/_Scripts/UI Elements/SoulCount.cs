using System.Collections;
using System.Xml.Serialization;
using Separated.Helpers;
using Separated.Player;
using TMPro;
using UnityEngine;

namespace Separated.UIElements
{
    public class SoulCount : GameUI
    {
        [SerializeField] private CanvasGroup _parentCanvasGroup;
        [SerializeField] private TextMeshProUGUI _curValue;

        [SerializeField] private CanvasGroup _changeAmountCanvasGroup;
        [SerializeField] private TextMeshProUGUI _changeAmount;
        [SerializeField] private float _offset;

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public override void Initialize()
        {

        }

        public void UpdateCount()
        {
            var playerSoul = PlayerControl.Instance.Inventory.SoulHeld;

            StartCoroutine(DOTweenUI.ChangeNumberValueCoroutine(_curValue, .5f, playerSoul));
        }

        public void ShowChangeValue(int amount)
        {
            _changeAmount.text = $"{(amount >= 0 ? '+' : "")} {amount}";

            StartCoroutine(ChangeSequenceCoroutine());

            IEnumerator ChangeSequenceCoroutine()
            {
                StartCoroutine(DOTweenUI.FadeCoroutine(_changeAmountCanvasGroup, .3f, true));
                yield return StartCoroutine(DOTweenUI.LerpCoroutine(_changeAmountCanvasGroup, .3f, _offset * Vector2.down, .2f));

                yield return new WaitForSeconds(.5f);

                StartCoroutine(DOTweenUI.FadeCoroutine(_changeAmountCanvasGroup, .7f, false));
                yield return StartCoroutine(DOTweenUI.LerpCoroutine(_changeAmountCanvasGroup, .7f, _offset * Vector2.up, 5f));

                UpdateCount();
            }
        }

        // void Awake()
        // {
        //     Initialize();
        // }
    }
}