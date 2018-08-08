/** This class is Auto-Generated **/

using UnityEditor;

namespace Editor.Generated
{
    public static class GMenuSwapMenuItems
    {
        [MenuItem("Menu/Swap To/MainMenu")]
        private static void SwapToMainMenu() => MenuSwap.SwapMenu("MainMenu");
        [MenuItem("Menu/Swap To/MainMenu", true)]
        private static bool SwapToMainMenuValidation() => MenuSwap.SwapMenuValidation("MainMenu");
        [MenuItem("Menu/Swap To/GameStartMenu")]
        private static void SwapToGameStartMenu() => MenuSwap.SwapMenu("GameStartMenu");
        [MenuItem("Menu/Swap To/GameStartMenu", true)]
        private static bool SwapToGameStartMenuValidation() => MenuSwap.SwapMenuValidation("GameStartMenu");
    }
}
