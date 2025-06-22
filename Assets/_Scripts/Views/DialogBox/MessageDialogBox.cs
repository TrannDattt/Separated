using UnityEngine;

namespace Separated.Views
{
    public class MessageDialogBox : DialogueBox
    {
        [SerializeField] private GameButton _confirmButton;

        public override void Init()
        {
            _confirmButton.OnClicked.AddListener(Hide);
        }
    }
}