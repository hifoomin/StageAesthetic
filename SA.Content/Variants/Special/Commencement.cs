using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using R2API.Utils;

namespace StageAesthetic.Variants.Special
{
    internal class Commencement
    {
        public static void Night(RampFog fog)
        {
            Skybox.NightSky();
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.28f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            // 0.1138 0.1086 0.15 1
            // 0.1012 0.1091 0.1226 1
            bruh2.overrideAmbientColor = new Color(0.0138f, 0.086f, 0.015f, 1);
            bruh2.overrideDirectionalColor = new Color(0.012f, 0.091f, 0.0226f, 1);
        }

        public static void Crimson(RampFog fog)
        {
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = -0.1f;
            fog.fogOne.value = 0.7f;
            fog.fogColorStart.value = new Color32(0, 0, 0, 0);
            fog.fogColorMid.value = new Color32(156, 31, 33, 50);
            fog.fogColorEnd.value = new Color32(93, 0, 18, 255);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.GetComponent<Light>().intensity = 0.15f;
            newSun.color = new Color32(255, 9, 0, 255);
            newSun.intensity = 0.4f;
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.79f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            bruh2.overrideAmbientColor = new Color(0.2138f, 0.1086f, 0.15f, 1);
            bruh2.overrideDirectionalColor = new Color(0.2012f, 0.1091f, 0.1226f, 1);
        }

        public static void Corruption(RampFog fog)
        {
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.3f;
            fog.fogColorStart.value = new Color32(77, 23, 107, 45);
            fog.fogColorMid.value = new Color32(104, 44, 107, 105);
            fog.fogColorEnd.value = new Color32(50, 0, 50, 255);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.GetComponent<Light>().intensity = 0.22f;
            newSun.color = new Color32(53, 94, 225, 255);
            newSun.intensity = 0.5f;
            newSun.shadowStrength = 1f;
            newSun.transform.eulerAngles = new Vector3(30.5f, 0f, 0f);
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.5f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            bruh2.overrideAmbientColor = new Color(0.2138f, 0.1086f, 0.2138f, 1);
            bruh2.overrideDirectionalColor = new Color(0.2012f, 0.1091f, 0.2012f, 1);
        }

        public static void Gray(RampFog fog)
        {
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.62f;
            fog.fogColorStart.value = new Color32(76, 76, 76, 50);
            fog.fogColorMid.value = new Color32(81, 75, 77, 159);
            fog.fogColorEnd.value = new Color32(44, 45, 52, 180);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            newSun.name = "Directional Light (SUN)";
            //newSun.color = new Color32(53, 94, 225, 255);
            //newSun.intensity = 0.5f;
            //newSun.shadowStrength = 1f;
            newSun.transform.eulerAngles = new Vector3(49.10302f, 313.86f, 308.234f);
            newSun.transform.localPosition = new Vector3(-26f, 138f, 335f);
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.28f;
            HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
            // 0.1138 0.1086 0.15 1
            // 0.1012 0.1091 0.1226 1
            bruh2.overrideAmbientColor = new Color(0.2138f, 0.2086f, 0.25f, 1);
            bruh2.overrideDirectionalColor = new Color(0.2012f, 0.2091f, 0.2226f, 1);
        }
    }
}