using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage1
{
    internal class DistantRoost
    {
        public static void Vanilla()
        {
            VanillaFoliage();
            Utils.AddRain(Utils.RainType.Drizzle);
        }

        public static void Sunny(RampFog fog, string scenename, ColorGrading cgrade)
        {
            Skybox.DaySky();
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 229, 255);
            sunLight.intensity = 1.8f;
            sunLight.shadowStrength = 0.75f;
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            if (scenename == "blackbeach")
            {
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("HOLDER: Weather Particles").transform.Find("BBSkybox").Find("CameraRelative").Find("Rain").gameObject.SetActive(false);
            }
            VanillaFoliage();
        }

        public static void Night(RampFog fog, string scenename, ColorGrading cgrade)
        {
            Skybox.NightSky();
            if (scenename == "blackbeach")
            {
                // Enabling some unused fog
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("SKYBOX").transform.GetChild(4).gameObject.SetActive(true);
            }
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
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
            VanillaFoliage();
        }

        public static void Void()
        {
            Skybox.VoidSky();
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            var s = GameObject.Find("SKYBOX").transform;
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
            AddSnow(SnowType.Light, -10f);
            // s.GetChild(19).GetChild(0).localPosition = new Vector3(0, 0, -10); something moved/broke with this

            GameObject.Find("HOLDER: Grass").SetActive(false);
            GameObject.Find("FOLIAGE").SetActive(false);
            s.GetChild(6).gameObject.SetActive(false);
            s.GetChild(11).gameObject.SetActive(false);
            VanillaFoliage();
            VoidMaterials();
        }

        public static void Overcast(RampFog fog, string scenename)
        {
            AddRain(RainType.Typhoon);
            fog.fogColorEnd.value = new Color(0.3272f, 0.3711f, 0.4057f, 1);
            fog.fogColorMid.value = new Color(0.2864f, 0.2667f, 0.3216f, 0.4f);
            fog.fogColorStart.value = new Color(0.2471f, 0.2471f, 0.2471f, 0.05f);
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.025f;
            fog.skyboxStrength.value = 0.03f;
            fog.fogIntensity.value = 0.88f;

            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(77, 188, 175, 255);
            sunLight.intensity = 1.7f;
            sunLight.shadowStrength = 0.6f;
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;

            GameObject wind = GameObject.Find("WindZone");
            wind.transform.eulerAngles = new Vector3(30, 20, 0);
            var windZone = wind.GetComponent<WindZone>();
            windZone.windMain = 1;
            windZone.windTurbulence = 1;
            windZone.windPulseFrequency = 0.5f;
            windZone.windPulseMagnitude = 0.5f;
            windZone.mode = WindZoneMode.Directional;
            windZone.radius = 100;

            if (scenename == "blackbeach")
                GameObject.Find("SKYBOX").transform.GetChild(3).gameObject.SetActive(true);

            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];

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
            VanillaFoliage();
        }

        public static void Abyssal(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(99, 27, 63, 25);
            fog.fogColorMid.value = new Color32(26, 61, 91, 150);
            fog.fogColorEnd.value = new Color32(68, 27, 27, 255);
            fog.SetAllOverridesTo(true);
            fog.skyboxStrength.value = 0.1f;
            fog.fogPower.value = 1f;
            fog.fogIntensity.value = 1f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.2f;
            //  cgrade.colorFilter.value = new Color32(150, 150, 150, 255);
            // cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color(0.75f, 0.75f, 0.75f, 1f);
            sunLight.intensity = 5f;
            sunLight.shadowStrength = 0.75f;
            sunLight.transform.eulerAngles = new Vector3(70f, 220f, -9.985f);
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;

            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar") || meshBase.name.Contains("DistantBridge"))
                    {
                        if (mr.sharedMaterial != null)
                        {
                            var light = meshBase.AddComponent<Light>();
                            light.color = new Color32(249, 212, 96, 255);
                            light.intensity = 10f;
                            light.range = 25f;
                        }
                        if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("RuinArch"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                var light = meshBase.AddComponent<Light>();
                                light.color = new Color32(181, 66, 34, 225);
                                light.intensity = 7.5f;
                                light.range = 15f;
                            }
                        }
                    }
                    if (meshBase.name == ("spmBbConif_LOD2"))
                    {
                        meshBase.gameObject.SetActive(false);
                    }
                    var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
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
                                    light.color = new Color32(249, 212, 96, 225);
                                    light.intensity = 16f;
                                    light.range = 35f;
                                }
                            }
                        }
                    }
                }
            }
            AbyssalFoliage();
            AbyssalMaterials();
        }

        public static void VanillaFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("bbSimpleGrassPrefab"))
                    {
                        mr.sharedMaterial.color = new Color32(11, 58, 28, 255);
                    }
                    if (meshBase.name.Contains("spmBbFern2"))
                    {
                        mr.sharedMaterial.color = new Color32(255, 255, 255, 255);
                    }
                    if (meshBase.name.Contains("spmBbFern3"))
                    {
                        mr.sharedMaterial.color = new Color32(229, 229, 229, 255);
                    }
                    if (meshBase.name.Contains("spmBush"))
                    {
                        var color = new Color32(255, 255, 255, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbDryBush"))
                    {
                        var color = new Color32(125, 125, 128, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Ivy"))
                    {
                        var color = new Color32(40, 47, 30, 146);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Vine"))
                    {
                        mr.sharedMaterial.color = new Color32(44, 49, 27, 255);
                    }

                    if (meshBase.name.Contains("spmBbConif_"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        var color = new Color32(125, 125, 128, 255);
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbConifYoung"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        var color = new Color32(125, 125, 128, 255);
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                }
            }
        }

        public static void AbyssalFoliage()
        {
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("bbSimpleGrassPrefab"))
                    {
                        mr.sharedMaterial.color = new Color32(45, 45, 45, 211);
                        meshBase.transform.localScale = new Vector3(5.28f, 3.798217104f, 5.28f);
                    }
                    if (meshBase.name.Contains("spmBbFern"))
                    {
                        meshBase.transform.localScale = new Vector3(2f, 2f, 2f);
                        mr.sharedMaterial.color = new Color32(50, 50, 50, 166);
                        meshBase.transform.localScale = new Vector3(3f, 3f, 3f);
                    }
                    if (meshBase.name.Contains("spmBush"))
                    {
                        var color = new Color32(50, 50, 50, 255);
                        mr.sharedMaterial.color = new Color32(30, 30, 30, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbDryBush"))
                    {
                        var color = new Color32(20, 20, 20, 255);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = color;
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color;
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Ivy"))
                    {
                        var sharedMaterials = mr.sharedMaterials;
                        var color1 = new Color32(58, 58, 58, 146);
                        var color2 = new Color32(119, 119, 119, 133);
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color1;
                            if (i == 1)
                            {
                                sharedMaterials[i].color = color2;
                            }
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("Vine"))
                    {
                        mr.sharedMaterial.color = new Color32(79, 63, 60, 255);
                    }

                    if (meshBase.name.Contains("spmBbConif_"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = new Color32(164, 35, 47, 255);
                        var color1 = new Color32(100, 75, 75, 255);
                        var color2 = new Color32(100, 100, 100, 255);
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color1;
                            if (i == 2)
                            {
                                sharedMaterials[i].color = color2;
                            }
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                    if (meshBase.name.Contains("spmBbConifYoung_L"))
                    {
                        meshBase.SetActive(true);
                        var sharedMaterials = mr.sharedMaterials;
                        // mr.sharedMaterial.color = new Color32(164, 35, 47, 255);
                        var color1 = new Color32(70, 70, 70, 255);
                        var color2 = new Color32(65, 68, 65, 255);
                        for (int i = 0; i < sharedMaterials.Length; i++)
                        {
                            sharedMaterials[i].color = color1;
                            if (i == 2)
                            {
                                sharedMaterials[i].color = color2;
                            }
                        }
                        mr.sharedMaterials = sharedMaterials;
                    }
                }
            }
        }

        public static void VoidMaterials()
        {
            var s = GameObject.Find("SKYBOX").transform;
            /*
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(188, 162, 162, 255);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion());
            terrainMat2.color = new Color32(188, 162, 162, 255);
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainGem.mat").WaitForCompletion();
            var detailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            var water = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat); // WHY DOES LOGGING SOMETHING MAKE IT LOAD MOST OF THE TIME UNITY???
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + water);
            */

            // if (terrainMat && terrainMat2 && detailMat && detailMat2 && water)
            {
                // GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                // GameObject.Find("GAMEPLAY SPACE").transform.GetChild(7).GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                GameObject.Find("GAMEPLAY SPACE").transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = Main.distantRoostVoidTerrainMat;
                GameObject.Find("GAMEPLAY SPACE").transform.GetChild(1).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = Main.distantRoostVoidTerrainMat;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Detail"))
                        {
                            if (mr.sharedMaterial)
                            {
                                // mr.sharedMaterial = detailMat;
                                mr.sharedMaterial = Main.distantRoostVoidDetailMat;
                            }
                        }
                        if (meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("RuinPillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                // mr.sharedMaterial = detailMat2;
                                mr.sharedMaterial = Main.distantRoostVoidDetailMat2;
                            }
                        }
                    }
                    if (meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar"))
                    {
                        if (mr.sharedMaterial)
                        {
                            // mr.sharedMaterial = terrainMat2;
                            mr.sharedMaterial = Main.distantRoostVoidTerrainMat2;
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
            // water.color = new Color32(0, 14, 255, 255);
            // s.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = water;
            s.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = Main.distantRoostVoidWaterMat;
        }

        public static void AbyssalMaterials()
        {
            /*
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            terrainMat.color = new Color32(0, 0, 0, 204);
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());
            terrainMat2.color = new Color32(0, 0, 0, 135);
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/TitanGoldDuringTP/matGoldHeart.mat").WaitForCompletion();
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuins.mat").WaitForCompletion());
            detailMat2.color = new Color32(181, 66, 34, 255);
            var water = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion());
            water.shaderKeywords = new string[] { "_BUMPLARGE_ON", "_DISPLACEMENTMODE_OFF", "_DISPLACEMENT_ON", "_DISTORTIONQUALITY_HIGH", "_EMISSION", "_FOAM_ON", "_NORMALMAP" };

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + terrainMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + detailMat2);
            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + water);
            */
            //if (terrainMat && terrainMat2 && detailMat && detailMat2 && water)
            {
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    var meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if (meshParent.name.Contains("Pillar") && meshParent.transform.Find("Foam") != null)
                            {
                                meshParent.transform.Find("Foam").gameObject.SetActive(false);
                            }
                            if (meshParent.name.Contains("terrain") && meshBase.name.Contains("Pillar"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    // mr.sharedMaterial = terrainMat;
                                    mr.sharedMaterial = Main.distantRoostAbyssalTerrainMat;
                                }
                            }
                            if (meshParent.name.Equals("Foliage") && meshBase.name.Contains("bbSimpleGrassPrefab"))
                            {
                                meshBase.SetActive(false);
                            }
                        }
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Shelf"))
                        {
                            if (mr.sharedMaterial)
                            {
                                // mr.sharedMaterial = terrainMat;
                                mr.sharedMaterial = Main.distantRoostAbyssalTerrainMat;
                            }
                        }
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("boulder") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Step") || meshBase.name.Contains("Tile") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Bowl") || meshBase.name.Contains("Marker") || meshBase.name.Contains("DistantBridge") || meshBase.name.Contains("Pebble"))
                        {
                            if (mr.sharedMaterial)
                            {
                                // mr.sharedMaterial = detailMat;
                                mr.sharedMaterial = Main.distantRoostAbyssalDetailMat;
                            }
                        }
                        if (meshBase.name.Contains("RuinGate") || meshBase.name.Contains("RuinArch") || meshBase.name.Contains("RuinPillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                // mr.sharedMaterial = detailMat2;
                                mr.sharedMaterial = Main.distantRoostAbyssalDetailMat2;
                            }
                        }
                        if (meshBase.name.Contains("DistantPillar") || meshBase.name.Contains("Cliff") || meshBase.name.Contains("ClosePillar"))
                        {
                            if (mr.sharedMaterial)
                            {
                                // mr.sharedMaterial = terrainMat2;
                                mr.sharedMaterial = Main.distantRoostAbyssalTerrainMat2;
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
                // water.color = new Color32(107, 23, 23, 255);
                // GameObject.Find("HOLDER: Water").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
                GameObject.Find("HOLDER: Water").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = Main.distantRoostAbyssalWaterMat;
            }
        }
    }
}