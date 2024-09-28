using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage1
{
    internal class ShatteredAbodes
    {
        public static void Verdant(RampFog fog, ColorGrading cgrade)
        {
            // Skybox.DaySky(); // //

            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;
            fog.fogIntensity.value = 0.4f;
            fog.fogPower.value = 0.5f;
            fog.skyboxStrength.value = 0f;

            GameObject rainParticles = GameObject.Find("CAMERA PARTICLES: RainParticles (1)");
            rainParticles.SetActive(false);
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            sun.transform.eulerAngles = new Vector3(60f, 65f, 210f);
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color32(255, 246, 180, 255);
            sunLight.intensity = 0.8f;
            sunLight.shadowStrength = 0.7f;
            // 30.512 64.27 209.701
            var shadows = sunLight.gameObject.GetComponent<NGSS_Directional>();
            shadows.NGSS_SHADOWS_RESOLUTION = NGSS_Directional.ShadowMapResolution.UseQualitySettings;
            cgrade.colorFilter.value = new Color32(255, 234, 194, 255);
            cgrade.colorFilter.overrideState = true;
            AbodesMaterials(Main.verdantTerrainMat, Main.verdantDetailMat, Main.verdantDetailMat2, Main.verdantDetailMat3);
        }

        public static void Abandoned(RampFog fog, PostProcessProfile ppProfile)
        {
            // Skybox.SunnyDistantRoostSky();
            GameObject rainParticles = GameObject.Find("CAMERA PARTICLES: RainParticles (1)");
            rainParticles.SetActive(false);
            GameObject grassParticles = GameObject.Find("LVCameraParticlesGrass");
            grassParticles.SetActive(false);
            AddSand(SandType.Gigachad);

            GameObject sun = GameObject.Find("Directional Light (SUN)");

            RampFog rampFog = ppProfile.GetSetting<RampFog>();
            fog.fogColorStart.value = new Color(0.49f, 0.363f, 0.374f, 0.1f);
            fog.fogColorMid.value = new Color(0.58f, 0.486f, 0.331f, 0.25f);
            fog.fogColorEnd.value = new Color32(214, 144, 123, 128);
            fog.fogZero.value = rampFog.fogZero.value;
            fog.fogIntensity.value = rampFog.fogIntensity.value;
            fog.fogPower.value = rampFog.fogPower.value;
            fog.fogOne.value = rampFog.fogOne.value;
            fog.skyboxStrength.value = 0.02f;
            // sun.transform.eulerAngles = new Vector3(45f, 200f, 0f);
            var sunLight = sun.GetComponent<Light>();
            sunLight.color = new Color(1f, 0.65f, 0.5f, 1f);
            sunLight.intensity = 1.6f;
            sunLight.shadowStrength = 0.7f;
            // 30.512 64.27 209.701
            AbodesMaterials(Main.shatteredAbodesAbandonedTerrainMat, Main.plainsAbandonedWaterMat, Main.plainsAbandonedDetailMat2, Main.groveAbandonedDetailMat2);
        }

        public static void AbodesMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            MeshRenderer[] meshList = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer)) as MeshRenderer[];
            foreach (MeshRenderer renderer in meshList)
            {
                GameObject meshBase = renderer.gameObject;
                if (meshBase != null)
                {
                    if ((meshBase.name.Contains("Grass") || meshBase.name.Contains("Fern")) && renderer.sharedMaterial)
                    {
                        GameObject.Destroy(meshBase);
                    }
                    if ((meshBase.name.Contains("HouseBuried") || meshBase.name.Contains("LVTerrain") || meshBase.name.Contains("LVArc_StormOutlook") || meshBase.name.Contains("BuriedHouse")) && renderer.sharedMaterials.Length == 2)
                    {
                        renderer.sharedMaterials = new Material[] { terrainMat, detailMat2 };
                    }
                    if ((meshBase.name.Contains("LVTerrainToggle") || meshBase.name.Contains("LVTerrainFar") || meshBase.name.Contains("Dune") || meshBase.name.Contains("BrokenAltar") || meshBase.name.Contains("LVTerrainBackground")) && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = terrainMat;
                    }
                    if ((meshBase.name.Contains("LVArc_Temple") || meshBase.name.Contains("LVArc_Houses") || meshBase.name.Contains("LVArc_CliffCave") || meshBase.name.Contains("LVArc_Bridge") || meshBase.name.Contains("LVArc_BrokenPillar")) && renderer.sharedMaterials.Length == 2)
                    {
                        renderer.sharedMaterials = new Material[] { detailMat2, terrainMat };
                    }
                    if (meshBase.name.Contains("Pillar") && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = detailMat2;
                    }
                    if ((meshBase.name.Contains("RockMedium") || meshBase.name.Contains("Pebble")) && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = detailMat;
                    }
                    if (meshBase.name.Contains("Crystal") && renderer.sharedMaterial)
                    {
                        renderer.sharedMaterial = detailMat3;
                    }
                }
            }
        }
    }
}