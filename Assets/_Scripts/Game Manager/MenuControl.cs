using System.Collections.Generic;
using Separated.Helpers;
using Separated.Views;

namespace Separated.GameManager
{
    public class MenuControl : Singleton<MenuControl>
    {
        private HashSet<GameMenu> _openedMenus = new();
        public bool HasOpenedMenu => _openedMenus.Count > 0;

        public void AddMenu(GameMenu menu)
        {
            if (_openedMenus.Contains(menu))
            {
                return;
            }

            _openedMenus.Add(menu);
        }

        public void RemoveMenu(GameMenu menu)
        {
            if (!_openedMenus.Contains(menu))
            {
                return;
            }

            _openedMenus.Remove(menu);
        }
    }
}