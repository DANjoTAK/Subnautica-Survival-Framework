
namespace SurvivalFramework
{
    internal class DefaultStats
    {
        #region Fields
        internal static Stat Vision { get; private set; }
        internal static Stat Walking { get; private set; }
        internal static Stat Swimming { get; private set; }

        public const string VISION = "vision";
        public const string WALKING = "walking";
        public const string SWIMMING = "swimming";
        #endregion

        internal static void Load()
        {
            Vision = StatManager.RegisterStat(new StatManager.RegisterStatArgs() {name = VISION, displayName = "Vision", hidden = true, value = 100f});
            Walking = StatManager.RegisterStat(new StatManager.RegisterStatArgs(){name = WALKING, displayName = "Walk-Speed", hidden = true, min = 0f, max = 200f, value = 100f});
            Swimming = StatManager.RegisterStat(new StatManager.RegisterStatArgs(){name = SWIMMING, displayName = "Swim-Speed", hidden = true, min = 0f, max = 200f, value = 100f});
        }
        internal static void Unload()
        {
            Vision = null;
            Walking = null;
            Swimming = null;
        }
    }
}
