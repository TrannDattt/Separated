using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Separated.Views
{
    public class MultiTagsMenu : GameMenu
    {
        [SerializeField] private TaggedMenu[] _taggedMenus;

        private Dictionary<MenuTag, GameMenu> _menuDict = new();

        public override void Initialize()
        {
            base.Initialize();

            _menuDict.Clear();
            foreach (var menu in _taggedMenus)
            {
                _menuDict.Add(menu.Tag, menu.Menu);
                menu.Tag.OnTagSelected.AddListener(OpenSelectedMenu);
            }
        }

        public override void Show()
        {
            base.Show();

            OpenSelectedMenu(_taggedMenus[0].Tag);
        }

        public void OpenSelectedMenu(MenuTag tag)
        {
            foreach (var key in _menuDict.Keys)
            {
                if (key == tag)
                {
                    key.Show();
                    _menuDict[key].Show();
                }
                else
                {
                    key.Hide();
                    _menuDict[key].Hide();
                }
            }
        }

        public override void Hide()
        {
            base.Hide();

            foreach (var menu in _menuDict.Values)
            {
                menu.Hide();
            }
        }
    }

    [Serializable]
    public class TaggedMenu
    {
        public MenuTag Tag;
        public GameMenu Menu;
    }
}