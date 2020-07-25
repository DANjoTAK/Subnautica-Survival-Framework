using System.Collections.Generic;
using System.IO;
using Oculus.Newtonsoft.Json;
using UnityEngine;

namespace SurvivalFramework
{
    public class SaveManager
    {
        //TODO Remove Initialize
        #region Fields
        internal static SaveManagerComponent component { get; private set; }
        internal static GameObject gameObject { get; private set; }
        public static bool isSaving { get; private set; } = false;
        public static bool hasLoaded { get; private set; } = false;
        #endregion

        internal static void CreateGameObject()
        {
            hasLoaded = false;
            isSaving = false;
            gameObject = new GameObject("SaveManagerComponent");
            component = gameObject.AddComponent<SaveManagerComponent>();
            // GameObject.DontDestroyOnLoad(gameObject);
            // GameObject.DontDestroyOnLoad(component);
        }

        #region StateChangedEvent
        public delegate void StateChangedEvent(StateChangedEventArgs e);
        public static event StateChangedEvent OnStateChanged;
        public enum StateChangedEventType
        {
            undefined,
            loadingStarted,
            loadingCompleted,
            loadingFailed,
            savingStarted,
            savingCompleted,
            savingFailed
        }
        public class StateChangedEventArgs
        {
            public StateChangedEventType type;
        }
        #endregion

        public static string GetSaveGameDirectory()
        {
            return Path.Combine(Path.Combine(Path.GetFullPath("SNAppData"), "SavedGames"), SaveLoadManager.GetTemporarySavePath());
        }
        public static string GetSaveGameStatFile()
        {
            return GetSaveGameDirectory() + "/customstats.json";
        }

        private static void Save()
        {
            OnStateChanged?.Invoke(new StateChangedEventArgs()
            {
                type = StateChangedEventType.savingStarted
            });
            Dictionary<string, SaveDataStat> saveDataStats = StatManager.GetSaveData();
            Directory.CreateDirectory(GetSaveGameDirectory());
            File.WriteAllText(GetSaveGameStatFile(), JsonConvert.SerializeObject(saveDataStats));
            OnStateChanged?.Invoke(new StateChangedEventArgs()
            {
                type = StateChangedEventType.savingCompleted
            });
        }
        private static void Load()
        {
            OnStateChanged?.Invoke(new StateChangedEventArgs()
            {
                type = StateChangedEventType.loadingStarted
            });
            if (!Directory.Exists(GetSaveGameDirectory()) || !File.Exists(GetSaveGameStatFile()))
            {
                StatManager.SetFromSaveData(new Dictionary<string, SaveDataStat>());
                OnStateChanged?.Invoke(new StateChangedEventArgs()
                {
                    type = StateChangedEventType.loadingCompleted
                });
                return;
            }
            Dictionary<string, SaveDataStat> saveDataStats =
                JsonConvert.DeserializeObject<Dictionary<string, SaveDataStat>>(
                    File.ReadAllText(GetSaveGameStatFile()));
            StatManager.SetFromSaveData(saveDataStats);
            OnStateChanged?.Invoke(new StateChangedEventArgs()
            {
                type = StateChangedEventType.loadingCompleted
            });
        }

        public class SaveDataStat
        {
            public string displayName;
            public bool hidden;
            public bool useMin;
            public bool useMax;
            public float min;
            public float max;
            public float value;
        }

        public class SaveManagerComponent : MonoBehaviour
        {

            public void Awake()
            {
                
            }

            public void Update()
            {
                bool _saving = SaveLoadManager.main.isSaving;

                if (_saving && !isSaving && SceneManager.isMainScene)
                {
                    isSaving = true;
                    Save();
                }
                else if (!_saving && isSaving)
                {
                    isSaving = false;
                }
                if (!hasLoaded && SceneManager.isMainScene)
                {
                    hasLoaded = true;
                    Load();
                } else if (hasLoaded && !SceneManager.isMainScene)
                {
                    hasLoaded = false;
                }
            }
        }
    }
}
