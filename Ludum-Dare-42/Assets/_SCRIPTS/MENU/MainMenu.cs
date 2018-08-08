using System;
using ATTRIBUTES;
using DATA;
using MANAGERS;

namespace MENU
{
    [MenuType(MenuTag.MainMenu)]
    public class MainMenu : Menu
    {
        protected override void SwitchToThis()
        {
            throw new NotImplementedException();
        } 
        
        public void SwitchToGameStartMenu() => MenuManager.Instance.MenuState = MenuTag.GameStartMenu;
    }
}