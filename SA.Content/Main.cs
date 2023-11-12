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
        public const string PluginVersion = "1.0.2";

        public static AssetBundle stageaesthetic;
        public static Shader cloudRemap = Addressables.LoadAssetAsync<Shader>("RoR2/Base/Shaders/HGCloudRemap.shader").WaitForCompletion();
        public static Shader snowTopped = Addressables.LoadAssetAsync<Shader>("RoR2/Base/Shaders/HGSnowTopped.shader").WaitForCompletion(); // who's snow and why are they being topped
        public static List<RampFog> DumbfuckRampFogs = new();
        public static Material[] materials;

        public static Texture2D abyssalDirt;
        public static Texture2D abyssalGrass;
        public static Texture2D abyssalGravel;

        public static Material abyssalPlatform;

        public static Material distantRoostVoidTerrainMat;
        public static Material distantRoostVoidTerrainMat2;
        public static Material distantRoostVoidDetailMat;
        public static Material distantRoostVoidDetailMat2;
        public static Material distantRoostVoidWaterMat;

        public static Material distantRoostAbyssalTerrainMat;
        public static Material distantRoostAbyssalTerrainMat2;
        public static Material distantRoostAbyssalDetailMat;
        public static Material distantRoostAbyssalDetailMat2;
        public static Material distantRoostAbyssalWaterMat;

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

            // pre-caching in hopes of eliminating the null material issue

            // Distant Roost (Void)

            distantRoostVoidTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion());
            distantRoostVoidTerrainMat.color = new Color32(188, 162, 162, 255);
            distantRoostVoidTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion());
            distantRoostVoidTerrainMat2.color = new Color32(188, 162, 162, 255);
            distantRoostVoidDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainGem.mat").WaitForCompletion();
            distantRoostVoidDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            distantRoostVoidWaterMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            distantRoostVoidWaterMat.color = new Color32(0, 14, 255, 255);

            // Distant Roost (Abyssal)

            distantRoostAbyssalTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            distantRoostAbyssalTerrainMat.color = new Color32(0, 0, 0, 204);
            distantRoostAbyssalTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());
            distantRoostAbyssalTerrainMat2.color = new Color32(0, 0, 0, 135);
            distantRoostAbyssalDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion();
            distantRoostAbyssalDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuins.mat").WaitForCompletion());
            distantRoostAbyssalDetailMat2.color = new Color32(181, 66, 34, 255);
            distantRoostAbyssalWaterMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            distantRoostAbyssalWaterMat.color = new Color32(107, 23, 23, 255);
            distantRoostAbyssalWaterMat.shaderKeywords = new string[] { "_BUMPLARGE_ON", "_DISPLACEMENTMODE_OFF", "_DISPLACEMENT_ON", "_DISTORTIONQUALITY_HIGH", "_EMISSION", "_FOAM_ON", "_NORMALMAP" };

            // Siphoned Forest (Desolate)

            var normal = Addressables.LoadAssetAsync<Texture2D>("RoR2/Base/Common/texNormalBumpyRock.jpg").WaitForCompletion();
            var side = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRockSide.png");
            var top = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRockTop.png");

            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(174, 153, 129, 255);
            terrainMat.SetFloat("_RedChannelSmoothness", 0.5063887f);
            terrainMat.SetFloat("_RedChannelBias", 1.2f);
            terrainMat.SetFloat("_RedChannelSpecularExponent", 20f);
            terrainMat.SetTexture("_RedChannelSideTex", side);
            terrainMat.SetTexture("_RedChannelTopTex", top);

            terrainMat.SetFloat("_GreenChannelBias", 1.87f);
            terrainMat.SetFloat("_GreenChannelSpecularStrength", 0f);
            terrainMat.SetFloat("_GreenChannelSpecularExponent", 20f);
            terrainMat.SetFloat("_GreenChannelSmoothnes", 0.4169469f);

            terrainMat.SetFloat("_BlueChannelBias", 1.3f);
            terrainMat.SetFloat("_BlueChannelSmoothness", 0.3059852f);

            terrainMat.SetFloat("_TextureFactor", 0.06f);
            terrainMat.SetFloat("_NormalStrength", 0.3f);

            terrainMat.SetFloat("_Depth", 0.1f);
            terrainMat.SetInt("_RampInfo", 5);
            terrainMat.SetTexture("_NormalTex", normal);

            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbBoulder.mat").WaitForCompletion();
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat").WaitForCompletion());
            detailMat2.color = new Color32(18, 79, 40, 255);
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonWaterBridge.mat").WaitForCompletion();
            var detailMat4 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            detailMat4.color = new Color32(205, 104, 12, 255);
            detailMat4.SetFloat("_Depth", 0.714f);
            detailMat4.SetFloat("_NormalStrength", 0.25f);
            detailMat4.SetFloat("_RedChannelBias", 0.17f);
            detailMat4.SetFloat("_RedChannelSpecularStrength", 0.0338f);
            detailMat4.SetFloat("_GreenChannelBias", 0f);
            detailMat4.SetTextureScale("_NormalTex", new Vector2(0.3f, 0.3f));
            var detailMat5 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            detailMat5.color = new Color32(255, 255, 255, 255);
            var detailMat6 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Captain/matCaptainSupplyDropEquipmentRestock.mat").WaitForCompletion());
            detailMat6.color = new Color32(80, 162, 90, 255);

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("PlasmaCore.ForgottenRelics"))
                ForgottenRelicsLoaded = true;

            SwapVariants.Initialize();

            // SwapVariants.SALogger.LogError("Forgotten Relics Loaded:" + ForgottenRelicsLoaded);
        }
    }
}