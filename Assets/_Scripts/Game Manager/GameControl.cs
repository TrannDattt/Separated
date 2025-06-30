using System;
using Separated.Enums;
using Separated.Helpers;
using Separated.Interfaces;
using Separated.Views;
using UnityEngine;

namespace Separated.GameManager
{
    public class GameControl : Singleton<GameControl>
    {
        public static EGameState CurState { get; private set; }

        private SoundControl _soundControl;

        private void ChangeToPause()
        {
            Time.timeScale = 0f;
        }

        private void ChangeToIngame()
        {
            Time.timeScale = 1f;
        }

        public void ChangeGameState(EGameState newState)
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

        void Start()
        {
            ChangeGameState(EGameState.InGame);
        }

        void OnEnable()
        {
            _soundControl = FindFirstObjectByType<SoundControl>();
        }

        void Update()
        {
            switch (CurState)
            {
                case EGameState.MainMenu:
                    break;

                case EGameState.InGame:
                    if (MenuControl.Instance.HasOpenedMenu)
                    {
                        ChangeGameState(EGameState.Pause);
                    }
                    break;

                case EGameState.Pause:
                    if (!MenuControl.Instance.HasOpenedMenu)
                    {
                        ChangeGameState(EGameState.InGame);
                    }
                    break;

                case EGameState.GameOver:
                    break;

                default:
                    break;
            }
        }
    }
}