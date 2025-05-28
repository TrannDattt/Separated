using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Separated.UIElements
{
    [RequireComponent(typeof(Image))]
    public class GameButton : GameUI, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite _normalIcon;
        [SerializeField] private Sprite _hoverIcon;
        [SerializeField] private Sprite _pressedIcon;

        private Image _buttonImage;
        private TextMeshProUGUI _buttonText;

        public UnityEvent OnClick;

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

        public override void Initialize()
        {
            _buttonImage = GetComponent<Image>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Awake()
        {
            Initialize();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _buttonImage.sprite = _pressedIcon;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _buttonImage.sprite = _normalIcon;
            OnClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonImage.sprite = _hoverIcon;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonImage.sprite = _normalIcon;
        }
    }
}