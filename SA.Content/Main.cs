using BepInEx;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace StageAesthetic
{
    [BepInPlugin(PluginAuthor, PluginName, PluginVersion)]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInDependency("PlasmaCore.ForgottenRelics", BepInDependency.DependencyFlags.SoftDependency)]
    internal class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        public const string PluginAuthor = "HIFU";
        public const string PluginName = "StageAesthetic";
        public const string PluginVersion = "1.0.0";

        public static AssetBundle stageaesthetic;
        public static Shader cloudRemap = Addressables.LoadAssetAsync<Shader>("RoR2/Base/Shaders/HGCloudRemap.shader").WaitForCompletion();
        public static Shader snowTopped = Addressables.LoadAssetAsync<Shader>("RoR2/Base/Shaders/HGSnowTopped.shader").WaitForCompletion(); // who's snow and why are they being topped
        public static List<RampFog> DumbfuckRampFogs = new();
        public static Material[] materials;

        public static Texture2D abyssalDirt;
        public static Texture2D abyssalGrass;
        public static Texture2D abyssalGravel;

        public static Material abyssalPlatform;

        public static bool ForgottenRelicsLoaded = false;

        public void Awake()
        {
            // increasing fogone increases the fog distance
            SwapVariants.SALogger = this.Logger;

            stageaesthetic = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("StageAesthetic.dll", "stageaesthetic"));

            materials = stageaesthetic.LoadAllAssets<Material>();

            foreach (Material material in materials)
            {
                switch (material.shader.name)
                {
                    case "StubbedShader/fx/hgcloudremap":
                        material.shader = cloudRemap;
                        break;

                    case "StubbedShader/deferred/hgsnowtopped":
                        material.shader = snowTopped;
                        break;
                }
            }

            abyssalDirt = stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texDirt.png");
            abyssalGrass = stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRedGrass.png");
            abyssalGravel = stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texLavenderGravel.png");

            abyssalPlatform = stageaesthetic.LoadAsset<Material>("Assets/StageAesthetic/Materials/matAbyssalPlatform.mat");

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("PlasmaCore.ForgottenRelics"))
                ForgottenRelicsLoaded = true;

            SwapVariants.Initialize();

            // SwapVariants.SALogger.LogError("Forgotten Relics Loaded:" + ForgottenRelicsLoaded);
        }
    }
}