using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants
{
    internal class AbyssalDepths
    {
        public static void VanillaChanges()
        {
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.intensity = 2f;
            GameObject.Find("Directional Light (SUN)").transform.localEulerAngles = new Vector3(35, 15, 351);
        }

        public static void HiveCave(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(67, 43, 68, 84);
            fog.fogColorMid.value = new Color32(44, 28, 45, 205);
            fog.fogColorEnd.value = new Color32(14, 9, 15, 255);
            fog.fogOne.value = 0.129f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(222, 127, 236, 255);
            sunLight.intensity = 3f;
            sunLight.shadowStrength = 0.3f;
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(58, 35, 60, 80);
            caveFog.fogColorMid.value = new Color32(44, 74, 55, 184);
            caveFog.fogColorEnd.value = new Color32(40, 68, 53, 255);
            cgrade.colorFilter.value = new Color32(84, 84, 173, 255);
            cgrade.colorFilter.overrideState = true;
            // Lighting: Magenta coral, orange otherwise
            LightChange("hive");
        }

        public static void DarkCave(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(76, 71, 119, 76);
            fog.fogColorMid.value = new Color32(73, 90, 104, 175);
            fog.fogColorEnd.value = new Color32(78, 94, 103, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(69, 201, 215, 255);
            sunLight.intensity = 1.2f;
            GameObject.Find("Directional Light (SUN)").transform.localEulerAngles = new Vector3(43, 78, 351);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(67, 65, 109, 76);
            caveFog.fogColorMid.value = new Color32(40, 68, 123, 161);
            caveFog.fogColorEnd.value = new Color32(46, 128, 148, 255);
            cgrade.colorFilter.value = new Color32(119, 207, 181, 255);
            cgrade.colorFilter.overrideState = true;
            // Lighting: Blue coral, cyan or green lighting otherwise
            LightChange("azure");
        }

        public static void MeadowCave(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(96, 67, 103, 33);
            fog.fogColorMid.value = new Color32(102, 66, 109, 148);
            fog.fogColorEnd.value = new Color32(148, 85, 166, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(205, 129, 255, 255);
            sunLight.intensity = 1f;
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(85, 57, 91, 33);
            caveFog.fogColorMid.value = new Color32(90, 55, 97, 148);
            caveFog.fogColorEnd.value = new Color32(135, 76, 149, 255);
            // Lighting: Pink coral, orange otherwise
            LightChange("meadow");
        }

        public static void LightChange(string variant)
        {
            switch (variant)
            {
                case "meadow":
                    coral = new Color(0.64f, 0.343f, 0.924f, 1);
                    chain = new Color(0.981f, 0.521f, 0.065f);
                    crystal = new Color(0.598f, 0.117f, 0.355f);
                    break;

                case "azure":
                    coral = new Color(0.188f, 0.444f, 0, 1);
                    chain = new Color(0.181f, 0.921f, 0.945f);
                    crystal = new Color(0f, 0.837f, 0.14f);
                    break;

                case "hive":
                    coral = new Color32(30, 209, 27, 255);
                    chain = new Color(0.981f, 0.521f, 0.065f);
                    crystal = new Color(0.718f, 0, 0.515f);
                    break;

                default:
                    break;
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
                        if (light.gameObject.transform.parent.gameObject.name.Equals("DCCoralPropMediumActive"))
                        {
                            light.color = coral;
                            var lightLP = light.transform.localPosition;
                            lightLP.z = 4;
                        }
                        else if (light.gameObject.transform.parent.gameObject.name.Equals("DCCrystalCluster Variant")) light.color = crystal;
                    }
                    if (light.gameObject.name.Equals("CrystalLight")) light.color = chain;
                }
            }
        }

        public static void CoralCave(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyCoralMaterials(); } catch { SwapVariants.AesLog.LogError("Coral Depths: Failed to change materials, trying again..."); } finally { ApplyCoralMaterials(); }
            fog.fogColorStart.value = new Color32(127, 70, 206, 20);
            fog.fogColorMid.value = new Color32(206, 70, 127, 33);
            fog.fogColorEnd.value = new Color32(190, 99, 136, 130);
            cgrade.colorFilter.value = new Color32(255, 255, 255, 22);
            Debug.Log("satur is + " + cgrade.saturation.value);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(204, 173, 186, 255);
            sunLight.intensity = 3f;
            sunLight.shadowStrength = 0.7f;
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light l in lightList)
            {
                if (l != null && !l.name.Contains("Light (SUN)"))
                {
                    l.color = new Color32(216, 192, 32, 255);
                    l.intensity = 50f;
                    l.range = 30f;
                }
                if (l.gameObject.GetComponent<FlickerLight>() != null)
                {
                    l.gameObject.GetComponent<FlickerLight>().enabled = false;
                }
            }
            GameObject.Find("DCPPInTunnels").SetActive(false);
        }

        private static Color coral;
        private static Color chain;
        private static Color crystal;

        public static void ApplyCoralMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain2.mat").WaitForCompletion());
            terrainMat.color = new Color32(128, 125, 216, 234);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain.mat").WaitForCompletion());
            terrainMat2.color = new Color32(109, 125, 216, 177);
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJRock.mat").WaitForCompletion());
            detailMat.color = new Color32(49, 0, 255, 255);
            var detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Titan/matTitanGoldArcaneFlare.mat").WaitForCompletion();
            if (terrainMat && terrainMat2 && detailMat && detailMat3)
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
                            if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("Ruin"))
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
                            if (meshBase.name.Contains("RuinBowl") && meshParent.name.Contains("RuinMarker"))
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
                        }
                        if (meshBase.name.Contains("Hero") || meshBase.name.Contains("Wall") || meshBase.name.Contains("Ceiling"))
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
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("Ruin") || meshBase.name.Contains("Column") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Coral") || meshBase.name.Contains("Heatvent") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("GiantRock") || meshBase.name.Contains("Stalagmite"))
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
                        if (meshBase.name.Contains("Crystal"))
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
                        if (meshBase.name.Contains("LightMesh"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat3;
                                if (meshBase.transform.childCount >= 1 && meshBase.transform.GetChild(0).name.Contains("Crystal"))
                                {
                                    meshBase.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = detailMat3;
                                }
                            }
                        }
                        if (meshBase.name.Contains("GiantStoneSlab") || meshBase.name.Contains("TerrainBackwall") || meshBase.name.Contains("Chain"))
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
                    }
                }
            }
        }
    }
}