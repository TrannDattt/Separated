using Separated.Player;
using UnityEngine;

namespace Separated.UIElements
{
    public class HUD : GameUI
    {
        [SerializeField] private HpBar _playerHpBar;

        public override void Show()
        {
            _playerHpBar.Show();
        }

        public override void Hide()
        {
            _playerHpBar.Hide();
        }

        public override void Initialize()
        {
            _playerHpBar.SetUnit(PlayerControl.Instance);
        }

        void Start()
        {
            Initialize();
        }
    }
}