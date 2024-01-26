using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using StageAesthetic.Variants;

namespace StageAesthetic.Variants.Stage2
{
    internal class WetlandAspect
    {
        public static void Morning(RampFog fog, ColorGrading cgrade)
        {
            Skybox.DaySky();
            fog.fogColorStart.value = new Color32(128, 121, 99, 13);
            fog.fogColorMid.value = new Color32(106, 141, 154, 130);
            fog.fogColorEnd.value = new Color32(104, 150, 199, 255);
            fog.fogZero.value = -0.058f;
            fog.fogPower.value = 1.2f;
            fog.fogIntensity.value = 0.937f;
            cgrade.colorFilter.value = new Color32(240, 213, 248, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0.52f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(255, 225, 181, 255);
            sunLight.intensity = 1.1f;
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(124, 86, 109, 0);
            caveOuter.fogColorMid.value = new Color32(154, 89, 127, 89);
            caveOuter.fogColorEnd.value = new Color32(227, 118, 219, 255);
            caveInner.fogColorStart.value = new Color32(131, 86, 94, 0);
            caveInner.fogColorMid.value = new Color32(137, 22, 24, 89);
            caveInner.fogColorEnd.value = new Color32(152, 8, 6, 255);
        }

        public static void Sunset(RampFog fog, ColorGrading cgrade)
        {
            Skybox.SunsetSky();
            fog.fogColorStart.value = new Color32(66, 66, 66, 50);
            fog.fogColorMid.value = new Color32(62, 18, 44, 126);
            fog.fogColorEnd.value = new Color32(123, 74, 61, 180);
            fog.skyboxStrength.value = 0.56f;
            fog.fogOne.value = 0.12f;
            fog.fogIntensity.overrideState = true;
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.8f;

            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color(1f, 0.75f, 0.75f, 1f);
            sunLight.intensity = 1f;

            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(127, 124, 84, 0);
            caveOuter.fogColorMid.value = new Color32(188, 163, 47, 88);
            caveOuter.fogColorEnd.value = new Color32(162, 123, 46, 255);
            caveInner.fogColorStart.value = new Color32(162, 192, 5, 0);
            caveInner.fogColorMid.value = new Color32(149, 154, 89, 89);
            caveInner.fogColorEnd.value = new Color32(217, 201, 11, 255);
        }

        public static void Night(RampFog fog)
        {
            Skybox.NightSky();
            AddRain(RainType.Rainstorm);

            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(14, 111, 160, 0);
            caveOuter.fogColorMid.value = new Color32(66, 76, 43, 89);
            caveOuter.fogColorEnd.value = new Color32(75, 84, 51, 255);
        }

        public static void Void(RampFog fog)
        {
            Skybox.VoidSky();
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(62, 12, 120, 0);
            caveOuter.fogColorMid.value = new Color32(66, 29, 132, 89);
            caveOuter.fogColorEnd.value = new Color32(187, 145, 238, 255);
            var terrain = GameObject.Find("HOLDER: Hero Assets").transform;
            terrain.GetChild(4).localPosition = new Vector3(-23.9f, -149.9f, 119f);
            GameObject.Find("HOLDER: Hidden Altar Stuff").transform.GetChild(1).gameObject.SetActive(false);
            var r = GameObject.Find("HOLDER: Ruin Pieces").transform;
            r.GetChild(22).gameObject.SetActive(false);
            GameObject.Find("HOLDER: Foliage").SetActive(false);
            AddSnow(SnowType.Light);
            VoidMaterials();
        }

        public static void VoidMaterials()
        {
            var s = GameObject.Find("HOLDER: Skybox").transform;
            var terrain = GameObject.Find("HOLDER: Hero Assets").transform;
            var terrainMat = Main.wetlandVoidTerrainMat;
            var terrainMat2 = Main.wetlandVoidTerrainMat2;
            var detailMat = Main.wetlandVoidDetailMat;
            var detailMat2 = Main.wetlandVoidDetailMat2;
            var detailMat3 = Main.wetlandVoidDetailMat3;
            var water = Main.wetlandVoidWaterMat;

            if (terrainMat && terrainMat2 && detailMat && detailMat2 && detailMat3 && water)
            {
                terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                terrain.GetChild(3).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = terrainMat2;
                terrain.GetChild(4).GetComponent<MeshRenderer>().sharedMaterial = water;
                terrain.GetChild(5).GetComponent<MeshRenderer>().sharedMaterial = water;
                terrain.GetChild(6).GetComponent<MeshRenderer>().sharedMaterial = terrainMat;
                s.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
                GameObject.Find("HOLDER: Ruin Pieces").transform.GetChild(6).gameObject.GetComponent<MeshRenderer>().sharedMaterial = detailMat3;
                var meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer mr in meshList)
                {
                    var meshBase = mr.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Boulder") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Blender") || meshBase.name.Contains("Trunk") || meshBase.name.Contains("Door") || meshBase.name.Contains("Frame"))
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat;
                                if (meshBase.transform.GetComponentInChildren<MeshRenderer>() != null)
                                {
                                    meshBase.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = detailMat;
                                }
                            }
                        }

                        var meshParent = meshBase.transform.parent;
                        if (meshParent != null)
                        {
                            if (meshBase.name.Contains("Mesh") && (meshParent.name.Contains("FSTree") || meshParent.name.Contains("FSRootBundle")))
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                            if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("FSRuinPillar"))
                            {
                                mr.sharedMaterial = detailMat;
                            }
                            if ((meshBase.name.Contains("RootBundleLargeCards") || meshBase.name.Contains("RootBundleSmallCards")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                            {
                                meshBase.gameObject.SetActive(false);
                            }
                            if ((meshBase.name.Contains("RootBundleLarge_LOD0") || meshBase.name.Contains("RootBundleLarge_LOD1") || meshBase.name.Contains("RootBundleLarge_LOD2") || meshBase.name.Contains("RootBundleSmall_LOD0") || meshBase.name.Contains("RootBundleSmall_LOD1") || meshBase.name.Contains("RootBundleSmall_LOD2")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                            {
                                mr.sharedMaterial = detailMat;
                            }
                        }

                        if (meshBase.name.Contains("Ruin") && meshBase.name != "FSGiantRuinDoorCollision")
                        {
                            if (mr.sharedMaterial != null)
                            {
                                mr.sharedMaterial = detailMat2;
                            }
                        }
                    }
                }
            }
        }
    }
}