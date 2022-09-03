using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class AphelianSanctuary
    {
        public static void NearRainSanctuary(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(94, 105, 178, 45);
            fog.fogColorMid.value = new Color32(116, 98, 178, 95);
            fog.fogColorEnd.value = new Color32(149, 92, 179, 180);
            cgrade.colorFilter.value = new Color32(133, 148, 178, 45);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0.2f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(178, 142, 151, 255);
            sunLight.intensity = 1.3f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.SetActive(false);
            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
        }

        public static void SunriseSanctuary(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(165, 109, 18, 65);
            fog.fogColorMid.value = new Color32(163, 76, 17, 145);
            fog.fogColorEnd.value = new Color32(163, 96, 115, 235);
            cgrade.colorFilter.value = new Color32(255, 255, 255, 65);
            cgrade.colorFilter.overrideState = true;
            var sun = GameObject.Find("Sun");
            sun.transform.localPosition = new Vector3(743, 500, -127);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 239, 211, 255);
            sunLight.intensity = 2f;
            sunLight.shadowNormalBias = 0.92f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.SetActive(false);
            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
        }

        public static void NightSanctuary(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(36, 89, 146, 65);
            fog.fogColorMid.value = new Color32(21, 58, 131, 135);
            fog.fogColorEnd.value = new Color32(0, 0, 71, 255);
            cgrade.colorFilter.value = new Color32(20, 20, 160, 15);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 3.5f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.SetActive(false);
            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
        }
    }
}