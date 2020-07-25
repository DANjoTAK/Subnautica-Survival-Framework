using Harmony;
using UnityEngine;

namespace SurvivalFramework
{
    public class InputHelper
    {
        internal static InputHelperComponent component { get; private set; }
        internal static GameObject gameObject { get; private set; }
        
        public static bool isEditingSign { get; private set; } = false;
        public static bool isEditingSubName { get; private set; } = false;
        public static bool isTypingInConsole { get { return DevConsole.instance.state; } }

        public static bool IsKeyDown(KeyCode key)
        {
            return Input.GetKeyDown(key) && !IngameMenu.main.focused && !isEditingSign && !isEditingSubName && !isTypingInConsole;
        }

        internal static void MainSceneLoaded()
        {
            //TODO
            return;
            gameObject = new GameObject("InputHelperComponent");
            component = gameObject.AddComponent<InputHelperComponent>();
        }
        internal static void MainSceneUnloaded()
        {
            gameObject = null;
            component = null;
        }

        #region Patches
        [HarmonyPatch(typeof(uGUI_SignInput))]
        [HarmonyPatch("OnSelect")]
        internal class SignInputSelectPatch
        {
            public static void Postfix()
            {
                isEditingSign = true;
            }
        }
        [HarmonyPatch(typeof(uGUI_SignInput))]
        [HarmonyPatch("OnDeselect")]
        internal class SignInputDeselectPatch
        {
            public static void Postfix()
            {
                isEditingSign = false;
            }
        }
        [HarmonyPatch(typeof(SubNameInput))]
        [HarmonyPatch("OnSelect")]
        internal class SubInputSelectPatch
        {
            public static void Postfix()
            {
                isEditingSubName = true;
            }
        }
        [HarmonyPatch(typeof(SubNameInput))]
        [HarmonyPatch("OnDeselect")]
        internal class SubInputDeselectPatch
        {
            public static void Postfix()
            {
                isEditingSubName = false;
            }
        }
        #endregion

        public class InputHelperComponent : MonoBehaviour
        {
            //TODO
        }
    }
}
