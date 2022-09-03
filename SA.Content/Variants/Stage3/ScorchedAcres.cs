using UnityEngine;

namespace StageAesthetic.Variants
{
    internal class ScorchedAcres
    {
        public static void SunsetAcres(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(30, 16, 52, 64);
            fog.fogColorMid.value = new Color32(62, 16, 47, 169);
            fog.fogColorEnd.value = new Color32(84, 3, 68, 255);
            fog.skyboxStrength.value = 0.076f;
            fog.fogZero.value = -0.019f;
            fog.fogOne.value = 0.211f;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            if (SwapVariants.WeatherEffects.Value)
            {
                lightBase.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 0, 135, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.55f;
            sunTransform.localEulerAngles = new Vector3(30, 198.5f, 218.841f);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            Vector3 sunPosition = sunBase.parent.localPosition;
            sunPosition.y = -67;
            Transform quad = sunBase.GetChild(1);
            quad.localScale = new Vector3(14, 14, 14);
            quad.localEulerAngles = new Vector3(270, 30, 0);
            quad.localPosition = new Vector3(0, 0, 0);
            sunBase.GetChild(0).gameObject.SetActive(false);
        }

        public static void MoonAcres(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(45, 49, 75, 30);
            fog.fogColorMid.value = new Color32(26, 25, 62, 130);
            fog.fogColorEnd.value = new Color32(39, 32, 56, 255);
            fog.skyboxStrength.value = 0.03f;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            lightBase.GetChild(1).GetChild(0).gameObject.SetActive(false); // embers
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(173, 175, 245, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.4f;
            sunLight.shadowBias = 0.05f;
            lightBase.Find("CameraRelative").Find("SunHolder").gameObject.SetActive(false);
            var eclipse = GameObject.Find("Weather, Wispgraveyard").scene.GetRootGameObjects()[6];
            if (eclipse != null)
            {
                eclipse.SetActive(true);
                eclipse.transform.GetChild(1).gameObject.SetActive(false); // lighting
                eclipse.transform.GetChild(2).gameObject.SetActive(false); // post-processing
                eclipse.transform.GetChild(4).gameObject.SetActive(false); // weather
                eclipse.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                eclipse.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
                Vector3 moonPosition = eclipse.transform.GetChild(3).GetChild(2).position;
                moonPosition.y = 263;
                eclipse.transform.GetChild(3).GetChild(2).localScale = new Vector3(8, 8, 8);
            }
            /*
            var dummylist = UnityEngine.Object.FindObjectsOfType(typeof(WeatherParticles)) as WeatherParticles[];
            for (var i = 0; i < dummylist.Length; i++)
            {
                Debug.Log(dummylist[i].name);
                Debug.Log(dummylist[i].gameObject.name);
                if (dummylist[i].gameObject.name.Equals("Skybox Assets"))
                {
                    Debug.Log("test");
                    Transform eclipseBase = dummylist[i].gameObject.transform.parent;
                    eclipseBase.gameObject.SetActive(true);
                    eclipseBase.Find("PP + Amb").gameObject.SetActive(false);
                    eclipseBase.Find("Directional Light (SUN)").gameObject.SetActive(false);
                }
            }
            dummylist = null;*/
        }

        public static void OddAcres(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(70, 90, 84, 0);
            fog.fogColorMid.value = new Color32(74, 99, 105, 130);
            fog.fogColorEnd.value = new Color32(77, 113, 85, 255);
            fog.skyboxStrength.value = 0;
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(152, 255, 207, 255);
            sunLight.intensity = 1;
            sunTransform.eulerAngles = new Vector3(75, 115, 180);
            lightBase.Find("CameraRelative").Find("SunHolder").gameObject.SetActive(false);
            lightBase.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            var eclipse = GameObject.Find("Weather, Wispgraveyard").scene.GetRootGameObjects()[6];
            if (eclipse != null)
            {
                eclipse.SetActive(true);
                eclipse.transform.GetChild(1).gameObject.SetActive(false); // lighting
                eclipse.transform.GetChild(2).gameObject.SetActive(false); // post-processing
                eclipse.transform.GetChild(4).GetChild(1).gameObject.SetActive(false); // dust
            }
        }

        public static void VanillaChanges()
        {
            var lightBase = GameObject.Find("Weather, Wispgraveyard").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(255, 135, 0, 255);
            sunLight.intensity = 3f;
            sunTransform.localEulerAngles = new Vector3(36, 0, 0);
            var sunBase = lightBase.Find("CameraRelative").Find("SunHolder").Find("Sphere");
            sunBase.position = new Vector3(-30, 2267, -3200);
            ;
            Transform[] quadCount = new Transform[] { sunBase.GetChild(0), sunBase.GetChild(1) };
            foreach (Transform quad in quadCount)
            {
                quad.localPosition = new Vector3(0, -1, 1);
                quad.localEulerAngles = new Vector3(270, 30, 0);
            }
        }
    }
}