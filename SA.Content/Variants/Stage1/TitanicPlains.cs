using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.RemoteConfigSettingsHelper;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage1
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

        public static void RainyPlains(RampFog fog, string scenename)
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
            AddRain(RainType.Rainstorm);
            if (scenename == "golemplains")
            {
                GameObject wind = GameObject.Find("WindZone");
                wind.transform.eulerAngles = new Vector3(30, 145, 0);
                var windZone = wind.GetComponent<WindZone>();
                windZone.windMain = 0.4f;
                windZone.windTurbulence = 0.8f;
            }
        }

        public static void NightPlains(RampFog fog, ColorGrading cgrade)
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

        public static void SandyPlains(RampFog fog)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(211, 151, 105, 255);
            sun.intensity = 1.05f;
            fog.fogColorStart.value = new Color32(137, 122, 83, 9);
            fog.fogColorMid.value = new Color32(150, 119, 82, 20);
            fog.fogColorEnd.value = new Color32(173, 138, 95, 170);
            fog.fogZero.value = -0.035f;
            fog.skyboxStrength.value = 0.25f;
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(230, 223, 174, 219);
            var detail = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimSandy.mat").WaitForCompletion();
            var detail2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrimLightSand.mat").WaitForCompletion();
            var detail3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolakeStoneTrim.mat").WaitForCompletion();
            var tar = Addressables.LoadAssetAsync<Material>("RoR2/Base/goolake/matGoolake.mat").WaitForCompletion();
            GameObject.Find("FOLIAGE: Grass").SetActive(false);
            var water = GameObject.Find("HOLDER: Water").transform.GetChild(0);
            water.localPosition = new Vector3(-564.78f, -170f, 133.4f);
            water.localScale = new Vector3(200f, 200f, 200f);
            if (terrainMat && detail && detail2 && detail3 && tar)
            {
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name == ("Wall North"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detail;
                            }
                        }
                        if (meshBase.name.Contains("Ring"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detail2;
                            }
                        }
                        if (meshBase.name.Contains("Block") || meshBase.name.Contains("MiniBridge") || meshBase.name.Contains("Circle"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detail3;
                            }
                        }
                        if (meshBase.name.Contains("Plane1x1x100x100"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = tar;
                            }
                        }
                    }
                }
            }
        }
    }
}