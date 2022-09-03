using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class AbandonedAqueduct
    {
        public static void LightChanges(string variant)
        {
            if (variant == "dark") chain = new Color(0.721f, 0.041f, 0.065f);
            if (variant == "rain") chain = new Color(0.761f, 0.821f, 0.926f);
            if (variant == "night") chain = new Color(0.221f, 0.961f, 0.925f);
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    if (light.gameObject.name.Equals("CrystalLight")) light.color = chain;
                }
            }
        }

        public static void DarkAqueduct(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(43, 23, 12, 144);
            fog.fogColorMid.value = new Color32(56, 30, 19, 195);
            fog.fogColorEnd.value = new Color32(66, 41, 29, 255);
            fog.skyboxStrength.value = 0.06f;
            fog.fogOne.value = 0.152f;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            GameObject.Find("HOLDER: Warning Flags").SetActive(false);
            base1.Find("Warning Signs").gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(190, 162, 154, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.6f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(53, 29, 16, 124);
            CaveFog.fogColorMid.value = new Color32(68, 37, 25, 186);
            CaveFog.fogColorEnd.value = new Color32(79, 51, 37, 255);
            LightChanges("dark");
        }

        public static void BlueAqueduct(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(57, 63, 76, 73);
            fog.fogColorMid.value = new Color32(62, 71, 83, 179);
            fog.fogColorEnd.value = new Color32(68, 77, 90, 255);
            fog.skyboxStrength.value = 0.055f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(68, 74, 86, 63);
            CaveFog.fogColorMid.value = new Color32(73, 83, 96, 164);
            CaveFog.fogColorEnd.value = new Color32(80, 89, 103, 255);
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(166, 221, 253, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.1f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
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
            }
            LightChanges("rain");
        }

        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.4f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
        }

        public static void NightAqueduct(RampFog fog, GameObject rain, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(28, 36, 56, 125);
            fog.fogColorMid.value = new Color32(28, 35, 47, 179);
            fog.fogColorEnd.value = new Color32(29, 33, 45, 255);
            fog.skyboxStrength.value = 0.055f;
            fog.fogOne.value = 0.082f;
            var CaveFog = GameObject.Find("GLUndergroundPPVolume").GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            CaveFog.fogColorStart.value = new Color32(37, 46, 67, 115);
            CaveFog.fogColorMid.value = new Color32(37, 45, 57, 167);
            CaveFog.fogColorEnd.value = new Color32(38, 42, 55, 255);
            cgrade.colorFilter.value = new Color32(140, 164, 221, 255);
            cgrade.colorFilter.overrideState = true;
            Transform base1 = GameObject.Find("HOLDER: Misc Props").transform;
            base1.Find("Props").GetChild(4).gameObject.SetActive(true);
            var lightBase = GameObject.Find("Weather, Goolake").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(140, 157, 176, 255);
            sunLight.intensity = 0.45f;
            sunLight.shadowStrength = 0.1f;
            sunTransform.localEulerAngles = new Vector3(42, 12, 180);
            if (Config.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 2000,
                    constantMax = 2000,
                    constantMin = 800,
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
            }
            LightChanges("night");
        }

        private static Color chain;
    }
}