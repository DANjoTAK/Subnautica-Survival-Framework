using System;

namespace SurvivalFramework
{
    public class SurvivalHelper
    {
        public static bool IsSurvival()
        {
            return GameModeUtils.RequiresSurvival();
        }
        public static float GetChangeFactor(float time, float value)
        {
            return time / value;
        }
        public static float GetNewValue(float time, float value, float factor, float min, float max)
        {
            float newValue = value + (time / factor);
            newValue = newValue > max ? max : newValue < min ? min : newValue;
            return newValue;
        }
        public static float GetChangeValue(float time, float factor)
        {
            return time / factor;
        }

        public static float GetTime()
        {
            return (float)DayNightCycle.main.GetDayNightCycleTime() +
                   (float)Math.Floor(DayNightCycle.main.GetDay());
        }
    }
}
