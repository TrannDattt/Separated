using Separated.Player;
using UnityEngine;

namespace Separated.UIElements
{
    public class HUD : GameUI
    {
        [SerializeField] private HpBar _playerHpBar;

        public override void Show()
        {
            // Implement show logic
        }

        public override void Hide()
        {
            // Implement hide logic
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