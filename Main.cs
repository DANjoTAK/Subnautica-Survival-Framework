using System.Reflection;
using Harmony;

namespace SurvivalFramework
{
    public class Main
    {
        internal static HarmonyInstance harmonyInstance { get; private set; }
        public static void Patch()
        {
            if (harmonyInstance != null) return;
            harmonyInstance = HarmonyInstance.Create("survivalframework");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            SceneManager.Initialize();
        }
    }
}
