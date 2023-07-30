using FRCSharp;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Stage2
{
    internal class DryBasin
    {
        public static void VanillaTweaks()
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.shadows = LightShadows.Soft;
        }

        public static void RainyBasin(TheCoolerRampFog fog, ColorGrading cgrade)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            sun2.shadows = LightShadows.Soft;
            fog.skyboxPower = 0f;
            fog.startColor = new Color32(139, 148, 227, 23);
            fog.middleColor = new Color32(163, 167, 236, 95);
            fog.endColor = new Color32(134, 134, 219, 255);
            fog.fogZero = -0.01f;
            fog.fogOne = 0.4f;
            fog.power = 1f;
            AddRain(RainType.Rainstorm);
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(100, 109, 121, 255);
        }

        public static void PurpleBasin(TheCoolerRampFog fog, ColorGrading cgrade)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            fog.startColor = new Color32(115, 152, 181, 39);
            fog.middleColor = new Color32(106, 67, 154, 255);
            fog.endColor = new Color32(134, 137, 219, 163);
            fog.fogZero = -0.005f;
            fog.fogOne = 0.09f;
            fog.power = 1f;
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(176, 94, 152, 255);
        }

        public static void MorningBasin(TheCoolerRampFog fog, ColorGrading cgrade)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.name = "Shitty Not Working Sun";
            var sun2 = Object.Instantiate(sun);
            sun2.name = "Directional Light (SUN)";
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            fog.gameObject.SetActive(false);
        }
    }
}