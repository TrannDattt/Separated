using UnityEngine;

namespace Separated.UIElements
{
    public abstract class GameUI : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void Initialize();
    }
}