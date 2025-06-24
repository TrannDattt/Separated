using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Separated.Views
{
    public class GameSwitchButton : GameUI, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Sprite _switchOffIcon;
        [SerializeField] protected Sprite _switchOnIcon;
        [SerializeField] protected Sprite _hoverIcon;

        [SerializeField] protected Image _buttonImage;
        [SerializeField, AllowNull] protected TextMeshProUGUI _buttonText;

        public bool IsOn { get; private set; } = false;

        public UnityEvent OnClicked;
        public UnityEvent<bool> OnStateChanged;

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

        public virtual void TurnOn()
        {
            IsOn = true;
            _buttonImage.sprite = _switchOnIcon;
            OnStateChanged?.Invoke(IsOn);
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            _buttonImage.sprite = _switchOffIcon;
            OnStateChanged?.Invoke(IsOn);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (IsOn)
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _buttonImage.sprite = _hoverIcon ?? _buttonImage.sprite;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _buttonImage.sprite = IsOn ? _switchOnIcon : _switchOffIcon;
        }

        void OnDestroy()
        {
            OnClicked = null;
            OnStateChanged = null;
        }
    }
}