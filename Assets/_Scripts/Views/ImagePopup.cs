using System;
using System.Collections;
using Separated.Helpers;
using Separated.Poolings;
using UnityEngine;
using UnityEngine.UI;

namespace Separated.Views
{
    public class ImagePopup : GameUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _img;
        [SerializeField] private float _offset;

        public event Action OnPopupFinished;

        public override void Hide()
        {
            
        }

        public void Initialize(float fillAmount = 1)
        {
            _img.fillAmount = fillAmount;
        }

        public override void Show()
        {
            _canvasGroup.alpha = 1;
        }

        public void Pop(bool moveUp)
        {
            var dir = moveUp ? Vector2.up : Vector2.down;
            var distance = _offset * dir;

            StartCoroutine(PopupCoroutine());

            IEnumerator PopupCoroutine()
            {
                Show();
                yield return StartCoroutine(DOTweenUI.LerpCoroutine(_canvasGroup, .5f, distance, .8f));
                
                yield return StartCoroutine(DOTweenUI.FadeCoroutine(_canvasGroup, 1f, false, 5f));

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