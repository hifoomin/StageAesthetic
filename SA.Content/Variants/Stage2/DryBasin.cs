/*
using FRCSharp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2
{
    internal class DryBasin
    {
        public static void VanillaChanges()
        {
            AddSand(SandType.Moderate);
        }

        public static void Overcast(TheCoolerRampFog fog, ColorGrading cgrade, RampFog fog2)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            sun2.shadows = LightShadows.Soft;

            fog.skyboxPower = 0f;
            fog.intensity = 1f;
            fog.power = 1f;
            fog.fogZero = -0.01f;
            fog.fogOne = 0.13f;
            fog.startColor = new Color32(219, 138, 136, 15);
            fog.middleColor = new Color32(156, 128, 109, 105);
            fog.endColor = new Color32(201, 143, 131, 255);

            fog2.skyboxStrength.value = 0f;
            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = -0.01f;
            fog2.fogOne.value = 0.13f;
            fog2.fogColorStart.value = new Color32(219, 138, 136, 15);
            fog2.fogColorMid.value = new Color32(156, 128, 109, 105);
            fog2.fogColorEnd.value = new Color32(201, 143, 131, 255);

            AddRain(RainType.Typhoon);
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(100, 109, 121, 255);
        }

        public static void Blue(TheCoolerRampFog fog, ColorGrading cgrade, RampFog fog2)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(149, 163, 217, 255);

            fog.startColor = new Color32(119, 158, 189, 11);
            fog.middleColor = new Color32(59, 135, 170, 35);
            fog.endColor = new Color32(148, 186, 221, 60);

            fog.fogZero = -0.005f;
            fog.fogOne = 0.09f;
            fog.power = 1f;
            fog.intensity = 0.8f;
            fog.skyboxPower = 1f;

            fog2.fogColorStart.value = new Color32(119, 158, 189, 11);
            fog2.fogColorMid.value = new Color32(59, 135, 170, 35);
            fog2.fogColorEnd.value = new Color32(148, 186, 221, 60);

            fog2.fogZero.value = -0.005f;
            fog2.fogOne.value = 0.09f;
            fog2.fogPower.value = 1f;
            fog2.fogIntensity.value = 0.8f;
            fog2.skyboxStrength.value = 1f;

            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(118, 129, 183, 255);
        }

        public static void Morning(TheCoolerRampFog fog, RampFog fog2)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(255, 243, 207, 255);
            sun2.intensity = 1f;
            sun2.shadowStrength = 0.8f;

            fog.skyboxPower = 1f;
            fog.power = 0.8f;
            fog.fogZero = -0.02f;
            fog.fogOne = 0.5f;
            fog.startColor = new Color32(0, 65, 255, 1);
            fog.middleColor = new Color32(226, 203, 255, 20);
            fog.endColor = new Color32(106, 0, 255, 53);

            fog2.skyboxStrength.value = 1f;
            fog2.fogPower.value = 0.8f;
            fog2.fogZero.value = -0.02f;
            fog2.fogOne.value = 0.5f;
            fog2.fogColorStart.value = new Color32(0, 65, 255, 1);
            fog2.fogColorMid.value = new Color32(226, 203, 255, 20);
            fog2.fogColorEnd.value = new Color32(106, 0, 255, 53);
        }
    }
}
*/