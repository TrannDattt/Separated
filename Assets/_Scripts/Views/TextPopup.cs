using System;
using System.Collections;
using Separated.Helpers;
using Separated.Poolings;
using TMPro;
using UnityEngine;

namespace Separated.Views
{
    public class TextPopup : GameUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _textPopup;
        [SerializeField] private float _offset;

        public event Action OnPopupFinished;

        public override void Hide()
        {
            StartCoroutine(DOTweenUI.FadeCoroutine(_canvasGroup, .7f, false));
        }

        public void Initialize()
        {
        }

        public override void Show()
        {
            StartCoroutine(DOTweenUI.FadeCoroutine(_canvasGroup, .3f, true));
        }

        public void SetContent(string content)
        {
            _textPopup.text = content;
        }

        public void Pop(bool moveUp)
        {
            var dir = moveUp ? Vector2.up : Vector2.down;
            var distance = _offset * dir;

            StartCoroutine(PopupCoroutine());

            IEnumerator PopupCoroutine()
            {
                Show();
                yield return StartCoroutine(DOTweenUI.LerpCoroutine(_canvasGroup, .3f, distance, .2f));

                yield return new WaitForSeconds(.5f);

                Hide();
                yield return StartCoroutine(DOTweenUI.LerpCoroutine(_canvasGroup, .7f, -distance, 5f));

                OnPopupFinished?.Invoke();
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            OnPopupFinished = null;
            UIPooling.ReturnToPool(this);
        }
    }
}