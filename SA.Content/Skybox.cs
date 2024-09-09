using RoR2;
using R2API;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic
{
    public class Skybox
    {
        private static readonly PostProcessProfile ppHelminth = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/DLC2/helminthroost/ppSceneHelminth.asset").WaitForCompletion();
        private static readonly PostProcessProfile ppScorched = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneWispGraveyard.asset").WaitForCompletion();
        private static readonly PostProcessProfile ppPlainsRoost = Object.Instantiate(Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGolemplainsFoggy.asset").WaitForCompletion());
        private static readonly PostProcessProfile ppSunset = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneWispGraveyard.asset").WaitForCompletion();
        private static readonly PostProcessProfile ppSick = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneMysterySpace.asset").WaitForCompletion();
        public static readonly Material sunMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Sun.mat").WaitForCompletion();
        private static readonly Material sunsetSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
        private static readonly Material spaceSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion();
        private static readonly Material spaceStarsMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/eclipseworld/matEclipseStarsSpheres.mat").WaitForCompletion();
        private static readonly GameObject eclipseSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/eclipseworld/Weather, Eclipse.prefab").WaitForCompletion(), "SAEclipseSkybox", false);
        private static readonly GameObject planetariumSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), "SADaySkybox", false);
        private static readonly GameObject voidStageSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion(), "SAVoidSkybox", false);
        public static readonly GameObject sun = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ancientloft/mdlAncientLoft_Terrain.fbx").WaitForCompletion().transform.GetChild(5).GetChild(0).gameObject, "SASun", false);

        public static void SunnyDistantRoostSky()
        {
            GameObject skybox = Object.Instantiate(planetariumSkybox, Vector3.zero, Quaternion.identity);
            PostProcessProfile ppProfile = Object.Instantiate(ppPlainsRoost);
            RampFog fog = ppProfile.GetSetting<RampFog>();
            fog.fogColorStart.value = new Color32(53, 66, 82, 18);
            fog.fogColorMid.value = new Color32(103, 67, 64, 154);
            fog.fogColorEnd.value = new Color32(146, 176, 255, 255);
            fog.fogOne.value = 0.2f;
            fog.fogZero.value = -0.05f;
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppProfile;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.skyboxMaterial = spaceSkyboxMat;
            ambLight.ambientIntensity = 0.75f;
            ambLight.ApplyLighting();

            skybox.transform.GetChild(1).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { spaceSkyboxMat, spaceStarsMat2 };
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(6).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
            foreach (Transform child in skybox.transform.GetChild(4).GetChild(0).GetChild(1).transform)
            {
                foreach (Transform child2 in child)
                {
                    if (child2.gameObject.name.Contains("Opaque") || child2.gameObject.name.Contains("Moon") || child2.gameObject.name.Contains("Rings"))
                        child2.gameObject.SetActive(false);
                }
            }
        }

        public static void DaySky()
        {
            GameObject skybox = Object.Instantiate(planetariumSkybox, Vector3.zero, Quaternion.identity);
            if (SceneManager.GetActiveScene().name == "snowyforest" || SceneManager.GetActiveScene().name == "foggyswamp" || SceneManager.GetActiveScene().name == "frozenwall" || SceneManager.GetActiveScene().name == "skymeadow")
                GameObject.Destroy(skybox.transform.GetChild(0).GetComponent<PostProcessVolume>());
            else
                skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppPlainsRoost;

            RampFog fog = ppPlainsRoost.GetSetting<RampFog>();
            fog.fogColorStart.value = new Color32(127, 127, 153, 25);
            fog.fogColorMid.value = new Color32(0, 106, 145, 150);
            fog.fogColorEnd.value = new Color32(0, 115, 119, 255);
            fog.fogZero.value = -0.01f;
            fog.fogOne.value = 0.15f;
            fog.fogPower.value = 2f;
            fog.skyboxStrength.value = 0.1f;

            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.skyboxMaterial = spaceSkyboxMat;
            ambLight.ambientIntensity = 1f;
            ambLight.ApplyLighting();
            skybox.transform.GetChild(1).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[2] { spaceSkyboxMat, spaceStarsMat2 };
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(6).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
            foreach (Transform child in skybox.transform.GetChild(4).GetChild(0).GetChild(1).transform)
            {
                foreach (Transform child2 in child)
                {
                    if (child2.gameObject.name.Contains("Opaque") || child2.gameObject.name.Contains("Moon") || child2.gameObject.name.Contains("Rings"))
                        child2.gameObject.SetActive(false);
                }
            }
        }

        public static void SunsetSky()
        {
            sun.GetComponent<MeshRenderer>().sharedMaterial = sunMat;
            GameObject skybox = Object.Instantiate(planetariumSkybox, Vector3.zero, Quaternion.identity);
            GameObject sunInstance = null;
            if (SceneManager.GetActiveScene().name != "ancientloft")
                sunInstance = Object.Instantiate(sun, skybox.transform);

            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppScorched;

            if (SceneManager.GetActiveScene().name == "ancientloft")
                skybox.transform.GetChild(1).GetComponent<Light>().color = new Color(0.75f, 0.25f, 0.25f);
            else if (SceneManager.GetActiveScene().name == "shipgraveyard")
                skybox.transform.GetChild(1).GetComponent<Light>().color = new Color(1f, 0.75f, 0.75f);
            else
                skybox.transform.GetChild(1).GetComponent<Light>().color = new Color(1f, 0.5f, 0.5f);

            skybox.transform.GetChild(1).GetComponent<Light>().intensity = 2f;

            string sceneName = SceneManager.GetActiveScene().name;

            switch (sceneName)
            {
                case "snowyforest":
                    skybox.transform.GetChild(1).gameObject.SetActive(false);
                    sunInstance.transform.localPosition = new Vector3(-225, 600, -500);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;

                case "golemplains":
                    skybox.transform.GetChild(1).GetComponent<Light>().intensity = 3f;
                    skybox.transform.GetChild(1).GetComponent<Light>().shadowStrength = 0.75f;
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(40, 90, 211);
                    sunInstance.transform.localPosition = new Vector3(-500, 0, -500);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;

                case "golemplains2":
                    skybox.transform.GetChild(1).GetComponent<Light>().intensity = 3f;
                    skybox.transform.GetChild(1).GetComponent<Light>().shadowStrength = 0.75f;
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(40, 180, 211);
                    sunInstance.transform.localPosition = new Vector3(500, 300, 2000);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 270, 0);
                    break;

                case "goolake":
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(40, 330, 211);
                    sunInstance.transform.localPosition = new Vector3(500, 100, -250);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 30, 0);
                    break;

                case "foggyswamp":
                    skybox.transform.GetChild(1).gameObject.SetActive(false);
                    sunInstance.transform.localPosition = new Vector3(1000, 0, -1000);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 80, 0);
                    break;

                case "wispgraveyard":
                    skybox.transform.GetChild(1).GetComponent<Light>().intensity = 3f;
                    skybox.transform.GetChild(1).GetComponent<Light>().shadowStrength = 0.75f;
                    break;

                case "frozenwall":
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(40, 90, 211);
                    sunInstance.transform.localPosition = new Vector3(-3000, 100, -30);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;

                case "ancientloft":
                    skybox.transform.GetChild(1).eulerAngles = new Vector3(60, 90, 0);
                    skybox.transform.GetChild(1).GetComponent<Light>().intensity = 3f;
                    break;

                case "shipgraveyard":
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(30, 0, 0);
                    sunInstance.transform.localPosition = new Vector3(-1000f, 350f, -1000f);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 80, 0);
                    break;
            }

            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.skyboxMaterial = sunsetSkyboxMat;
            ambLight.ambientIntensity = 1f;
            ambLight.ApplyLighting();
            skybox.transform.GetChild(4).GetChild(0).GetChild(2).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetComponent<MeshRenderer>().sharedMaterials = new Material[1] { sunsetSkyboxMat };
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(6).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(11).gameObject.SetActive(false);
            skybox.transform.GetChild(4).GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

        public static void NightSky()
        {
            var newWeather = Object.Instantiate(eclipseSkybox, Vector3.zero, Quaternion.identity);
            var moonLight = newWeather.transform.GetChild(1).GetComponent<Light>();
            moonLight.color = new Color(0.8f, 0.8f, 1f, 1f);

            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "dampcavesimple")
            {
                moonLight.intensity = 1.25f;
            }
            else if (sceneName == "moon2")
            {
                moonLight.intensity = 0.25f;
            }
            else
            {
                moonLight.intensity = 1f;
            }

            moonLight.shadowStrength = 0.5f;

            newWeather.transform.GetChild(2).GetComponent<PostProcessVolume>().profile = ppSick;

            if (sceneName == "moon2")
            {
                newWeather.transform.GetChild(2).GetComponent<PostProcessVolume>().priority = 9999f;
            }

            var ambLight = newWeather.transform.GetChild(2).GetComponent<SetAmbientLight>();

            if (sceneName == "blackbeach" || sceneName == "blackbeach2")
            {
                ambLight.ambientIntensity = 1.25f;
            }
            else
            {
                ambLight.ambientIntensity = 1f;
            }

            if (sceneName == "frozenwall" || sceneName == "snowyforest" || sceneName == "moon2")
            {
                ambLight.ambientIntensity = 0.5f;
            }

            ambLight.ApplyLighting();
            newWeather.transform.GetChild(0).GetComponent<ReflectionProbe>().Reset();
            newWeather.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
        }

        public static void VoidSky()
        {
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            GameObject probe = GameObject.Find("Reflection Probe");
            if (sun)
                sun.SetActive(false);
            if ((bool)probe)
                probe.SetActive(false);
            string sceneName = SceneManager.GetActiveScene().name;
            GameObject skybox = Object.Instantiate(voidStageSkybox, Vector3.zero, Quaternion.identity);
            switch (sceneName)
            {
                case "blackbeach":
                    skybox.transform.eulerAngles = new Vector3(30, 0, 220);
                    break;

                case "snowyforest":
                    skybox.transform.eulerAngles = new Vector3(0, 100, 140);
                    break;

                case "sulfurpools":
                    skybox.transform.eulerAngles = new Vector3(0, 180, 140);
                    break;

                default:
                    skybox.transform.Rotate(new Vector3(180, 0, 0));
                    break;
            }
            if (sceneName == "goolake")
                skybox.transform.GetChild(0).GetChild(1).GetComponent<Light>().intensity = 3f;
            else if (sceneName == "sulfurpools")
                skybox.transform.GetChild(0).GetChild(1).GetComponent<Light>().intensity = 1.5f;
            else
                skybox.transform.GetChild(0).GetChild(1).GetComponent<Light>().intensity = 1f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetChild(0).GetComponent<SetAmbientLight>();
            if (SceneManager.GetActiveScene().name != "frozenwall" && SceneManager.GetActiveScene().name != "snowyforest")
                ambLight.ambientIntensity = 0.75f;
            else
                ambLight.ambientIntensity = 1f;
            ambLight.ApplyLighting();
        }

        public static void SingularitySky()
        {
            GameObject sun = GameObject.Find("Directional Light (SUN)");
            GameObject probe = GameObject.Find("Reflection Probe");
            if (sun)
                sun.SetActive(false);
            if ((bool)probe)
                probe.SetActive(false);
            GameObject skybox = Object.Instantiate(voidStageSkybox, Vector3.zero, Quaternion.identity);
            if (SceneManager.GetActiveScene().name != "wispgraveyard")
            {
                skybox.transform.Rotate(new Vector3(0, 0, 70));
                skybox.transform.GetChild(0).GetChild(1).localRotation = Quaternion.Euler(0, 280, 180);
            }
            else
            {
                skybox.transform.Rotate(new Vector3(60, 0, 0));
                skybox.transform.GetChild(0).GetChild(1).localRotation = Quaternion.Euler(60, 0, 0);
            }

            skybox.transform.GetChild(0).GetChild(1).GetComponent<Light>().intensity = 2f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.ambientIntensity = 1f;
            ambLight.ApplyLighting();
        }
    }
}