using System;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Views;
using UnityEngine;

namespace Separated.GameManager
{
    public class GameControl : MonoBehaviour, IEventListener<EGameState>
    {
        public static EGameState CurState { get; private set; }

        private static void ChangeToPause()
        {
            Time.timeScale = 0f;
        }

        private static void ChangeToIngame()
        {
            Time.timeScale = 1f;
        }

        public static void ChangeGameState(EGameState newState)
        {
            if (CurState == newState)
                return;

            CurState = newState;
            switch (newState)
            {
                case EGameState.MainMenu:
                    // MainMenuView.Instance.Show();
                    break;

                case EGameState.InGame:
                    ChangeToIngame();
                    break;

                case EGameState.Pause:
                    ChangeToPause();
                    break;

                case EGameState.GameOver:
                    // GameOverView.Instance.Show();
                    break;

                default:
                    break;
            }
        }

        public void OnEventNotify(EGameState eventData)
        {
            Debug.Log(1);
            ChangeGameState(eventData);
        }

        void Start()
        {
            var menuOpenedEvent = EventManager.GetEvent<EGameState>(EventManager.EEventType.UIUpdated);
            menuOpenedEvent.AddListener(this);
        }
    }
}