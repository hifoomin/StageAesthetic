using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.AI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage4
{
    internal class SunderedGrove
    {
        public static void VanillaJungle()
        {
            VanillaFoliage();
        }

        public static void GreenJungle(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(91, 99, 66, 20);
            fog.fogColorMid.value = new Color32(107, 105, 68, 67);
            fog.fogColorEnd.value = new Color32(119, 116, 74, 150);
            fog.skyboxStrength.value = 0.126f;
            cgrade.colorFilter.value = new Color32(139, 196, 171, 20);
            cgrade.colorFilter.overrideState = true;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(242, 239, 202, 255);
            sunLight.intensity = 3.1f;
            sunTransform.localEulerAngles = new Vector3(30, 175, 180);
            VanillaFoliage();
        }

        public static void SunJungle(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(46, 85, 98, 0);
            fog.fogColorMid.value = new Color32(51, 70, 84, 64);
            fog.fogColorEnd.value = new Color32(92, 127, 131, 180);
            cgrade.colorFilter.value = new Color32(251, 186, 170, 255);
            cgrade.colorFilter.overrideState = true;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(242, 239, 202, 255);
            sunLight.intensity = 4f;
            sunTransform.localEulerAngles = new Vector3(60, 15, -4);
            VanillaFoliage();
        }

        public static void StormJungle(RampFog fog, ColorGrading cgrade)
        {
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(203, 221, 243, 255);
            sunLight.intensity = 3f;
            fog.fogColorStart.value = new Color32(44, 45, 58, 17);
            fog.fogColorMid.value = new Color32(46, 50, 60, 132);
            fog.fogColorEnd.value = new Color32(76, 81, 84, 255);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.095f;
            fog.skyboxStrength.value = 0.126f;
            cgrade.colorFilter.value = new Color32(148, 206, 183, 255);
            cgrade.colorFilter.overrideState = true;

            var rain = lightBase.Find("CameraRelative").gameObject;
            rain.SetActive(false);

            AddRain(RainType.Typhoon);
            VanillaFoliage();
        }

        public static void SandyJungle(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(255, 108, 0, 32);
            fog.fogColorMid.value = new Color32(183, 139, 62, 28);
            fog.fogColorEnd.value = new Color32(196, 152, 70, 255);
            fog.fogZero.value = -0.01f;
            fog.fogOne.value = 0.35f;
            fog.skyboxStrength.value = 0.02f;
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(255, 222, 185, 39);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion());
            terrainMat2.color = new Color32(166, 157, 27, 59);
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            detailMat.color = new Color32(166, 157, 27, 59);
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matClayGooDebuff.mat").WaitForCompletion();
            var shroomMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolake.mat").WaitForCompletion());
            shroomMat.color = new Color32(176, 153, 57, 255);
            var c = GameObject.Find("Cloud Floor").transform;
            if (terrainMat && terrainMat2 && detailMat && water && shroomMat)
            {
                c.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                var seshList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];
                foreach (SkinnedMeshRenderer smr in seshList)
                {
                    var meshBase = smr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("BounceStem"))
                        {
                            switch (smr.sharedMaterial)
                            {
                                case null:
                                    try { smr.sharedMaterial = water; } catch (Exception e) { SwapVariants.AesLog.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    smr.sharedMaterial = water;
                                    break;
                            }
                        }
                    }
                }
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Gianticus") || meshBase.name.Contains("Tree Big Bottom") || meshBase.name.Contains("Tree D") || meshBase.name.Contains("Wall") || meshBase.name.Contains("RJRoot") || meshBase.name.Contains("RJShroomShelf"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("RJTriangle") || meshBase.name.Contains("BbRuinArch") || meshBase.name.Contains("RJShroomBig"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat2;
                            }
                        }
                        if (meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Root Bridge") || meshBase.name.Contains("Vine Tree"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }

                        if (meshBase.name.Contains("Moss Cover") || meshBase.name.Contains("RJShroomShelf") || meshBase.name.Contains("RJShroomBig") || meshBase.name.Contains("RJShroomSmall") || meshBase.name.Contains("RJMossPatch"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = shroomMat;
                            }
                        }

                        if (meshBase.name.Contains("RJTwistedTreeBig"))
                        {
                            meshBase.SetActive(false);
                        }
                    }
                }
            }

            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light l in lightList)
            {
                var meshBase = l.gameObject;
                if (meshBase != null)
                {
                    l.color = new Color32(255, 131, 117, 255);
                    l.range = 15;
                    l.intensity = 1f;
                }
            }
            c.GetChild(0).localScale = new Vector3(2000, 2000, 2000);
            c.GetChild(0).localPosition = new Vector3(0, 0, 0);
            c.GetChild(1).gameObject.SetActive(false);
            c.GetChild(2).gameObject.SetActive(false);
            c.GetChild(3).gameObject.SetActive(false);
            c.GetChild(4).gameObject.SetActive(false);
            GameObject.Find("GROUP: DistantTreeFoliage").SetActive(false);
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
                    if (meshBase.name.Contains("RJShroomFoliage_") || meshBase.name.Contains("RJTreeBigFoliage_"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 255, 255, 255);
                    }
                    if (meshBase.name.Contains("RJMossFoliage_"))
                    {
                        mr.sharedMaterial.color = new Color32(122, 215, 221, 255);
                    }
                    if (meshBase.name.Contains("RJTowerTreeFoliage_"))
                    {
                        var color = new Color32(171, 171, 171, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("RJHangingMoss_"))
                    {
                        mr.sharedMaterial.color = new Color32(105, 130, 110, 255);
                    }
                    if (meshBase.name.Contains("spmFern1_"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 255, 255, 255);
                    }
                    if (meshBase.name.Contains("spmRJgrass1_"))
                    {
                        mr.sharedMaterial.color = new Color32(190, 164, 179, 255);
                    }
                    if (meshBase.name.Contains("spmRJgrass2_"))
                    {
                        mr.sharedMaterial.color = new Color32(207, 207, 207, 255);
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
                    if (meshBase.name.Contains("RJShroomFoliage_") || meshBase.name.Contains("RJTreeBigFoliage_"))
                    {
                        mr.sharedMaterial.color = new Color32(0, 0, 0, 255);
                    }
                    if (meshBase.name.Contains("RJMossFoliage_"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 149, 0, 255);
                    }
                    if (meshBase.name.Contains("RJTowerTreeFoliage_"))
                    {
                        var color = new Color32(255, 95, 0, 103);
                        var sharedMaterials = mr.sharedMaterials;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("RJHangingMoss_"))
                    {
                        mr.sharedMaterial.color = new Color32(0, 0, 0, 102);
                    }
                    if (meshBase.name.Contains("spmFern1_"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 101, 58, 86);
                    }
                    if (meshBase.name.Contains("spmRJgrass1_"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 37, 0, 255);
                    }
                    if (meshBase.name.Contains("spmRJgrass2_"))
                    {
                        mr.sharedMaterial.color = new Color32(0, 0, 0, 0);
                    }
                }
            }
        }
    }
}