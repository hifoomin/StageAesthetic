﻿using UnityEngine;
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

        public static void StormyMeadow(RampFog fog)
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
            lightBase.Find("CameraRelative").Find("Rain").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("SmallStars").gameObject.SetActive(false);
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

        public static void EpicMeadow(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color(0.176f, 0.019f, 0.013f, 0.3569f);
            fog.fogColorMid.value = new Color(0.151f, 0.061f, 0, 0.775f);
            fog.fogColorEnd.value = new Color(0.059f, 0f, 0f, 1);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.27f;
            fog.skyboxStrength.value = 0.05f;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 71, 0, 255);
            sunLight.intensity = 3;
            sunLight.shadowStrength = 0.55f;
            lightBase.Find("CameraRelative").Find("SmallStars").gameObject.SetActive(true);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("ShatteredMoonMesh").gameObject.SetActive(false);
            GameObject.Find("SMSkyboxPrefab").transform.Find("MoonHolder").Find("MoonMesh").gameObject.SetActive(true);
            cgrade.colorFilter.value = new Color(1, 0.632f, 0.471f);
            GameObject.Find("Cloud Floor").SetActive(false);
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

        public static void TitanicMeadow(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(125, 141, 160, 0);
            fog.fogColorMid.value = new Color32(119, 144, 175, 60);
            fog.fogColorEnd.value = new Color32(93, 144, 213, 110);
            fog.fogZero.value = -0.02f;
            fog.skyboxStrength.value = 0.1f;
            var tpmt = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion();
            var tpmtb = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrainBlender.mat").WaitForCompletion();
            var tpmrvp = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion();
            var tpmhm = Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderHeavyMoss.mat").WaitForCompletion();
            var samtgp = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardProps.mat").WaitForCompletion();
            var mtsmme = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetMetalMilitaryEmission.mat").WaitForCompletion();
            var maws = Addressables.LoadAssetAsync<Material>("RoR2/Junk/AncientWisp/matAncientWillowispSpiral.mat").WaitForCompletion();
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                var meshParent = meshBase.transform.parent;
                if (meshBase != null)
                {
                    if (meshParent != null)
                    {
                        if (meshBase.name.Contains("Plateau") && meshParent.name.Contains("skymeadow_terrain"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmt;
                            }
                        }
                        if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("HOLDER: Spinning Rocks"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmrvp;
                            }
                        }
                        if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("GROUP: Rocks"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmhm;
                            }
                        }
                        if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("FORMATION"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmt;
                            }
                        }
                        if (meshBase.name.Contains("Terrain") && meshParent.name.Contains("skymeadow_terrain"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmtb;
                            }
                        }
                        if (meshBase.name.Contains("SMRock") && meshParent.name.Contains("P13"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmrvp;
                            }
                        }
                        if (meshBase.name.Contains("SMPebble") && meshParent.name.Contains("Underground"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmrvp;
                            }
                        }
                        if (meshBase.name.Contains("SMSpikeBridge") && meshParent.name.Contains("Underground"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmhm;
                            }
                        }
                        if (meshBase.name.Contains("Plateau Under") && meshParent.name.Contains("Underground"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmtb;
                            }
                        }

                        if (meshBase.name.Contains("Boulder") && meshParent.name.Contains("PortalDialerEvent"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = tpmrvp;
                            }
                        }
                        if (meshBase.name.Contains("Base") && meshParent.name.Contains("PowerCoil"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = mtsmme;
                            }
                        }
                        if (meshBase.name.Contains("Coil") && meshParent.name.Contains("PowerCoil"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = maws;
                            }
                        }
                        if (meshBase.name.Contains("InteractableMesh") && meshParent.name.Contains("PortalDialerButton"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = mtsmme;
                            }
                        }
                    }
                    if (meshBase.name.Contains("SMPebble"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = tpmrvp;
                        }
                    }
                    if (meshBase.name.Contains("SMSpikeBridge"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = tpmhm;
                        }
                    }
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
                    if (meshBase.name.Contains("PowerLine"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = samtgp;
                        }
                    }
                    if (meshBase.name.Contains("MegaTeleporter"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = samtgp;
                        }
                    }
                    if (meshBase.name.Contains("HumanCrate"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = mtsmme;
                        }
                    }
                    if (meshBase.name.Contains("mdlGeyser"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = tpmrvp;
                        }
                    }
                    if (meshBase.name.Contains("BbRuinArch_LOD0"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            meshBase.SetActive(false);
                        }
                    }
                }
            }

            GameObject.Find("HOLDER: Terrain").transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = tpmt;
            GameObject.Find("HOLDER: Mauling Rocks").SetActive(false);
            var btp = GameObject.Find("PortalDialerEvent").transform;
            btp.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            btp.GetChild(0).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(2).GetChild(4).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(2).GetChild(5).gameObject.SetActive(false);
            btp.GetChild(0).GetChild(1).GetChild(9).gameObject.SetActive(false);
            GameObject.Find("ArtifactFormulaHolderMesh").GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            GameObject.Find("Stairway").GetComponent<MeshRenderer>().sharedMaterial = tpmt;
            try { GameObject.Find("Plateau 13 (1)").GetComponent<MeshRenderer>().sharedMaterial = tpmt; } catch { }
            GameObject.Find("GROUP: Large Flowers").SetActive(false);
            GameObject.Find("FORMATION (5)").transform.localPosition = new Vector3(-140f, -6.08f, 491.99f);
            try { GameObject.Find("P13").transform.GetChild(2).gameObject.SetActive(false); } catch { }

            var r = GameObject.Find("HOLDER: Randomization").transform;

            // Plateau Tall

            r.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;

            // Plateau 6

            r.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(1).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(1).GetChild(1).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;

            // Plateau 9

            r.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(2).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;

            // Plateau 11

            r.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;

            // Plateau 13

            r.GetChild(4).GetChild(1).GetChild(3).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;

            // Plateau 15

            r.GetChild(5).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(5).GetChild(0).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(5).GetChild(0).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(5).GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmtb;
            r.GetChild(5).GetChild(1).GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(5).GetChild(1).GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
            r.GetChild(5).GetChild(2).GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = tpmhm;
        }
    }
}