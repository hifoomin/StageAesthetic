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
            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
            VanillaFoliage();
        }

        public static void Sunset(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(144, 102, 42, 0);
            fog.fogColorMid.value = new Color32(109, 53, 10, 141);
            fog.fogColorEnd.value = new Color32(158, 78, 40, 235);
            fog.fogOne.value = 0.12f;
            fog.fogZero.value = -0.015f;
            fog.fogPower.value = 0.8f;
            fog.fogIntensity.value = 0.63f;
            cgrade.colorFilter.value = new Color32(255, 255, 255, 255);
            fog.skyboxStrength.value = 0f;
            cgrade.colorFilter.overrideState = true;
            var terrain = GameObject.Find("HOLDER: Terrain").transform;
            var terrain2 = terrain.Find("mdlAncientLoft_Terrain");
            var sun = terrain2.Find("Sun");
            sun.transform.localPosition = new Vector3(400f, -300f, -127.3f);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(226, 185, 144, 255);
            sunLight.intensity = 1f;
            sunLight.shadowNormalBias = 0.61f;
            sunLight.shadowStrength = 0.877f;
            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
            VanillaFoliage();
            AddSand(SandType.Light);
        }

        public static void Singularity(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(36, 89, 146, 45);
            fog.fogColorMid.value = new Color32(21, 58, 131, 98);
            fog.fogColorEnd.value = new Color32(0, 0, 71, 255);
            fog.fogIntensity.value = 0.63f;
            fog.fogPower.value = 0.8f;
            fog.fogZero.value = -0.015f;
            fog.fogOne.value = 0.1f;
            cgrade.colorFilter.value = new Color32(36, 36, 166, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 2.1f;
            sunLight.shadowStrength = 0.5f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.SetActive(false);
            var fog2 = GameObject.Find("DeepFog");
            fog2.SetActive(false);
            VanillaFoliage();
        }

        public static void Abyssal(RampFog fog, ColorGrading cgrade)
        {
            var cloud = GameObject.Find("Cloud3");
            cgrade.SetAllOverridesTo(true);
            cgrade.colorFilter.value = new Color32(181, 178, 219, 255);
            cgrade.saturation.value = -5f;
            cloud.transform.localPosition = new Vector3(-22.8f, -70f, 46.7f);
            fog.fogColorStart.value = new Color32(102, 51, 40, 81);
            fog.fogColorMid.value = new Color32(56, 87, 89, 93);
            fog.fogColorEnd.value = new Color32(104, 23, 54, 255);
            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 234, 209, 255);
            sunLight.intensity = 2f;
            sunLight.shadowStrength = 0.6f;
            var fog1 = GameObject.Find("HOLDER: Cards");
            fog1.transform.position = new Vector3(0f, 48f, 0f);

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
                            light.color = new Color32(249, 212, 96, 225);
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
            AbyssalMaterials();
            AbyssalFoliage();
        }

        public static void AbyssalMaterials()
        {
            var dirt = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texDirt.png");
            var notThatReb = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texRedGrass.png");
            var notThatWhite = Main.stageaesthetic.LoadAsset<Texture2D>("Assets/StageAesthetic/Materials/texLavenderGravel.png");

            var cloud = GameObject.Find("Cloud3");
            var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            var stupidList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];
            var terrainMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainWalls.mat").WaitForCompletion());
            terrainMat.SetFloat("_GreenChannelBias", 0.4312f);
            terrainMat.SetFloat("_BlueChannelBias", -0.257f);
            terrainMat.SetTexture("_GreenChannelTex", notThatReb);
            terrainMat.SetTexture("_BlueChannelTex", notThatWhite);
            /*
            var terrainMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcavesimple/matDCBoulder.mat").WaitForCompletion());
            terrainMat2.color = new Color32(162, 0, 0, 194);
            terrainMat2.SetFloat("_NormalStrength", 0.16f);
            terrainMat2.SetFloat("_SpecularStrength", 0.03f);
            terrainMat2.SetFloat("_SpecularExponent", 8f);
            terrainMat2.SetFloat("_Smoothness", 0.6477f);
            terrainMat2.SetFloat("_SnowSpecularStrength", 0.015f);
            terrainMat2.SetFloat("_SnowSpecularExponent", 8f);
            terrainMat2.SetFloat("_SnowSmoothness", 0.5f);
            terrainMat2.SetFloat("_Depth", 0.29f);
            terrainMat2.SetFloat("_TriplanarTextureFactor", 0.19f);
            terrainMat2.SetTexture("_SnowTex", dirt);
            terrainMat2.SetTextureScale("_SnowTex", new Vector2(1f, 1f));
            terrainMat2.SetTextureScale("_NormalTex", new Vector2(3f, 1.5f));
            */
            var terrainMat2 = Main.stageaesthetic.LoadAsset<Material>("Assets/StageAesthetic/Materials/matAbyssalPlatform.mat");
            var detailMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/Titan/matTitanGold.mat").WaitForCompletion());
            detailMat.color = new Color32(144, 130, 82, 255);
            var detailMat2 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/dampcave/matDCTerrainGiantColumns.mat").WaitForCompletion());
            detailMat2.SetFloat("_BlueChannelBias", 0.1f);
            detailMat2.SetFloat("_BlueChannelSpecularStrength", 0.2545f);
            detailMat2.SetFloat("_BlueChannelSpecularExponent", 3.09f);
            detailMat2.SetFloat("_BlueChannelSmoothness", 0.3f);
            detailMat2.SetFloat("_NormalStrength", 0.923f);
            detailMat2.SetFloat("_TextureFactor", 0.041f);
            detailMat2.SetFloat("_Depth", 0.42f);
            detailMat2.SetFloat("_RedChannelBias", 2f);
            var detailMat3 = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/snowyforest/matSFSap.mat").WaitForCompletion());
            detailMat3.color = new Color32(87, 79, 52, 255);
            detailMat3.SetFloat("_SpecularStrength", 0.2041f);
            detailMat3.SetFloat("_SpecularExponent", 10.7f);

            detailMat3.shaderKeywords = new string[] { "IGNORE_BIAS", "MICROFACET_SNOW", "TRIPLANAR" };

            if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3)
            {
                cloud.GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    var meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if (meshParent.name.Contains("TempleTop") && meshBase.name.Contains("RuinBlock"))
                            {
                                if (mr.sharedMaterial)
                                {
                                    mr.sharedMaterial = terrainMat;
                                }
                            }
                        }
                        if (meshBase.name.Equals("Terrain"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try
                                    {
                                        var sharedMaterials = mr.sharedMaterials;
                                        for (int i = 0; i < mr.sharedMaterials.Length; i++)
                                        {
                                            sharedMaterials[i] = detailMat2;
                                        }
                                        mr.sharedMaterials = sharedMaterials;
                                    }
                                    catch (Exception e) { SwapVariants.SALogger.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    var sharedMaterials2 = mr.sharedMaterials;
                                    for (int i = 0; i < mr.sharedMaterials.Length; i++)
                                    {
                                        sharedMaterials2[i] = detailMat2;
                                    }
                                    mr.sharedMaterials = sharedMaterials2;
                                    break;
                            }
                        }
                        if (meshBase.name.Contains("Platform") || (meshBase.name.Contains("Terrain") && !meshBase.name.Equals("Terrain")) || meshBase.name.Contains("Temple") || meshBase.name.Contains("Bridge") || meshBase.name.Contains("Dirt"))
                        {
                            switch (mr.sharedMaterial)
                            {
                                case null:
                                    try
                                    {
                                        var sharedMaterials = mr.sharedMaterials;
                                        for (int i = 0; i < mr.sharedMaterials.Length; i++)
                                        {
                                            sharedMaterials[i] = terrainMat;
                                            if (i == 1)
                                            {
                                                sharedMaterials[i] = terrainMat2;
                                            }
                                        }
                                        mr.sharedMaterials = sharedMaterials;
                                    }
                                    catch (Exception e) { SwapVariants.SALogger.LogWarning(e.Message + "\n" + e.StackTrace); };
                                    break;

                                default:
                                    var sharedMaterials2 = mr.sharedMaterials;
                                    for (int i = 0; i < mr.sharedMaterials.Length; i++)
                                    {
                                        sharedMaterials2[i] = terrainMat;
                                        if (i == 1)
                                        {
                                            sharedMaterials2[i] = terrainMat2;
                                        }
                                    }
                                    mr.sharedMaterials = sharedMaterials2;
                                    break;
                            }
                        }
                        bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("BrokenPot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pillar") || meshBase.name.Contains("Boulder") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                        if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble") || biggerProps)
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }
                        if (meshBase.name.Contains("CircleArchwayAnimatedMesh"))
                        {
                            var sharedMaterials = mr.sharedMaterials;
                            for (int i = 0; i < mr.sharedMaterials.Length; i++)
                            {
                                sharedMaterials[i] = terrainMat;
                                if (i == 1)
                                {
                                    sharedMaterials[i] = detailMat;
                                }
                            }
                            mr.sharedMaterials = sharedMaterials;
                        }
                        if (meshBase.name.Contains("SunCloud") || meshBase.name.Contains("spmGlGrass") || meshBase.name.Contains("AncientLoftGrass") || meshBase.name.Contains("mdlLilyPad"))
                        {
                            meshBase.SetActive(false);
                        }
                        if (meshBase.name.Contains("Tile") || meshBase.name.Contains("Step"))
                        {
                            if (mr.sharedMaterial)
                            {
                                mr.sharedMaterial = detailMat3;
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
                        if (meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble") || biggerProps)
                        {
                            if (smr.sharedMaterial)
                            {
                                smr.sharedMaterial = detailMat2;
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