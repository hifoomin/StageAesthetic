using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage2
{
    internal class AbandonedAqueduct
    {
        public static void LightChanges(string variant)
        {
            if (variant == "dark") chain = new Color(0.721f, 0.041f, 0.065f);
            if (variant == "rain") chain = new Color(0.761f, 0.821f, 0.926f);
            if (variant == "night") chain = new Color(0.221f, 0.961f, 0.925f);
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    if (light.gameObject.name.Equals("CrystalLight")) light.color = chain;
                }
            }
        }

        public static void DarkAqueduct(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(51, 27, 58, 65);
            fog.fogColorMid.value = new Color32(27, 11, 33, 165);
            fog.fogColorEnd.value = new Color32(63, 24, 15, 255);
            fog.skyboxStrength.value = 0f;
            fog.fogOne.value = 0.2f;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            GameObject.Find("HOLDER: Warning Flags").SetActive(false);
            base1.Find("Warning Signs").gameObject.SetActive(true);
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.SetActive(false);
            newSun.intensity = 1.6f;
            newSun.color = new Color32(113, 45, 21, 255);
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(67, 35, 76, 65);
            CaveFog.fogColorMid.value = new Color32(41, 17, 51, 165);
            CaveFog.fogColorEnd.value = new Color32(84, 31, 20, 255);
            LightChanges("dark");
            VanillaFoliage();
        }

        public static void BlueAqueduct(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(57, 63, 76, 73);
            fog.fogColorMid.value = new Color32(62, 71, 83, 179);
            fog.fogColorEnd.value = new Color32(68, 77, 90, 255);
            fog.skyboxStrength.value = 0.055f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(68, 74, 86, 63);
            CaveFog.fogColorMid.value = new Color32(73, 83, 96, 164);
            CaveFog.fogColorEnd.value = new Color32(80, 89, 103, 255);
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(166, 221, 253, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.1f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            AddRain(RainType.Rainstorm);
            LightChanges("rain");
            VanillaFoliage();
        }

        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.4f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            VanillaFoliage();
        }

        public static void NightAqueduct(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(28, 36, 56, 125);
            fog.fogColorMid.value = new Color32(28, 35, 47, 179);
            fog.fogColorEnd.value = new Color32(29, 33, 45, 255);
            fog.skyboxStrength.value = 0.055f;
            fog.fogOne.value = 0.082f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(37, 46, 67, 115);
            CaveFog.fogColorMid.value = new Color32(37, 45, 57, 167);
            CaveFog.fogColorEnd.value = new Color32(38, 42, 55, 255);
            cgrade.colorFilter.value = new Color32(140, 164, 221, 255);
            cgrade.colorFilter.overrideState = true;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(140, 157, 176, 255);
            sunLight.intensity = 0.45f;
            sunLight.shadowStrength = 0.1f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            AddRain(RainType.Monsoon);
            LightChanges("night");
            VanillaFoliage();
        }

        private static Color chain;

        public static void SunderedAqueduct(RampFog fog, ColorGrading cgrade)
        {
            try { ApplySunderedMaterials(); } catch { SwapVariants.AesLog.LogError("Sundered Aqueduct: Failed to change materials, trying again..."); } finally { ApplySunderedMaterials(); }
            fog.fogColorStart.value = new Color32(85, 49, 99, 35);
            fog.fogColorMid.value = new Color32(99, 58, 135, 75);
            fog.fogColorEnd.value = new Color32(112, 66, 117, 255);
            fog.skyboxStrength.value = 0.055f;
            fog.fogOne.value = 0.082f;
            AddRain(RainType.Drizzle);
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.SetActive(false);
            newSun.intensity = 1.6f;
            newSun.color = new Color32(134, 193, 216, 255);
            var waterfall = GameObject.Find("HOLDER: GameplaySpace").transform.GetChild(2);
            waterfall.gameObject.SetActive(true);
            waterfall.GetChild(0).gameObject.SetActive(false);
            waterfall.GetChild(1).gameObject.SetActive(false);
            waterfall.GetChild(2).gameObject.SetActive(false);
            waterfall.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GLUndergroundPPVolume").SetActive(false);
            var caveLight = GameObject.Find("AmbientLight").GetComponent<Light>();
            caveLight.color = new Color32(150, 29, 119, 255);
            cgrade.saturation.value = -2f;
            SunderedFoliage();
        }

        public static void SunderedFoliage()
        {
            var lineList = Object.FindObjectsOfType(typeof(LineRenderer)) as LineRenderer[];
            foreach (LineRenderer lr in lineList)
            {
                var lineBase = lr.gameObject;
                if (lineBase != null)
                {
                    lr.sharedMaterial.color = new Color32(115, 57, 75, 255);
                }
            }
        }

        public static void VanillaFoliage()
        {
            var lineList = Object.FindObjectsOfType(typeof(LineRenderer)) as LineRenderer[];
            foreach (LineRenderer lr in lineList)
            {
                var lineBase = lr.gameObject;
                if (lineBase != null)
                {
                    lr.sharedMaterial.color = new Color32(141, 42, 42, 255);
                }
            }
        }

        public static void ApplySunderedMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain2.mat").WaitForCompletion());
            terrainMat.color = new Color32(255, 156, 206, 184);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain.mat").WaitForCompletion());
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJSandstone.mat").WaitForCompletion());
            detailMat.color = new Color32(221, 77, 102, 231);
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidMetalTrimGrassy.mat").WaitForCompletion());
            detailMat2.color = new Color32(130, 61, 74, 150);
            var detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion();
            if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3)
            {
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("SandDune"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat2;
                            }
                        }
                        if (meshBase.name.Contains("SandstonePillar") || meshBase.name.Contains("Dam") || meshBase.name.Contains("AqueductFullLong") || meshBase.name.Contains("AqueductPartial"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if ((meshBase.name.Contains("Bridge") && !meshBase.name.Contains("Decal") || meshBase.name.Contains("RuinedRing") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Eel")))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("FlagPoleMesh") || meshBase.name.Contains("RuinTile"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
                            }
                        }
                        if (meshBase.name.Contains("AqueductCap"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try
                                    {
                                        var sharedMaterials = mr.sharedMaterials;
                                        for (int i = 0; i < sharedMaterials.Length; i++)
                                        {
                                            sharedMaterials[i] = detailMat2;
                                        }
                                        mr.sharedMaterials = sharedMaterials;
                                    }
                                    catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    var sharedMaterials2 = mr.sharedMaterials;
                                    for (int i = 0; i < sharedMaterials2.Length; i++)
                                    {
                                        sharedMaterials2[i] = detailMat2;
                                    }
                                    mr.sharedMaterials = sharedMaterials2;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}