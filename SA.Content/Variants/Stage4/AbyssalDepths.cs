using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

namespace StageAesthetic.Variants.Stage4
{
    internal class AbyssalDepths
    {
        public static void VanillaChanges()
        {
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.intensity = 3f;
            sunLight.transform.localEulerAngles = new Vector3(35, 15, 351);
            sunLight.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            sunLight.shadowStrength = 0.6f;
        }

        public static void Blue(RampFog fog, ColorGrading cgrade)
        {
            /*
            fog.fogColorStart.value = new Color32(17, 63, 72, 100);
            fog.fogColorMid.value = new Color32(43, 125, 114, 74);
            fog.fogColorEnd.value = new Color32(16, 74, 72, 160);
            */
            fog.fogColorStart.value = new Color32(48, 102, 102, 81); // A cyan-ish color
            fog.fogColorMid.value = new Color32(61, 87, 94, 93);    // A darker cyan
            fog.fogColorEnd.value = new Color32(32, 142, 121, 200); // A bluish-green, which complements red
            fog.fogOne.value = 0.3f;
            fog.fogIntensity.value = 0.65f;
            fog.fogZero.value = -0.02f;

            fog.skyboxStrength.value = 0f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(229, 214, 255, 255);
            sunLight.intensity = 1.2f;
            sunLight.shadowStrength = 0.6f;
            sunLight.transform.eulerAngles = new Vector3(65f, 222.6395f, 202.9964f);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(78, 55, 80, 60);
            caveFog.fogColorMid.value = new Color32(64, 75, 94, 144);
            caveFog.fogColorEnd.value = new Color32(60, 73, 88, 205);
            SimMaterials(Main.abyssalSimulacrumTerrainMat, Main.abyssalSimulacrumDetailMat, Main.abyssalSimulacrumDetailMat3, Main.abyssalSimulacrumDetailMat2, Main.abyssalSimulacrumBoulderMat);
            // Lighting: Magenta coral, orange otherwise
            LightChange("hive");
        }

        public static void Night(RampFog fog, ColorGrading cgrade)
        {
            Skybox.NightSky();
            GameObject.Find("CEILING").SetActive(false);
            GameObject.Find("SceneInfo").GetComponent<PostProcessVolume>().enabled = false;
            GameObject.Find("Directional Light (SUN)").SetActive(false);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(67, 65, 109, 76);
            caveFog.fogColorMid.value = new Color32(40, 68, 123, 161);
            caveFog.fogColorEnd.value = new Color32(46, 128, 148, 200);
            // cgrade.colorFilter.value = new Color32(119, 207, 181, 255);
            //cgrade.colorFilter.overrideState = true;
            // Lighting: Blue coral, cyan or green lighting otherwise
            LightChange("azure");
        }

        public static void Orange(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(44, 18, 62, 100);
            fog.fogColorEnd.value = new Color32(61, 74, 123, 150);
            fog.skyboxStrength.value = 0.02f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.75f;

            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color(1f, 1f, 0.75f, 1f);
            sunLight.intensity = 1f;
            sunLight.transform.eulerAngles = new Vector3(70f, 19.64314f, 9.985f);
            sunLight.shadowStrength = 0.75f;

            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(85, 57, 91, 33);
            caveFog.fogColorMid.value = new Color32(90, 55, 97, 100);
            caveFog.fogColorEnd.value = new Color32(135, 76, 149, 150);

            SimMaterials(Main.abyssalGoldTerrainMat, Main.distantRoostAbyssalDetailMat, Main.abyssalSimulacrumDetailMat3, Main.distantRoostAbyssalDetailMat2, Main.abyssalSimulacrumBoulderMat);

            // Lighting: Pink coral, orange otherwise
            LightChange("meadow");
        }

        public static void LightChange(string variant)
        {
            switch (variant)
            {
                case "meadow":
                    coral = new Color(0.64f, 0.343f, 0.924f, 1);
                    chain = new Color(0.981f, 0.521f, 0.065f);
                    crystal = new Color(0.598f, 0.117f, 0.355f);
                    break;

                case "azure":
                    coral = new Color(0.188f, 0.444f, 0, 1);
                    chain = new Color(0.181f, 0.921f, 0.945f);
                    crystal = new Color(0f, 0.837f, 0.14f);
                    break;

                case "hive":
                    coral = new Color32(30, 209, 27, 255);
                    chain = new Color(0.981f, 0.521f, 0.065f);
                    crystal = new Color(0.718f, 0, 0.515f);
                    break;

                default:
                    break;
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
                        if (light.gameObject.transform.parent.gameObject.name.Equals("DCCoralPropMediumActive"))
                        {
                            light.color = coral;
                            var lightLP = light.transform.localPosition;
                            lightLP.z = 4;
                        }
                        else if (light.gameObject.transform.parent.gameObject.name.Equals("DCCrystalCluster Variant")) light.color = crystal;
                    }
                    if (light.gameObject.name.Equals("CrystalLight")) light.color = chain;
                }
            }
        }

        public static void Coral(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(127, 70, 206, 0);
            fog.fogColorMid.value = new Color32(185, 72, 119, 50);
            fog.fogColorEnd.value = new Color32(183, 93, 129, 125);
            GameObject.Find("Directional Light (SUN)").transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(130, 163, 175, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.75f;
            var lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light l in lightList)
            {
                if (l != null && !l.name.Contains("Light (SUN)"))
                {
                    l.color = new Color32(216, 192, 32, 255);
                    l.intensity = 25f;
                    l.range = 30f;
                }
                if (l.gameObject.GetComponent<FlickerLight>() != null)
                {
                    l.gameObject.GetComponent<FlickerLight>().enabled = false;
                }
            }
            GameObject.Find("DCPPInTunnels").SetActive(false);
            AddRain(RainType.Drizzle);
            // terrain detail, tree?, ruins
            SimMaterials(Main.abyssalSimulacrumTerrainMat, Main.abyssalSimulacrumDetailMat, Main.abyssalSimulacrumDetailMat3, Main.abyssalSimulacrumDetailMat2, Main.abyssalSimulacrumBoulderMat);
        }

        private static Color coral;
        private static Color chain;
        private static Color crystal;

        public static void SimMaterials(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3, Material boulderMat)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3 && boulderMat)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    Transform meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("Ruin") && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat3;
                            if (meshBase.name.Contains("RuinBowl") && meshParent.name.Contains("RuinMarker") && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat3;
                        }
                        if ((meshBase.name.Contains("Hero") || meshBase.name.Contains("Ceiling")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Boulder") || (meshBase.name.Contains("GiantRock") && !meshBase.name.Contains("Slab"))) && renderer.sharedMaterial)
                            renderer.sharedMaterial = boulderMat;
                        if ((meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Coral") || meshBase.name.Contains("Heatvent") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Stalagmite")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if (meshBase.name.Contains("Ruin") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if (meshBase.name.Contains("Column") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if (meshBase.name.Contains("Crystal") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if (meshBase.name.Contains("LightMesh") && renderer.sharedMaterial)
                        {
                            if (meshBase.transform.childCount >= 1 && meshBase.transform.GetChild(0).name.Contains("Crystal"))
                                meshBase.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = detailMat;
                        }
                        if (meshBase.name.Contains("Spike") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("DCGiantRockSlab") || meshBase.name.Contains("GiantStoneSlab") || meshBase.name.Contains("TerrainBackwall") || meshBase.name.Contains("Chain") || meshBase.name.Contains("Wall")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                    }
                }
            }
        }
    }
}