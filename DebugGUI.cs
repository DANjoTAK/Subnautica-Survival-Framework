using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalFramework
{
    class DebugGUI
    {
        public static DebugGUIComponent component { get; private set; }
        public static GameObject gameObject { get; private set; }

        internal static void MainSceneLoaded()
        {
            gameObject = new GameObject("DebugGUIComponent");
            component = gameObject.AddComponent<DebugGUIComponent>();
        }
        internal static void MainSceneUnloaded()
        {
            gameObject = null;
            component = null;
        }

        public class DebugGUIComponent : MonoBehaviour
        {
            public void Awake()
            {
                
            }

            public void Update()
            {

            }

            public void OnGUI()
            {
                if (SceneManager.isIngame && true)
                {
                    string nformat = "0.##";
                    String _vals = "-- STATS (Name: value/max) --";
                    _vals += Environment.NewLine + "Health: " + Player.main.liveMixin.health.ToString(nformat);
                    _vals += Environment.NewLine + "Food: " + Player.main.liveMixin.GetComponent<Survival>().food.ToString(nformat);
                    _vals += Environment.NewLine + "Water: " + Player.main.liveMixin.GetComponent<Survival>().water.ToString(nformat);
                    foreach (KeyValuePair<string, Stat> keyValuePair in StatManager.GetStats())
                    {
                        _vals += Environment.NewLine + keyValuePair.Key + ": " + keyValuePair.Value.RealValue.ToString(nformat);
                    }
                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = Color.white;
                    style.fontSize = 16;
                    GUI.Label(new Rect(100, 100, 400, 600), _vals, style);
                }
                else
                {

                }
            }
        }
    }
}
