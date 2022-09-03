using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class SirensCall
    {
        public static void ShipNight(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(39, 107, 92, 0);
            fog.fogColorMid.value = new Color32(15, 62, 50, 99);
            fog.fogColorEnd.value = new Color32(4, 25, 22, 255);
            cgrade.colorFilter.value = new Color32(171, 223, 227, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0.8f;
            fog.fogOne.value = 0.085f;
            var lightBase = GameObject.Find("Weather, Shipgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(155, 163, 227, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.4f;
        }

        public static void ShipSkies(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(64, 67, 103, 154);
            fog.fogColorEnd.value = new Color32(126, 156, 166, 255);
            var lightBase = GameObject.Find("Weather, Shipgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 239, 223, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(33, 0, 0);
            // Remove rain
        }

        public static void ShipDeluge(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(58, 62, 68, 0);
            fog.fogColorMid.value = new Color32(46, 67, 76, 130);
            fog.fogColorEnd.value = new Color32(78, 94, 87, 255);
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.057f;
            // Remove rain
            if (Config.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 2500,
                    constantMax = 2500,
                    constantMin = 1000,
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
                rain.transform.eulerAngles = new Vector3(78, 25, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            }
        }
    }
}