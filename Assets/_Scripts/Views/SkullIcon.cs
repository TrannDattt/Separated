using Separated.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Separated.Views
{
    public class SkullIcon : GameSwitchButton
    {
        [SerializeField] private Sprite _disabledIcon;
        [SerializeField] private CanvasGroup _canvasGroup;

        public BeastData CurData { get; private set; }

        public override void Hide()
        {
        }

        public override void Show()
        {
        }

        public void Initialize(BeastData beastData)
        {
            CurData = beastData;

            _switchOnIcon = beastData ? beastData.ActiveSkullIcon : _disabledIcon;
            _switchOffIcon = beastData ? beastData.InactiveSkullIcon : _disabledIcon;

            _buttonImage.sprite = !beastData ? _disabledIcon : IsOn ? _switchOnIcon : _switchOffIcon;
            _buttonText.text = beastData ? beastData.Name : "???";
        }

        public void EnableIcon(BeastData beastData)
        {
            if (!beastData)
            {
                Debug.LogWarning("Beast data is null, cannot enable icon.");
                return;
            }

            CurData = beastData;
            _canvasGroup.alpha = 1f;

            Initialize(beastData);
        }

        public override void TurnOn()
        {
            if (!CurData)
            {
                return;
            }

            base.TurnOn();
        }

        public override void TurnOff()
        {
            if (!CurData)
            {
                return;
            }

            base.TurnOff();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnClicked?.Invoke();

            base.OnPointerDown(eventData);
        }
    }
}