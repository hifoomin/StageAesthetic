using System.Collections.Generic;
using System.Text;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace StageAesthetic.Variants.Stage3
{
    internal class FogboundLagoon
    {
        public static void Clear(RampFog fog)
        {
            fog.fogPower.value = 1f;
            fog.fogColorStart.value = new Color32(130, 126, 27, 5);
            fog.fogColorMid.value = new Color32(84, 116, 117, 61);
            fog.fogColorEnd.value = new Color32(95, 135, 181, 255);
            fog.fogOne.value = 0.7f;
            fog.fogZero.value = -0.04f;
            var sun = GameObject.Find("HOLDER: Lights, FX, Wind").transform.GetChild(0);
            var sun2 = Object.Instantiate(sun);
            sun.gameObject.SetActive(false);
            var newSun = sun2.GetComponent<Light>();
            newSun.intensity = 1f;
            newSun.shadowStrength = 0.7f;

            var waterPP = GameObject.Find("HOLDER: Water").transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>();
            var fog2 = waterPP.profile.GetSetting<RampFog>();
            fog2.fogColorStart.value = new Color32(130, 126, 27, 0);
            fog2.fogColorMid.value = new Color32(77, 142, 136, 61);
            fog2.fogColorEnd.value = new Color32(78, 110, 123, 230);
            fog2.fogIntensity.value = 1f;
            fog2.fogPower.value = 1f;
            fog2.fogZero.value = 0f;
            fog2.fogOne.value = 0.2f;
            var cg = waterPP.profile.GetSetting<ColorGrading>();
            cg.colorFilter.value = new Color32(191, 255, 244, 255);
        }

        public static void Twilight(RampFog fog)
        {
            fog.fogIntensity.value = 0.8f;
            fog.fogPower.value = 0.43f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.4f;
            fog.fogColorStart.value = new Color32(120, 113, 158, 0);
            fog.fogColorMid.value = new Color32(128, 102, 72, 57);
            fog.fogColorEnd.value = new Color32(85, 74, 91, 233);
            fog.skyboxStrength.value = 0.02f;
            var sun = GameObject.Find("HOLDER: Lights, FX, Wind").transform.GetChild(0);
            var sun2 = Object.Instantiate(sun);
            sun.gameObject.SetActive(false);
            var newSun = sun2.GetComponent<Light>();
            newSun.intensity = 0.7f;
            newSun.color = new Color32(255, 213, 250, 255);
            var waterPP = GameObject.Find("HOLDER: Water").transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>();
            var fog2 = waterPP.profile.GetSetting<RampFog>();
            var cg = waterPP.profile.GetSetting<ColorGrading>();
            fog2.fogColorStart.value = new Color32(255, 193, 238, 21);
            fog2.fogColorMid.value = new Color32(100, 84, 107, 36);
            fog2.fogColorEnd.value = new Color32(91, 65, 86, 255);
        }

        public static void Overcast(RampFog fog, ColorGrading cg)
        {
            fog.fogColorStart.value = new Color32(140, 117, 150, 37);
            fog.fogColorMid.value = new Color32(84, 89, 117, 50);
            fog.fogColorEnd.value = new Color32(74, 87, 91, 255);
            fog.skyboxStrength.value = 0.015f;
            fog.fogZero.value = -0.12f;
            cg.SetAllOverridesTo(true);
            cg.colorFilter.value = new Color32(136, 157, 162, 255);
            var wc = GameObject.Find("HOLDER: Weather");
            var waterPP = GameObject.Find("HOLDER: Water").transform.GetChild(0).GetChild(0).GetComponent<PostProcessVolume>();
            waterPP.weight = 1f;
            var fog2 = waterPP.profile.GetSetting<RampFog>();
            var cg2 = waterPP.profile.GetSetting<ColorGrading>();
            var vn = waterPP.profile.GetSetting<Vignette>();
            vn.SetAllOverridesTo(true);
            vn.intensity.value = 0.2f;
            vn.color.value = new Color32(89, 86, 138, 255);
            cg2.SetAllOverridesTo(true);
            cg2.contrast.value = 20f;
            cg2.colorFilter.value = new Color32(83, 197, 153, 255);
            cg2.tint.value = 30f;
            fog2.fogColorStart.value = new Color32(249, 193, 255, 62);
            fog2.fogColorMid.value = new Color32(67, 89, 89, 13);
            fog2.fogColorEnd.value = new Color32(53, 62, 51, 255);
            fog2.fogOne.value = 0.06f;
            AddRain(RainType.Typhoon);

            var wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
        }
    }
}