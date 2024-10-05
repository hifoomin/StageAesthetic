using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage2
{
    internal class AphelianSanctuary
    {
        public static void Twilight(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(94, 144, 178, 20);
            fog.fogColorMid.value = new Color32(94, 113, 140, 97);
            fog.fogColorEnd.value = new Color32(149, 92, 179, 170);
            cgrade.colorFilter.value = new Color32(133, 148, 178, 40);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0.2f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(178, 142, 151, 255);
            sunLight.intensity = 1.3f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.SetActive(false);
            //var fog2 = GameObject.Find("DeepFog");
            //fog2.SetActive(false);
            VanillaFoliage();
        }
                                
        public static void Sunset(RampFog fog, ColorGrading cgrade)
        {
            GameObject.Find("Directional Light (SUN)").gameObject.SetActive(false);
            Skybox.SunsetSky();

            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 150);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 255);
            fog.skyboxStrength.value = 0.1f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = -0.02f;
            fog.fogOne.value = 0.05f;

            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
            VanillaFoliage();
            AddSand(SandType.Light);
        }

        public static void Singularity(RampFog fog, ColorGrading cgrade)
        {
            GameObject.Find("Directional Light (SUN)").gameObject.SetActive(false);
            Skybox.SingularitySky();
            var sun = GameObject.Find("AL_Sun").gameObject;
            sun.SetActive(false);
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.SetActive(false);
            // var fog2 = GameObject.Find("DeepFog");
            // fog2.SetActive(false);
            VanillaFoliage();
            GameObject.Find("Passing Cloud").SetActive(false);
        }

        public static void Abyssal(RampFog fog, ColorGrading cgrade)
        {
            var cloud = GameObject.Find("Cloud3");
            // cgrade.SetAllOverridesTo(true);
            // cgrade.colorFilter.value = new Color32(181, 178, 219, 255);
            cloud.transform.localPosition = new Vector3(-22.8f, -138f, 46.7f);
            fog.fogColorStart.value = new Color32(102, 68, 68, 81);
            fog.fogColorMid.value = new Color32(94, 71, 71, 93);
            fog.fogColorEnd.value = new Color32(124, 62, 62, 255);
            /*
            fog.fogColorStart.value = new Color32(102, 51, 40, 81);
            fog.fogColorMid.value = new Color32(56, 87, 89, 93);
            fog.fogColorEnd.value = new Color32(104, 23, 54, 255);
            */
            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(150, 150, 150, 255);
            sunLight.intensity = 2.5f;
            // sunLight.shadowStrength = 0.4f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.transform.localPosition = new Vector3(0f, 110f, 0f);

            var cloud1 = fog1.transform.GetChild(1);
            cloud1.transform.localPosition = new Vector3(87.5f, -66f, 0f);
            cloud1.transform.localScale = new Vector3(120f, 120f, 120f);

            for (int i = 0; i < 5; i++)
            {
                var instantiated = Object.Instantiate(cloud1.gameObject);
                instantiated.transform.localPosition = new Vector3(87.5f + (2.5f * i), -10f + (i * 12f), 0f - (i * 5f));
                instantiated.transform.localScale = new Vector3(120f, 120f, 120f);
            }

            var fuckYou = fog1.transform.GetChild(0);
            fuckYou.transform.localPosition = new Vector3(-38.4f, -66f, -7.5f);

            var sun = GameObject.Find("Sun");
            sun.SetActive(false);
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            var stupidList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];

            foreach (MeshRenderer mr in meshList)
            {
                var meshBase = mr.gameObject;
                var meshParent = meshBase.transform.parent;
                if (meshBase != null)
                {
                    if (meshParent != null)
                    {
                        bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("BrokenPot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Tile") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pillar") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Step") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                        if (biggerProps)
                        {
                            var light = meshBase.AddComponent<Light>();
                            light.color = new Color32(125, 43, 48, 225);
                            light.intensity = 6f;
                            light.range = 24f;
                        }
                    }
                }
            }
            foreach (SkinnedMeshRenderer smr in stupidList)
            {
                var meshBase = smr.gameObject;
                if (meshBase != null)
                {
                    bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Tile") || meshBase.name.Contains("RuinBlock") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pillar") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Step") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                    if (biggerProps)
                    {
                        var light = meshBase.AddComponent<Light>();
                        light.color = new Color32(249, 212, 96, 225);
                        light.intensity = 6f;
                        light.range = 24f;
                    }
                }
            }
            AbyssalFoliage();
            AbyssalMaterials(Main.distantRoostAbyssalTerrainMat, Main.distantRoostAbyssalTerrainMat2, Main.distantRoostAbyssalDetailMat, Main.distantRoostAbyssalDetailMat2, Main.distantRoostAbyssalDetailMat);
        }

        public static void AbyssalMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Material templeMat)
        {
            MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            SkinnedMeshRenderer[] sMeshList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];

            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    Transform meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if ((meshParent.name.Contains("TempleTop") && meshBase.name.Contains("RuinBlock") || meshBase.name.Contains("GPRuinBlockQuarter")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat2;
                        }
                        if (meshBase.name.Equals("Terrain") && renderer.sharedMaterials.Length > 0)
                            renderer.sharedMaterials = new Material[] { terrainMat, terrainMat, terrainMat };
                        if ((meshBase.name.Contains("Terrain") && !meshBase.name.Equals("Terrain") || meshBase.name.Contains("Dirt") || meshBase.name.Contains("TerrainPlatform")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Platform") || meshBase.name.Contains("Temple") || meshBase.name.Contains("Bridge")) && renderer.sharedMaterial)
                            renderer.sharedMaterials = new Material[] { templeMat, terrainMat };
                        bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("BrokenPot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Pillar") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                        if ((biggerProps || meshBase.name.Contains("AncientLoft_SculptureSM") || meshBase.name.Contains("FountainSM")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if ((meshBase.name.Contains("Tile") || meshBase.name.Contains("Step") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble") || meshBase.name.Contains("Boulder") || meshBase.name.Equals("LightStatue_Stone")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if (meshBase.name.Contains("CircleArchwayAnimatedMesh"))
                        {
                            Material[] sharedMaterials = renderer.sharedMaterials;
                            for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                            {
                                sharedMaterials[i] = detailMat2;
                                if (i == 1)
                                    sharedMaterials[i] = detailMat3;
                            }
                            renderer.sharedMaterials = sharedMaterials;
                        }
                    }
                }
                foreach (SkinnedMeshRenderer sRenderer in sMeshList)
                {
                    GameObject meshBase = sRenderer.gameObject;
                    if (meshBase != null)
                    {
                        bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("RuinBlock") || meshBase.name.Contains("Pillar") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                        if (biggerProps)
                            sRenderer.sharedMaterial = detailMat3;
                        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Tile") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Step") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble")) && sRenderer.sharedMaterial)
                            sRenderer.sharedMaterial = detailMat;
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
                if (meshBase)
                {
                    if (meshBase.name.Contains("spmBonsai1V1_LOD0") || meshBase.name.Contains("spmBonsai1V2_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(195, 195, 195, 255);
                            if (mr.sharedMaterials.Length >= 4)
                            {
                                mr.sharedMaterials[3].color = new Color32(195, 195, 195, 255);
                            }
                        }
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
                if (meshBase)
                {
                    if (meshBase.name.Contains("spmBonsai1V1_LOD0") || meshBase.name.Contains("spmBonsai1V2_LOD0"))
                    {
                        if (mr.sharedMaterial)
                        {
                            mr.sharedMaterial.color = new Color32(134, 53, 255, 255);
                            if (mr.sharedMaterials.Length >= 4)
                            {
                                mr.sharedMaterials[3].color = new Color32(134, 53, 255, 255);
                            }
                        }
                    }
                }
            }
        }
    }
}