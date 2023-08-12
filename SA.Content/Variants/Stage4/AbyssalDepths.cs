using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage4
{
    internal class AbyssalDepths
    {
        public static void VanillaChanges()
        {
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.intensity = 1.5f;
            sunLight.transform.localEulerAngles = new Vector3(35, 15, 351);
            sunLight.color = new Color32(207, 207, 142, 255);
            sunLight.shadowStrength = 0.5f;
        }

        public static void Blue(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(17, 63, 72, 100);
            fog.fogColorMid.value = new Color32(43, 125, 114, 74);
            fog.fogColorEnd.value = new Color32(16, 74, 72, 255);
            fog.fogOne.value = 0.3f;
            fog.fogIntensity.value = 1f;
            fog.fogZero.value = -0.02f;

            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(254, 214, 229, 255);
            sunLight.intensity = 2.5f;
            sunLight.shadowStrength = 0.6f;
            sunLight.transform.eulerAngles = new Vector3(65f, 222.6395f, 202.9964f);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(58, 35, 60, 80);
            caveFog.fogColorMid.value = new Color32(44, 74, 55, 184);
            caveFog.fogColorEnd.value = new Color32(40, 68, 53, 255);
            cgrade.colorFilter.value = new Color32(84, 84, 173, 255);
            cgrade.colorFilter.overrideState = true;
            cgrade.postExposure.value = 1.1f;
            cgrade.postExposure.overrideState = true;
            // Lighting: Magenta coral, orange otherwise
            LightChange("hive");
        }

        public static void Night(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(93, 41, 111, 28);
            fog.fogColorMid.value = new Color32(104, 73, 91, 160);
            fog.fogColorEnd.value = new Color32(187, 65, 80, 255);
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 1f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.14f;
            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(69, 201, 215, 255);
            sunLight.intensity = 4f;
            sunLight.transform.eulerAngles = new Vector3(60f, 78f, 351f);
            sunLight.shadowStrength = 0.5f;
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

        public static void Orange(RampFog fog)
        {
            fog.fogIntensity.value = 1f;
            fog.fogPower.overrideState = true;
            fog.fogPower.value = 0.26f;
            fog.fogZero.value = -0.002f;
            fog.fogOne.value = 0.3f;
            fog.fogColorStart.value = new Color32(9, 4, 2, 141);
            fog.fogColorMid.value = new Color32(99, 81, 32, 35);
            fog.fogColorEnd.value = new Color32(132, 76, 44, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(191, 148, 74, 255);
            sunLight.intensity = 1.2f;
            sunLight.transform.eulerAngles = new Vector3(70f, 19.64314f, 9.985f);
            sunLight.shadowStrength = 0.6f;
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

        public static void Coral(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(127, 70, 206, 15);
            fog.fogColorMid.value = new Color32(185, 72, 119, 33);
            fog.fogColorEnd.value = new Color32(183, 93, 129, 130);
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
            AddRain(RainType.Drizzle);
            CoralMaterials();
        }

        private static Color coral;
        private static Color chain;
        private static Color crystal;

        public static void CoralMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain2.mat").WaitForCompletion());
            terrainMat.color = new Color32(128, 125, 216, 234);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTerrain.mat").WaitForCompletion());
            terrainMat2.color = new Color32(109, 125, 216, 177);
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJRock.mat").WaitForCompletion());
            detailMat.color = new Color32(49, 0, 255, 255);
            var detailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Titan/matTitanGoldArcaneFlare.mat").WaitForCompletion();

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat3);

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
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat;
                                }
                            }
                            if (meshBase.name.Contains("RuinBowl") && meshParent.name.Contains("RuinMarker"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = detailMat;
                                }
                            }
                        }
                        if (meshBase.name.Contains("Hero") || meshBase.name.Contains("Wall") || meshBase.name.Contains("Ceiling"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("Ruin") || meshBase.name.Contains("Column") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Coral") || meshBase.name.Contains("Heatvent") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("GiantRock") || meshBase.name.Contains("Stalagmite"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("Crystal"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
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
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat2;
                            }
                        }
                    }
                }
            }
        }
    }
}