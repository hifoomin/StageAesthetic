using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using R2API.Utils;

namespace StageAesthetic.Variants.Special
{
    internal class ReformedAltar
    {
        public static void Verdant(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;

            GameObject sun = GameObject.Find("Directional Light (SUN)");
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);

            Material terrainMat = new Material(Main.verdantTerrainMat);
            Material detailMat = Main.verdantDetailMat;
            Material detailMat2 = new Material(Main.reformedAltarTempleMat);
            Material detailMat22 = Main.verdantDetailMat2;
            Material detailMat3 = Main.verdantDetailMat3;
            Material grassMat = Main.verdantGrassMat;
            Material grassBlueMat = Main.verdantBlueGrassMat;
            // terrainMat.shaderKeywords = new string[] { "DOUBLESAMPLE", "MICROFACET_SNOW", "USE_ALPHA_AS_MASK", "USE_VERTEX_COLORS", "USE_VERTICAL_BIAS", "BINARYBLEND" };
            // _MainTex texTLTrimBasecolor
            // _NormalTex texTLTrimNormal
            // _SnowTex texTLTerrainGrassGreen
            // _SnowNormalTex null
            // _DirtTex texTLShipRustBasecolor
            // _DirtNormalTex null

            detailMat2.SetTexture("_GreenChannelTex", detailMat22.GetTexture("_SnowTex"));
            detailMat2.SetTexture("_BlueChannelTex", detailMat22.GetTexture("_MainTex"));
            detailMat2.SetTexture("_RedChannelTopTex", detailMat22.GetTexture("_DirtTex"));
            detailMat2.SetTexture("_RedChannelSideTex", detailMat22.GetTexture("_DirtTex"));
            // _GreenChannelTex sand
            // _RedChannelTopTex
            // _RedChannelSideTex
            MeshRenderer[] meshList = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                GameObject meshBase = renderer.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("GrassSmall") && renderer.sharedMaterial)
                    {
                        // 0.3786 0.6321 0.5703 1
                        renderer.sharedMaterial.color = new Color(0.3786f, 0.6321f, 0.5703f, 1);
                    }
                }
            }
            AltarMaterials(terrainMat, detailMat, detailMat2, detailMat3, grassBlueMat);
        }

        public static void Helminth(RampFog fog)
        {
            fog.fogColorEnd.value = new Color(0.3208f, 0.1234f, 0.1044f, 1f);
            fog.fogColorMid.value = new Color(0.5176f, 0.3338f, 0.2706f, 0.4471f);
            fog.fogColorStart.value = new Color(0.7453f, 0.3527f, 0.2988f, 0f);

            // intensity 0.588
            // fogone 0.601
            // texHRCrystalDiffuse blue
            // texHRTerrainHorizontalDiffuse green
            // texHRWallBrickColor redtop
            // 1 0.7468 0.5868 1
            GameObject.Find("Leaves").SetActive(false);
            GameObject.Find("FallenLeaf").SetActive(false);
            GameObject.Find("LTVineHanging").SetActive(false);
            GameObject.Find("LTVineHangingB").SetActive(false);

            GameObject sun = GameObject.Find("Directional Light (SUN)");
            Light sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color(0.5647f, 0.8706f, 0.8863f, 1);

            MeshRenderer[] meshList = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                GameObject meshBase = renderer.gameObject;
                if (meshBase != null)
                {
                    if (meshBase.name.Contains("LTCrystals") && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = Main.helminthObsidianMat;
                    }
                    if (meshBase.name.Contains("GrassSmall") && renderer.sharedMaterial)
                    {
                        // 0.3786 0.6321 0.5703 1
                        renderer.sharedMaterial.color = new Color(1f, 0.7468f, 0.5868f, 1);
                    }
                }
            }

            Material terrainMat = new Material(Main.helminthTerrainMat);
            terrainMat.SetTexture("_GreenChannelTex", Main.helminthTerrainMat.GetTexture("_BlueChannelTex"));
            terrainMat.SetTexture("_BlueChannelTex", Main.helminthTerrainMat.GetTexture("_GreenChannelTex"));
            Material detailMat2 = new Material(Main.helminthDetailMat2);
            detailMat2.shaderKeywords = new string[] { "DOUBLESAMPLE", "MICROFACET_SNOW", "USE_ALPHA_AS_MASK", "USE_VERTEX_COLORS", "USE_VERTICAL_BIAS", "BINARYBLEND" };
            AltarMaterials(terrainMat, Main.helminthDetailMat, detailMat2, Main.helminthDetailMat3, Main.helminthGrassMat2);
        }

        private static void AltarMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Material grassMat2)
        {
            MeshRenderer[] meshList = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                GameObject meshBase = renderer.gameObject;
                if (meshBase != null)
                {
                    if (grassMat2 && meshBase.name.Contains("GrassTall") && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = grassMat2;
                    }
                    if ((meshBase.name.Contains("LTTerrain") || meshBase.name.Contains("Dune")) && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = terrainMat;
                    }
                    if ((meshBase.name.Contains("LTCeiling") || meshBase.name.Contains("LTTemple") || meshBase.name.Contains("Altar") || meshBase.name.Contains("Arches") || meshBase.name.Contains("LTColumn") || meshBase.name.Contains("LTStairs")) && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = detailMat2;
                    }
                    if ((meshBase.name.Contains("LTCeilingRoots") || meshBase.name.Contains("Tube")) && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = detailMat3;
                    }
                    if ((meshBase.name.Contains("Gold") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Coral") || meshBase.name.Contains("Crystal")) && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = detailMat;
                    }
                }
            }
        }
    }
}