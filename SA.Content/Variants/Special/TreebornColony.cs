using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using R2API.Utils;
using System.Linq;
using System.Collections.Generic;

namespace StageAesthetic.Variants.Special
{
    internal class TreebornColony
    {
        public static void Night()
        {
            Skybox.NightSky();
            GameObject.Find("meshBHFog").SetActive(false);
            GameObject.Find("Weather, Habitat").SetActive(false);
        }

        public static void Overcast(RampFog fog)
        {
            AddRain(RainType.Typhoon);
            fog.fogColorEnd.value = new Color(0.3272f, 0.3711f, 0.4057f, 1);
            fog.fogColorMid.value = new Color(0.2864f, 0.2667f, 0.3216f, 0.2941f);
            fog.fogColorStart.value = new Color(0.2471f, 0.2471f, 0.2471f, 0);
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;

            GameObject.Find("BHGodRay").SetActive(false);
            GameObject.Find("Directional Light (SUN)").GetComponent<Light>().color = new Color(0.7529f, 0.7137f, 0.6157f, 1);
            GameObject.Find("meshBHFog").GetComponent<MeshRenderer>().sharedMaterial = Main.meridianStormCloudMat;
            GameObject wind = GameObject.Find("WindZone");
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 0.5f;
            windZone.windTurbulence = 0.65f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;
        }

        public static void Sunny(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            // 0.6078 0.6431 0.5373 1
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;
            fog.fogPower.value = 1f;
            GameObject.Find("meshBHFog").SetActive(false);
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            sun.transform.eulerAngles = new Vector3(90, 0, 0); // 64.0354 107.4961 67.9778
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);
        }
    }
}