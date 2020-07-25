using Harmony;

namespace SurvivalFramework
{
    class PlayerPatcher
    {
        #region PlayerSuffocationUpdate
        [HarmonyPatch(typeof(Player))]
        [HarmonyPatch("SuffocationUpdate")]
        class PlayerSuffocationUpdate
        {
            static void Postfix(Player __instance)
            {
                if (!SceneManager.isIngame) return;
                float suffocation = __instance.suffocation.t; // -1f
                float vision = DefaultStats.Vision == null ? 1f : 1 - ((1 - (DefaultStats.Vision.RealValue / 100f)) * 0.76f);
                uGUI.main.overlays.Set(0, 1f - (vision < suffocation ? vision : suffocation));
            }
        }
        #endregion

        #region PlayerMotorUpdate
        [HarmonyPatch(typeof(PlayerMotor))]
        [HarmonyPatch("Update")]
        class PlayerMotorUpdate
        {
            static void Postfix(PlayerMotor __instance)
            {
                if (__instance is GroundMotor)
                {
                    if (Player.main.motorMode != Player.MotorMode.Walk &&
                        Player.main.motorMode != Player.MotorMode.Run) return;
                    float walking = DefaultStats.Walking == null ? 1f : DefaultStats.Walking.RealValue / 100f;
                    float realwalking = walking <= 1f ? 1f + walking * 2.5f : 3.5f + (walking - 1f) * 1.5f;
                    float realsprinting = walking <= 1f ? 1f + walking : 2f;
                    float realjumping = walking <= 1f ? 1f + walking : 2f + (walking - 1f);
                    __instance.forwardMaxSpeed = realwalking;
                    __instance.sprintModifier = realsprinting;
                    __instance.jumpHeight = realjumping;
                } else if (__instance is UnderwaterMotor)
                {
                    if (Player.main.motorMode != Player.MotorMode.Dive &&
                        Player.main.motorMode != Player.MotorMode.Seaglide) return;
                    float swimming = DefaultStats.Swimming == null ? 1f : DefaultStats.Swimming.RealValue / 100f;
                    float realswimming = swimming <= 1 ? 1f + swimming * 4f : 5f + (swimming - 1f) * 2.5f;
                    float realseaglide = swimming <= 1 ? 20f + swimming * 5f : 25f + (swimming - 1f) * 5f;
                    __instance.forwardMaxSpeed = Player.main.motorMode == Player.MotorMode.Seaglide ? realseaglide : realswimming;
                }
            }
        }
        #endregion
    }
}
