using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace StageAesthetic.Variants
{
    internal class DistantRoost
    {
        public static void VanillaBeach(GameObject rain, string scenename)
        {
            if (Config.WeatherEffects.Value && scenename == "blackbeach2") UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
        }

        public static void LightBeach(RampFog fog, string scenename, ColorGrading cgrade)
        {
            // Much more sun than vanilla roost - almost enough to give it a cel-shaded look on lower texture settings. The whiter lighting allows the green/brown elements to stand out more.
            fog.fogColorStart.value = new Color32(107, 125, 123, 25);
            fog.fogColorMid.value = new Color32(129, 154, 152, 69);
            fog.fogColorEnd.value = new Color32(156, 194, 189, 114);
            fog.skyboxStrength.value = 0.13f;
            fog.fogZero.value = 0;
            fog.fogOne.value = 0.142f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.8f;
            sunLight.shadowStrength = 1;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            if (scenename == "blackbeach")
            {
                // There's unused fog assets here too, enabling those
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
                // Removing rain
                GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
            }
        }

        public static void DarkBeach(RampFog fog, string scenename, ColorGrading cgrade)
        {
            // Dark and purple, not much else to say here really
            fog.fogColorStart.value = new Color32(24, 20, 43, 32);
            fog.fogColorMid.value = new Color32(33, 25, 49, 130);
            fog.fogColorEnd.value = new Color32(43, 35, 62, 255);
            fog.skyboxStrength.value = 0.03f;
            fog.fogPower.value = 0.6f;
            cgrade.colorFilter.value = new Color32(197, 182, 249, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(106, 69, 160, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.45f;
            if (scenename == "blackbeach")
            {
                // Enabling some unused fog
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 15;
                            light.range = 50;
                        }
                    }
                }
            }
        }

        public static void VoidBeach(RampFog fog, ColorGrading cgrade)
        {
            // Dark and purple, not much else to say here really
            fog.fogColorStart.value = new Color32(40, 19, 40, 32);
            fog.fogColorMid.value = new Color32(48, 25, 48, 255);
            fog.fogColorEnd.value = new Color32(61, 34, 58, 255);
            fog.skyboxStrength.value = 0.03f;
            fog.fogPower.value = 0.35f;
            fog.fogIntensity.value = 0.99f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.25f;
            cgrade.colorFilter.value = new Color32(213, 183, 247, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(152, 69, 158, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.45f;
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 9;
                            light.range = 70;
                            light.color = new Color32(109, 58, 119, 140);
                        }
                    }
                }
            }
            var vfm = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion());
            vfm.color = new Color32(188, 162, 162, 255);
            var vfme = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion());
            vfme.color = new Color32(188, 162, 162, 255);
            var vfmg = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainGem.mat").WaitForCompletion();
            var vfmh = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = vfm;
            GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = vfm;
            GameObject.Find("HOLDER: Grass").SetActive(false);
            GameObject.Find("FOLIAGE").SetActive(false);
            var s = GameObject.Find("SKYBOX").transform;
            s.GetChild(6).gameObject.SetActive(false);
            s.GetChild(11).gameObject.SetActive(false);
            s.GetChild(19).GetChild(0).localPosition = new Vector3(0, 0, -10);
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = vfmg;
                        }
                    }
                    if (meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = vfmh;
                        }
                    }
                    if (meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = vfme;
                        }
                    }
                    if (meshBase.name.Contains("Decal") || meshBase.name.Contains("spmBbFern2"))
                    {
                        meshBase.SetActive(false);
                    }
                    if (meshBase.name.Contains("GlowyBall"))
                    {
                        mr.sharedMaterial.color = new Color32(109, 58, 119, 140);
                    }
                }
            }
            var water = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            water.color = new Color32(0, 14, 255, 255);
            s.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = water;
        }

        public static void FoggyBeach(RampFog fog, string scenename, GameObject rain)
        {
            // Stormy weather. Might be a bit too murky
            fog.fogColorStart.value = new Color32(31, 46, 63, 50);
            fog.fogColorMid.value = new Color(0.205f, 0.269f, 0.288f, 0.76f);
            fog.fogColorEnd.value = new Color32(71, 82, 88, 255);
            fog.skyboxStrength.value = 0.02f;
            fog.fogPower.value = 0.35f;
            fog.fogIntensity.value = 0.99f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1.7f;
            sunLight.shadowStrength = 0.6f;
            // Strong rain
            if (Config.WeatherEffects.Value)
            {
                if (scenename == "blackbeach") GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 3000,
                    constantMax = 3000,
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
                rain.transform.eulerAngles = new Vector3(75, 20, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
                GameObject wind = GameObject.Find("WindZone");
                wind.transform.eulerAngles = new Vector3(30, 20, 0);
                var windZone = wind.GetComponent<WindZone>();
                windZone.windMain = 1;
                windZone.windTurbulence = 1;
                windZone.windPulseFrequency = 0.5f;
                windZone.windPulseMagnitude = 0.5f;
                windZone.mode = WindZoneMode.Directional;
                windZone.radius = 100;
            }
            // Enabling some unused fog
            if (scenename == "blackbeach") GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
            // Aiding visibility by increasing the lighting effect of the crystal pillars in both stages
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (lightParent.gameObject.name.Equals("BbRuinBowl") || lightParent.gameObject.name.Equals("BbRuinBowl (1)") || lightParent.gameObject.name.Equals("BbRuinBowl (2)"))
                        {
                            light.intensity = 10;
                            light.range = 30;
                        }
                    }
                }
            }
        }
    }
}