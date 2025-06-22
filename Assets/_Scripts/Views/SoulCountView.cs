using System.Collections;
using System.Xml.Serialization;
using Separated.Data;
using Separated.GameManager;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Player;
using Separated.Poolings;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Separated.Views
{
    public class SoulCountView : GameUI, IEventListener<int>
    {
        [SerializeField] private CanvasGroup _parentCanvasGroup;
        [SerializeField] private TextMeshProUGUI _curValue;
        [SerializeField] private TextPopup _soulChange;

        private PlayerInventoryManager _playerInventory;

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public void Initialize(PlayerInventoryManager playerInventory)
        {
            // var inventoryChangedEvent = EventManager.GetEvent<int>(EventManager.EEventType.InventoryUpdated);
            // inventoryChangedEvent.AddListener(this);
            _playerInventory = playerInventory;
        }

        public void OnEventNotify(int eventData)
        {
            ShowChangeValue(eventData);
        }

        public void UpdateCount()
        {
            var newValue = _playerInventory.SoulHeld;
            StartCoroutine(DOTweenUI.ChangeNumberValueCoroutine(_curValue, .5f, newValue));
        }

        public void ShowChangeValue(int amount)
        {
            var content = $"{(amount >= 0 ? '+' : "")} {amount}";
            var spawnRect = _curValue.GetComponent<RectTransform>();
            UIPooling.GetFromPool(_soulChange, Vector2.zero, spawnRect, (popup) =>
            {
                var newPopup = popup as TextPopup;
                newPopup.SetContent(content);
                newPopup.OnPopupFinished += UpdateCount;

                newPopup.Pop(false);
            });            
        }
    }
}