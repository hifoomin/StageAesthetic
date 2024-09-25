using HG;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage1
{
    internal class SiphonedForest
    {
        public static void Vanilla()
        {
            DisableSiphonedSnow();
            AddSnow(SnowType.Moderate);
            VanillaFoliage();
        }

        public static void Purple(RampFog fog, ColorGrading cgrade)
        {
            Skybox.VoidSky();
            GameObject surroundingTrees = GameObject.Find("Treecards");
            if (surroundingTrees)
                surroundingTrees.SetActive(false);
            //cgrade.colorFilter.value = new Color32(255, 201, 255, 30);
            // cgrade.colorFilter.overrideState = true;

            DisableSiphonedSnow();
            AddSnow(SnowType.Gigachad);
            VanillaFoliage();
        }

        public static void Night(RampFog fog, ColorGrading cgrade)
        {
            Skybox.NightSky(0);

            fog.fogColorStart.value = new Color32(112, 125, 166, 50);
            fog.fogColorMid.value = new Color32(80, 80, 110, 64);
            fog.fogColorEnd.value = new Color32(42, 42, 72, 249);
            fog.skyboxStrength.value = 0.3f;
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            aurora.SetActive(false);
            var godrays = GameObject.Find("Godrays");
            godrays.SetActive(false);
            sunLight.color = new Color32(110, 110, 180, 255);
            sunLight.intensity = 2.5f;
            sunLight.shadowStrength = 0.5f;
            cgrade.colorFilter.value = new Color32(110, 110, 140, 25);
            cgrade.colorFilter.overrideState = true;

            DisableSiphonedSnow();
            AddSnow(SnowType.Heavy);
            VanillaFoliage();

            /*
            GameObject surroundingTrees = GameObject.Find("Treecards");
            if (surroundingTrees)
                surroundingTrees.SetActive(false);
            var aurora = GameObject.Find("mdlSnowyForestAurora");
            aurora.SetActive(false);
            var godrays = GameObject.Find("Godrays");
            godrays.SetActive(false);

            DisableSiphonedSnow(); //
            AddSnow(SnowType.Heavy);
            VanillaFoliage();
            */
        }

        public static void Crimson(RampFog fog, ColorGrading cgrade)
        {
            /*
      // fog end 0.3208 0.1234 0.1044 1
      // fog mid 0.5176 0.3338 0.2706 0.4471
      // fog start 0.7453 0.3527 0.2988 0
            fog.fogColorStart.value = new Color32(140, 70, 70, 0);
            fog.fogColorMid.value = new Color32(120, 50, 40, 75);
            fog.fogColorEnd.value = new Color32(90, 35, 46, 150);
            */
            fog.fogColorStart.value = new Color(0.7453f, 0.3527f, 0.2988f, 0);
            fog.fogColorMid.value = new Color(0.6176f, 0.3338f, 0.2706f, 0.4471f);
            fog.fogColorEnd.value = new Color(0.4208f, 0.1234f, 0.1044f, 0.75f);
            fog.SetAllOverridesTo(true);
            fog.skyboxStrength.value = 0.01f;
            fog.fogPower.value = 1f;
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.02f;
            /*
            fog.fogPower.value = 0.35f;
            fog.fogOne.value = 0.108f;
            fog.fogZero.value = -0.007f;
            */
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(200, 175, 150, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.5f;
            sunLight.transform.eulerAngles = new Vector3(55f, 0f, 0f);
            //  cgrade.colorFilter.value = new Color32(255, 255, 201, 255);
            // cgrade.colorFilter.overrideState = true;

            var skybox = GameObject.Find("HOLDER: Skybox").transform;
            var godrays = skybox.Find("Godrays").gameObject;
            godrays.SetActive(false);

            var cavePP1 = skybox.Find("SFPortalCard").gameObject;
            cavePP1.SetActive(false);

            var cavePP2 = skybox.Find("SFPortalCard (1)").gameObject;
            cavePP2.SetActive(false);

            DisableSiphonedSnow();
            AddRain(RainType.Typhoon, true);
            VanillaFoliage();
        }

        public static void Morning(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(117, 154, 255, 7);
            fog.fogColorMid.value = new Color32(111, 196, 248, 45);
            fog.fogColorEnd.value = new Color32(117, 154, 255, 255);
            fog.skyboxStrength.value = 0.1f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(205, 158, 90, 255);
            sunLight.intensity = 6f;
            sunLight.shadowStrength = 0.88f;
            cgrade.colorFilter.value = new Color32(111, 196, 248, 17);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(40, 153.0076f, 50f);

            DisableSiphonedSnow();
            AddSnow(SnowType.Light);
            VanillaFoliage();
        }

        public static void Desolate(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(206, 117, 255, 5);
            fog.fogColorMid.value = new Color32(228, 144, 255, 40);
            fog.fogColorEnd.value = new Color32(178, 209, 255, 255);
            fog.fogOne.value = 2.4f;
            fog.skyboxStrength.value = 0.1f;
            var shittyNotWorkingSun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            var sunLight = GameObject.Instantiate(shittyNotWorkingSun.gameObject).GetComponent<Light>();
            shittyNotWorkingSun.name = "Shitty Not Working Sun";
            shittyNotWorkingSun.gameObject.SetActive(false);
            var aurora = GameObject.Find("mdlSnowyForestAurora");

            aurora.SetActive(false);
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 5f;
            sunLight.shadowStrength = 0.3f;
            cgrade.colorFilter.value = new Color32(197, 233, 255, 255);
            cgrade.colorFilter.overrideState = true;
            sunLight.transform.localEulerAngles = new Vector3(48f, 333.0076f, 230f);

            var foliage = SceneManager.GetActiveScene().GetRootGameObjects()[3];
            if (foliage)
            {
                var icicles = foliage.transform.GetChild(5);

                icicles.gameObject.SetActive(false);
            }

            DisableSiphonedSnow();
            AddRain(RainType.Rainstorm);
            DesolateFoliage();
            DesolateMaterials();
        }

        public static void DesolateMaterials()
        {
            var terrainMat = Main.siphonedDesolateTerrainMat;
            var detailMat = Main.siphonedDesolateDetailMat;
            var detailMat2 = Main.siphonedDesolateDetailMat2;
            var water = Main.siphonedDesolateWaterMat;
            var detailMat4 = Main.siphonedDesolateDetailMat4;
            var detailMat5 = Main.siphonedDesolateDetailMat5;
            var detailMat6 = Main.siphonedDesolateDetailMat6;

            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            var particleList = Object.FindObjectsOfType(typeof(ParticleSystem)) as ParticleSystem[];
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase && !lightBase.name.Contains("Directional Light (SUN)"))
                {
                    light.color = new Color32(53, 56, 148, 255);
                    light.intensity = 5f;
                    light.range = 120f;
                    var flickerLight = light.GetComponent<FlickerLight>();
                    if (flickerLight)
                        flickerLight.enabled = false;
                }
            }
            foreach (ParticleSystem ps in particleList)
            {
                var particleBase = ps.gameObject;
                if (particleBase)
                {
                    if (particleBase.name.Contains("Fire") || particleBase.name.Contains("HeatGas"))
                    {
                        particleBase.SetActive(false);
                    }
                }
            }
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("SnowPile"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = terrainMat;
                        }
                    }
                    if (meshBase.name == "SF_GiantTreesTops")
                    {
                        meshBase.gameObject.SetActive(false);
                    }
                    if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rock") || meshBase.name.Contains("mdlSFCeilingSpikes"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = detailMat;
                        }
                    }
                    if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("SF_Aqueduct") || meshBase.name == "meshSnowyForestFirepitFloor" || meshBase.name.Contains("meshSnowyForestFirepitRing") || meshBase.name.Contains("meshSnowyForestFirepitJar") || (meshBase.name.Contains("meshSnowyForestPot") && meshBase.name != "meshSnowyForestPotSap") || meshBase.name.Contains("mdlSFHangingLantern") || meshBase.name.Contains("mdlSFBrokenLantern") || meshBase.name.Contains("meshSnowyForestCrate"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = detailMat2;
                        }
                    }
                    if (meshBase.name.Contains("SF_TreeLog") || meshBase.name.Contains("SF_TreeTrunk") || meshBase.name.Contains("SF_GiantTrees") || meshBase.name.Contains("SF_SurroundingTrees"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = detailMat4;
                        }
                    }
                    if (meshBase.name.Contains("mdlSnowyForestTreeStump"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = detailMat5;
                            mr.sharedMaterials[0] = Main.siphonedDesolateTreeRingMat;
                            mr.sharedMaterials[1] = detailMat5;
                        }
                    }
                    if (meshBase.name.Contains("mdlSFHangingLanternRope") || meshBase.name.Contains("mdlSFLanternRope"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = detailMat6;
                        }
                    }
                    if (meshBase.name == "meshSnowyForestFirepitFloor (1)" || meshBase.name.Contains("SF_Sap") || meshBase.name.Contains("goo"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial = water;
                        }
                    }
                    if (meshBase.name == "meshSnowyForestPotSap")
                    {
                        meshBase.SetActive(false);
                    }
                }
            }
        }

        public static void DisableSiphonedSnow()
        {
            if (!Config.Config.WeatherEffects.Value)
            {
                return;
            }
            var skybox = GameObject.Find("HOLDER: Skybox").transform;
            var snowParticles = skybox.Find("CAMERA PARTICLES: SnowParticles").gameObject;
            snowParticles.SetActive(false);
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
                            mr.sharedMaterial.color = new Color32(168, 168, 141, 255);
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

        public static void DesolateFoliage()
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
                            mr.sharedMaterial.color = new Color32(43, 66, 48, 255);
                        }
                        meshBase.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }
                    if (meshBase.name.Contains("spmBbDryBush_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(81, 55, 101, 255);
                            if (mr.sharedMaterials.Length >= 2)
                            {
                                mr.sharedMaterials[1].color = new Color32(25, 87, 71, 255);
                            }
                        }
                        meshBase.transform.localScale = new Vector3(2f, 2f, 2f);
                    }
                }
            }
        }
    }
}