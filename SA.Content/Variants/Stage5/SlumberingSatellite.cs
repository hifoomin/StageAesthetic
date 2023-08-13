using FRCSharp;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage5
{
    internal class SlumberingSatellite
    {
        public static void Vanilla()
        {
            VanillaFoliage();
        }

        public static void Morning(TheCoolerRampFog fog, RampFog fog2)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(255, 219, 160, 255);
            sun.intensity = 2f;
            sun.shadowStrength = 0.85f;

            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.05f;
            fog.fogOne = 0.2f;
            fog.startColor = new Color32(164, 232, 230, 3);
            fog.middleColor = new Color32(101, 132, 134, 6);
            fog.endColor = new Color32(89, 123, 132, 255);
            fog.skyboxPower = 0f;

            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.05f;
            fog2.fogOne.value = 0.2f;
            fog2.fogColorStart.value = new Color32(164, 232, 230, 3);
            fog2.fogColorMid.value = new Color32(101, 132, 134, 6);
            fog2.fogColorEnd.value = new Color32(89, 123, 132, 255);
            fog2.skyboxStrength.value = 0f;
            VanillaFoliage();
        }

        public static void Overcast(TheCoolerRampFog fog, RampFog fog2)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(160, 255, 208, 255);
            sun.intensity = 1f;
            sun.shadowStrength = 0.6f;

            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.07f;
            fog.fogOne = 0.1f;
            fog.startColor = new Color32(134, 99, 69, 50);
            fog.middleColor = new Color32(140, 115, 94, 6);
            fog.endColor = new Color32(87, 74, 66, 255);
            fog.skyboxPower = 0f;

            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.07f;
            fog2.fogOne.value = 0.1f;
            fog2.fogColorStart.value = new Color32(134, 99, 69, 50);
            fog2.fogColorMid.value = new Color32(140, 115, 94, 6);
            fog2.fogColorEnd.value = new Color32(87, 74, 66, 255);
            fog2.skyboxStrength.value = 0f;

            AddRain(RainType.Typhoon);

            VanillaFoliage();
        }

        public static void Blue(TheCoolerRampFog fog, RampFog fog2)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(160, 255, 208, 255);
            sun.intensity = 1f;
            sun.shadowStrength = 0.6f;

            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.07f;
            fog.fogOne = 0.15f;
            fog.startColor = new Color32(69, 107, 134, 50);
            fog.middleColor = new Color32(94, 140, 135, 6);
            fog.endColor = new Color32(66, 84, 87, 255);
            fog.skyboxPower = 0f;

            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.07f;
            fog2.fogOne.value = 0.15f;
            fog2.fogColorStart.value = new Color32(69, 107, 134, 50);
            fog2.fogColorMid.value = new Color32(94, 140, 135, 6);
            fog2.fogColorEnd.value = new Color32(66, 84, 87, 255);
            fog2.skyboxStrength.value = 0f;
            VanillaFoliage();
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
    }
}