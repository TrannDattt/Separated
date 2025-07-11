using Separated.Enums;
using Separated.GameManager;
using Separated.Interfaces;
using UnityEngine;
using static Separated.GameManager.EventManager;

namespace Separated.Views
{
    public class GameMenu : GameUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private EAnimationType _animationType;
        [SerializeField] private bool _closeOnStart;

        protected GenericEvent<EEventType, EGameState> _menuOpenedEvent => GetGenericEvent<EGameState>(EEventType.UIUpdated);

        protected bool _isOpened = true;

        public override void Show()
        {
            if (_isOpened) return;

            // Implement show logic based on animation type
            // _canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            _isOpened = true;

            MenuControl.Instance.AddMenu(this);
        }

        public override void Hide()
        {
            if (!_isOpened) return;

            // Implement hide logic
            // _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
            _isOpened = false;

            MenuControl.Instance.RemoveMenu(this);
        }

        public virtual void Initialize()
        {
            // Implement initialization logic
            if (_closeOnStart)
            {
                Hide();
                // Debug.Log("Hide ");
            }
        }

        void Start()
        {
            Initialize();
        }
    }
}