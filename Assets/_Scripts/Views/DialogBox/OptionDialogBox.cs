using UnityEngine;

namespace Separated.Views
{
    public class OptionDialogBox : DialogueBox
    {
        [SerializeField] private GameButton _confirmButton;
        [SerializeField] private GameButton _declineButton;

        public override void Init()
        {
            _confirmButton.OnClicked.AddListener(Hide);
            _declineButton.OnClicked.AddListener(Hide);
        }
    }
}