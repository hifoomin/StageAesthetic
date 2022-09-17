using IL.RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class SkyMeadow
    {
        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(239, 231, 211, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 1f;
            VanillaFoliage();
        }

        public static void NightMeadow(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(38, 59, 69, 33);
            fog.fogColorMid.value = new Color32(12, 15, 59, 131);
            fog.fogColorEnd.value = new Color32(18, 3, 45, 255);
            fog.fogZero.value = -0.08f;
            fog.skyboxStrength.value = 0.25f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(126, 138, 227, 255);
            sunLight.intensity = 3f;
            sunLight.shadowStrength = 0.4f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            VanillaFoliage();
        }

        public static void StormyMeadow(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(76, 86, 98, 0);
            fog.fogColorMid.value = new Color32(67, 62, 88, 159);
            fog.fogColorEnd.value = new Color32(75, 73, 96, 255);
            fog.fogZero.value = -0.02f;
            fog.skyboxStrength.value = 0.1f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(142, 156, 202, 255);
            sunLight.intensity = 0.6f;
            sunLight.shadowStrength = 0.3f;
            if (Config.WeatherEffects.Value)
            {
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
                rain.transform.eulerAngles = new Vector3(300, 0, 0);
                rain.transform.localScale = new Vector3(12, 12, 1);
                Object.Instantiate(rain);
                GameObject wind = GameObject.Find("WindZone");
                wind.transform.eulerAngles = new Vector3(30, 20, 0);
                var windZone = wind.GetComponent<WindZone>();
                windZone.windMain = 1;
                windZone.windTurbulence = 1;
                windZone.windPulseFrequency = 0.5f;
                windZone.windPulseMagnitude = 5f;
                windZone.mode = WindZoneMode.Directional;
                windZone.radius = 100;
            }
            GameObject.Find("SMSkyboxPrefab").transform.Find("SmallStars").gameObject.SetActive(false);
            VanillaFoliage();
        }

        public static void EpicMeadow(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(56, 8, 6, 91);
            fog.fogColorMid.value = new Color32(32, 4, 56, 198);
            fog.fogColorEnd.value = new Color(0.059f, 0f, 0f, 1);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.27f;
            fog.skyboxStrength.value = 0.05f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 0, 0, 255);
            sunLight.intensity = 2.5f;
            sunLight.shadowStrength = 0.6f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            cgrade.colorFilter.value = new Color(1, 0.632f, 0.471f);
            cgrade.contrast.value = 18f;
            cgrade.brightness.value = 14f;
            GameObject.Find("Cloud Floor").SetActive(false);
            VanillaFoliage();
        }

        public static void TitanicMeadow(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(125, 141, 160, 0);
            fog.fogColorMid.value = new Color32(119, 144, 175, 60);
            fog.fogColorEnd.value = new Color32(93, 144, 213, 110);
            fog.fogZero.value = -0.02f;
            fog.skyboxStrength.value = 0.1f;
            var terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion();
            var terrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrainBlender.mat").WaitForCompletion();
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion();
            var detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderHeavyMoss.mat").WaitForCompletion();
            var detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardProps.mat").WaitForCompletion();
            var detailMat4 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat").WaitForCompletion();
            var detailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/Junk/AncientWisp/matAncientWillowispSpiral.mat").WaitForCompletion();
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                var meshParent = meshBase.transform.parent;
                if (meshBase != null)
                {
                    if (meshParent != null)
                    {
                        if ((meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain")) || (meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if ((meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks")) || (meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13")) || (meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground")) || (meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if ((meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks")) || (meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if ((meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain")) || (meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = terrainMat2;
                            }
                        }
                        if ((meshBase.name.Contains("Base") && meshParent.name.Contains("PowerCoil")) || (meshBase.name.Contains("InteractableMesh") && meshParent.name.Contains("PortalDialerButton")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat4;
                            }
                        }
                        if (meshBase.name.Contains("Coil") && meshParent.name.Contains("PowerCoil"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat5;
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMPebble") || meshBase.name.Contains("mdlGeyser"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat;
                        }
                    }
                    if (meshBase.name.Contains("SMSpikeBridge"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat2;
                        }
                    }

                    if (meshBase.name.Contains("PowerLine") || meshBase.name.Contains("MegaTeleporter") || meshBase.name.Contains("BbRuinGateDoor") || meshBase.name.Contains("BbRuinArch"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat3;
                        }
                    }
                    if (meshBase.name.Contains("HumanCrate") || meshBase.name.Contains("BbRuinPillar"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat4;
                        }
                    }
                }
            }

            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            GameObject.Find("HOLDER: Mauling Rocks").SetActive(false);
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0);
            btp.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            btp.GetChild(3).gameObject.SetActive(false);
            btp.GetChild(4).gameObject.SetActive(false);
            btp.GetChild(5).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(1).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(2).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(1).GetChild(9).gameObject.SetActive(false);
            GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            GameObject.Find("Stairway").GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = terrainMat; } catch { }
            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);

            var r = GameObject.Find("HOLDER: Randomization").transform;

            // can i even do a for loop here? seems really complicated lol

            // Plateau Tall

            r.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            // Plateau 6

            r.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(1).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(1).GetChild(1).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            // Plateau 9

            r.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(2).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            // Plateau 11

            r.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;

            // Plateau 13

            r.GetChild(4).GetChild(1).GetChild(3).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;

            // Plateau 15

            r.GetChild(5).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(5).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(5).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(1).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(2).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            TitanicFoliage();
        }

        public static void SandyMeadow(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(125, 141, 160, 0);
            fog.fogColorMid.value = new Color32(183, 139, 62, 60);
            fog.fogColorEnd.value = new Color32(196, 152, 70, 110);
            fog.fogZero.value = -0.07f;
            fog.skyboxStrength.value = 0.05f;
            // var detail3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrim.mat").WaitForCompletion();
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(230, 223, 174, 219);
            var terrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat").WaitForCompletion();
            var detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            var detailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetConstructionWild.mat").WaitForCompletion());
            detailMat3.color = new Color32(248, 219, 175, 255);
            var detailMat4 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetSwampyRuinsProjectedLight.mat").WaitForCompletion());
            detailMat4.color = new Color32(217, 191, 168, 255);
            var detailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/MajorAndMinorConstruct/matMajorConstructDefenseMatrixEdges.mat").WaitForCompletion();
            var detailMat6 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetClayPots.mat").WaitForCompletion();
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matClayGooDebuff.mat").WaitForCompletion();
            // really wanted to use matgswater here but turns out there were shader bugs
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                var meshParent = meshBase.transform.parent;
                if (meshBase != null)
                {
                    if (meshParent != null)
                    {
                        if ((meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain")) || (meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if ((meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks")) || (meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13")) || (meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground")) || (meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if ((meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks")) || (meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if ((meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain")) || (meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = terrainMat2;
                            }
                        }
                        if ((meshBase.name.Contains("Base") && meshParent.name.Contains("PowerCoil")) || (meshBase.name.Contains("InteractableMesh") && meshParent.name.Contains("PortalDialerButton")))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat4;
                            }
                        }
                        if (meshBase.name.Contains("Coil") && meshParent.name.Contains("PowerCoil"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat5;
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMPebble") || meshBase.name.Contains("mdlGeyser"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat;
                        }
                    }
                    if (meshBase.name.Contains("SMSpikeBridge"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat2;
                        }
                    }
                    if (meshBase.name.Contains("PowerLine") || meshBase.name.Contains("MegaTeleporter") || meshBase.name.Contains("BbRuinGateDoor"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat3;
                        }
                    }
                    if (meshBase.name.Contains("HumanCrate"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat4;
                        }
                    }
                    if (meshBase.name.Contains("BbRuinPillar"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = detailMat6;
                        }
                    }
                    if (meshBase.name.Contains("BbRuinArch"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = terrainMat;
                        }
                    }
                }
            }
            var c = GameObject.Find("Cloud Floor").transform;
            GameObject.Find("Hard Floor").SetActive(false);
            c.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
            c.GetChild(0).localPosition = new Vector3(0, 30, 0);
            c.GetChild(0).localScale = new Vector3(600, 600, 600);
            c.GetChild(1).gameObject.SetActive(false);
            c.GetChild(2).gameObject.SetActive(false);
            c.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("HOLDER: Mauling Rocks").SetActive(false);
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0);
            btp.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            btp.GetChild(3).gameObject.SetActive(false);
            btp.GetChild(4).gameObject.SetActive(false);
            btp.GetChild(5).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(1).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(2).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(1).GetChild(9).gameObject.SetActive(false);
            GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            GameObject.Find("Stairway").GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = terrainMat; } catch { }
            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);

            var r = GameObject.Find("HOLDER: Randomization").transform;

            // can i even do a for loop here? seems really complicated lol

            // Plateau Tall

            r.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            // Plateau 6

            r.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(1).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(1).GetChild(1).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            // Plateau 9

            r.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(2).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            // Plateau 11

            r.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;

            // Plateau 13

            r.GetChild(4).GetChild(1).GetChild(3).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;

            // Plateau 15

            r.GetChild(5).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(5).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
            r.GetChild(5).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(1).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
            r.GetChild(5).GetChild(2).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat2;

            SandyFoliage();
        }

        public static void VanillaFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Edge Clouds"))
                    {
                        meshBase.SetActive(false);
                    }
                    if (meshBase.name.Contains("spmSMGrass"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(236, 161, 182, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(236, 161, 182, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMVineBody"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(144, 158, 70, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmSMHangingVinesCluster_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(130, 150, 171, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SGMushroom"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(255, 255, 255, 255);
                        }
                    }
                }
            }
        }

        public static void TitanicFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("spmSMGrass"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(198, 255, 95, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(213, 246, 99, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMVineBody"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(144, 158, 70, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmSMHangingVinesCluster_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(144, 158, 70, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(133, 191, 127, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(192, 255, 0, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SGMushroom"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(162, 176, 46, 255);
                        }
                    }
                }
            }
        }

        public static void SandyFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("spmSMGrass"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(255, 186, 95, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(216, 171, 88, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMVineBody"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(213, 158, 70, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmSMHangingVinesCluster_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(213, 158, 70, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(73, 58, 42, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(84, 68, 49, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SGMushroom"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(255, 90, 0, 255);
                        }
                    }
                }
            }
        }
    }
}