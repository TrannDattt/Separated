using TMPro;
using UnityEngine;

namespace Separated.Views
{
    public abstract class DialogueBox : GameUI
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _message;

        public override void Show()
        {

        }

        public override void Hide()
        {

        }

        public abstract void Init();

        public void SetMessage(string message)
        {
            _message.text = message;
        }

        void Awake()
        {
            Init();
        }
    }
}