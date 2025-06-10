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

            StartCoroutine(ChangeValueCoroutine(.5f, playerSoul));
        }

        public void ShowChangeValue(int amount)
        {
            _changeAmount.text = $"{(amount >= 0 ? '+' : "")} {amount}";

            StartCoroutine(ChangeSequenceCoroutine());

            IEnumerator ChangeSequenceCoroutine()
            {
                StartCoroutine(FadeCoroutine(_changeAmountCanvasGroup, .3f, true));
                yield return StartCoroutine(LerpCoroutine(_changeAmountCanvasGroup, .3f, _offset * Vector2.down));

                yield return new WaitForSeconds(.5f);

                StartCoroutine(FadeCoroutine(_changeAmountCanvasGroup, .7f, false));
                yield return StartCoroutine(LerpCoroutine(_changeAmountCanvasGroup, .7f, _offset * Vector2.up));

                UpdateCount();
            }
        }

        private IEnumerator LerpCoroutine(CanvasGroup target, float lerpTime, Vector2 moveDir)
        {
            float elapsedTime = 0;
            var startPos = target.GetComponent<RectTransform>().anchoredPosition;
            var targetPos = startPos + moveDir;

            while (elapsedTime < lerpTime)
            {
                float changedAmount = GraphCalc.EasyEaseOut(.2f, elapsedTime / lerpTime);
                var curPos = new Vector2(startPos.x + moveDir.x * changedAmount, startPos.y + moveDir.y * changedAmount);
                target.GetComponent<RectTransform>().anchoredPosition = curPos;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.GetComponent<RectTransform>().anchoredPosition = targetPos;
        }

        private IEnumerator FadeCoroutine(CanvasGroup target, float fadeTime, bool isFadeIn)
        {
            float elapsedTime = 0;

            while (elapsedTime < fadeTime)
            {
                target.alpha = isFadeIn ? elapsedTime : 1 - elapsedTime;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.alpha = isFadeIn ? 1 : 0;
        }

        private IEnumerator ChangeValueCoroutine(float changeTime, int targetValue)
        {
            float elapsedTime = 0;
            int startValue = int.Parse(_curValue.text);
            int amount = targetValue - startValue;

            while (elapsedTime < changeTime)
            {
                float changeAmount = amount * (float)(elapsedTime / changeTime);
                int curValue = startValue + (int)changeAmount;
                _curValue.text = curValue.ToString();
                // Debug.Log(changeAmount);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _curValue.text = targetValue.ToString();
        }

        // void Awake()
        // {
        //     Initialize();
        // }
    }
}