using Separated.GameSystems;
using TMPro;

namespace Separated.UIElements
{
    public class DialogueDisplay : GameUI
    {
        private TextMeshProUGUI _characterNameText;
        private TextMeshProUGUI _dialogueText;

        public void ChangeContent(Dialogue dialogue)
        {
            if (dialogue == null) return;

            _characterNameText.text = dialogue.Speaker;
            _dialogueText.text = dialogue.IsAnnonymous ? "???" : dialogue.Content;
        }

        public override void Show()
        {
            // Implement show logic for dialogue display
        }

        public override void Hide()
        {
            // Implement hide logic for dialogue display
        }

        public override void Initialize()
        {
            // Implement initialization logic for dialogue display
        }
    }
}