using Separated.Enums;
using Separated.GameManager;
using Separated.Interfaces;
using UnityEngine;

namespace Separated.Views
{
    public class GameMenu : GameUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private EAnimationType _animationType;
        [SerializeField] private bool _closeOnStart;

        protected Event<EGameState> _uiInteractionEvent => EventManager.GetEvent<EGameState>();

        protected bool _isOpened = true;

        public override void Show()
        {
            if (_isOpened) return;

            // Implement show logic based on animation type
            // _canvasGroup.alpha = 1;
            _uiInteractionEvent.Notify(EGameState.Pause);
            gameObject.SetActive(true);
            _isOpened = true;
        }

        public override void Hide()
        {
            if (!_isOpened) return;

            // Implement hide logic
            // _canvasGroup.alpha = 0;
            _uiInteractionEvent.Notify(EGameState.InGame);
            gameObject.SetActive(false);
            _isOpened = false;
        }

        public virtual void Initialize()
        {
            // Implement initialization logic
            if (_closeOnStart)
            {
                Hide();
                Debug.Log("Hide ");
            }
        }

        void Start()
        {
            Initialize();
        }
    }
}