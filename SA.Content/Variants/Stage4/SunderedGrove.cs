using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class SunderedGrove
    {
        public static void GreenJungle(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(33, 35, 26, 18);
            fog.fogColorMid.value = new Color32(30, 33, 24, 147);
            fog.fogColorEnd.value = new Color32(27, 29, 20, 255);
            fog.skyboxStrength.value = 0.126f;
            cgrade.colorFilter.value = new Color32(198, 251, 125, 255);
            cgrade.colorFilter.overrideState = true;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(242, 239, 202, 255);
            sunLight.intensity = 3f;
            sunTransform.localEulerAngles = new Vector3(30, 175, 180);
        }

        public static void SunJungle(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(46, 85, 98, 0);
            fog.fogColorMid.value = new Color32(51, 70, 84, 143);
            fog.fogColorEnd.value = new Color32(92, 127, 131, 255);
            cgrade.colorFilter.value = new Color32(251, 186, 170, 255);
            cgrade.colorFilter.overrideState = true;
            var lightBase = GameObject.Find("HOLDER: Weather Set 1").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(242, 239, 202, 255);
            sunLight.intensity = 4f;
            sunTransform.localEulerAngles = new Vector3(60, 15, -4);
        }

        public static void StormJungle(RampFog fog, GameObject rain, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(44, 45, 58, 17);
            fog.fogColorMid.value = new Color32(46, 50, 60, 132);
            fog.fogColorEnd.value = new Color32(76, 81, 84, 255);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.095f;
            fog.skyboxStrength.value = 0.126f;
            cgrade.colorFilter.value = new Color32(130, 156, 146, 255);
            cgrade.colorFilter.overrideState = true;
            if (Config.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 1500,
                    constantMax = 1500,
                    constantMin = 600,
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
                rain.transform.eulerAngles = new Vector3(75, 210, 0);
                rain.transform.localScale = new Vector3(16, 16, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            }
        }
    }
}