using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class RallypointDelta
    {
        public static void OceanWall(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(47, 52, 62, 80);
            fog.fogColorMid.value = new Color32(72, 80, 98, 212);
            fog.fogColorEnd.value = new Color32(90, 101, 119, 255);
            fog.skyboxStrength.value = 0.15f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.4f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            GameObject.Find("HOLDER: Skybox").transform.Find("Water").localPosition = new Vector3(-1260, -66, 0);
            sunLight.color = new Color32(177, 184, 200, 255);
            sunLight.intensity = 1.2f;
        }

        public static void NightWall(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(33, 33, 56, 76);
            fog.fogColorMid.value = new Color32(38, 38, 55, 165);
            fog.fogColorEnd.value = new Color32(25, 24, 46, 255);
            fog.skyboxStrength.value = 0.7f;
            cgrade.colorFilter.value = new Color32(130, 123, 255, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(127, 168, 217, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.4f;
            GameObject.Find("Directional Light (SUN)").transform.eulerAngles = new Vector3(50, 275, 2);
        }

        public static void GreenWall(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(42, 72, 68, 0);
            fog.fogColorMid.value = new Color32(50, 68, 61, 163);
            fog.fogColorEnd.value = new Color32(35, 62, 52, 255);
            fog.skyboxStrength.value = 0.45f;
            cgrade.colorFilter.value = new Color32(178, 255, 230, 255);
            cgrade.colorFilter.overrideState = true;
        }
    }
}