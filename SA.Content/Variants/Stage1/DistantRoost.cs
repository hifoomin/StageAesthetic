using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants
{
    internal class DistantRoost
    {
        public static void VanillaBeach(GameObject rain, string scenename)
        {
            VanillaFoliage();
            if (Config.WeatherEffects.Value && scenename == "blackbeach2") Object.Instantiate(rain, Vector3.zero, Quaternion.identity);
        }

        public static void LightBeach(RampFog fog, string scenename, ColorGrading cgrade)
        {
            // Much more sun than vanilla roost - almost enough to give it a cel-shaded look on lower texture settings. The whiter lighting allows the green/brown elements to stand out more.
            fog.fogColorStart.value = new Color32(107, 125, 123, 25);
            fog.fogColorMid.value = new Color32(129, 154, 152, 69);
            fog.fogColorEnd.value = new Color32(156, 194, 189, 114);
            fog.skyboxStrength.value = 0.13f;
            fog.fogZero.value = 0;
            fog.fogOne.value = 0.142f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.8f;
            sunLight.shadowStrength = 1;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            if (scenename == "blackbeach")
            {
                // There's unused fog assets here too, enabling those
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
                // Removing rain
                GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
            }
            VanillaFoliage();
        }

        public static void DarkBeach(RampFog fog, string scenename, ColorGrading cgrade)
        {
            // Dark and purple, not much else to say here really
            fog.fogColorStart.value = new Color32(24, 20, 43, 32);
            fog.fogColorMid.value = new Color32(33, 25, 49, 130);
            fog.fogColorEnd.value = new Color32(43, 35, 62, 255);
            fog.skyboxStrength.value = 0.03f;
            fog.fogPower.value = 0.6f;
            cgrade.colorFilter.value = new Color32(197, 182, 249, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(106, 69, 160, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.45f;
            if (scenename == "blackbeach")
            {
                // Enabling some unused fog
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 15;
                            light.range = 50;
                        }
                    }
                }
            }
            VanillaFoliage();
        }

        public static void VoidBeach(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyVoidMaterials(); } catch { SwapVariants.AesLog.LogError("Void Roost: Failed to change materials, trying again..."); } finally { ApplyVoidMaterials(); }
            fog.fogColorStart.value = new Color32(40, 19, 40, 32);
            fog.fogColorMid.value = new Color32(48, 25, 48, 255);
            fog.fogColorEnd.value = new Color32(61, 34, 58, 255);
            fog.skyboxStrength.value = 0.03f;
            fog.fogPower.value = 0.35f;
            fog.fogIntensity.value = 0.99f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.25f;
            cgrade.colorFilter.value = new Color32(213, 183, 247, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(152, 69, 158, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.45f;
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            var s = GameObject.Find("SKYBOX").transform;
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 9;
                            light.range = 70;
                            light.color = new Color32(109, 58, 119, 140);
                        }
                    }
                }
            }
            s.GetChild(19).GetChild(0).localPosition = new Vector3(0, 0, -10);

            GameObject.Find("HOLDER: Grass").SetActive(false);
            GameObject.Find("FOLIAGE").SetActive(false);
            s.GetChild(6).gameObject.SetActive(false);
            s.GetChild(11).gameObject.SetActive(false);
            VanillaFoliage();
        }

        public static void FoggyBeach(RampFog fog, string scenename, GameObject rain)
        {
            // Stormy weather. Might be a bit too murky
            fog.fogColorStart.value = new Color32(31, 46, 63, 50);
            fog.fogColorMid.value = new Color(0.205f, 0.269f, 0.288f, 0.76f);
            fog.fogColorEnd.value = new Color32(71, 82, 88, 255);
            fog.skyboxStrength.value = 0.02f;
            fog.fogPower.value = 0.35f;
            fog.fogIntensity.value = 0.99f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1.7f;
            sunLight.shadowStrength = 0.6f;
            // Strong rain
            if (Config.WeatherEffects.Value)
            {
                if (scenename == "blackbeach") GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 3000,
                    constantMax = 3000,
                    constantMin = 600,
                    curve = epic2.curve,
                    curveMax = epic2.curveMax,
                    curveMin = epic2.curveMax,
                    curveMultiplier = epic2.curveMultiplier,
                    mode = epic2.mode
                };
                var epic3 = rainParticle.colorOverLifetime;
                epic3.enabled = false;
                var epic4 = rainParticle.main;
                epic4.scalingMode = ParticleSystemScalingMode.Shape;
                rain.transform.eulerAngles = new Vector3(75, 20, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
                GameObject wind = GameObject.Find("WindZone");
                wind.transform.eulerAngles = new Vector3(30, 20, 0);
                var windZone = wind.GetComponent<WindZone>();
                windZone.windMain = 1;
                windZone.windTurbulence = 1;
                windZone.windPulseFrequency = 0.5f;
                windZone.windPulseMagnitude = 0.5f;
                windZone.mode = WindZoneMode.Directional;
                windZone.radius = 100;
            }
            // Enabling some unused fog
            if (scenename == "blackbeach") GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 10;
                            light.range = 30;
                        }
                    }
                }
            }
            VanillaFoliage();
        }

        public static void GoldBeach(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyGoldMaterials(); } catch { SwapVariants.AesLog.LogError("Abyssal Roost: Failed to change materials, trying again..."); } finally { ApplyGoldMaterials(); }
            fog.fogColorStart.value = new Color32(84, 27, 27, 15);
            fog.fogColorMid.value = new Color32(91, 26, 26, 90);
            fog.fogColorEnd.value = new Color32(107, 23, 23, 225);
            fog.skyboxStrength.value = 0f;
            fog.fogPower.value = 0.35f;
            fog.fogIntensity.value = 0.99f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.25f;
            cgrade.colorFilter.value = new Color32(213, 183, 128, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(154, 46, 49, 255);
            sunLight.intensity = 1.6f;
            sunLight.shadowStrength = 1f;
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            var light = meshBase.AddComponent<Light>();
                            light.color = new Color32(249, 212, 96, 255);
                            light.intensity = 10f;
                            light.range = 25f;
                        }
                        if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("RuinArch"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                var light = meshBase.AddComponent<Light>();
                                light.color = new Color32(181, 66, 34, 225);
                                light.intensity = 7.5f;
                                light.range = 15f;
                            }
                        }
                    }
                }
            }
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.color = new Color32(249, 212, 96, 225);
                            light.intensity = 16f;
                            light.range = 35f;
                        }
                    }
                }
            }
            AbyssalFoliage();
        }

        public static void VanillaFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("bbSimpleGrassPrefab"))
                    {
                        mr.sharedMaterial.color = new Color32(11, 58, 28, 255);
                    }
                    if (meshBase.name.Contains("spmBbFern2"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 255, 255, 255);
                    }
                    if (meshBase.name.Contains("spmBbFern3"))
                    {
                        mr.sharedMaterial.color = new Color32(229, 229, 229, 255);
                    }
                    if (meshBase.name.Contains("spmBush"))
                    {
                        var color = new Color32(255, 255, 255, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbDryBush"))
                    {
                        var color = new Color32(125, 125, 128, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Ivy"))
                    {
                        var color = new Color32(40, 47, 30, 146);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Vine"))
                    {
                        mr.sharedMaterial.color = new Color32(44, 49, 27, 255);
                    }

                    if (meshBase.name.Contains("spmBbConif_"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        var color = new Color32(125, 125, 128, 255);
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbConifYoung"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        var color = new Color32(125, 125, 128, 255);
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                }
            }
        }

        public static void AbyssalFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("bbSimpleGrassPrefab"))
                    {
                        mr.sharedMaterial.color = new Color32(95, 42, 45, 255);
                        meshBase.transform.localScale = new Vector3(8f, 5.7548744f, 8f);
                    }
                    if (meshBase.name.Contains("spmBbFern"))
                    {
                        meshBase.transform.localScale = new Vector3(2f, 2f, 2f);
                        mr.sharedMaterial.color = new Color32(255, 0, 6, 166);
                        meshBase.transform.localScale = new Vector3(3f, 3f, 3f);
                    }
                    if (meshBase.name.Contains("spmBush"))
                    {
                        var color = new Color32(255, 255, 255, 255);
                        mr.sharedMaterial.color = new Color32(255, 255, 255, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbDryBush"))
                    {
                        var color = new Color32(125, 125, 128, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Ivy"))
                    {
                        var sharedMaterials = mr.sharedMaterials;
                        var color1 = new Color32(58, 58, 58, 146);
                        var color2 = new Color32(119, 21, 21, 133);
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color1;
                            if (i == 1)
                            {
                                sharedMaterials[i].color = color2;
                            }
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Vine"))
                    {
                        mr.sharedMaterial.color = new Color32(79, 21, 20, 255);
                    }

                    if (meshBase.name.Contains("spmBbConif_"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = new Color32(164, 35, 47, 255);
                        var color1 = new Color32(191, 60, 40, 255);
                        var color2 = new Color32(255, 175, 119, 255);
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color1;
                            if (i == 2)
                            {
                                sharedMaterials[i].color = color2;
                            }
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbConifYoung"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = new Color32(164, 35, 47, 255);
                        var color1 = new Color32(191, 60, 40, 255);
                        var color2 = new Color32(255, 175, 119, 255);
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color1;
                            if (i == 2)
                            {
                                sharedMaterials[i].color = color2;
                            }
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                }
            }
        }

        public static void ApplyVoidMaterials()
        {
            var s = GameObject.Find("SKYBOX").transform;
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(188, 162, 162, 255);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion());
            terrainMat2.color = new Color32(188, 162, 162, 255);
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainGem.mat").WaitForCompletion();
            var detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            var water = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            if (terrainMat && terrainMat2 && detailMat && detailMat2 && water)
            {
                GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = detailMat; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = detailMat;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = detailMat2; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = detailMat2;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = terrainMat2; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = terrainMat2;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("spmBbFern2"))
                        {
                            meshBase.SetActive(false);
                        }
                        if (meshBase.name.Contains("GlowyBall"))
                        {
                            mr.sharedMaterial.color = new Color32(109, 58, 119, 140);
                        }
                    }
                }
                water.color = new Color32(0, 14, 255, 255);
                s.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = water;
            }
        }

        public static void ApplyGoldMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            terrainMat.color = new Color32(0, 0, 0, 204);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());
            terrainMat2.color = new Color32(0, 0, 0, 135);
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion();
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuins.mat").WaitForCompletion());
            detailMat2.color = new Color32(181, 66, 34, 255);
            var water = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            if (terrainMat && terrainMat2 && detailMat && detailMat2 && water)
            {
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    var meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if (meshParent.name.Contains("Pillar") && meshParent.transform.Find("Foam") != null)
                            {
                                meshParent.transform.Find("Foam").gameObject.SetActive(false);
                            }
                            if (meshParent.name.Contains("terrain") && meshBase.name.Contains("Pillar"))
                            {
                                switch (mr.sharedMaterial)
                                {
                                    case null:
                                        try { mr.sharedMaterial = terrainMat; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                        break;

                                    default:
                                        mr.sharedMaterial = terrainMat;
                                        break;
                                }
                            }
                            if (meshParent.name.Equals("Foliage") && meshBase.name.Contains("bbSimpleGrassPrefab"))
                            {
                                meshBase.SetActive(false);
                            }
                        }
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Shelf"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = terrainMat; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = terrainMat;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = detailMat; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = detailMat;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("Pebble"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = detailMat; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = detailMat;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("RuinArch"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = detailMat2; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = detailMat2;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = terrainMat2; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = terrainMat2;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("Decal") || meshBase.name.Contains("spmBbFern2"))
                        {
                            meshBase.SetActive(false);
                        }
                        if (meshBase.name.Contains("GlowyBall"))
                        {
                            mr.sharedMaterial.color = new Color32(109, 58, 119, 140);
                        }
                    }
                }
                water.color = new Color32(107, 23, 23, 255);
                GameObject.Find("HOLDER: Water").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
            }
        }
    }
}