using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage1
{
    internal class TitanicPlains
    {
        public static void Sunset(RampFog fog)
        {
            Skybox.SunsetSky();

            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 126);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 200);
            fog.skyboxStrength.value = 0.02f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1.1f;
            fog.fogPower.value = 0.8f;

            GameObject weather = GameObject.Find("Weather, Golemplains");
            if (weather)
                weather.SetActive(false);
            VanillaFoliage();
        }

        public static void Overcast(RampFog fog, string scenename)
        {
            fog.fogColorStart.value = new Color32(34, 45, 62, 18);
            fog.fogColorMid.value = new Color32(72, 84, 103, 165);
            fog.fogColorEnd.value = new Color32(97, 109, 129, 200);
            fog.skyboxStrength.value = 0.075f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;

            var lightBase = GameObject.Find("Weather, Golemplains").transform;
            var sunTransform = lightBase.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1.5f;
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
            VanillaFoliage();
        }

        public static void Night(RampFog fog, ColorGrading cgrade)
        {
            Skybox.NightSky();
            var weather = GameObject.Find("Weather, Golemplains");
            weather.SetActive(false);
            Object.Instantiate(rain, Vector3.zero, Quaternion.identity);
            VanillaFoliage();
        }

        public static void Nostalgic(RampFog fog)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color(0.7450981f, 0.8999812f, 0.9137255f);
            sun.intensity = 1.34f;

            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            Debug.Log("NOSTALGIA PLAINS W");
            VanillaFoliage();
        }

        public static void Abandoned(RampFog fog, PostProcessProfile ppProfile)
        {
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color(1f, 0.65f, 0.5f, 1f);
            sun.intensity = 1f;

            RampFog rampFog = ppProfile.GetSetting<RampFog>();

            fog.fogColorStart.value = new Color(0.59f, 0.363f, 0.374f, 0f);
            fog.fogColorMid.value = new Color(0.68f, 0.486f, 0.331f, 0.25f);
            fog.fogColorEnd.value = new Color(0.87f, 0.839f, 0.482f, 0.5f);
            fog.fogZero.value = rampFog.fogZero.value;
            fog.fogIntensity.value = rampFog.fogIntensity.value;
            fog.fogPower.value = rampFog.fogPower.value;
            fog.fogOne.value = rampFog.fogOne.value;
            fog.skyboxStrength.value = 0.01f;

            var terrainMat = Main.plainsAbandonedTerrainMat;
            var detailMat = Main.plainsAbandonedDetailMat;
            var detailMat2 = Main.plainsAbandonedDetailMat2;
            var detailMat3 = Main.plainsAbandonedDetailMat3;
            var tar = Main.plainsAbandonedWaterMat;

            GameObject.Find("FOLIAGE: Grass").SetActive(false);
            var water = GameObject.Find("HOLDER: Water").transform.GetChild(0);
            water.localPosition = new Vector3(-564.78f, -170f, 133.4f);
            water.localScale = new Vector3(200f, 200f, 200f);
            AddSand(SandType.Gigachad);
            VanillaFoliage();
            if (terrainMat && detailMat && detailMat2 && detailMat3 && tar)
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
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("Ring"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                        if (meshBase.name.Contains("Block") || meshBase.name.Contains("MiniBridge") || meshBase.name.Contains("Circle"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
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

        public static void VanillaFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("spmGPGrass_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(96, 94, 32, 255);
                        }
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(125, 125, 128, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(125, 125, 128, 255);
                            }
                        }
                    }
                }
            }
        }
    }
}