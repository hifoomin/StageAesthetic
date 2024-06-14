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
            AddRain(RainType.Monsoon);
            ChangeFlames(Main.moonRedFlameMat, new Color32(156, 31, 33, 255));
        }

        public static void Corruption(RampFog fog)
        {
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.3f;
            fog.fogColorStart.value = new Color32(77, 23, 107, 45);
            fog.fogColorMid.value = new Color32(104, 44, 107, 105);
            fog.fogColorEnd.value = new Color32(75, 0, 75, 255);
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
            AddRain(RainType.Typhoon);
            ChangeFlames(Main.moonPurpleFlameMat, new Color32(77, 23, 107, 255));
        }

        public static void Gray(RampFog fog)
        {
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 1f;
            fog.fogZero.value = -0.01f;
            fog.fogOne.value = 0.1f;
            fog.fogColorStart.value = new Color32(120, 120, 120, 50);
            fog.fogColorMid.value = new Color32(100, 100, 100, 100);
            fog.fogColorEnd.value = new Color32(90, 90, 90, 200);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)");
            sun.SetActive(false);
            sun.name = "Shitty Not Working Sun";
            var newSun = Object.Instantiate(sun).GetComponent<Light>();
            newSun.name = "Directional Light (SUN)";
            newSun.color = new Color32(53, 94, 225, 255);
            newSun.intensity = 0.5f;
            newSun.shadowStrength = 1f;
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

        private static void ChangeFlames(Material flameMat, Color flameColor)
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if ((meshBase.name.Contains("BazaarLight") || meshBase.name.Contains("mdlLunarCoolingBowlLarge")) && mr.sharedMaterial)
                    {
                        ParticleSystemRenderer fire = meshBase.transform.GetComponentInChildren<ParticleSystemRenderer>();
                        if (fire)
                        {
                            fire.sharedMaterial = flameMat;
                            Light fireLight = meshBase.transform.GetComponentInChildren<Light>();
                            if (fireLight)
                                fireLight.color = flameColor;
                        }
                    }
                }
            }
        }

        /* there are some things that are excluded like the bowls at soul and the chimera parts but this is all the important stuff
                public static void MoonMaterialSwap(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
                {
                    if (terrainMat && detailMat && detailMat2 && detailMat3)
                    {
                        MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                        foreach (MeshRenderer renderer in meshList)
                        {
                            GameObject meshBase = renderer.gameObject;
                            if (meshBase != null)
                            {
                                if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                                {
                                    GameObject.Destroy(meshBase);
                                }
                                if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("terrain") || meshBase.name.Contains("OuterRing_Cave")) && renderer.sharedMaterial)
                                    renderer.sharedMaterial = terrainMat;
                                if (meshBase.name.Contains("Rock") && renderer.sharedMaterial)
                                    renderer.sharedMaterial = detailMat;
                                if ((meshBase.name.Contains("Bridge") || meshBase.name.Contains("MoonPillar") || meshBase.name.Contains("Roof") || meshBase.name.Contains("Chain") || meshBase.name.Contains("Arena") || meshBase.name.Contains("otherside") || meshBase.name.Contains("MoonPlatform") || meshBase.name.Equals("ramp up") || meshBase.name.Equals("railing") || (meshBase.name.Contains("Quarry") && !meshBase.name.Contains("Fog") && !meshBase.name.Contains("Rock")) || meshBase.name.Contains("Connector") || meshBase.name.Contains("Octagon") || meshBase.name.Contains("Temple") || meshBase.name.Contains("Column") || meshBase.name.Contains("CoolingBowlLarge") || (meshBase.name.Contains("Wall") && !meshBase.name.Contains("CylinderWall") && !meshBase.name.Contains("Godray")) || meshBase.name.Contains("Floor") || meshBase.name.Contains("Barrier") || meshBase.name.Contains("WaterTrough") || meshBase.name.Contains("Busted") || meshBase.name.Contains("disc") || meshBase.name == "Base") && renderer.sharedMaterial)
                                    renderer.sharedMaterial = detailMat2;
                                if ((meshBase.name.Contains("mdlroot") || meshBase.name.Contains("Stib")) && renderer.sharedMaterial)
                                    renderer.sharedMaterial = detailMat3;
                            }
                        }
                    }
                }
        */
    }
}