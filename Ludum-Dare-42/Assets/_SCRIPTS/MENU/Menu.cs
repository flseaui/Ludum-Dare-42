using System.Reflection;
using ATTRIBUTES;
using DATA;
using MANAGERS;
using UnityEngine;

namespace MENU
{
    [MenuType(MenuTag.BlankMenu)]
    public abstract class Menu : MonoBehaviour
    {
        public MenuTag Type => GetType().GetCustomAttribute<MenuTypeAttribute>().MenuType;

        protected void Awake()
        {
            MenuManager.Instance.MenuStateChanged += SetupMenu;
            MenuManager.Instance.Menus.Add(Type, this);
        }

        private void SetupMenu(object sender, MenuManager.MenuChangedEventArgs e)
        {
            if (e.Menu != Type) return;

            SwitchToThis();
        }

        protected abstract void SwitchToThis();
    }
}