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
        public const string PluginVersion = "1.1.0";

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

        public static Material plainsAbandonedTerrainMat;
        public static Material plainsAbandonedDetailMat;
        public static Material plainsAbandonedDetailMat2;
        public static Material plainsAbandonedDetailMat3;
        public static Material plainsAbandonedWaterMat;

        public static Texture2D siphonedDesolateNormal;
        public static Texture2D siphonedDesolateSide;
        public static Texture2D siphonedDesolateTop;
        public static Material siphonedDesolateTerrainMat;
        public static Material siphonedDesolateDetailMat;
        public static Material siphonedDesolateDetailMat2;
        public static Material siphonedDesolateDetailMat3;
        public static Material siphonedDesolateDetailMat4;
        public static Material siphonedDesolateDetailMat5;
        public static Material siphonedDesolateWaterMat;

        public static Material aqueductSunderedTerrainMat;
        public static Material aqueductSunderedTerrainMat2;
        public static Material aqueductSunderedDetailMat;
        public static Material aqueductSunderedDetailMat2;
        public static Material aqueductSunderedDetailMat3;

        public static Material wetlandVoidTerrainMat;
        public static Material wetlandVoidTerrainMat2;
        public static Material wetlandVoidDetailMat;
        public static Material wetlandVoidDetailMat2;
        public static Material wetlandVoidDetailMat3;
        public static Material wetlandVoidWaterMat;

        public static Material rpdTitanicTerrainMat;
        public static Material rpdTitanicDetailMat;
        public static Material rpdTitanicDetailMat2;
        public static Material rpdTitanicWaterMat;

        public static Material spVoidTerrainMat;
        public static Material spVoidDetailMat;
        public static Material spVoidWaterMat;

        public static Material spHellTerrainMat;
        public static Material spHellDetailMat;
        public static Material spHellWaterMat;

        public static Material sirensAphelianTerrainMat;
        public static Material sirensAphelianTerrainMat2;
        public static Material sirensAphelianDetailMat;
        public static Material sirensAphelianDetailMat2;
        public static Material sirensAphelianDetailMat3;

        public static Material abyssalSimulacrumTerrainMat;
        public static Material abyssalSimulacrumBoulderMat;
        public static Material abyssalSimulacrumDetailMat;
        public static Material abyssalSimulacrumDetailMat2;
        public static Material abyssalSimulacrumDetailMat3;

        public static Material groveAbandonedTerrainMat;
        public static Material groveAbandonedTerrainMat2;
        public static Material groveAbandonedDetailMat;
        public static Material groveAbandonedDetailMat2;
        public static Material groveAbandonedWaterMat;

        public static Material skyMeadowAbyssalTerrainMat;
        public static Material skyMeadowAbyssalTerrainMat2;
        public static Material skyMeadowAbyssalDetailMat;
        public static Material skyMeadowAbyssalDetailMat2;
        public static Material skyMeadowAbyssalDetailMat3;
        public static Material skyMeadowAbyssalDetailMat4;
        public static Material skyMeadowAbyssalDetailMat5;
        public static Material skyMeadowAbyssalWaterMat;

        public static Material skyMeadowTitanicTerrainMat;
        public static Material skyMeadowTitanicTerrainMat2;
        public static Material skyMeadowTitanicDetailMat;
        public static Material skyMeadowTitanicDetailMat2;
        public static Material skyMeadowTitanicDetailMat3;
        public static Material skyMeadowTitanicDetailMat4;
        public static Material skyMeadowTitanicDetailMat5;

        public static Material skyMeadowAbandonedTerrainMat;
        public static Material skyMeadowAbandonedTerrainMat2;
        public static Material skyMeadowAbandonedDetailMat;
        public static Material skyMeadowAbandonedDetailMat2;
        public static Material skyMeadowAbandonedDetailMat3;
        public static Material skyMeadowAbandonedDetailMat4;
        public static Material skyMeadowAbandonedDetailMat5;
        public static Material skyMeadowAbandonedDetailMat6;
        public static Material skyMeadowAbandonedWaterMat;

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
            distantRoostVoidDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat").WaitForCompletion();
            distantRoostVoidDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            distantRoostVoidWaterMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            distantRoostVoidWaterMat.color = new Color32(0, 14, 255, 255);

            // Distant Roost (Abyssal)

            distantRoostAbyssalTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            distantRoostAbyssalTerrainMat.color = new Color32(0, 0, 0, 204);
            distantRoostAbyssalTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());
            distantRoostAbyssalTerrainMat2.color = new Color32(0, 0, 0, 135);
            distantRoostAbyssalDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcavesimple/matDCBoulder.mat").WaitForCompletion();
            distantRoostAbyssalDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion());
            distantRoostAbyssalWaterMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            distantRoostAbyssalWaterMat.color = new Color32(107, 23, 23, 255);
            distantRoostAbyssalWaterMat.shaderKeywords = new string[] { "_BUMPLARGE_ON", "_DISPLACEMENTMODE_OFF", "_DISPLACEMENT_ON", "_DISTORTIONQUALITY_HIGH", "_EMISSION", "_FOAM_ON", "_NORMALMAP" };

            // Titanic Plains (Abandoned)

            plainsAbandonedTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            plainsAbandonedTerrainMat.color = new Color32(230, 223, 174, 219);
            plainsAbandonedDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat").WaitForCompletion();
            plainsAbandonedDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            plainsAbandonedDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrim.mat").WaitForCompletion();
            plainsAbandonedWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolake.mat").WaitForCompletion();

            // Siphoned Forest (Desolate)

            siphonedDesolateNormal = Addressables.LoadAssetAsync<Texture2D>("RoR2/Base/Common/texNormalBumpyRock.jpg").WaitForCompletion();
            siphonedDesolateSide = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRockSide.png");
            siphonedDesolateTop = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRockTop.png");

            siphonedDesolateTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbTerrain.mat").WaitForCompletion());
            siphonedDesolateTerrainMat.color = new Color32(174, 153, 129, 255);
            siphonedDesolateTerrainMat.SetFloat("_RedChannelSmoothness", 0.5063887f);
            siphonedDesolateTerrainMat.SetFloat("_RedChannelBias", 1.2f);
            siphonedDesolateTerrainMat.SetFloat("_RedChannelSpecularExponent", 20f);
            siphonedDesolateTerrainMat.SetTexture("_RedChannelSideTex", siphonedDesolateSide);
            siphonedDesolateTerrainMat.SetTexture("_RedChannelTopTex", siphonedDesolateTop);

            siphonedDesolateTerrainMat.SetFloat("_GreenChannelBias", 1.87f);
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelSpecularStrength", 0f);
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelSpecularExponent", 20f);
            siphonedDesolateTerrainMat.SetFloat("_GreenChannelSmoothnes", 0.4169469f);

            siphonedDesolateTerrainMat.SetFloat("_BlueChannelBias", 1.3f);
            siphonedDesolateTerrainMat.SetFloat("_BlueChannelSmoothness", 0.3059852f);

            siphonedDesolateTerrainMat.SetFloat("_TextureFactor", 0.06f);
            siphonedDesolateTerrainMat.SetFloat("_NormalStrength", 0.3f);

            siphonedDesolateTerrainMat.SetFloat("_Depth", 0.1f);
            siphonedDesolateTerrainMat.SetInt("_RampInfo", 5);
            siphonedDesolateTerrainMat.SetTexture("_NormalTex", siphonedDesolateNormal);

            siphonedDesolateDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbBoulder.mat").WaitForCompletion();
            siphonedDesolateDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat").WaitForCompletion());
            siphonedDesolateDetailMat2.color = new Color32(18, 79, 40, 255);
            siphonedDesolateWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonWaterBridge.mat").WaitForCompletion();
            siphonedDesolateDetailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            siphonedDesolateDetailMat3.color = new Color32(205, 104, 12, 255);
            siphonedDesolateDetailMat3.SetFloat("_Depth", 0.714f);
            siphonedDesolateDetailMat3.SetFloat("_NormalStrength", 0.25f);
            siphonedDesolateDetailMat3.SetFloat("_RedChannelBias", 0.17f);
            siphonedDesolateDetailMat3.SetFloat("_RedChannelSpecularStrength", 0.0338f);
            siphonedDesolateDetailMat3.SetFloat("_GreenChannelBias", 0f);
            siphonedDesolateDetailMat3.SetTextureScale("_NormalTex", new Vector2(0.3f, 0.3f));
            siphonedDesolateDetailMat4 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion());
            siphonedDesolateDetailMat4.color = new Color32(255, 255, 255, 255);
            siphonedDesolateDetailMat5 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Captain/matCaptainSupplyDropEquipmentRestock.mat").WaitForCompletion());
            siphonedDesolateDetailMat5.color = new Color32(80, 162, 90, 255);

            // Abandoned Aqueduct (Sundered)

            aqueductSunderedTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain2.mat").WaitForCompletion());
            aqueductSunderedTerrainMat.color = new Color32(255, 156, 206, 184);
            aqueductSunderedTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain.mat").WaitForCompletion());
            aqueductSunderedDetailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJSandstone.mat").WaitForCompletion());
            aqueductSunderedDetailMat.color = new Color32(221, 77, 102, 231);
            aqueductSunderedDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidMetalTrimGrassy.mat").WaitForCompletion());
            aqueductSunderedDetailMat2.color = new Color32(130, 61, 74, 150);
            aqueductSunderedDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion();

            // Wetland Aspect (Void)

            wetlandVoidTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion();
            wetlandVoidTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion());
            wetlandVoidTerrainMat.color = new Color32(171, 167, 234, 132);
            wetlandVoidDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat").WaitForCompletion();
            wetlandVoidDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            wetlandVoidDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTrim.mat").WaitForCompletion();
            wetlandVoidWaterMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion());
            wetlandVoidWaterMat.color = new Color32(82, 24, 109, 255);

            // Rallypoint Delta (Titanic)

            rpdTitanicTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion());
            rpdTitanicTerrainMat.color = new Color32(95, 96, 132, 232);
            rpdTitanicTerrainMat.SetFloat("_Depth", 0.1740239f);
            rpdTitanicTerrainMat.SetFloat("_BlueChannelBias", 0.9805416f);
            rpdTitanicDetailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion());
            rpdTitanicDetailMat.color = new Color32(76, 90, 115, 78);
            rpdTitanicDetailMat.SetFloat("_SpecularStrength", 0.009451796f);
            rpdTitanicDetailMat.SetFloat("_Depth", 0.135765f);
            rpdTitanicDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuinsProjectedHuge.mat").WaitForCompletion());
            rpdTitanicDetailMat2.color = new Color32(209, 171, 29, 198);
            rpdTitanicDetailMat2.SetFloat("_NormalStrength", 0.1499685f);
            rpdTitanicDetailMat2.SetFloat("_SpecularStrength", 0.227f);
            rpdTitanicDetailMat2.SetFloat("_SpecularExponent", 5.497946f);
            rpdTitanicDetailMat2.SetFloat("_Smoothness", 0.4f);
            rpdTitanicDetailMat2.SetFloat("_SnowSpecularStrength", 0.1436673f);
            rpdTitanicDetailMat2.SetFloat("_SnowSpecularExponent", 0.9451796f);
            rpdTitanicDetailMat2.SetFloat("_SnowSmoothness", 1f);
            rpdTitanicDetailMat2.SetFloat("_SnowBias", -0.7378702f);
            rpdTitanicDetailMat2.SetFloat("_Depth", 0.07435415f);
            rpdTitanicDetailMat2.SetFloat("_TriplanarTextureFactor", 0.4f);
            rpdTitanicWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion();

            // Sulfur Pools (Void)

            spVoidTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion());
            spVoidTerrainMat.color = new Color32(255, 255, 255, 96);
            spVoidDetailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat").WaitForCompletion());
            // detailMat.color = new Color32(212, 214, 238, 255);
            spVoidWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSPWaterGreen.mat").WaitForCompletion();

            // Sulfur Pools (Hell)

            spHellTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            spHellTerrainMat.color = new Color32(0, 0, 0, 204);
            spHellDetailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion());
            spHellWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion();

            // Siren's Call (Aphelian)

            sirensAphelianTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Terrain.mat").WaitForCompletion());
            sirensAphelianTerrainMat.color = new Color32(138, 176, 167, 255);
            sirensAphelianTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat").WaitForCompletion());
            sirensAphelianTerrainMat2.color = new Color32(138, 176, 167, 255);
            sirensAphelianDetailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetAlien1BossEmissionDirty.mat").WaitForCompletion());
            sirensAphelianDetailMat.color = new Color32(252, 154, 72, 235);
            sirensAphelianDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_StoneSurface.mat").WaitForCompletion());
            sirensAphelianDetailMat2.color = new Color32(178, 127, 68, 159);
            sirensAphelianDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/MajorAndMinorConstruct/matMajorConstructDefenseMatrixEdges.mat").WaitForCompletion();

            // Sundered Grove (Abandanoned)

            groveAbandonedTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            groveAbandonedTerrainMat.color = new Color32(255, 222, 185, 39);
            groveAbandonedTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion());
            groveAbandonedTerrainMat2.color = new Color32(166, 157, 27, 59);
            groveAbandonedDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            groveAbandonedDetailMat.color = new Color32(166, 157, 27, 59);
            groveAbandonedWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matClayGooDebuff.mat").WaitForCompletion();
            groveAbandonedDetailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolake.mat").WaitForCompletion());
            groveAbandonedDetailMat2.color = new Color32(176, 153, 57, 255);

            // Abyssal Depths (Simulacrum)

            abyssalSimulacrumTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCTerrainFloorInfiniteTower.mat").WaitForCompletion();
            abyssalSimulacrumBoulderMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCBoulderInfiniteTower.mat").WaitForCompletion();
            abyssalSimulacrumDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion();
            abyssalSimulacrumDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matTrimSheetLemurianRuinsHeavyInfiniteTower.mat").WaitForCompletion();
            abyssalSimulacrumDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCTerrainWallsInfiniteTower.mat").WaitForCompletion();

            // Sky Meadow (Abyssal)

            skyMeadowAbyssalTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            skyMeadowAbyssalTerrainMat.color = new Color32(57, 0, 255, 42);
            skyMeadowAbyssalTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainFloor.mat").WaitForCompletion();
            skyMeadowAbyssalTerrainMat2.color = new Color32(255, 0, 0, 255);
            skyMeadowAbyssalDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion();
            skyMeadowAbyssalDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion();
            skyMeadowAbyssalDetailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetConstructionDestroyed.mat").WaitForCompletion());
            skyMeadowAbyssalDetailMat3.color = new Color32(255, 136, 103, 255);
            skyMeadowAbyssalDetailMat4 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat").WaitForCompletion();
            skyMeadowAbyssalDetailMat5 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCCoralActive.mat").WaitForCompletion());
            skyMeadowAbyssalDetailMat5.color = new Color32(255, 10, 0, 255);
            skyMeadowAbyssalWaterMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Cleanse/matWaterPack.mat").WaitForCompletion());
            skyMeadowAbyssalWaterMat.color = new Color32(217, 0, 255, 255);

            // Sky Meadow (Abandoned)

            skyMeadowAbandonedTerrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            skyMeadowAbandonedTerrainMat.color = new Color32(230, 223, 174, 219);
            skyMeadowAbandonedTerrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion());
            skyMeadowAbandonedTerrainMat2.color = new Color32(255, 188, 160, 223);
            skyMeadowAbandonedDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat").WaitForCompletion();
            skyMeadowAbandonedDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            skyMeadowAbandonedDetailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetConstructionWild.mat").WaitForCompletion());
            skyMeadowAbandonedDetailMat3.color = new Color32(248, 219, 175, 255);
            skyMeadowAbandonedDetailMat4 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetSwampyRuinsProjectedLight.mat").WaitForCompletion());
            skyMeadowAbandonedDetailMat4.color = new Color32(217, 191, 168, 255);
            skyMeadowAbandonedDetailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/MajorAndMinorConstruct/matMajorConstructDefenseMatrixEdges.mat").WaitForCompletion();
            skyMeadowAbandonedDetailMat6 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetClayPots.mat").WaitForCompletion();
            skyMeadowAbandonedWaterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matClayGooDebuff.mat").WaitForCompletion();

            // Sky Meadow (Titanic)

            skyMeadowTitanicTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion();
            skyMeadowTitanicTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrainBlender.mat").WaitForCompletion();
            skyMeadowTitanicDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion();
            skyMeadowTitanicDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderHeavyMoss.mat").WaitForCompletion();
            skyMeadowTitanicDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardProps.mat").WaitForCompletion();
            skyMeadowTitanicDetailMat4 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat").WaitForCompletion();
            skyMeadowTitanicDetailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/Junk/AncientWisp/matAncientWillowispSpiral.mat").WaitForCompletion();

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("PlasmaCore.ForgottenRelics"))
                ForgottenRelicsLoaded = true;

            SwapVariants.Initialize();

            // SwapVariants.SALogger.LogError("Forgotten Relics Loaded:" + ForgottenRelicsLoaded);
        }
    }
}