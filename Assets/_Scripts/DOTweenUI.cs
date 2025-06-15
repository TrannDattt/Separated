using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Separated.Helpers
{
    public static class DOTweenUI
    {
        public static IEnumerator LerpCoroutine(CanvasGroup target, float lerpTime, Vector2 distance, float acce = 1)
        {
            float elapsedTime = 0;
            var startPos = target.GetComponent<RectTransform>().anchoredPosition;
            var targetPos = startPos + distance;

            while (elapsedTime < lerpTime)
            {
                float changedAmount = Mathf.Pow(elapsedTime / lerpTime, acce);
                var curPos = new Vector2(startPos.x + distance.x * changedAmount, startPos.y + distance.y * changedAmount);
                target.GetComponent<RectTransform>().anchoredPosition = curPos;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.GetComponent<RectTransform>().anchoredPosition = targetPos;
        }

        public static IEnumerator FadeCoroutine(CanvasGroup target, float fadeTime, bool isFadeIn, float acce = 1)
        {
            float elapsedTime = 0;

            while (elapsedTime < fadeTime)
            {
                var alpha = isFadeIn ? elapsedTime : 1 - elapsedTime;
                target.alpha = Mathf.Pow(alpha, acce);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.alpha = isFadeIn ? 1 : 0;
        }

        public static IEnumerator ChangeColorCoroutine(Color curColor, float changeTime, Color targetColor, UnityEvent<Color> onColorChanged, float acce = 1)
        {
            float elapsedTime = 0;
            var offset = targetColor - curColor;
            // var offset = new(targetColor.r - curColor.r, targetColor.)

            while (elapsedTime < changeTime)
            {
                var curOffset = Mathf.Pow(elapsedTime / changeTime, acce) * offset;
                var newColor = curColor + curOffset;
                onColorChanged?.Invoke(newColor);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            onColorChanged?.Invoke(targetColor);
        }

        public static IEnumerator ChangeNumberValueCoroutine(TextMeshProUGUI target, float changeTime, int targetValue, float acce = 1)
        {
            float elapsedTime = 0;
            int startValue = int.Parse(target.text);
            int amount = targetValue - startValue;

            while (elapsedTime < changeTime)
            {
                float changeAmount = amount * Mathf.Pow(elapsedTime / changeTime, acce);
                int curValue = startValue + (int)changeAmount;
                target.text = curValue.ToString();
                // Debug.Log(changeAmount);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            target.text = targetValue.ToString();
        }
    }
}
