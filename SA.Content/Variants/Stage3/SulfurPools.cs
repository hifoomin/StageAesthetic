using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class SulfurPools
    {
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
            var asgm = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Terrain.mat").WaitForCompletion();
            var asdpm = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_DimondPattern.mat").WaitForCompletion();
            var water = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion();
            var terrain = GameObject.Find("mdlSPTerrain").transform;
            var sphere = GameObject.Find("mdlSPSphere").transform;
            terrain.GetChild(0).localPosition = new Vector3(0f, 0f, -20f);
            terrain.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = water;
            terrain.GetChild(2).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            terrain.GetChild(4).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            terrain.GetChild(7).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            terrain.GetChild(8).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            terrain.GetChild(9).gameObject.SetActive(false);
            terrain.GetChild(10).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            terrain.GetChild(11).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            terrain.GetChild(13).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            sphere.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(3).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(4).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(5).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(6).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(7).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            sphere.GetChild(11).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asgm;
            sphere.GetChild(12).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(13).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            sphere.GetChild(14).gameObject.GetComponent<MeshRenderer>().sharedMaterial = asdpm;
            GameObject.Find("HOLDER: SulfurPods").SetActive(false);
            string[] targets = { "SulfurPodBody(Clone)" };
            foreach (string name in targets)
            {
                GameObject go = GameObject.Find(name);
                //if the tree exist then destroy it
                if (go != null)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}