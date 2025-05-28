using System.Collections.Generic;
using UnityEngine;
using Separated.Helpers;
using System;
using Separated.UIElements;

namespace Separated.GameSystems
{
    public class DialogueSystem : Singleton<DialogueSystem>
    {
        [SerializeField] private DialogueDisplay _dialogueDisplay;

        private Queue<Dialogue> _dialogueQueue = new();
        private bool isDialogueActive = false;

        public event Action OnDialogueFinished;

        public void PrepareDialogue(List<Dialogue> dialogues)
        {
            if (dialogues == null || dialogues.Count == 0)
            {
                Debug.LogWarning("No dialogues provided to prepare.");
                return;
            }

            _dialogueQueue.Clear();
            dialogues.ForEach(d => _dialogueQueue.Enqueue(d));
            isDialogueActive = false;
        }

        public void StartDialogue()
        {
            if (!isDialogueActive)
            {
                isDialogueActive = true;
                _dialogueDisplay.Show();
                Debug.Log("Dialogue started.");
            }

            var newDialogue = _dialogueQueue.Dequeue();
            ShowDialogue(newDialogue);
        }

        private void ShowDialogue(Dialogue dialogue)
        {
            // Logic to display the dialogue on the UI
            Debug.Log("Showing dialogue: " + dialogue.Content + " by " + dialogue.Speaker);
        }

        public void EndDialogue()
        {
            if (!isDialogueActive) return;

            Debug.Log("Dialogue ended.");
            isDialogueActive = false;
            _dialogueDisplay.Hide();
            _dialogueQueue.Clear();

            OnDialogueFinished?.Invoke();
        }

        private void GetUserInput()
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && isDialogueActive)
            {
                if (_dialogueQueue.Count > 0)
                {
                    StartDialogue();
                }
                else
                {
                    EndDialogue();
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _dialogueDisplay.Initialize();
        }

        private void Update()
        {
            GetUserInput();
        }
    }

    [Serializable]
    public class Dialogue
    {
        [field: SerializeField] public string Speaker { get; set; }
        [field: SerializeField] public string Content { get; set; }
        [field: SerializeField] public bool IsAnnonymous { get; set; }

        // public Dialogue(GameObject speaker, string content, bool isAnnonymous = false)
        // {
        //     Speaker = isAnnonymous ? "???" : speaker.name;
        //     Content = content;
        // }
    }
}