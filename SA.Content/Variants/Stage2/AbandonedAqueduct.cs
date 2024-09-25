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

        public static void Dawn(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 126);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 200);
            fog.skyboxStrength.value = 0.02f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1.1f;
            fog.fogPower.value = 0.8f;

            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            GameObject.Find("HOLDER: Warning Flags").SetActive(false);
            // base1.Find("Warning Signs").gameObject.SetActive(true);
            // NRE above
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.SetActive(false);
            newSun.intensity = 3f;
            newSun.shadowStrength = 0.5f;
            newSun.color = new Color32(102, 102, 166, 255);
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(67, 35, 76, 65);
            CaveFog.fogColorMid.value = new Color32(41, 17, 51, 125);
            CaveFog.fogColorEnd.value = new Color32(84, 31, 20, 200);
            LightChanges("dark");
            VanillaFoliage();
            AddSand(SandType.Light);
        }

        public static void Sunrise(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(57, 63, 76, 73);
            fog.fogColorMid.value = new Color32(62, 71, 83, 129);
            fog.fogColorEnd.value = new Color32(68, 77, 90, 255);
            fog.skyboxStrength.value = 0.055f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(68, 74, 86, 63);
            CaveFog.fogColorMid.value = new Color32(73, 83, 96, 100);
            CaveFog.fogColorEnd.value = new Color32(80, 89, 103, 200);
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sunTransform).GetComponent<Light>();
            newSun.color = new Color32(125, 125, 255, 255);
            // 0.226 0.2148 0.6638 1
            newSun.intensity = 2f;
            newSun.shadowStrength = 0.25f;
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
            AddSand(SandType.Light);
        }

        public static void Night(RampFog fog, ColorGrading cgrade)
        {
            Skybox.NightSky();
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(37, 46, 67, 75);
            CaveFog.fogColorMid.value = new Color32(37, 45, 57, 127);
            CaveFog.fogColorEnd.value = new Color32(38, 42, 55, 200);
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            GameObject.Find("Weather, Goolake").SetActive(false);

            AddRain(RainType.Monsoon);
            LightChanges("night");
            VanillaFoliage();
            AddSand(SandType.Gigachad);
        }

        private static Color chain;

        public static void Sundered(RampFog fog, ColorGrading cgrade)
        {
            Skybox.VoidSky();
            AddRain(RainType.Drizzle);
            var weather = GameObject.Find("Weather, Goolake");
            weather.SetActive(false);
            var waterfall = GameObject.Find("HOLDER: GameplaySpace").transform.GetChild(2);
            waterfall.gameObject.SetActive(true);
            waterfall.GetChild(0).gameObject.SetActive(false);
            // NRE above
            waterfall.GetChild(1).gameObject.SetActive(false);
            waterfall.GetChild(2).gameObject.SetActive(false);
            waterfall.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GLUndergroundPPVolume").SetActive(false);
            var caveLight = GameObject.Find("AmbientLight").GetComponent<Light>();
            caveLight.color = new Color32(150, 29, 119, 255);
            //cgrade.saturation.value = -2f;
            SunderedFoliage();
            SunderedMaterials();
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

        public static void SunderedMaterials()
        {
            var terrainMat = Main.aqueductSunderedTerrainMat;
            var terrainMat2 = Main.aqueductSunderedTerrainMat2;
            var detailMat = Main.aqueductSunderedDetailMat;
            var detailMat2 = Main.aqueductSunderedDetailMat2;
            var detailMat3 = Main.aqueductSunderedDetailMat3;

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
                                    catch (Exception e) { SwapVariants.SALogger.LogWarning(e.Message + "\n" + e.StackTrace); };
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