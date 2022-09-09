using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class SulfurPools
    {
        public static void VanillaPools()
        {
            VanillaWater();
        }

        public static void CoralBluePools(RampFog fog)
        {
            fog.skyboxStrength.value = 0.08f;

            fog.fogColorStart.value = new Color32(0, 140, 145, 40);
            fog.fogColorMid.value = new Color32(0, 106, 145, 90);
            fog.fogColorEnd.value = new Color32(0, 140, 145, 190);
            //fog.fogZero.value = -0.019f;
            //fog.fogOne.value = 0.211f;

            var sunTransform = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(130, 163, 204, 255);
            sunLight.useColorTemperature = true;
            sunLight.colorTemperature = 0f;
            sunLight.intensity = 1.6f;
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
        }

        public static void HellOnEarthPools(RampFog fog)
        {
            fog.skyboxStrength.value = 0f;

            fog.fogColorStart.value = new Color32(45, 0, 0, 45);
            fog.fogColorMid.value = new Color32(55, 10, 0, 45);
            fog.fogColorEnd.value = new Color32(65, 0, 15, 190);
            //fog.fogZero.value = -0.019f;
            //fog.fogOne.value = 0.211f;

            var sunTransform = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(204, 130, 139, 255);
            sunLight.intensity = 1.9f;
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
            var terrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Terrain.mat").WaitForCompletion();
            var detailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_DimondPattern.mat").WaitForCompletion();
            var water = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion();
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            var sphere = GameObject.Find("mdlSPSphere").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            terrain.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = water;
            terrain.GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            terrain.GetChild(4).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            terrain.GetChild(7).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            terrain.GetChild(8).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            terrain.GetChild(9).gameObject.SetActive(false);
            terrain.GetChild(10).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            terrain.GetChild(11).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            terrain.GetChild(13).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            sphere.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(3).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(4).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(5).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(6).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(7).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            sphere.GetChild(11).gameObject.GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
            sphere.GetChild(12).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(13).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            sphere.GetChild(14).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat;
            GameObject.Find("HOLDER: SulfurPods").SetActive(false);
            string[] targets = { "SulfurPodBody(Clone)" };
            foreach (string name in targets)
            {
                GameObject go = GameObject.Find(name);
                // annihilate all pods
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
            VanillaWater();
        }

        public static void VoidPools(RampFog fog, ColorGrading cgrade)
        {
            fog.skyboxStrength.value = 0f;

            fog.fogColorStart.value = new Color32(21, 35, 114, 35);
            fog.fogColorMid.value = new Color32(16, 38, 137, 55);
            fog.fogColorEnd.value = new Color32(16, 16, 99, 170);

            var sunTransform = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sunTransform.gameObject.GetComponent<Light>();
            sunLight.color = new Color32(178, 116, 211, 255);
            sunLight.intensity = 1.6f;
            sunLight.shadowStrength = 0.84f;
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
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(255, 255, 255, 96);
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainGem.mat").WaitForCompletion());
            // detailMat.color = new Color32(212, 214, 238, 255);
            var water = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSPWaterGreen.mat").WaitForCompletion();
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            terrain.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = water;
            GameObject.Find("HOLDER: SulfurPods").SetActive(false);
            string[] targets = { "SulfurPodBody(Clone)" };
            foreach (string name in targets)
            {
                GameObject go = GameObject.Find(name);
                // annihilate all pods
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                var meshParent = meshBase.transform.parent;
                if (meshBase != null)
                {
                    if (meshParent != null)
                    {
                    }
                    if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Mountain"))
                    {
                        mr.sharedMaterial = terrainMat;
                    }
                    if (meshBase.name.Contains("meshSPSphere") || meshBase.name.Contains("SPHeatVent") || meshBase.name.Contains("Crystal") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Spikes") || meshBase.name.Contains("Dome") || meshBase.name.Contains("Cave") || meshBase.name.Contains("Eel"))
                    {
                        mr.sharedMaterial = detailMat;
                    }
                    if (meshBase.name.Contains("Moss") || meshBase.name.Contains("SPCoral") || meshBase.name.Contains("HeatGas") || meshBase.name.Contains("Stinky") || meshBase.name.Contains("Grass") || meshBase.name.Contains("Vine"))
                    {
                        meshBase.SetActive(false);
                    }
                }
            }
            VoidWater();
        }

        public static void VanillaWater()
        {
            var waterBlue = GameObject.Find("meshSPWaterBlue").GetComponent<MeshRenderer>().sharedMaterial;
            waterBlue.color = new Color32(255, 219, 0, 255);
            var waterGreen = GameObject.Find("meshSPWaterGreen").GetComponent<MeshRenderer>().sharedMaterial;
            waterGreen.color = new Color32(255, 219, 0, 255);
            var waterRed = GameObject.Find("meshSPWaterRed").GetComponent<MeshRenderer>().sharedMaterial;
            waterRed.color = new Color32(207, 0, 148, 255);
            var waterYellow = GameObject.Find("meshSPWaterYellow").GetComponent<MeshRenderer>().sharedMaterial;
            waterYellow.color = new Color32(255, 219, 0, 255);
        }

        public static void VoidWater()
        {
            var waterBlue = GameObject.Find("meshSPWaterBlue").GetComponent<MeshRenderer>().sharedMaterial;
            waterBlue.color = new Color32(255, 219, 0, 184);
            var waterGreen = GameObject.Find("meshSPWaterGreen").GetComponent<MeshRenderer>().sharedMaterial;
            waterGreen.color = new Color32(0, 255, 229, 255);
            var waterRed = GameObject.Find("meshSPWaterRed").GetComponent<MeshRenderer>().sharedMaterial;
            waterRed.color = new Color32(178, 38, 171, 200);
            var waterYellow = GameObject.Find("meshSPWaterYellow").GetComponent<MeshRenderer>().sharedMaterial;
            waterYellow.color = new Color32(0, 184, 255, 212);
        }
    }
}