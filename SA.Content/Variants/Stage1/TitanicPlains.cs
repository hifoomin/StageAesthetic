using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class TitanicPlains
    {
        public static void SunsetPlains(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(45, 49, 75, 0);
            fog.fogColorMid.value = new Color32(113, 75, 58, 130);
            fog.fogColorEnd.value = new Color32(178, 93, 82, 255);
            fog.skyboxStrength.value = 0.156f;
            fog.fogZero.value = -0.049f;
            fog.fogOne.value = 0.211f;
            fog.fogIntensity.value = 0.769f;
            fog.fogPower.value = 0.5f;
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 135, 0, 255);
            sunLight.intensity = 1.14f;
            sunLight.shadowStrength = 0.877f;
            sunTransform.localEulerAngles = new Vector3(27, 268, 88);
        }

        public static void RainyPlains(RampFog fog, GameObject rain, string scenename)
        {
            fog.fogColorStart.value = new Color32(34, 45, 62, 18);
            fog.fogColorMid.value = new Color32(72, 84, 103, 165);
            fog.fogColorEnd.value = new Color32(97, 109, 129, 255);
            fog.skyboxStrength.value = 0.075f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(64, 144, 219, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(50, 17, 270);
            if (Config.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 900,
                    constantMax = 900,
                    constantMin = 240,
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
                rain.transform.eulerAngles = new Vector3(85, 145, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
                if (scenename == "golemplains")
                {
                    GameObject wind = GameObject.Find("WindZone");
                    wind.transform.eulerAngles = new Vector3(30, 145, 0);
                    var windZone = wind.GetComponent<WindZone>();
                    windZone.windMain = 0.4f;
                    windZone.windTurbulence = 0.8f;
                }
            }
        }

        public static void NightPlains(RampFog fog, GameObject rain, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(0, 166, 255, 255);
            fog.fogColorMid.value = new Color32(51, 79, 94, 34);
            fog.fogColorEnd.value = new Color32(12, 18, 54, 255);
            fog.skyboxStrength.value = 0.08f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 1f;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.06f;
            cgrade.colorFilter.value = new Color32(180, 184, 255, 255);
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(0, 132, 255, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.7f;
            sunTransform.localEulerAngles = new Vector3(38, 270, 97);
            Object.Instantiate(rain, Vector3.zero, Quaternion.identity);
        }

        public static void SunrisePlains()
        {
            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(178, 218, 255, 255);
            sunLight.intensity = 1.5f;
            sunTransform.localEulerAngles = new Vector3(33, 267, 277);
        }

        public static void NostalgiaPlains(RampFog fog)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color(0.7450981f, 0.8999812f, 0.9137255f);
            sun.intensity = 1.34f;
            fog.fogColorStart.value = new Color32(93, 127, 106, 3);
            fog.fogColorMid.value = new Color32(119, 153, 132, 7);
            fog.fogColorEnd.value = new Color32(101, 158, 144, 130);
            fog.fogPower.value = 0.5f;
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
        }
    }
}