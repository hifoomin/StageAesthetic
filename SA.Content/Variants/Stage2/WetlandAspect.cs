using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class WetlandAspect
    {
        public static void PinkSwamp(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(90, 69, 105, 13);
            fog.fogColorMid.value = new Color32(130, 105, 154, 161);
            fog.fogColorEnd.value = new Color32(169, 119, 227, 255);
            cgrade.colorFilter.value = new Color32(233, 189, 245, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(198, 152, 223, 255);
            sunLight.intensity = 0.9f;
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(124, 86, 109, 0);
            caveOuter.fogColorMid.value = new Color32(154, 89, 127, 89);
            caveOuter.fogColorEnd.value = new Color32(227, 118, 219, 255);
            caveInner.fogColorStart.value = new Color32(131, 86, 94, 0);
            caveInner.fogColorMid.value = new Color32(137, 22, 24, 89);
            caveInner.fogColorEnd.value = new Color32(152, 8, 6, 255);
        }

        public static void GoldSwamp(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(129, 94, 43, 9);
            fog.fogColorMid.value = new Color32(131, 96, 37, 135);
            fog.fogColorEnd.value = new Color32(129, 90, 34, 255);
            cgrade.colorFilter.value = new Color32(251, 199, 180, 255);
            cgrade.colorFilter.overrideState = true;
            fog.skyboxStrength.value = 0;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(221, 151, 104, 255);
            sunLight.intensity = 1.3f;
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            var caveInner = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("NonBlended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(127, 124, 84, 0);
            caveOuter.fogColorMid.value = new Color32(188, 163, 47, 88);
            caveOuter.fogColorEnd.value = new Color32(162, 123, 46, 255);
            caveInner.fogColorStart.value = new Color32(162, 192, 5, 0);
            caveInner.fogColorMid.value = new Color32(149, 154, 89, 89);
            caveInner.fogColorEnd.value = new Color32(217, 201, 11, 255);
        }

        public static void MoreSwamp(RampFog fog, GameObject rain)
        {
            fog.fogColorStart.value = new Color32(33, 43, 41, 87);
            fog.fogColorMid.value = new Color32(45, 60, 51, 173);
            fog.fogColorEnd.value = new Color32(47, 60, 48, 255);
            fog.fogOne.value = 0.355f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(128, 205, 170, 255);
            sunLight.intensity = 0.32f;
            sunLight.shadowStrength = 0.477f;
            if (Config.WeatherEffects.Value)
            {
                var rainParticle = rain.GetComponent<ParticleSystem>();
                var epic = rainParticle.emission;
                var epic2 = epic.rateOverTime;
                epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                {
                    constant = 700,
                    constantMax = 700,
                    constantMin = 220,
                    curve = epic2.curve,
                    curveMax = epic2.curveMax,
                    curveMin = epic2.curveMax,
                    curveMultiplier = epic2.curveMultiplier,
                    mode = epic2.mode
                };
                var epic3 = rainParticle.colorOverLifetime;
                epic3.enabled = false;
                var epic4 = rainParticle.main;
                epic4.scalingMode = ParticleSystemScalingMode.Shape;
                rain.transform.eulerAngles = new Vector3(87, 110, 0);
                rain.transform.localScale = new Vector3(14, 14, 1);
                UnityEngine.Object.Instantiate<GameObject>(rain, Vector3.zero, Quaternion.identity);
            }
            var caveOuter = GameObject.Find("HOLDER: Hidden Altar Stuff").transform.Find("Blended").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveOuter.fogColorStart.value = new Color32(14, 111, 160, 0);
            caveOuter.fogColorMid.value = new Color32(66, 76, 43, 89);
            caveOuter.fogColorEnd.value = new Color32(75, 84, 51, 255);
        }

        public static void VoidSwamp(RampFog fog)
        {
            try { ApplyVoidMaterials(); } catch { SwapVariants.AesLog.LogError("Void Aspect: Failed to change materials, trying again..."); } finally { ApplyVoidMaterials(); }
            var s = GameObject.Find("HOLDER: Skybox").transform;
            s.GetChild(0).localPosition = new Vector3(24.45f, -50f, -84.87f);
            fog.fogColorStart.value = new Color32(62, 12, 62, 87);
            fog.fogColorMid.value = new Color32(66, 29, 74, 173);
            fog.fogColorEnd.value = new Color32(82, 24, 89, 255);
            fog.fogOne.value = 0.355f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(187, 145, 238, 255);
            sunLight.intensity = 1f;
            sunLight.shadowStrength = 0.6f;
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
        }

        public static void ApplyVoidMaterials()
        {
            var s = GameObject.Find("HOLDER: Skybox").transform;
            var terrain = GameObject.Find("HOLDER: Hero Assets").transform;
            var vfm = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrain.mat").WaitForCompletion();
            var vfme = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion());
            vfme.color = new Color32(171, 167, 234, 132);
            var vfmg = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainGem.mat").WaitForCompletion();
            var vfmh = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaHeatvent1.mat").WaitForCompletion();
            var vfmt = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTrim.mat").WaitForCompletion();
            terrain.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = vfme;
            terrain.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial = vfme;
            terrain.GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = vfme;
            terrain.GetChild(3).GetChild(2).GetComponent<MeshRenderer>().sharedMaterial = vfme;
            var water = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion());
            water.color = new Color32(82, 24, 109, 255);
            terrain.GetChild(4).GetComponent<MeshRenderer>().sharedMaterial = water;
            terrain.GetChild(5).GetComponent<MeshRenderer>().sharedMaterial = water;
            terrain.GetChild(6).GetComponent<MeshRenderer>().sharedMaterial = vfm;
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
                            mr.sharedMaterial = vfmg;
                            if (meshBase.transform.GetComponentInChildren<MeshRenderer>() != null)
                            {
                                meshBase.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = vfmg;
                            }
                        }
                    }

                    var meshParent = meshBase.transform.parent;
                    if (meshParent != null)
                    {
                        if (meshBase.name.Contains("Mesh") && (meshParent.name.Contains("FSTree") || meshParent.name.Contains("FSRootBundle")))
                        {
                            mr.sharedMaterial = vfmh;
                        }
                        if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("FSRuinPillar"))
                        {
                            mr.sharedMaterial = vfmg;
                        }
                        if ((meshBase.name.Contains("RootBundleLargeCards") || meshBase.name.Contains("RootBundleSmallCards")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                        {
                            meshBase.gameObject.SetActive(false);
                        }
                        if ((meshBase.name.Contains("RootBundleLarge_LOD0") || meshBase.name.Contains("RootBundleLarge_LOD1") || meshBase.name.Contains("RootBundleLarge_LOD2") || meshBase.name.Contains("RootBundleSmall_LOD0") || meshBase.name.Contains("RootBundleSmall_LOD1") || meshBase.name.Contains("RootBundleSmall_LOD2")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                        {
                            mr.sharedMaterial = vfmg;
                        }
                    }

                    if (meshBase.name.Contains("Ruin") && meshBase.name != "FSGiantRuinDoorCollision")
                    {
                        if (mr.sharedMaterial != null)
                        {
                            mr.sharedMaterial = vfmh;
                        }
                    }
                }
            }
            s.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = water;
            GameObject.Find("HOLDER: Ruin Pieces").transform.GetChild(6).gameObject.GetComponent<MeshRenderer>().sharedMaterial = vfmt;
        }
    }
}