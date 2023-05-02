using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using R2API.Utils;

namespace StageAesthetic.Variants
{
    internal class Commencement
    {
        public static void DarkCommencement(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.08f, 0.05f, 0.12f, 0.4f);
            fog.fogColorMid.value = new Color(0.13f, 0.14f, 0.19f, 0.625f);
            fog.fogColorEnd.value = new Color(0f, 0f, 0f, 1f);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(178, 238, 238, 255);
            sun.intensity = 1.9f;
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

            var bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
            bruh.weight = 0.79f;
        }

        public static void CrimsonCommencement(RampFog fog)
        {
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = -0.1f;
            fog.fogOne.value = 0.7f;
            fog.fogColorStart.value = new Color32(174, 0, 9, 59);
            fog.fogColorMid.value = new Color32(156, 31, 33, 69);
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
        }

        public static void CorruptionCommencement(RampFog fog)
        {
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.1f;
            fog.fogColorStart.value = new Color32(77, 23, 107, 95);
            fog.fogColorMid.value = new Color32(104, 44, 107, 105);
            fog.fogColorEnd.value = new Color32(74, 0, 50, 255);
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
        }

        public static void GrayCommencement(RampFog fog)
        {
            fog.fogIntensity.value = 0.908f;
            fog.fogPower.value = 0.4f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.62f;
            fog.fogColorStart.value = new Color32(76, 76, 76, 131);
            fog.fogColorMid.value = new Color32(81, 75, 77, 159);
            fog.fogColorEnd.value = new Color32(44, 45, 52, 255);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            sun.SetActive(false);
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
        }
    }
}