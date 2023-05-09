using FRCSharp;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class DryBasin
    {
        public static void RainyBasin(TheCoolerRampFog fog, ColorGrading cgrade, GameObject rain)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var sun2 = Object.Instantiate(sun);
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            fog.skyboxPower = 0f;
            fog.startColor = new Color32(115, 152, 181, 0);
            fog.middleColor = new Color32(82, 96, 130, 255);
            fog.endColor = new Color32(134, 137, 219, 255);
            fog.fogZero = -0.005f;
            fog.fogOne = 0.11f;
            fog.power = 0.9f;
            if (Config.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 700,
                    constantMax = 700,
                    constantMin = 220,
                    curve = epic2.curve,
                    curveMax = epic2.curveMax,
                    curveMin = epic2.curveMax,
                    curveMultiplier = epic2.curveMultiplier,
                    mode = epic2.mode
                };
                var epic3 = rainParticle.colorOverLifetime;
                epic3.enabled = false;
                var epic4 = rainParticle.main;
                epic4.scalingMode = ParticleSystemScalingMode.Shape;
                rain.transform.eulerAngles = new Vector3(87, 110, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            }
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(100, 109, 121, 255);
        }

        public static void PurpleBasin(TheCoolerRampFog fog, ColorGrading cgrade)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var sun2 = Object.Instantiate(sun);
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
            var sun2 = Object.Instantiate(sun);
            sun.gameObject.SetActive(false);
            sun2.color = new Color32(142, 161, 159, 255);
            fog.startColor = new Color32(115, 152, 181, 39);
            fog.middleColor = new Color32(106, 67, 154, 255);
            fog.endColor = new Color32(134, 137, 219, 163);
            fog.fogZero = 0f;
            fog.fogOne = 0.2f;
            fog.power = 1f;
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(232, 203, 164, 255);
        }
    }
}