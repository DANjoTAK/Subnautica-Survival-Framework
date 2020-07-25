using UnityEngine;
using UnityEngine.SceneManagement;

namespace SurvivalFramework
{
    public class SceneManager
    {
        #region Fields
        private static bool initialized = false;
        internal static SceneManagerComponent component { get; private set; }
        internal static GameObject gameObject { get; private set; }
        public static bool isMainScene { get; private set; } = false;
        public static bool isIngame { get; private set; } = false;
        #endregion

        internal static void Initialize()
        {
            if (initialized) return;
            initialized = true;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        internal static void CreateGameObject()
        {
            gameObject = new GameObject("SceneManagerComponent");
            component = gameObject.AddComponent<SceneManagerComponent>();
            // GameObject.DontDestroyOnLoad(gameObject);
            // GameObject.DontDestroyOnLoad(component);
        }

        #region IngameStatusChangedEvent
        /// <summary>
        /// Fires as soon as the player is ingame and again when he is no longer.
        /// </summary>
        public static event IngameStatusChangedEvent OnIngameStatusChanged;
        public delegate void IngameStatusChangedEvent(IngameStatusChangedEventArgs e);
        public class IngameStatusChangedEventArgs
        {
            public bool ingame;
        }
        #endregion
        #region MainSceneStatusChangedEvent
        public static event MainSceneStatusChangedEvent OnMainSceneStatusChanged;
        public delegate void MainSceneStatusChangedEvent(MainSceneStatusChangedEventArgs e);
        public class MainSceneStatusChangedEventArgs
        {
            public bool isMainScene;
        }
        #endregion

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Equals("Main"))
            {
                isMainScene = true;
                CreateGameObject();
                SaveManager.CreateGameObject();
                DebugGUI.MainSceneLoaded();
                InputHelper.MainSceneLoaded();
                OnMainSceneStatusChanged?.Invoke(new MainSceneStatusChangedEventArgs()
                {
                    isMainScene = true
                });
            }
        }

        private static void OnSceneUnloaded(Scene scene)
        {
            if (scene.name.Equals("Main"))
            {
                isMainScene = false;
                isIngame = false;
                OnIngameStatusChanged?.Invoke(new IngameStatusChangedEventArgs()
                {
                    ingame = false
                });
                OnMainSceneStatusChanged?.Invoke(new MainSceneStatusChangedEventArgs()
                {
                    isMainScene = false
                });
                StatManager.Unload();
                InputHelper.MainSceneUnloaded();
                DebugGUI.MainSceneUnloaded();
            }
        }

        public class SceneManagerComponent : MonoBehaviour
        {

            public void Awake()
            {

            }

            public void Update()
            {
                if (!isIngame && (isMainScene && !uGUI.main.loading.IsLoading && !uGUI.main.intro.showing))
                {
                    isIngame = true;
                    OnIngameStatusChanged?.Invoke(new IngameStatusChangedEventArgs()
                    {
                        ingame = true
                    });
                }
                // else if (isIngame && (!isMainScene || uGUI.main.loading.IsLoading || uGUI.main.intro.showing))
                // {
                //     isIngame = false;
                //     OnIngameStatusChanged?.Invoke(new IngameStatusChangedEventArgs()
                //     {
                //         ingame = false
                //     });
                // }
            }
        }
    }
}
