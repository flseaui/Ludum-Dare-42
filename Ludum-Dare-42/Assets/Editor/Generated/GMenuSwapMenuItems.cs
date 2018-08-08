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

        [MenuItem("Menu/Swap To/OtherMenu")]
        private static void SwapToOtherMenu() => MenuSwap.SwapMenu("OtherMenu");
        [MenuItem("Menu/Swap To/OtherMenu", true)]
        private static bool SwapToOtherMenuValidation() => MenuSwap.SwapMenuValidation("OtherMenu");

    }
}
