using Separated.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Separated.Views
{
    public class SkullIcon : GameUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Sprite _defaultIcon;

        public bool IsActive { get; private set; } = false;

        private BeastData _curData;

        public override void Hide()
        {
        }

        public override void Show()
        {
        }

        public void Initialize(BeastData beastData)
        {
            _curData = beastData;

            _icon.sprite = beastData ? beastData.InactiveSkullIcon : _defaultIcon;
            _name.text = beastData ? beastData.Name : "???";
        }

        public void EnableIcon(BeastData beastData)
        {
            if (!beastData)
            {
                Debug.LogWarning("Beast data is null, cannot enable icon.");
                return;
            }

            _curData = beastData;
            _icon.sprite = beastData.InactiveSkullIcon;
            _name.text = beastData.Name;
            _canvasGroup.alpha = 1f;
        }

        public void Activate()
        {
            if (!_curData)
            {
                return;
            }

            IsActive = true;
            _icon.sprite = _curData.ActiveSkullIcon;
        }

        public void Deactivate()
        {
            if (!_curData)
            {
                return;
            }

            IsActive = false;
            _icon.sprite = _curData.InactiveSkullIcon;
        }
    }
}