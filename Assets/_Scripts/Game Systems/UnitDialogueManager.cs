using System.Collections.Generic;
using UnityEngine;

namespace Separated.GameSystems
{
    public class UnitDialogueManager : MonoBehaviour
    {
        public enum EDialogueType
        {
            FirstEncounter,
            Normal,
            Quest
        }

        [SerializeField] private List<Dialogue> _firstEncounterDialogues;
        [SerializeField] private List<Dialogue> _normalDialogues;
        [SerializeField] private List<Dialogue> _questDialogues;

        public void TriggerDialogue()
        {
            if (_firstEncounterDialogues.Count > 0)
            {
                TriggerDialogue(EDialogueType.FirstEncounter);
                return;
            }
            
            if (_questDialogues.Count > 0)
            {
                TriggerDialogue(EDialogueType.Quest);
                return;
            }

            TriggerDialogue(EDialogueType.Normal);
        }

        private void TriggerDialogue(EDialogueType dialogueType)
        {
            List<Dialogue> dialoguesToUse = new();

            switch (dialogueType)
            {
                case EDialogueType.FirstEncounter:
                    dialoguesToUse = _firstEncounterDialogues;
                    _firstEncounterDialogues.Clear();
                    break;
                case EDialogueType.Normal:
                    dialoguesToUse = _normalDialogues;
                    break;
                case EDialogueType.Quest:
                    dialoguesToUse = _questDialogues;
                    break;
            }

            if (dialoguesToUse.Count > 0)
            {
                DialogueSystem.Instance.PrepareDialogue(dialoguesToUse);
                DialogueSystem.Instance.StartDialogue();
            }
            else
            {
                Debug.LogWarning("No dialogues available for type: " + dialogueType);
            }
        }
    }
}