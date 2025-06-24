using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Separated.Views
{
    [RequireComponent(typeof(Image))]
    public class GameButton : GameUI, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Sprite _normalIcon;
        [SerializeField] protected Sprite _hoverIcon;
        [SerializeField] protected Sprite _pressedIcon;

        [SerializeField] protected Image _buttonImage;
        [SerializeField, AllowNull] protected TextMeshProUGUI _buttonText;

        public UnityEvent OnClicked;

        public void ChangeContent(string text)
        {
            _buttonText.text = text;
        }

        public void ChangeImage(Sprite icon)
        {
            _buttonImage.sprite = icon;
        }

        public override void Show()
        {
            // Implement show logic
        }

        public override void Hide()
        {
            // Implement hide logic
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _buttonImage.sprite = _pressedIcon;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _buttonImage.sprite = _normalIcon;
            // Debug.Log($"Button clicked => Invoke {OnClicked.GetPersistentEventCount()} listeners");
            OnClicked?.Invoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _buttonImage.sprite = _hoverIcon;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _buttonImage.sprite = _normalIcon;
        }

        void OnDestroy()
        {
            OnClicked = null;
            
        }
    }
}