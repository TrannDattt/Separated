using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Separated.Views
{
    public class MenuTag : GameButton
    {
        public UnityEvent<MenuTag> OnTagSelected;
        public bool IsSelected { get; private set; }

        public override void Hide()
        {
            IsSelected = false;
            
            var deactiveColor = _buttonImage.color;
            deactiveColor.a = .5f;
            _buttonImage.color = deactiveColor;
        }

        public override void Show()
        {
            IsSelected = true;

            var activeColor = _buttonImage.color;
            activeColor.a = 1;
            _buttonImage.color = activeColor;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!IsSelected)
            {
                base.OnPointerUp(eventData);
                OnTagSelected?.Invoke(this);
            }
        }
    }
}