using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class RallypointDelta
    {
        public static void OceanWall(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(47, 52, 62, 80);
            fog.fogColorMid.value = new Color32(72, 80, 98, 212);
            fog.fogColorEnd.value = new Color32(90, 101, 119, 255);
            fog.skyboxStrength.value = 0.15f;
            fog.fogZero.value = -0.05f;
            fog.fogOne.value = 0.4f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            GameObject.Find("HOLDER: Skybox").transform.Find("Water").localPosition = new Vector3(-1260, -66, 0);
            sunLight.color = new Color32(177, 184, 200, 255);
            sunLight.intensity = 1.2f;
        }

        public static void NightWall(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(33, 33, 56, 76);
            fog.fogColorMid.value = new Color32(38, 38, 55, 165);
            fog.fogColorEnd.value = new Color32(25, 24, 46, 255);
            fog.skyboxStrength.value = 0.7f;
            cgrade.colorFilter.value = new Color32(130, 123, 255, 255);
            cgrade.colorFilter.overrideState = true;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(127, 168, 217, 255);
            sunLight.intensity = 0.9f;
            sunLight.shadowStrength = 0.4f;
            GameObject.Find("Directional Light (SUN)").transform.eulerAngles = new Vector3(50, 275, 2);
        }

        public static void GreenWall(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(42, 72, 68, 0);
            fog.fogColorMid.value = new Color32(50, 68, 61, 163);
            fog.fogColorEnd.value = new Color32(35, 62, 52, 255);
            fog.skyboxStrength.value = 0.45f;
            cgrade.colorFilter.value = new Color32(178, 255, 230, 255);
            cgrade.colorFilter.overrideState = true;
        }

        public static void TitanicWall(RampFog fog, ColorGrading cgrade)
        {
            try { ApplyTitanicMaterials(); } catch { SwapVariants.AesLog.LogError("Titanic Delta: Failed to change materials, trying again..."); } finally { ApplyTitanicMaterials(); }
            fog.fogColorStart.value = new Color32(116, 153, 173, 12);
            fog.fogColorMid.value = new Color32(88, 130, 153, 45);
            fog.fogColorEnd.value = new Color32(79, 140, 173, 255);
            fog.skyboxStrength.value = 0f;
            // cgrade.colorFilter.value = new Color32(178, 255, 230, 255);
            // cgrade.colorFilter.overrideState = true;
            var sunLight = Object.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
            GameObject.Find("Directional Light (SUN)").SetActive(false);
            sunLight.color = new Color32(255, 212, 153, 255);
            sunLight.intensity = 2f;
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null && !lightBase.name.Contains("Light (SUN)"))
                {
                    light.color = new Color32(255, 185, 0, 255);
                    light.intensity = 0.08f;
                    light.range = 4f;
                }
            }
            GameObject.Find("CAMERA PARTICLES: SnowParticles").SetActive(false);
            GameObject.Find("STATIC PARTICLES: Cave Entrance System").SetActive(false);
            GameObject.Find("HOLDER: ShippingCenter").transform.GetChild(3).gameObject.SetActive(false);
        }

        public static void ApplyTitanicMaterials()
        {
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPTerrain.mat").WaitForCompletion());
            terrainMat.color = new Color32(0, 2, 185, 245);
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/golemplains/matGPBoulderMossyProjected.mat").WaitForCompletion());
            detailMat.color = new Color32(0, 52, 146, 78);
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetGoldRuinsProjectedHuge.mat").WaitForCompletion());
            detailMat2.color = new Color32(255, 185, 0, 255);
            var bloodShrineMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/ShrineBlood/matShrineBlood.mat").WaitForCompletion();
            var chanceShrineMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/ShrineChance/matShrineChance.mat").WaitForCompletion();
            var combatShrineMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/ShrineCombat/matShrineCombat.mat").WaitForCompletion();
            var water = Addressables.LoadAssetAsync<Material>("RoR2/Base/goldshores/matGSWater.mat").WaitForCompletion();
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("Terrain") || meshBase.name.Contains("Snow"))
                    {
                        mr.sharedMaterial = terrainMat;
                    }
                    if (meshBase.name.Contains("Glacier") || meshBase.name.Contains("Stalagmite") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("CavePillar"))
                    {
                        mr.sharedMaterial = detailMat;
                    }
                    if (meshBase.name.Contains("GroundMesh") || meshBase.name.Contains("GroundStairs") || meshBase.name.Contains("VerticalPillar") || meshBase.name.Contains("Human") || meshBase.name.Contains("Barrier"))
                    {
                        mr.sharedMaterial = detailMat2;
                    }
                    if (meshBase.name.Contains("HumanChainLink"))
                    {
                        meshBase.SetActive(false);
                    }
                    if (meshBase.name.Contains("mdlShrineHealing"))
                    {
                        mr.sharedMaterial = bloodShrineMat;
                    }
                    if (meshBase.name.Contains("mdlShrineChance"))
                    {
                        mr.sharedMaterial = chanceShrineMat;
                    }
                    if (meshBase.name.Contains("mdlShrineCombat"))
                    {
                        mr.sharedMaterial = combatShrineMat;
                    }
                    if (meshBase.name.Contains("ShrineChanceSand") || meshBase.name.Contains("ShrineBloodSand") || meshBase.name.Contains("ShrineCombatSand"))
                    {
                        meshBase.SetActive(false);
                    }
                }
            }
            GameObject.Find("HOLDER: Skybox").transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
        }
    }
}