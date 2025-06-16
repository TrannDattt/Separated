using System.Collections;
using System.Xml.Serialization;
using Separated.Helpers;
using Separated.Player;
using Separated.Poolings;
using TMPro;
using UnityEngine;

namespace Separated.UIElements
{
    public class SoulCount : GameUI
    {
        [SerializeField] private CanvasGroup _parentCanvasGroup;
        [SerializeField] private TextMeshProUGUI _curValue;
        [SerializeField] private TextPopup _soulChange;

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public void Initialize()
        {

        }

        public void UpdateCount()
        {
            var playerSoul = PlayerControl.Instance.Inventory.SoulHeld;

            StartCoroutine(DOTweenUI.ChangeNumberValueCoroutine(_curValue, .5f, playerSoul));
        }

        public void ShowChangeValue(int amount)
        {
            var content = $"{(amount >= 0 ? '+' : "")} {amount}";
            var spawnRect = _curValue.GetComponent<RectTransform>();
            var newPopup = UIPooling.GetFromPool(_soulChange, Vector2.zero, spawnRect) as TextPopup;

            newPopup.SetContent(content);
            newPopup.OnPopupFinished += UpdateCount;

            newPopup.Pop(false);
        }
    }
}