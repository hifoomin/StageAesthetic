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
        private static readonly PostProcessProfile ppPlains = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneGolemplainsFoggy.asset").WaitForCompletion();
        private static readonly PostProcessProfile ppPlainsRoost = Object.Instantiate(Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneGolemplainsFoggy.asset").WaitForCompletion());
        private static readonly PostProcessProfile ppSunset = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneWispGraveyard.asset").WaitForCompletion();
        private static readonly PostProcessProfile ppSick = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/ppSceneMysterySpace.asset").WaitForCompletion();
        private static readonly Material sunMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matAncientLoft_Sun.mat").WaitForCompletion();
        private static readonly Material sunsetSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matSkybox4.mat").WaitForCompletion();
        private static readonly Material spaceSkyboxMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/sulfurpools/matSkyboxSP.mat").WaitForCompletion();
        private static readonly Material spaceStarsMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/eclipseworld/matEclipseStarsSpheres.mat").WaitForCompletion();
        private static readonly GameObject eclipseSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/eclipseworld/Weather, Eclipse.prefab").WaitForCompletion(), "SAEclipseSkybox", false);
        private static readonly GameObject planetariumSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidraid/Weather, Void Raid Starry Night Variant.prefab").WaitForCompletion(), "SADaySkybox", false);
        private static readonly GameObject voidStageSkybox = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/voidstage/Weather, Void Stage.prefab").WaitForCompletion(), "SAVoidSkybox", false);
        private static readonly GameObject sun = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ancientloft/mdlAncientLoft_Terrain.fbx").WaitForCompletion().transform.GetChild(5).GetChild(0).gameObject, "SASun", false);

        public static void SunnyDistantRoostSky()
        {
            GameObject skybox = Object.Instantiate(planetariumSkybox, Vector3.zero, Quaternion.identity);
            skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppPlainsRoost;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.skyboxMaterial = spaceSkyboxMat;
            ambLight.ambientIntensity = 1f;
            ambLight.ApplyLighting();

            var pp = skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            pp.fogColorEnd.value = new Color32(125, 118, 108, 178);
            pp.skyboxStrength.value = 0f;

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
                skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppPlains;
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

            if (SceneManager.GetActiveScene().name != "goolake")
            {
                //skybox.transform.GetChild(1).GetComponent<Light>().color = new Color(1f, 0f, 0f);
                GameObject.Destroy(skybox.transform.GetChild(0).GetComponent<PostProcessVolume>());
            }

            if (SceneManager.GetActiveScene().name == "ancientloft")
                skybox.transform.GetChild(1).GetComponent<Light>().color = new Color(0.75f, 0.25f, 0.25f);
            else
                skybox.transform.GetChild(1).GetComponent<Light>().color = new Color(1f, 0.5f, 0.5f);

            string sceneName = SceneManager.GetActiveScene().name;

            switch (sceneName)
            {
                case "snowyforest":
                    skybox.transform.GetChild(1).gameObject.SetActive(false);
                    sunInstance.transform.localPosition = new Vector3(-225, 600, -500);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;

                case "golemplains":
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(40, 90, 211);
                    sunInstance.transform.localPosition = new Vector3(-500, 0, -500);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;

                case "golemplains2":
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

                case "frozenwall":
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(40, 90, 211);
                    sunInstance.transform.localPosition = new Vector3(-3000, 100, -30);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;

                case "ancientloft":
                    skybox.transform.GetChild(1).GetComponent<Light>().intensity = 0.8f;
                    break;

                case "shipgraveyard":
                    skybox.transform.GetChild(1).rotation = Quaternion.Euler(30, 0, 0);
                    sunInstance.transform.localPosition = new Vector3(-1000, 150, -750);
                    sunInstance.transform.rotation = Quaternion.Euler(0, 80, 0);
                    break;
            }
            if (SceneManager.GetActiveScene().name == "goolake")
                skybox.transform.GetChild(0).GetComponent<PostProcessVolume>().profile = ppPlains;
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
            GameObject newWeather = Object.Instantiate(eclipseSkybox, Vector3.zero, Quaternion.identity);
            Light moonLight = newWeather.transform.GetChild(1).GetComponent<Light>();
            if (SceneManager.GetActiveScene().name == "dampcavesimple")
                moonLight.intensity = 1.5f;
            moonLight.shadowStrength = 0.25f;
            newWeather.transform.GetChild(2).GetComponent<PostProcessVolume>().profile = ppSick;
            newWeather.transform.GetChild(2).GetComponent<PostProcessVolume>().priority = 9999f;
            SetAmbientLight ambLight = newWeather.transform.GetChild(2).GetComponent<SetAmbientLight>();
            if (SceneManager.GetActiveScene().name == "blackbeach" || SceneManager.GetActiveScene().name == "blackbeach2")
                ambLight.ambientIntensity = 1.5f;
            else if (SceneManager.GetActiveScene().name != "ancientloft" && SceneManager.GetActiveScene().name != "foggyswamp")
                ambLight.ambientIntensity = 1.25f;
            else
                ambLight.ambientIntensity = 1f;
            if (SceneManager.GetActiveScene().name == "frozenwall" || SceneManager.GetActiveScene().name == "snowyforest" || SceneManager.GetActiveScene().name == "moon2")
                ambLight.ambientIntensity = 0.5f;

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

            GameObject skybox = Object.Instantiate(voidStageSkybox, Vector3.zero, Quaternion.identity);
            skybox.transform.Rotate(new Vector3(180, 0, 0));
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
                skybox.transform.GetChild(0).GetChild(1).localRotation = Quaternion.Euler(0, 300, 190);
            }
            else
            {
                skybox.transform.Rotate(new Vector3(60, 0, 0));
                skybox.transform.GetChild(0).GetChild(1).localRotation = Quaternion.Euler(60, 0, 0);
            }

            skybox.transform.GetChild(0).GetChild(1).GetComponent<Light>().intensity = 1f;
            SetAmbientLight ambLight = skybox.transform.GetChild(0).GetChild(0).GetComponent<SetAmbientLight>();
            ambLight.ambientIntensity = 1f;
            ambLight.ApplyLighting();
        }
    }
}