using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage3
{
    internal class RallypointDelta
    {
        public static void OceanWall(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(47, 52, 62, 50);
            fog.fogColorMid.value = new Color32(72, 80, 98, 165);
            fog.fogColorEnd.value = new Color32(90, 101, 119, 255);
            fog.skyboxStrength.value = 0.15f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.4f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            sunLight.color = new Color32(177, 205, 232, 255);
            sunLight.intensity = 0.5f;
            GameObject.Find("HOLDER: Skybox").transform.Find("Water").localPosition = new Vector3(-1260, -66, 0);
            sunLight.color = new Color32(155, 174, 200, 255);
            sunLight.intensity = 1.3f;
            sunLight.name = "Directional Light (SUN)";
            AddRain(RainType.Monsoon);
            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
        }

        public static void NightWall(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyNightMaterials(); } catch { SwapVariants.AesLog.LogError("Night Delta: Failed to change materials, trying again..."); } finally { ApplyNightMaterials(); }
            fog.fogColorStart.value = new Color32(33, 33, 56, 76);
            fog.fogColorMid.value = new Color32(38, 38, 55, 165);
            fog.fogColorEnd.value = new Color32(25, 24, 46, 255);
            fog.skyboxStrength.value = 0.7f;
            cgrade.colorFilter.value = new Color32(130, 123, 255, 255);
            cgrade.colorFilter.overrideState = true;
            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            sun.SetActive(false);
            sunLight.color = new Color32(167, 165, 172, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.6f;
            sunLight.transform.eulerAngles = new Vector3(70, 250, 5);
            sunLight.name = "Directional Light (SUN)";
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null && !lightBase.name.Contains("Light (SUN)"))
                {
                    light.type = LightType.Point;
                    light.shape = LightShape.Cone;
                    light.color = new Color32(233, 233, 190, 255);
                    light.intensity = 0.25f;
                    light.range = 65f;
                    if (lightBase.GetComponent<FlickerLight>() != null)
                    {
                        lightBase.GetComponent<FlickerLight>().enabled = false;
                    }
                    if (lightBase.GetComponent<LightIntensityCurve>() != null)
                    {
                        lightBase.GetComponent<LightIntensityCurve>().enabled = false;
                    }
                }
            }
        }

        public static void GreenWall(RampFog fog)
        {
            try { ApplyGreenMaterials(); } catch { SwapVariants.AesLog.LogError("Green Delta: Failed to change materials, trying again..."); } finally { ApplyGreenMaterials(); }
            fog.fogColorStart.value = new Color32(42, 93, 68, 30);
            fog.fogColorMid.value = new Color32(49, 127, 79, 95);
            fog.fogColorEnd.value = new Color32(47, 153, 105, 255);
            fog.skyboxStrength.value = 0.35f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sunLight.gameObject.name = "Directional Light (SUN)";
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            sunLight.color = new Color32(177, 205, 232, 255);
            sunLight.intensity = 0.5f;
            fog.fogOne.value = 0.7f;
        }

        public static void TitanicWall(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyTitanicMaterials(); } catch { SwapVariants.AesLog.LogError("Titanic Delta: Failed to change materials, trying again..."); } finally { ApplyTitanicMaterials(); }
            fog.fogColorStart.value = new Color32(116, 153, 173, 12);
            fog.fogColorMid.value = new Color32(88, 130, 153, 45);
            fog.fogColorEnd.value = new Color32(79, 140, 173, 255);
            fog.skyboxStrength.value = 0f;
            // cgrade.colorFilter.value = new Color32(178, 255, 230, 255);
            // cgrade.colorFilter.overrideState = true;
            var sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            sunLight.name = "Directional Light (SUN)";
            sunLight.color = new Color32(255, 212, 153, 255);
            sunLight.intensity = 2f;
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null && !lightBase.name.Contains("Light (SUN)"))
                {
                    light.color = new Color32(255, 185, 0, 255);
                    light.intensity = 0.08f;
                    light.range = 4f;
                }
            }
            GameObject.Find("CAMERA PARTICLES: SnowParticles").SetActive(false);
            GameObject.Find("STATIC PARTICLES: Cave Entrance System").SetActive(false);
            GameObject.Find("HOLDER: ShippingCenter").transform.GetChild(3).gameObject.SetActive(false);
        }

        public static void ApplyTitanicMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(0, 2, 185, 245);
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion());
            detailMat.color = new Color32(0, 52, 146, 78);
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuinsProjectedHuge.mat").WaitForCompletion());
            detailMat2.color = new Color32(255, 185, 0, 255);
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion();
            if (terrainMat && detailMat && detailMat2 && water)
            {
                GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Snow"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Glacier") || meshBase.name.Contains("Stalagmite") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("CavePillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("GroundMesh") || meshBase.name.Contains("GroundStairs") || meshBase.name.Contains("VerticalPillar") || meshBase.name.Contains("Human") || meshBase.name.Contains("Barrier"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if (meshBase.name.Contains("HumanChainLink"))
                        {
                            meshBase.SetActive(false);
                        }
                        // too early to change shrine/enemy skins/detail
                    }
                }
            }
        }

        public static void ApplyGreenMaterials()
        {
            var waterMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSPWaterYellow.mat").WaitForCompletion();
            if (waterMat)
            {
                GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = waterMat;
            }
        }

        public static void ApplyNightMaterials()
        {
            var terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion();
            var waterMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion();
            var iceMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/snowyforest/matSFIce.mat").WaitForCompletion());
            iceMat.color = new Color32(242, 237, 254, 216);
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            var water = GameObject.Find("HOLDER: Skybox").transform.GetChild(0);
            var ice = Object.Instantiate(water);
            ice.transform.position = new Vector3(-1260, -115, 0);
            if (terrainMat && waterMat && iceMat)
            {
                ice.GetComponent<MeshRenderer>().sharedMaterial = iceMat;
                water.GetComponent<MeshRenderer>().sharedMaterial = waterMat;
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Snow"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Stalagmite") && meshBase.GetComponent<Light>() == null)
                        {
                            meshBase.AddComponent<Light>();
                        }
                    }
                }
            }
        }
    }
}