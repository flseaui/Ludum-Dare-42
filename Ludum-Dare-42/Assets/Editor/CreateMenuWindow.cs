using System;
using MANAGERS;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CreateMenuWindow : EditorWindow
    {
        private string _editorWindowText = "Choose a menu name: ";
        private string _menuName = string.Empty;
        
        private void OnGUI()
        {
            _menuName = EditorGUILayout.TextField(_editorWindowText, _menuName);
            
            if (GUILayout.Button("Create new menu"))
                MenuManager.Instance.CreateMenu(_menuName);
            
            if (GUILayout.Button("Abort"))
                Close();
        }

        [MenuItem("Menu/Create new menu")]
        public static void CreateMenuCreationWindow()
        {
            var window = CreateInstance<CreateMenuWindow>();
            window.ShowUtility();
        }
    }
}