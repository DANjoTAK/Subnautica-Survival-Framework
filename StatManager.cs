using System.Collections.Generic;

namespace SurvivalFramework
{
    public class StatManager
    {
        #region Fields
        private static Dictionary<string, Stat> stats;
        #endregion

        #region StatsUnloadedEvent
        /// <summary>
        /// Fires once the stats are about to be removed.
        /// </summary>
        public static event StatsUnloadedEvent OnStatsUnloaded;
        public delegate void StatsUnloadedEvent(StatsUnloadedEventArgs e);
        public class StatsUnloadedEventArgs
        {
            
        }
        #endregion
        #region StatsLoadedEvent
        /// <summary>
        /// Fires once the stats have been loaded.
        /// </summary>
        public static event StatsLoadedEvent OnStatsLoaded;
        public delegate void StatsLoadedEvent(StatsLoadedEventArgs e);
        public class StatsLoadedEventArgs
        {
            
        }
        #endregion

        public static Dictionary<string, SaveManager.SaveDataStat> GetSaveData()
        {
            if (stats == null) return null;
            Dictionary<string, SaveManager.SaveDataStat> saveData = new Dictionary<string, SaveManager.SaveDataStat>();
            foreach (KeyValuePair<string, Stat> keyValuePair in stats)
            {
                saveData.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return saveData;
        }
        public static Dictionary<string, Stat> GetStats()
        {
            if (stats == null) return null;
            return new Dictionary<string, Stat>(stats);
        }

        internal static void SetFromSaveData(Dictionary<string, SaveManager.SaveDataStat> dataStats)
        {
            Dictionary<string, Stat> newStats = new Dictionary<string, Stat>();
            foreach (KeyValuePair<string, SaveManager.SaveDataStat> keyValuePair in dataStats)
            {
                Stat saveStat = new Stat(keyValuePair.Value);
                newStats.Add(keyValuePair.Key, saveStat);
            }
            stats = newStats;
            DefaultStats.Load();
            OnStatsLoaded?.Invoke(new StatsLoadedEventArgs()
            {

            });
        }

        internal static void Unload()
        {
            if (stats == null) return;
            DefaultStats.Unload();
            OnStatsUnloaded?.Invoke(new StatsUnloadedEventArgs() {

            });
            stats = null;
        }

        public static Stat RegisterStat(RegisterStatArgs args)
        {
            //TODO Overwrite certain properties.
            if (stats == null) return null;
            if (stats.ContainsKey(args.name)) return stats[args.name];
            Stat newStat = new Stat(args);
            stats.Add(args.name, newStat);
            return newStat;
        }
        public static Stat GetStat(string name)
        {
            if (stats == null) return null;
            if (!stats.ContainsKey(name)) return null;
            return stats[name];
        }

        public class RegisterStatArgs
        {
            public string name = "undefined"; // NOT CHANGING THIS WILL CAUSE A LOT OF PROBLEMS AND CONFLICTS
            public string displayName = "Undefined";
            public bool hidden = false;

            public bool useMin = true;
            public bool useMax = true;
            public float min = 0f;
            public float max = 100f;
            public float value = 0f;


            public static implicit operator Stat.StatArgs(RegisterStatArgs args)
            {
                Stat.StatArgs _args = new Stat.StatArgs()
                {
                    displayName = args.displayName,
                    hidden = args.hidden,
                    useMinValue = args.useMin,
                    useMaxValue = args.useMax,
                    minValue = args.min,
                    maxValue = args.max,
                    value = args.value,
                };
                return _args;
            }
        }
    }
}
