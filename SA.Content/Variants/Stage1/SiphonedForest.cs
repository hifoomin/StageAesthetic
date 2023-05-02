using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants
{
    internal class SiphonedForest
    {
        public static void ExtraSnowyForest(RampFog fog, ColorGrading cgrade)
        {
            // Much more sun than vanilla roost - almost enough to give it a cel-shaded look on lower texture settings. The whiter lighting allows the green/brown elements to stand out more.
            fog.fogColorStart.value = new Color32(140, 140, 215, 25);
            fog.fogColorMid.value = new Color32(185, 185, 235, 200);
            fog.fogColorEnd.value = new Color32(255, 255, 255, 255);
            // fog.fogZero.value = 0;
            // fog.fogOne.value = 0.142f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 1.3f;
            sunLight.shadowStrength = 0.7f;
            cgrade.colorFilter.value = new Color32(245, 245, 255, 30);
            cgrade.colorFilter.overrideState = true;

            var sp = GameObject.Find("CAMERA PARTICLES: SnowParticles");
            sp.transform.GetChild(0).GetComponent<MaintainRotation>().eulerAngles = new Vector3(45, 90, 0);
            sp.transform.GetChild(1).GetComponent<MaintainRotation>().eulerAngles = new Vector3(45, 90, 0);
            var snow = sp.transform.GetChild(0).GetComponent<ParticleSystem>();
            var main = snow.main;
            main.flipRotation = 10f;
            var shape = snow.shape;
            // main.duration = 2f;
            var em = snow.emission;
            em.rateOverTime = 7000;
            em.burstCount = 0;
            main.startSpeed = 50f;
            main.maxParticles = 7000;
            main.startLifetime = 1f;
            shape.scale = new Vector3(200, 200, 120);
            var harderSnow = GameObject.Find("CAMERA PARTICLES: SnowParticles").transform.GetChild(1).GetComponent<ParticleSystem>();
            var hMain = harderSnow.main;
            hMain.flipRotation = 10f;
            var hShape = harderSnow.shape;
            // hMain.duration = 2f;
            var hEm = harderSnow.emission;
            hEm.rateOverTime = 14000;
            hEm.burstCount = 0;
            hMain.startSpeed = 100f;
            hMain.maxParticles = 14000;
            main.startLifetime = 1f;
            hShape.scale = new Vector3(200, 200, 120);
        }

        public static void NightForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(110, 110, 140, 45);
            fog.fogColorMid.value = new Color32(80, 80, 110, 85);
            fog.fogColorEnd.value = new Color32(20, 20, 35, 225);
            fog.skyboxStrength.value = 0.3f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            aurora.SetActive(false);
            var godrays = GameObject.Find("Godrays");
            godrays.SetActive(false);
            sunLight.color = new Color32(110, 110, 180, 255);
            sunLight.intensity = 2.5f;
            sunLight.shadowStrength = 0.5f;
            cgrade.colorFilter.value = new Color32(110, 110, 140, 25);
            cgrade.colorFilter.overrideState = true;
        }

        public static void CrimsonForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(180, 50, 50, 35);
            fog.fogColorMid.value = new Color32(50, 30, 30, 120);
            fog.fogColorEnd.value = new Color32(90, 30, 30, 225);
            fog.skyboxStrength.value = 0.08f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            var snow = GameObject.Find("CAMERA PARTICLES: SnowParticles");
            snow.SetActive(false);
            aurora.SetActive(false);
            sunLight.color = new Color32(180, 110, 110, 255);
            sunLight.intensity = 1.5f;
            sunLight.shadowStrength = 0.5f;
            cgrade.colorFilter.value = new Color32(255, 255, 255, 23);
            cgrade.colorFilter.overrideState = true;
        }

        public static void MorningForest(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(117, 154, 255, 7);
            fog.fogColorMid.value = new Color32(111, 196, 248, 45);
            fog.fogColorEnd.value = new Color32(117, 154, 255, 255);
            fog.skyboxStrength.value = 0.1f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            var snow = GameObject.Find("CAMERA PARTICLES: SnowParticles");
            snow.SetActive(false);
            aurora.SetActive(false);
            sunLight.color = new Color32(205, 158, 90, 255);
            sunLight.intensity = 6f;
            sunLight.shadowStrength = 0.88f;
            cgrade.colorFilter.value = new Color32(111, 196, 248, 17);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(40, 153.0076f, 50f);
        }

        public static void DesolateForest(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyRoostMaterials(); } catch { SwapVariants.AesLog.LogError("Abyssal Roost: Failed to change materials, trying again..."); } finally { ApplyRoostMaterials(); }
            fog.fogColorStart.value = new Color32(237, 117, 255, 7);
            fog.fogColorMid.value = new Color32(230, 111, 248, 45);
            fog.fogColorEnd.value = new Color32(136, 62, 174, 255);
            fog.skyboxStrength.value = 0.1f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            var snow = GameObject.Find("CAMERA PARTICLES: SnowParticles");
            snow.SetActive(false);
            aurora.SetActive(false);
            sunLight.color = new Color32(255, 185, 250, 255);
            sunLight.intensity = 12f;
            sunLight.shadowStrength = 0.55f;
            cgrade.colorFilter.value = new Color32(111, 196, 248, 17);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(40, 153.0076f, 50f);

            var foliage = GameObject.Find("HOLDER: Foliage").scene.GetRootGameObjects()[3];
            var iciles = foliage.transform.GetChild(5);
            iciles.gameObject.SetActive(false);
        }

        public static void ApplyRoostMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbTerrain.mat").WaitForCompletion());
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbTerrainLight.mat").WaitForCompletion());
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/blackbeach/matBbBoulder.mat").WaitForCompletion();
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Temple.mat").WaitForCompletion());
            detailMat2.color = new Color32(18, 79, 40, 255);
            var detailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/TreasureCacheVoid/matLockboxVoidRocks.mat").WaitForCompletion());
            detailMat3.color = new Color32(103, 172, 130, 255);
            if (terrainMat && terrainMat2 && detailMat && detailMat2)
            {
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain"))
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
                        if (meshBase.name == "meshSnowyForestGiantTreesTops")
                        {
                            meshBase.gameObject.SetActive(false);
                        }
                        if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock"))
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
                        if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("meshSnowyForestAqueduct"))
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
                        if (meshBase.name.Contains("SnowPile"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try { mr.sharedMaterial = detailMat3; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    mr.sharedMaterial = detailMat3;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}