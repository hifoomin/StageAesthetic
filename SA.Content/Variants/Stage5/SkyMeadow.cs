using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage5
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

        public static void Night(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(85, 67, 93, 0);
            fog.fogColorMid.value = new Color32(12, 15, 59, 131);
            fog.fogColorEnd.value = new Color32(18, 3, 45, 255);
            fog.fogZero.value = -0.08f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.6f;
            fog.fogOne.value = 0.37f;
            fog.skyboxStrength.value = 0.25f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(126, 138, 227, 255);
            sunLight.intensity = 3f;
            sunLight.shadowStrength = 0.5f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            VanillaFoliage();
        }

        public static void Overcast(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(98, 94, 76, 0);
            fog.fogColorMid.value = new Color32(66, 65, 93, 120);
            fog.fogColorEnd.value = new Color32(72, 79, 95, 160);
            fog.fogZero.value = -0.02f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.5f;
            fog.fogOne.value = 0.1f;
            fog.skyboxStrength.value = 0.1f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            var sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(202, 183, 142, 255);
            sunLight.intensity = 1.5f;
            sunLight.shadowStrength = 0.6f;
            AddRain(RainType.Typhoon);
            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
            GameObject.Find("SMSkyboxPrefab").transform.Find("SmallStars").gameObject.SetActive(false);
            VanillaFoliage();
        }

        public static void Abyssal(RampFog fog, ColorGrading cgrade)
        {
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(181, 178, 219, 255);
            fog.fogColorStart.value = new Color32(100, 70, 70, 0);
            fog.fogColorMid.value = new Color32(60, 50, 40, 120);
            fog.fogColorEnd.value = new Color32(120, 35, 46, 225);
            fog.skyboxStrength.value = 0.1f;

            AddRain(RainType.Typhoon, true);

            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var hardFloor = lightBase.GetChild(6);
            hardFloor.gameObject.SetActive(false);
            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).gameObject.SetActive(false);
            var sun = lightBase.GetChild(0).GetComponent<Light>();
            sun.color = Color.gray;
            sun.intensity = 1.6f;
            sun.shadowStrength = 0.75f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            var terrainMat = Main.skyMeadowAbyssalTerrainMat2;
            var terrainMat2 = Main.skyMeadowAbyssalTerrainMat2;
            var detailMat = Main.skyMeadowAbyssalDetailMat;
            var detailMat2 = Main.skyMeadowAbyssalDetailMat2;
            var detailMat3 = Main.skyMeadowAbyssalDetailMat3;
            var detailMat4 = Main.skyMeadowAbyssalDetailMat4;
            var detailMat5 = Main.skyMeadowAbyssalDetailMat5;
            var water = Main.skyMeadowAbyssalWaterMat;

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat3);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat4);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat5);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + water);

            var r = GameObject.Find("HOLDER: Randomization").transform;
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0);
            if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3 && detailMat4 && detailMat5 && water)
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
                            if (meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat;
                                }
                            }
                            if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13") || meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground") || meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent") || meshBase.name.Contains("BbRuinPillar") || (meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks")))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat;
                                }
                            }
                            if (meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat2;
                                }
                            }
                            if (meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat2;
                                }
                            }
                            if (meshBase.name.Contains("Base") && meshParent.name.Contains("PowerCoil") || meshBase.name.Contains("InteractableMesh") && meshParent.name.Contains("PortalDialerButton"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat4;
                                }
                            }
                            if (meshBase.name.Contains("Coil") && meshParent.name.Contains("PowerCoil"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat5;
                                }
                            }
                        }
                        if (meshBase.name.Contains("SMPebble") || meshBase.name.Contains("mdlGeyser"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("SMSpikeBridge"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }

                        if (meshBase.name.Contains("PowerLine") || meshBase.name.Contains("MegaTeleporter") || meshBase.name.Contains("BbRuinGateDoor") || meshBase.name.Contains("BbRuinArch"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
                            }
                        }
                        if (meshBase.name.Contains("HumanCrate") || meshBase.name.Contains("BbRuinPillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat4;
                            }
                        }
                    }
                }
                GameObject.Find("HOLDER: Terrain").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                btp.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
                GameObject.Find("Stairway").GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = terrainMat; } catch { }
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
                var floor = lightBase.GetChild(6);
                floor.localScale = new Vector3(4000f, 4000f, 4000f);
                floor.gameObject.GetComponent<MeshRenderer>().sharedMaterial = water;
            }

            btp.GetChild(3).gameObject.SetActive(false);
            btp.GetChild(4).gameObject.SetActive(false);
            btp.GetChild(5).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(1).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(2).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(1).GetChild(9).gameObject.SetActive(false);

            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);

            // can i even do a for loop here? seems really complicated lol

            AbyssalFoliage();
        }

        public static void Titanic(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(125, 141, 160, 0);
            fog.fogColorMid.value = new Color32(119, 144, 175, 60);
            fog.fogColorEnd.value = new Color32(94, 137, 195, 110);
            fog.fogZero.value = -0.02f;
            fog.skyboxStrength.value = 0.1f;

            var terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion();
            var terrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrainBlender.mat").WaitForCompletion();
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion();
            var detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderHeavyMoss.mat").WaitForCompletion();
            var detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardProps.mat").WaitForCompletion();
            var detailMat4 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat").WaitForCompletion();
            var detailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/Junk/AncientWisp/matAncientWillowispSpiral.mat").WaitForCompletion();

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat3);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat4);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat5);

            var r = GameObject.Find("HOLDER: Randomization").transform;
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0);
            if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3 && detailMat4 && detailMat5)
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
                            if (meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat;
                                }
                            }
                            if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13") || meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground") || meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat;
                                }
                            }
                            if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks") || meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat2;
                                }
                            }
                            if (meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat2;
                                }
                            }
                            if (meshBase.name.Contains("Base") && meshParent.name.Contains("PowerCoil") || meshBase.name.Contains("InteractableMesh") && meshParent.name.Contains("PortalDialerButton"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat4;
                                }
                            }
                            if (meshBase.name.Contains("Coil") && meshParent.name.Contains("PowerCoil"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat5;
                                }
                            }
                        }
                        if (meshBase.name.Contains("SMPebble") || meshBase.name.Contains("mdlGeyser"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("SMSpikeBridge"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }

                        if (meshBase.name.Contains("PowerLine") || meshBase.name.Contains("MegaTeleporter") || meshBase.name.Contains("BbRuinGateDoor") || meshBase.name.Contains("BbRuinArch"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
                            }
                        }
                        if (meshBase.name.Contains("HumanCrate") || meshBase.name.Contains("BbRuinPillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat4;
                            }
                        }
                    }
                }
                GameObject.Find("HOLDER: Terrain").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                btp.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
                GameObject.Find("Stairway").GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = terrainMat; } catch { }
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
            }

            btp.GetChild(3).gameObject.SetActive(false);
            btp.GetChild(4).gameObject.SetActive(false);
            btp.GetChild(5).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(1).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(2).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(1).GetChild(9).gameObject.SetActive(false);

            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);

            // can i even do a for loop here? seems really complicated lol

            TitanicFoliage();
        }

        public static void Abandoned(RampFog fog)
        {
            AddSand(SandType.Gigachad);
            // var detail3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrim.mat").WaitForCompletion();
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(230, 223, 174, 219);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion());
            terrainMat2.color = new Color32(255, 188, 160, 223);
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat").WaitForCompletion();
            var detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            var detailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetConstructionWild.mat").WaitForCompletion());
            detailMat3.color = new Color32(248, 219, 175, 255);
            var detailMat4 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetSwampyRuinsProjectedLight.mat").WaitForCompletion());
            detailMat4.color = new Color32(217, 191, 168, 255);
            var detailMat5 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/MajorAndMinorConstruct/matMajorConstructDefenseMatrixEdges.mat").WaitForCompletion();
            var detailMat6 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetClayPots.mat").WaitForCompletion();
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matClayGooDebuff.mat").WaitForCompletion();

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat3);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat4);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat5);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat6);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + water);

            var c = GameObject.Find("Cloud Floor").transform;
            var btp = GameObject.Find("PortalDialerEvent").transform.GetChild(0);

            var r = GameObject.Find("HOLDER: Randomization").transform;
            // really wanted to use matgswater here but turns out there were shader bugs
            if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3 && detailMat4 && detailMat5 && detailMat6 && water)
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
                            if (meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat;
                                }
                            }
                            if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks") || meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13") || meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground") || meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat;
                                }
                            }
                            if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks") || meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat2;
                                }
                            }
                            if (meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain") || meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat2;
                                }
                            }
                            if (meshBase.name.Contains("Base") && meshParent.name.Contains("PowerCoil") || meshBase.name.Contains("InteractableMesh") && meshParent.name.Contains("PortalDialerButton"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat4;
                                }
                            }
                            if (meshBase.name.Contains("Coil") && meshParent.name.Contains("PowerCoil"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat5;
                                }
                            }
                        }
                        if (meshBase.name.Contains("SMPebble") || meshBase.name.Contains("mdlGeyser"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("SMSpikeBridge"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if (meshBase.name.Contains("PowerLine") || meshBase.name.Contains("MegaTeleporter") || meshBase.name.Contains("BbRuinGateDoor"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
                            }
                        }
                        if (meshBase.name.Contains("HumanCrate"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat4;
                            }
                        }
                        if (meshBase.name.Contains("BbRuinPillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat6;
                            }
                        }
                        if (meshBase.name.Contains("BbRuinArch"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                    }
                }
                c.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
                btp.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
                GameObject.Find("Stairway").GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = terrainMat; } catch { }
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
            }
            GameObject.Find("Hard Floor").SetActive(false);
            c.GetChild(0).localPosition = new Vector3(0, 30, 0);
            c.GetChild(0).localScale = new Vector3(600, 600, 600);
            c.GetChild(1).gameObject.SetActive(false);
            c.GetChild(2).gameObject.SetActive(false);
            c.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).gameObject.SetActive(false);
            btp.GetChild(3).gameObject.SetActive(false);
            btp.GetChild(4).gameObject.SetActive(false);
            btp.GetChild(5).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(1).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(2).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(2).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(1).GetChild(9).gameObject.SetActive(false);

            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);

            // can i even do a for loop here? seems really complicated lol

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

        public static void AbyssalFoliage()
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
                            mr.sharedMaterial.color = new Color32(255, 165, 0, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(255, 125, 0, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMVineBody"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(166, 120, 37, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmSMHangingVinesCluster_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(181, 77, 45, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(191, 163, 127, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(255, 209, 0, 255);
                            }
                        }
                    }
                    if (meshBase.name.Contains("SGMushroom"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial.color = new Color32(255, 108, 0, 255);
                        }
                    }
                }
            }
        }
    }
}