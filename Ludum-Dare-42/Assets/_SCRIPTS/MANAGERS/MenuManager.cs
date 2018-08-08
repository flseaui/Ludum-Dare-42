using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DATA;
using MENU;
using MISC;
using UnityEngine;

namespace MANAGERS
{
    public class MenuManager : Singleton<MenuManager>, INotifyPropertyChanged
    {
        public delegate void MenuChangedEventHandler(object sender, MenuChangedEventArgs e);

        private MenuTag _menuState;
        private MenuTag _previousMenuState;

        [SerializeField] private MenuTag _startingMenu = MenuTag.BlankMenu;
        [SerializeField] private GameObject _blankMenuPrefab;

        public Dictionary<MenuTag, Menu> Menus = new Dictionary<MenuTag, Menu>();

        public MenuTag MenuState
        {
            get => _menuState;
            set
            {
                if (value == _menuState) return;

                _previousMenuState = _menuState;
                _menuState = value;

                if (_previousMenuState != value)
                {
                    Menus[_menuState].transform.FindObjectsWithTag("MenuPanel").FirstOrDefault()?.SetActive(true);
                    Menus[_previousMenuState].transform.FindObjectsWithTag("MenuPanel").FirstOrDefault()
                        ?.SetActive(false);
                }

                OnPropertyChanged();
                OnMenuStateChanged(new MenuChangedEventArgs(_menuState));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SwitchToPreviousMenu()
        {
            MenuState = _previousMenuState;
        }

        private void Awake()
        {
            _menuState = _startingMenu;
            _previousMenuState = _startingMenu;
        }

        public void CreateMenu(string menuName)
        {
            var menu = Instantiate(_blankMenuPrefab, GameObject.Find("Canvas").transform, false);
            menu.name = menuName;
            menu.transform.Find("BlankMenuPanel").name = $"{ menuName }Panel";
        }

        public void DeleteMenu(string menuName)
        {
            DestroyImmediate(GameObject.Find(menuName));
        }
        
        private void OnMenuStateChanged(MenuChangedEventArgs e)
        {
            var handler = MenuStateChanged;
            handler?.Invoke(this, e);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        // [CallerMemberName] sets propertyName to the name of the caller to the method
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public event MenuChangedEventHandler MenuStateChanged;

        public class MenuChangedEventArgs : EventArgs
        {
            public MenuChangedEventArgs(MenuTag menu)
            {
                Menu = menu;
            }

            public MenuTag Menu { get; }
        }
    }
}