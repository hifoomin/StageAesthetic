using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Experimental.TerrainAPI.TerrainUtility;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage3
{
    internal class SulfurPools
    {
        public static void Vanilla()
        {
            VanillaWater();
        }

        public static void Coral(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(127, 127, 153, 25);
            fog.fogColorMid.value = new Color32(0, 106, 145, 150);
            fog.fogColorEnd.value = new Color32(0, 115, 119, 255);
            fog.fogZero.value = -0.01f;
            fog.fogOne.value = 0.15f;
            fog.fogPower.value = 2f;
            fog.skyboxStrength.value = 0.1f;

            var sunTransform = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(130, 163, 204, 255);
            sunLight.useColorTemperature = true;
            sunLight.colorTemperature = 0f;
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.7f;
            var fogg = GameObject.Find("mdlSPTerrain");
            fogg.transform.GetChild(3).gameObject.SetActive(false);
            fogg.transform.GetChild(5).gameObject.SetActive(false);
            fogg.transform.GetChild(12).gameObject.SetActive(false);
            fogg.transform.GetChild(14).gameObject.SetActive(false);
            var goofyAhh = GameObject.Find("PP + Amb").GetComponent<PostProcessVolume>().sharedProfile;
            try { goofyAhh.RemoveSettings<DepthOfField>(); } catch { }
            try { goofyAhh.RemoveSettings<Bloom>(); } catch { }
            try { goofyAhh.RemoveSettings<Vignette>(); } catch { }
            var fuckYou = GameObject.Find("HOLDER: Skybox");
            fuckYou.transform.GetChild(10).gameObject.SetActive(false);
            fuckYou.transform.GetChild(11).gameObject.SetActive(false);
            fuckYou.transform.GetChild(12).gameObject.SetActive(false);
            fuckYou.transform.GetChild(13).gameObject.SetActive(false);
            GameObject.Find("SPCavePP").SetActive(false);
            VanillaWater();
            AddRain(RainType.Drizzle);
        }

        public static void Hell(RampFog fog, ColorGrading cgrade)
        {
            // cgrade.SetAllOverridesTo(true);
            // cgrade.colorFilter.value = new Color32(181, 178, 219, 255);
            fog.fogColorStart.value = new Color32(102, 51, 40, 81);
            fog.fogColorMid.value = new Color32(56, 87, 89, 93);
            fog.fogColorEnd.value = new Color32(104, 23, 54, 200);
            fog.skyboxStrength.value = 0.05f;

            var sunTransform = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color(0.75f, 0.75f, 0.75f, 1f);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.6f;
            var fogg = GameObject.Find("mdlSPTerrain");
            fogg.transform.GetChild(3).gameObject.SetActive(false);
            fogg.transform.GetChild(5).gameObject.SetActive(false);
            fogg.transform.GetChild(12).gameObject.SetActive(false);
            fogg.transform.GetChild(14).gameObject.SetActive(false);
            var goofyAhh = GameObject.Find("PP + Amb").GetComponent<PostProcessVolume>().sharedProfile;
            try { goofyAhh.RemoveSettings<DepthOfField>(); } catch { }
            try { goofyAhh.RemoveSettings<Bloom>(); } catch { }
            try { goofyAhh.RemoveSettings<Vignette>(); } catch { }
            var fuckYou = GameObject.Find("HOLDER: Skybox");
            fuckYou.transform.GetChild(10).gameObject.SetActive(false);
            fuckYou.transform.GetChild(11).gameObject.SetActive(false);
            fuckYou.transform.GetChild(12).gameObject.SetActive(false);
            fuckYou.transform.GetChild(13).gameObject.SetActive(false);
            GameObject.Find("SPCavePP").SetActive(false);
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            terrain.GetChild(9).gameObject.SetActive(false);
            AddRain(RainType.Typhoon, true);
            VanillaWater();
            HellMaterials();
        }

        public static void Void(RampFog fog, ColorGrading cgrade)
        {
            var sunTransform = GameObject.Find("Directional Light (SUN)");
            sunTransform.gameObject.SetActive(false);
            var goofyAhh = GameObject.Find("PP + Amb");
            if (goofyAhh)
                goofyAhh.gameObject.SetActive(false);
            Skybox.VoidSky();

            var fogg = GameObject.Find("mdlSPTerrain");
            fogg.transform.GetChild(3).gameObject.SetActive(false);
            fogg.transform.GetChild(5).gameObject.SetActive(false);
            fogg.transform.GetChild(12).gameObject.SetActive(false);
            fogg.transform.GetChild(14).gameObject.SetActive(false);
            var fuckYou = GameObject.Find("HOLDER: Skybox");
            fuckYou.transform.GetChild(10).gameObject.SetActive(false);
            fuckYou.transform.GetChild(11).gameObject.SetActive(false);
            fuckYou.transform.GetChild(12).gameObject.SetActive(false);
            fuckYou.transform.GetChild(13).gameObject.SetActive(false);
            GameObject.Find("SPCavePP").SetActive(false);

            var terrainMat = Main.spVoidTerrainMat;
            var detailMat = Main.spVoidDetailMat;
            var water = Main.spVoidWaterMat;

            var terrain = GameObject.Find("mdlSPTerrain").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            if (terrainMat && detailMat && water)
            {
                terrain.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = water;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Mountain"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = terrainMat;
                            }
                        }
                        if (meshBase.name.Contains("meshSPSphere") || meshBase.name.Contains("SPHeatVent") || meshBase.name.Contains("Crystal") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Spikes") || meshBase.name.Contains("Dome") || meshBase.name.Contains("Cave") || meshBase.name.Contains("Eel"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("Moss") || meshBase.name.Contains("SPCoral") || meshBase.name.Contains("HeatGas") || meshBase.name.Contains("Stinky") || meshBase.name.Contains("Grass") || meshBase.name.Contains("Vine"))
                        {
                            meshBase.SetActive(false);
                        }
                    }
                }
            }

            VoidWater();
        }

        public static void VanillaWater()
        {
            var waterBlue = GameObject.Find("meshSPWaterBlue").GetComponent<MeshRenderer>().sharedMaterial;
            var waterGreen = GameObject.Find("meshSPWaterGreen").GetComponent<MeshRenderer>().sharedMaterial;
            var waterRed = GameObject.Find("meshSPWaterRed").GetComponent<MeshRenderer>().sharedMaterial;
            var waterYellow = GameObject.Find("meshSPWaterYellow").GetComponent<MeshRenderer>().sharedMaterial;
            if (waterBlue && waterGreen && waterRed && waterYellow)
            {
                waterBlue.color = new Color32(255, 219, 0, 255);
                waterGreen.color = new Color32(255, 219, 0, 255);
                waterRed.color = new Color32(207, 0, 148, 255);
                waterYellow.color = new Color32(255, 219, 0, 255);
            }
        }

        public static void VoidWater()
        {
            var waterBlue = GameObject.Find("meshSPWaterBlue").GetComponent<MeshRenderer>().sharedMaterial;
            var waterGreen = GameObject.Find("meshSPWaterGreen").GetComponent<MeshRenderer>().sharedMaterial;
            var waterRed = GameObject.Find("meshSPWaterRed").GetComponent<MeshRenderer>().sharedMaterial;
            var waterYellow = GameObject.Find("meshSPWaterYellow").GetComponent<MeshRenderer>().sharedMaterial;
            if (waterBlue && waterGreen && waterRed && waterYellow)
            {
                waterBlue.color = new Color32(255, 219, 0, 184);
                waterGreen.color = new Color32(0, 255, 229, 255);
                waterRed.color = new Color32(178, 38, 171, 200);
                waterYellow.color = new Color32(0, 184, 255, 212);
            }
        }

        public static void HellMaterials()
        {
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            var sphere = GameObject.Find("mdlSPSphere").transform;
            // var terrainMat = GameObject.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Terrain.mat").WaitForCompletion());
            var terrainMat = Main.spHellTerrainMat;
            var detailMat = Main.spHellDetailMat;
            var water = Main.spHellWaterMat;

            var zero = terrain.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            var one = terrain.GetChild(2).gameObject.GetComponent<MeshRenderer>();
            var two = terrain.GetChild(4).gameObject.GetComponent<MeshRenderer>();
            var three = terrain.GetChild(7).gameObject.GetComponent<MeshRenderer>();
            var four = terrain.GetChild(8).gameObject.GetComponent<MeshRenderer>();
            var five = terrain.GetChild(10).gameObject.GetComponent<MeshRenderer>();
            var six = terrain.GetChild(11).gameObject.GetComponent<MeshRenderer>();
            var seven = terrain.GetChild(13).gameObject.GetComponent<MeshRenderer>();
            var eight = sphere.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            var nine = sphere.GetChild(3).gameObject.GetComponent<MeshRenderer>();
            var ten = sphere.GetChild(4).gameObject.GetComponent<MeshRenderer>();
            var eleven = sphere.GetChild(5).gameObject.GetComponent<MeshRenderer>();
            var twelve = sphere.GetChild(6).gameObject.GetComponent<MeshRenderer>();
            var thirteen = sphere.GetChild(7).gameObject.GetComponent<MeshRenderer>();
            var fourteen = sphere.GetChild(11).gameObject.GetComponent<MeshRenderer>();
            var fifteen = sphere.GetChild(12).gameObject.GetComponent<MeshRenderer>();
            var sixteen = sphere.GetChild(13).gameObject.GetComponent<MeshRenderer>();
            var seventeen = sphere.GetChild(14).gameObject.GetComponent<MeshRenderer>();
            if (terrainMat && detailMat && water)
            {
                if (zero.sharedMaterial)
                    zero.sharedMaterial = water;
                if (one.sharedMaterial)
                    one.sharedMaterial = terrainMat;
                if (two.sharedMaterial)
                    two.sharedMaterial = terrainMat;
                if (three.sharedMaterial)
                    three.sharedMaterial = detailMat;
                if (four.sharedMaterial)
                    four.sharedMaterial = terrainMat;
                if (five.sharedMaterial)
                    five.sharedMaterial = detailMat;
                if (six.sharedMaterial)
                    six.sharedMaterial = terrainMat;
                if (seven.sharedMaterial)
                    seven.sharedMaterial = terrainMat;
                if (eight.sharedMaterial)
                    eight.sharedMaterial = detailMat;
                if (nine.sharedMaterial)
                    nine.sharedMaterial = detailMat;
                if (ten.sharedMaterial)
                    ten.sharedMaterial = detailMat;
                if (eleven.sharedMaterial)
                    eleven.sharedMaterial = detailMat;
                if (twelve.sharedMaterial)
                    twelve.sharedMaterial = detailMat;
                if (thirteen.sharedMaterial)
                    thirteen.sharedMaterial = terrainMat;
                if (fourteen.sharedMaterial)
                    fourteen.sharedMaterial = terrainMat;
                if (fifteen.sharedMaterial)
                    fifteen.sharedMaterial = detailMat;
                if (sixteen.sharedMaterial)
                    sixteen.sharedMaterial = detailMat;
                if (seventeen.sharedMaterial)
                    seventeen.sharedMaterial = detailMat;
                // the shit j guh gah
            }
        }
    }
}