using Separated.Player;
using UnityEngine;

namespace Separated.Views
{
    public class HUD : GameUI
    {
        [SerializeField] private HpBarView _playerHpBar;

        public override void Show()
        {
            _playerHpBar.Show();
        }

        public override void Hide()
        {
            _playerHpBar.Hide();
        }

        public void Initialize()
        {
            // _playerHpBar.Initialize(PlayerControl.Instance);
        }

        void Start()
        {
            Initialize();
        }
    }
}