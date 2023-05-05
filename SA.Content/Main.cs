using BepInEx;
using UnityEngine;
using System.Reflection;
using R2API;
using HarmonyLib;
using System.Collections.Generic;
using RoR2;

namespace StageAesthetic
{
    [BepInPlugin("com.HIFU.StageAesthetic", "StageAesthetic", "0.7.3")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInDependency("PlasmaCore.ForgottenRelics", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(PrefabAPI.PluginGUID)]
    internal class Main : BaseUnityPlugin
    {
        public static AssetBundle stageaesthetic;
        public static Harmony Harmony;
        public static List<RampFog> DumbfuckRampFogs = new();

        public void Awake()
        {
            // increasing fogone increases the fog distance
            SwapVariants.AesLog = this.Logger;

            /*
            Harmony = new Harmony("com.HIFU.StageAesthetic"); // uh oh!
            Harmony.PatchAll(typeof(TheCoolerRampFogAccessor));
            SceneCatalog.onMostRecentSceneDefChanged += (_) => DumbfuckRampFogs.Clear();
            */

            stageaesthetic = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("StageAesthetic.dll", "stageaesthetic"));
            SwapVariants.Initialize();
        }
    }
}