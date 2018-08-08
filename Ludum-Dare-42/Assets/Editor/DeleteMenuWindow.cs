using MANAGERS;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DeleteMenuWindow : EditorWindow
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Delete current Menu"))
                MenuManager.Instance.DeleteMenu(MenuManager.Instance.MenuState.ToString());
        }
        
        [MenuItem("Menu/Delete menu")]
        public static void CreateMenuDeletionWindow()
        {
            var window = CreateInstance<DeleteMenuWindow>();
            window.ShowUtility();
        }
    }
}