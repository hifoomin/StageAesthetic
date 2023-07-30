using BepInEx;
using UnityEngine;
using System.Reflection;
using R2API;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace StageAesthetic
{
    [BepInPlugin("com.HIFU.StageAesthetic", "StageAesthetic", "0.9.0")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInDependency("PlasmaCore.ForgottenRelics", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(PrefabAPI.PluginGUID)]
    internal class Main : BaseUnityPlugin
    {
        public static AssetBundle stageaesthetic;
        public static Shader cloudRemap = Addressables.LoadAssetAsync<Shader>("RoR2/Base/Shaders/HGCloudRemap.shader").WaitForCompletion();
        public static Harmony Harmony;
        public static List<RampFog> DumbfuckRampFogs = new();

        public void Awake()
        {
            // increasing fogone increases the fog distance
            SwapVariants.AesLog = this.Logger;

            stageaesthetic = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("StageAesthetic.dll", "stageaesthetic"));

            var materials = stageaesthetic.LoadAllAssets<Material>();

            foreach (Material material in materials)
            {
                switch (material.shader.name)
                {
                    case "StubbedShader/fx/hgcloudremap":
                        material.shader = cloudRemap;
                        break;
                }
            }

            SwapVariants.Initialize();
        }
    }
}