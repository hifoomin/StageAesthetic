using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class VoidLocus
    {
        public static void GreenLocus(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(89, 151, 116, 35);
            fog.fogColorMid.value = new Color32(70, 136, 116, 60);
            fog.fogColorEnd.value = new Color32(70, 116, 140, 170);
            cgrade.colorFilter.value = new Color32(101, 128, 140, 20);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(92, 43, 102, 255);
            sunLight.intensity = 2f;
            Debug.Log("purple locus");
        }

        public static void BlueLocus(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(52, 52, 99, 35);
            fog.fogColorMid.value = new Color32(35, 46, 99, 50);
            fog.fogColorEnd.value = new Color32(0, 8, 48, 160);
            cgrade.colorFilter.value = new Color32(35, 46, 99, 70);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(30, 28, 99, 255);
            sunLight.intensity = 2.6f;
            Debug.Log("blue locus");
        }

        public static void PinkLocus(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(99, 24, 34, 25);
            fog.fogColorMid.value = new Color32(102, 21, 37, 60);
            fog.fogColorEnd.value = new Color32(35, 3, 17, 170);
            cgrade.colorFilter.value = new Color32(96, 18, 47, 40);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(96, 32, 47, 255);
            sunLight.intensity = 2.1f;
            Debug.Log("red locus");
        }
    }
}