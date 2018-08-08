using System;
using System.IO;
using System.Linq;
using System.Text;
using DATA;
using MISC;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public static class MenuSwap
    {
        public static void SwapMenu(string menuName)
        {
            var previousMenu = GameObject.FindGameObjectsWithTag("MenuPanel").FirstOrDefault(x => x.activeSelf);
            if (previousMenu != null) previousMenu.SetActive(false);
            
            var menu = GameObject.Find(menuName).transform.FindObjectsWithTag("MenuPanel").FirstOrDefault();
            if (menu != null) menu.SetActive(true);

            EditorUtils.Collapse(menu, false);
            EditorUtils.SelectObject(menu);
        }

        public static bool SwapMenuValidation(string menuName)
        {
            if (!SceneManager.GetActiveScene().name.Equals("_MENU")) return false;
            
            var first = GameObject.Find(menuName).transform.FindObjectsWithTag("MenuPanel").FirstOrDefault();

            return first != null
                   && !Application.isPlaying
                   && !first.activeSelf;
        }
        
   }
}