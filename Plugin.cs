using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SuperTrampers;
using SuperTrampers.Database;
using SuperTrampers.GameSettings;
using UnityEngine.Device;

namespace SurvislandTweaks;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static ConfigEntry<int> FPS;
    internal static ConfigEntry<bool> FastWheel;
    internal static ConfigEntry<float> VertSpeedFactor;
    internal static ManualLogSource Log;

    private void Awake()
    {
        // Plugin startup logic
        Log = Logger;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        FPS = Config.Bind<int>("General", "FPS", 99999, "Set upper limit");
        FastWheel = Config.Bind<bool>("General", "FastWheel", true, "True/False.  Speed up Destroy, Extinguish etc, on the wheel");
        VertSpeedFactor = Config.Bind<float>("General", "VertSpeedFactor", 1f, "Adjust vertical mouse pitch speed by this factor");
        Harmony.CreateAndPatchAll(typeof(Patches));
        Log.LogInfo("Patches Applied");
    }

    private class Patches
    {
        [HarmonyPatch(typeof(GraphicSetting), nameof(GraphicSetting.SetQualitySettings))]
        [HarmonyPostfix]
        static void GraphicSettingSetQualitySettingsPostfix()
        {
            Application.targetFrameRate = FPS.Value > 0 ? FPS.Value : 60;
        }

        [HarmonyPatch(typeof(RouletteDataBase), nameof(RouletteDataBase.TryGetButtonFunctionDelay))]
        [HarmonyPostfix]
        static void RouletteDataBaseTryGetButtonFunctionDelayPostfix(ref float delay)
        {
            if (FastWheel.Value)
                delay *= 0.25f;
        }

        [HarmonyPatch(typeof(CameraRig), "Awake")]
        [HarmonyPostfix]
        static void CameraRigAwakePostfix(ref float ___pitchSpeed)
        {
            ___pitchSpeed *= VertSpeedFactor.Value;
        }
    }
}