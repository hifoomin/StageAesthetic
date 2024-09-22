using UnityEngine;
using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using StageAesthetic.Variants.Stage1;
using StageAesthetic.Variants.Stage2;
using StageAesthetic.Variants.Stage3;
using StageAesthetic.Variants.Stage4;
using StageAesthetic.Variants.Stage5;
using StageAesthetic.Variants.Special;
using RoR2.UI;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.AddressableAssets;

namespace StageAesthetic
{
    public class SwapVariants
    {
        public static string currentVariantName;
        private static PostProcessProfile ppNostalgia = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGolemplains.asset").WaitForCompletion();
        private static PostProcessProfile ppGoolake = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGoolake.asset").WaitForCompletion();

        public static void Initialize()
        {
            SetConfig();
            On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
            SceneManager.sceneLoaded += TitleScreen;
            Run.onRunStartGlobal += ApplyConfig;
            On.RoR2.UI.AssignStageToken.Start += AssignStageToken_Start;
            // On.RoR2.Networking.NetworkManagerSystemSteam.OnClientConnect += (s, u, t) => { };
        }

        private static void AssignStageToken_Start(On.RoR2.UI.AssignStageToken.orig_Start orig, AssignStageToken self)
        {
            orig(self);
            if (DisplayVariantName.Value && currentVariantName != "")
                self.titleText.text += " (" + currentVariantName + ")";
        }

        private static void TitleScreen(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "title")
            {
                var menuBase = GameObject.Find("MainMenu").transform;

                if (TitleScene.Value)
                {
                    var graphicBase = GameObject.Find("HOLDER: Title Background").transform;
                    graphicBase.Find("Terrain").gameObject.SetActive(true);
                    graphicBase.Find("CamDust").gameObject.SetActive(true);
                    graphicBase.Find("Misc Props").Find("DeadCommando").localPosition = new Vector3(16, -2f, 27);

                    var menuRain = menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject.GetComponent<ParticleSystem>();

                    var emission = menuRain.emission;
                    var rateOverTime = emission.rateOverTime;
                    emission.rateOverTime = new ParticleSystem.MinMaxCurve()
                    {
                        constant = 100,
                        constantMax = 100,
                        constantMin = 60,
                        curve = rateOverTime.curve,
                        curveMax = rateOverTime.curveMax,
                        curveMin = rateOverTime.curveMax,
                        curveMultiplier = rateOverTime.curveMultiplier,
                        mode = rateOverTime.mode
                    };

                    var colorOverLifetime = menuRain.colorOverLifetime;
                    colorOverLifetime.enabled = false;

                    menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").eulerAngles = new Vector3(80, 90, 0);

                    var menuWind = GameObject.Find("HOLDER: Title Background").transform.Find("FX").Find("WindZone").gameObject.GetComponent<WindZone>();
                    menuWind.windMain = 0.5f;
                    menuWind.windTurbulence = 1;
                    StopSounds();
                    PlaySound(SoundType.Wind);
                }
            }
        }

        private static void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            ChangeProfile(SceneManager.GetActiveScene().name);
            orig(self);
        }

        private static void ChangeProfile(string sceneName)
        {
            if (!rain)
            {
                rain = Main.stageaesthetic.LoadAsset<GameObject>("Stage Aesthetic Rain.prefab");
                rain.transform.eulerAngles = new Vector3(90, 0, 0);
            }

            if (!snow)
            {
                snow = Main.stageaesthetic.LoadAsset<GameObject>("Stage Aesthetic Snow.prefab");
                snow.transform.eulerAngles = new Vector3(90, 0, 0);
            }

            if (!sand)
            {
                sand = Main.stageaesthetic.LoadAsset<GameObject>("Stage Aesthetic Sand.prefab");
                // sand.transform.eulerAngles = new Vector3(90, 0, 0);
            }
            Debug.LogWarning("Past the prefab loading");
            ulong seed = Run.instance ? (ulong)(Run.instance.GetStartTimeUtc().Ticks ^ (Run.instance.stageClearCount << 16)) : 0;
            Xoroshiro128Plus rng = new(seed);

            var currentScene = SceneInfo.instance;
            if (currentScene) volume = currentScene.GetComponent<PostProcessVolume>();

            if (!volume || !volume.isActiveAndEnabled)
            {
                GameObject alt = GameObject.Find("PP + Amb");
                if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                {
                    alt = GameObject.Find("PP, Global");
                    // AesLog.LogError("alt is " + alt.name);
                }
                if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                {
                    alt = GameObject.Find("GlobalPostProcessVolume, Base");
                    // AesLog.LogError("alt is " + alt.name);
                }
                if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                {
                    alt = GameObject.Find("PP+Amb");
                    // AesLog.LogError("alt is " + alt.name);
                }
                if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
                {
                    alt = GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("SandOvercast")?.gameObject;
                    // AesLog.LogError("alt is " + alt.name);
                }
                if (alt)
                {
                    volume = alt.GetComponent<PostProcessVolume>();
                    // AesLog.LogError("setting volume to alt");
                }
                if (sceneName == "moon2")
                {
                    // AesLog.LogError("scenename is moon2");
                    volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                    volume.profile.AddSettings<RampFog>();

                    volume.enabled = true;
                    volume.isGlobal = true;
                    volume.priority = 9999f;
                }
            }
            if (volume)
            {
                var rampFog = volume.profile.GetSetting<RampFog>();

                var colorGrading = volume.profile.GetSetting<ColorGrading>() ?? volume.profile.AddSettings<ColorGrading>();
                switch (sceneName)
                {
                    case "blackbeach":

                        #region DistantRoost

                        int distantRoostCounter = rng.RangeInt(0, distantRoostList.Count);

                        if (distantRoostList.Count > 1 && distantRoostCounter == distantRoostVariant)
                            distantRoostCounter = (distantRoostCounter + 1) % distantRoostList.Count;

                        string[] distantRoostArray = distantRoostList.ToArray();
                        string selectedDistantRoostVariant = distantRoostArray[distantRoostCounter];
                        if (selectedDistantRoostVariant == "Vanilla")
                        {
                            if (DistantRoostChanges.Value)
                                DistantRoost.Vanilla();
                            DistantRoost.VanillaFoliage();
                            StopSounds();
                            PlaySound(SoundType.Wind);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedDistantRoostVariant)
                            {
                                case "Sunny":
                                    DistantRoost.Sunny(rampFog, sceneName, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Overcast":
                                    DistantRoost.Overcast(rampFog, sceneName);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Void":
                                    GameObject cliff = GameObject.Find("mdlBBCliffLarge1 (6)");
                                    GameObject cliff2 = GameObject.Find("mdlBBCliffLarge1 (5)");
                                    if (cliff)
                                        GameObject.Destroy(cliff);
                                    if (cliff2)
                                        GameObject.Destroy(cliff2);
                                    DistantRoost.Void();
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedDistantRoostVariant;
                        distantRoostVariant = distantRoostCounter;

                        #endregion DistantRoost

                        break;

                    case "blackbeach2":

                        #region DistantRoostAlt

                        int distantRoostAltCounter = rng.RangeInt(0, distantRoostAltList.Count);

                        if (distantRoostAltList.Count > 1 && distantRoostAltCounter == distantRoostAltVariant)
                            distantRoostAltCounter = (distantRoostAltCounter + 1) % distantRoostAltList.Count;

                        string[] distantRoostAltArray = distantRoostAltList.ToArray();
                        string selectedDistantRoostAltVariant = distantRoostAltArray[distantRoostAltCounter];
                        if (selectedDistantRoostAltVariant == "Vanilla")
                        {
                            if (DistantRoostChanges.Value)
                                DistantRoost.Vanilla();
                            DistantRoost.VanillaFoliage();
                            StopSounds();
                            PlaySound(SoundType.Wind);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedDistantRoostAltVariant)
                            {
                                case "Sunny":
                                    DistantRoost.Sunny(rampFog, sceneName, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Overcast":
                                    DistantRoost.Overcast(rampFog, sceneName);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Night":
                                    DistantRoost.Night(rampFog, sceneName, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Abyssal":
                                    DistantRoost.Abyssal(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.WaterStream);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedDistantRoostAltVariant;
                        distantRoostAltVariant = distantRoostAltCounter;

                        #endregion DistantRoostAlt

                        break;

                    case "snowyforest":

                        #region SiphonedForest

                        int siphonedForestCounter = rng.RangeInt(0, siphonedForestList.Count);

                        if (siphonedForestList.Count > 1 && siphonedForestCounter == siphonedForestVariant)
                            siphonedForestCounter = (siphonedForestCounter + 1) % siphonedForestList.Count;

                        string[] siphonedForestArray = siphonedForestList.ToArray();
                        string selectedSiphonedForestVariant = siphonedForestArray[siphonedForestCounter];
                        if (selectedSiphonedForestVariant == "Vanilla")
                        {
                            SiphonedForest.Vanilla();
                            StopSounds();
                            PlaySound(SoundType.Wind);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedSiphonedForestVariant)
                            {
                                case "Night":
                                    SiphonedForest.Night(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Morning":
                                    SiphonedForest.Morning(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Purple":
                                    SiphonedForest.Purple(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Crimson":
                                    SiphonedForest.Crimson(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                case "Desolate":
                                    SiphonedForest.Desolate(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedSiphonedForestVariant;
                        siphonedForestVariant = siphonedForestCounter;

                        #endregion SiphonedForest

                        break;

                    case string n when (n == "golemplains" || n == "golemplains2"):

                        #region TitanicPlainsAndAlt

                        int titanicPlainsCounter = rng.RangeInt(0, titanicPlainsList.Count);

                        if (titanicPlainsList.Count > 1 && titanicPlainsCounter == titanicPlainsVariant)
                            titanicPlainsCounter = (titanicPlainsCounter + 1) % titanicPlainsList.Count;

                        string[] titanicPlainsArray = titanicPlainsList.ToArray();
                        string selectedTitanicPlainsVariant = titanicPlainsArray[titanicPlainsCounter];
                        if (selectedTitanicPlainsVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.DayNature);
                        }
                        else
                            switch (selectedTitanicPlainsVariant)
                            {
                                case "Nostalgic":
                                    volume.profile = ppNostalgia;
                                    TitanicPlains.Nostalgic(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Sunset":
                                    TitanicPlains.Sunset(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Overcast":
                                    TitanicPlains.Overcast(rampFog, sceneName);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Night":
                                    GameObject.Destroy(volume);
                                    TitanicPlains.Night(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Abandoned":
                                    TitanicPlains.Abandoned(rampFog, ppGoolake);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;
                            }
                        currentVariantName = selectedTitanicPlainsVariant;
                        titanicPlainsVariant = titanicPlainsCounter;

                        #endregion TitanicPlainsAndAlt

                        break;

                    case "habitat":

                        #region TreebornColony

                        int treebornColonyCounter = rng.RangeInt(0, treebornColonyList.Count);

                        if (treebornColonyList.Count > 1 && treebornColonyCounter == treebornColonyVariant)
                            treebornColonyCounter = (treebornColonyCounter + 1) % treebornColonyList.Count;

                        string[] treebornColonyArray = treebornColonyList.ToArray();
                        string selectedtreebornColonyVariant = treebornColonyArray[treebornColonyCounter];

                        if (selectedtreebornColonyVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedtreebornColonyVariant)
                            {
                                case "Sunny":
                                    SetAmbientLight amb = volume.GetComponent<SetAmbientLight>();
                                    amb.ambientSkyColor = new Color(0.88078f, 0.8431f, 0.5373f, 1);
                                    amb.ApplyLighting();
                                    TreebornColony.Sunny(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Overcast":
                                    SetAmbientLight amb2 = volume.GetComponent<SetAmbientLight>();
                                    amb2.ambientSkyColor = new Color(0.5373f, 0.6354f, 0.6431f, 1);
                                    amb2.ambientIntensity = 0.61f;
                                    amb2.ApplyLighting();
                                    TreebornColony.Meridian(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Night":
                                    TreebornColony.Night();
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;
                            }

                        currentVariantName = selectedtreebornColonyVariant;
                        reformedAltarVariant = treebornColonyCounter;

                        #endregion TreebornColony

                        break;

                    case "lemuriantemple":

                        #region ReformedAltar

                        int reformedAltarCounter = rng.RangeInt(0, reformedAltarList.Count);

                        if (reformedAltarList.Count > 1 && reformedAltarCounter == reformedAltarVariant)
                            reformedAltarCounter = (reformedAltarCounter + 1) % reformedAltarList.Count;

                        string[] reformedAltarArray = reformedAltarList.ToArray();
                        string selectedReformedAltarVariant = reformedAltarArray[reformedAltarCounter];

                        if (selectedReformedAltarVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedReformedAltarVariant)
                            {
                                case "Verdant":
                                    ReformedAltar.Verdant(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Helminth":
                                    ReformedAltar.Helminth(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }

                        currentVariantName = selectedReformedAltarVariant;
                        reformedAltarVariant = reformedAltarCounter;

                        #endregion ReformedAltar

                        break;

                    case "lakes":

                        #region VerdantFalls

                        int verdantFallsCounter = rng.RangeInt(0, verdantFallsList.Count);

                        if (verdantFallsList.Count > 1 && verdantFallsCounter == verdantFallsVariant)
                            verdantFallsCounter = (verdantFallsCounter + 1) % verdantFallsList.Count;

                        string[] verdantFallsArray = verdantFallsList.ToArray();
                        string selectedverdantFallsVariant = verdantFallsArray[verdantFallsCounter];

                        if (selectedverdantFallsVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.WaterStream);
                        }
                        else
                            switch (selectedverdantFallsVariant)
                            {
                                case "Sunny":
                                    VerdantFalls.Sunny(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Purple":
                                    VerdantFalls.Purple(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.WaterStream);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.WaterStream);
                                    break;
                            }

                        currentVariantName = selectedverdantFallsVariant;
                        shatteredAbodesVariant = verdantFallsCounter;

                        #endregion VerdantFalls

                        break;

                    case "village":

                        #region ShatteredAbodes

                        int shatteredAbodesCounter = rng.RangeInt(0, shatteredAbodesList.Count);

                        if (shatteredAbodesList.Count > 1 && shatteredAbodesCounter == shatteredAbodesVariant)
                            shatteredAbodesCounter = (shatteredAbodesCounter + 1) % shatteredAbodesList.Count;

                        string[] shatteredAbodesArray = shatteredAbodesList.ToArray();
                        string selectedShatteredAbodesVariant = shatteredAbodesArray[shatteredAbodesCounter];

                        if (selectedShatteredAbodesVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedShatteredAbodesVariant)
                            {
                                case "Verdant":
                                    ShatteredAbodes.Verdant(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Abandoned":
                                    ShatteredAbodes.Abandoned(rampFog, ppGoolake);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }

                        currentVariantName = selectedShatteredAbodesVariant;
                        shatteredAbodesVariant = shatteredAbodesCounter;

                        #endregion ShatteredAbodes

                        break;

                    case "goolake":

                        #region AbandonedAqueduct

                        int abandonedAqueductCounter = rng.RangeInt(0, abandonedAqueductList.Count);

                        if (abandonedAqueductList.Count > 1 && abandonedAqueductCounter == abandonedAqueductVariant)
                            abandonedAqueductCounter = (abandonedAqueductCounter + 1) % abandonedAqueductList.Count;

                        string[] abandonedAqueductArray = abandonedAqueductList.ToArray();
                        string selectedAbandonedAqueductVariant = abandonedAqueductArray[abandonedAqueductCounter];
                        if (selectedAbandonedAqueductVariant == "Vanilla")
                        {
                            if (AbandonedAqueductChanges.Value)
                                AbandonedAqueduct.VanillaChanges();
                            StopSounds();
                            PlaySound(SoundType.Wind);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedAbandonedAqueductVariant)
                            {
                                case "Dawn":
                                    AbandonedAqueduct.Dawn(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Sunrise":
                                    AbandonedAqueduct.Sunrise(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Night":
                                    AbandonedAqueduct.Night(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Sundered":
                                    AbandonedAqueduct.Sundered(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedAbandonedAqueductVariant;
                        abandonedAqueductVariant = abandonedAqueductCounter;

                        #endregion AbandonedAqueduct

                        break;

                    case "ancientloft":

                        #region AphelianSanctuary

                        int aphelianSanctuaryCounter = rng.RangeInt(0, aphelianSanctuaryList.Count);

                        if (aphelianSanctuaryList.Count > 1 && aphelianSanctuaryCounter == aphelianSanctuaryVariant)
                            aphelianSanctuaryCounter = (aphelianSanctuaryCounter + 1) % aphelianSanctuaryList.Count;

                        string[] aphelianSanctuaryArray = aphelianSanctuaryList.ToArray();
                        string selectedAphelianSanctuaryVariant = aphelianSanctuaryArray[aphelianSanctuaryCounter];
                        if (selectedAphelianSanctuaryVariant == "Vanilla")
                        {
                            GameObject sun = GameObject.Find("AL_Sun");
                            if (sun)
                            {
                                sun.SetActive(false);
                                GameObject newSun = GameObject.Instantiate(Skybox.sun, sun.transform.parent);
                                newSun.transform.localPosition = new Vector3(-897.0126f, 350f, 209.9904f);
                                newSun.transform.eulerAngles = new Vector3(275f, 90f, 90f);
                                newSun.GetComponent<MeshRenderer>().sharedMaterial = Skybox.sunMat;
                            }
                            StopSounds();
                            PlaySound(SoundType.Wind);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedAphelianSanctuaryVariant)
                            {
                                case "Singularity":
                                    AphelianSanctuary.Singularity(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Twilight":
                                    AphelianSanctuary.Twilight(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Sunset":
                                    GameObject sun = GameObject.Find("AL_Sun");
                                    if (sun)
                                    {
                                        sun.SetActive(false);
                                        GameObject newSun = GameObject.Instantiate(Skybox.sun, sun.transform.parent);
                                        newSun.transform.localPosition = new Vector3(-897.0126f, 350f, 209.9904f);
                                        newSun.transform.eulerAngles = new Vector3(275f, 90f, 90f);
                                        newSun.GetComponent<MeshRenderer>().sharedMaterial = Skybox.sunMat;
                                    }
                                    AphelianSanctuary.Sunset(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Abyssal":
                                    AphelianSanctuary.Abyssal(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedAphelianSanctuaryVariant;
                        aphelianSanctuaryVariant = aphelianSanctuaryCounter;

                        #endregion AphelianSanctuary

                        break;
                    /*
                    case "drybasin":

                        #region DryBasin

                        ForgottenRelicsJ.DryBasin(rng, colorGrading);

                        #endregion DryBasin

                        break;
                    */
                    case "foggyswamp":

                        #region WetlandAspect

                        int wetlandAspectCounter = rng.RangeInt(0, wetlandAspectList.Count);

                        if (wetlandAspectList.Count > 1 && wetlandAspectCounter == wetlandAspectVariant)
                            wetlandAspectCounter = (wetlandAspectCounter + 1) % wetlandAspectList.Count;

                        string[] wetlandAspectArray = wetlandAspectList.ToArray();
                        string selectedWetlandAspectVariant = wetlandAspectArray[wetlandAspectCounter];
                        if (selectedWetlandAspectVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedWetlandAspectVariant)
                            {
                                case "Sunset":
                                    WetlandAspect.Sunset(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Morning":
                                    WetlandAspect.Morning(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Night":
                                    WetlandAspect.Night(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                case "Void":
                                    WetlandAspect.Void(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedWetlandAspectVariant;
                        wetlandAspectVariant = wetlandAspectCounter;

                        #endregion WetlandAspect

                        break;

                    case "frozenwall":

                        #region RallypointDelta

                        int rallypointDeltaCounter = rng.RangeInt(0, rallypointDeltaList.Count);

                        if (rallypointDeltaList.Count > 1 && rallypointDeltaCounter == rallypointDeltaVariant)
                            rallypointDeltaCounter = (rallypointDeltaCounter + 1) % rallypointDeltaList.Count;

                        string[] rallypointDeltaArray = rallypointDeltaList.ToArray();
                        string selectedRallypointDeltaVariant = rallypointDeltaArray[rallypointDeltaCounter];
                        if (selectedRallypointDeltaVariant == "Vanilla")
                        {
                            RallypointDelta.VanillaChanges();
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedRallypointDeltaVariant)
                            {
                                case "Night":
                                    RallypointDelta.Night(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Overcast":
                                    RallypointDelta.Overcast(rampFog, volume);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                case "Sunset":
                                    RallypointDelta.Sunset(rampFog, volume);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Titanic":
                                    RallypointDelta.Titanic(rampFog, colorGrading, volume);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedRallypointDeltaVariant;
                        rallypointDeltaVariant = rallypointDeltaCounter;

                        #endregion RallypointDelta

                        break;

                    case "wispgraveyard":

                        #region ScorchedAcres

                        int scorchedAcresCounter = rng.RangeInt(0, scorchedAcresList.Count);

                        if (scorchedAcresList.Count > 1 && scorchedAcresCounter == scorchedAcresVariant)
                            scorchedAcresCounter = (scorchedAcresCounter + 1) % scorchedAcresList.Count;

                        string[] scorchedAcresArray = scorchedAcresList.ToArray();
                        string selectedScorchedAcresVariant = scorchedAcresArray[scorchedAcresCounter];
                        if (selectedScorchedAcresVariant == "Vanilla")
                        {
                            if (ScorchedAcresChanges.Value)
                                ScorchedAcres.VanillaChanges();
                            StopSounds();
                            PlaySound(SoundType.DayNature);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedScorchedAcresVariant)
                            {
                                case "Sunset":
                                    ScorchedAcres.Sunset(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Night":
                                    ScorchedAcres.Night(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Jade":
                                    ScorchedAcres.Jade(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Sunny Beta":
                                    ScorchedAcres.SunnyBeta(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Crimson Beta":
                                    ScorchedAcres.CrimsonBeta(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                case "Twilight":
                                    ScorchedAcres.Twilight(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;
                            }
                        currentVariantName = selectedScorchedAcresVariant;
                        scorchedAcresVariant = scorchedAcresCounter;

                        #endregion ScorchedAcres

                        break;

                    case "sulfurpools":

                        #region SulfurPools

                        int sulfurPoolsCounter = rng.RangeInt(0, sulfurPoolsList.Count);
                        if (sulfurPoolsList.Count > 1 && sulfurPoolsCounter == sulfurPoolsVariant)
                            sulfurPoolsCounter = (sulfurPoolsCounter + 1) % sulfurPoolsList.Count;

                        string[] sulfurPoolsArray = sulfurPoolsList.ToArray();
                        string selectedSulfurPoolsVariant = sulfurPoolsArray[sulfurPoolsCounter];
                        if (selectedSulfurPoolsVariant == "Vanilla")
                        {
                            SulfurPools.Vanilla();
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.WaterStream);
                        }
                        else
                            switch (selectedSulfurPoolsVariant)
                            {
                                case "Coral":
                                    SulfurPools.Coral(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.WaterStream);
                                    break;

                                case "Hell":
                                    SulfurPools.Hell(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Void":
                                    SulfurPools.Void(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.WaterStream);
                                    break;
                            }
                        currentVariantName = selectedSulfurPoolsVariant;
                        sulfurPoolsVariant = sulfurPoolsCounter;

                        #endregion SulfurPools

                        break;

                    case "FBLScene":

                        #region FogboundLagoon

                        int fogboundLagoonCounter = rng.RangeInt(0, fogboundLagoonList.Count);

                        if (fogboundLagoonList.Count > 1 && fogboundLagoonCounter == fogboundLagoonVariant)
                            fogboundLagoonCounter = (fogboundLagoonCounter + 1) % fogboundLagoonList.Count;

                        string[] fogboundLagoonArray = fogboundLagoonList.ToArray();
                        string selectedFogboundLagoonVariant = fogboundLagoonArray[fogboundLagoonCounter];
                        if (selectedFogboundLagoonVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.WaterStream);
                        }
                        else
                            switch (selectedFogboundLagoonVariant)
                            {
                                case "Clear":
                                    FogboundLagoon.Clear(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    PlaySound(SoundType.WaterStream);
                                    break;

                                case "Twilight":
                                    FogboundLagoon.Twilight(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    PlaySound(SoundType.WaterStream);
                                    break;

                                case "Overcast":
                                    FogboundLagoon.Overcast(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.WaterStream);
                                    break;
                            }
                        currentVariantName = selectedFogboundLagoonVariant;
                        fogboundLagoonVariant = fogboundLagoonCounter;

                        #endregion FogboundLagoon

                        break;

                    case "dampcavesimple":

                        #region AbyssalDepths

                        int abyssalDepthsCounter = rng.RangeInt(0, abyssalDepthsList.Count);

                        if (abyssalDepthsList.Count > 1 && abyssalDepthsCounter == abyssalDepthsVariant)
                            abyssalDepthsCounter = (abyssalDepthsCounter + 1) % abyssalDepthsList.Count;

                        string[] abyssalDepthsArray = abyssalDepthsList.ToArray();
                        string selectedAbyssalDepthsVariant = abyssalDepthsArray[abyssalDepthsCounter];
                        if (selectedAbyssalDepthsVariant == "Vanilla")
                        {
                            if (AbyssalDepthsChanges.Value)
                                AbyssalDepths.VanillaChanges();
                            StopSounds();
                            PlaySound(SoundType.Wind);
                            currentVariantName = "Vanilla";
                        }
                        else
                        {
                            GameObject camera = GameObject.Find("Main Camera(Clone)");
                            if (camera)
                                camera.transform.GetChild(0).GetComponent<PostProcessLayer>().breakBeforeColorGrading = true;

                            switch (selectedAbyssalDepthsVariant)
                            {
                                case "Blue":
                                    AbyssalDepths.Blue(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Night":
                                    AbyssalDepths.Night(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Orange":
                                    AbyssalDepths.Orange(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                case "Coral":
                                    AbyssalDepths.Coral(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        }
                        currentVariantName = selectedAbyssalDepthsVariant;
                        abyssalDepthsVariant = abyssalDepthsCounter;

                        #endregion AbyssalDepths

                        break;

                    case "shipgraveyard":

                        #region SirensCall

                        int sirensCallCounter = rng.RangeInt(0, sirensCallList.Count);

                        if (sirensCallList.Count > 1 && sirensCallCounter == sirensCallVariant)
                            sirensCallCounter = (sirensCallCounter + 1) % sirensCallList.Count;

                        string[] sirensCallArray = sirensCallList.ToArray();
                        string selectedSirensCallVariant = sirensCallArray[sirensCallCounter];
                        if (selectedSirensCallVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedSirensCallVariant)
                            {
                                case "Night":
                                    SirensCall.Night(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Sunny":
                                    SirensCall.Sunny(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Overcast":
                                    SirensCall.Overcast(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Aphelian":
                                    SirensCall.Aphelian(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedSirensCallVariant;
                        sirensCallVariant = sirensCallCounter;

                        #endregion SirensCall

                        break;

                    case "rootjungle":

                        #region SunderedGrove

                        int sunderedGroveCounter = rng.RangeInt(0, sunderedGroveList.Count);

                        if (sunderedGroveList.Count > 1 && sunderedGroveCounter == sunderedGroveVariant)
                            sunderedGroveCounter = (sunderedGroveCounter + 1) % sunderedGroveList.Count;

                        string[] sunderedGroveArray = sunderedGroveList.ToArray();
                        string selectedSunderedGroveVariant = sunderedGroveArray[sunderedGroveCounter];
                        if (selectedSunderedGroveVariant == "Vanilla")
                        {
                            SunderedGrove.Vanilla();
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Wind);
                        }
                        else
                            switch (selectedSunderedGroveVariant)
                            {
                                case "Jade":
                                    SunderedGrove.Jade(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                case "Sunny":
                                    SunderedGrove.Sunny(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Overcast":
                                    SunderedGrove.Overcast(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Abandoned":
                                    SunderedGrove.Abandoned(rampFog, ppGoolake);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;
                            }
                        currentVariantName = selectedSunderedGroveVariant;
                        sunderedGroveVariant = sunderedGroveCounter;

                        #endregion SunderedGrove

                        break;

                    case "helminthroost":

                        #region HelminthHatchery

                        int helminthHatcheryCounter = rng.RangeInt(0, helminthHatcheryList.Count);

                        if (helminthHatcheryList.Count > 1 && helminthHatcheryCounter == helminthHatcheryVariant)
                            helminthHatcheryCounter = (helminthHatcheryCounter + 1) % helminthHatcheryList.Count;

                        string[] helminthHatcheryArray = helminthHatcheryList.ToArray();
                        string selectedHelminthHatcheryVariant = helminthHatcheryArray[helminthHatcheryCounter];
                        if (selectedHelminthHatcheryVariant == "Vanilla")
                        {
                            if (HelminthHatcheryVanillaChanges.Value)
                                HelminthHatchery.VanillaChanges(rampFog);
                            StopSounds();
                            PlaySound(SoundType.Void);
                            currentVariantName = "Vanilla";
                        }
                        else
                        {
                            switch (selectedHelminthHatcheryVariant)
                            {
                                case "Lunar":
                                    HelminthHatchery.Lunar(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;
                            }
                        }
                        currentVariantName = selectedHelminthHatcheryVariant;
                        helminthHatcheryVariant = helminthHatcheryCounter;

                        #endregion HelminthHatchery

                        break;

                    case "skymeadow":

                        #region SkyMeadow

                        int skyMeadowCounter = rng.RangeInt(0, skyMeadowList.Count);

                        if (skyMeadowList.Count > 1 && skyMeadowCounter == skyMeadowVariant)
                            skyMeadowCounter = (skyMeadowCounter + 1) % skyMeadowList.Count;

                        string[] skyMeadowArray = skyMeadowList.ToArray();
                        string selectedSkyMeadowVariant = skyMeadowArray[skyMeadowCounter];
                        if (selectedSkyMeadowVariant == "Vanilla")
                        {
                            if (SkyMeadowChanges.Value)
                                SkyMeadow.VanillaChanges();
                            StopSounds();
                            PlaySound(SoundType.DayNature);
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedSkyMeadowVariant)
                            {
                                case "Night":
                                    SkyMeadow.Night(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.NightNature);
                                    break;

                                case "Overcast":
                                    SkyMeadow.Overcast(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Abyssal":
                                    SkyMeadow.Abyssal(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                case "Titanic":
                                    SkyMeadow.Titanic(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;

                                case "Abandoned":
                                    SkyMeadow.Abandoned(rampFog, ppGoolake);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.DayNature);
                                    break;
                            }
                        currentVariantName = selectedSkyMeadowVariant;
                        skyMeadowVariant = skyMeadowCounter;

                        #endregion SkyMeadow

                        break;
                    /*
                    case "slumberingsatellite":

                        #region SlumberingSatellite

                        ForgottenRelicsJ.SlumberingSatellite(rng, volume);

                        #endregion SlumberingSatellite

                        break;
                    */
                    case "moon2":

                        #region Commencement

                        int commencementCounter = rng.RangeInt(0, commencementList.Count);

                        if (commencementList.Count > 1 && commencementCounter == commencementVariant)
                            commencementCounter = (commencementCounter + 1) % commencementList.Count;

                        string[] commencementArray = commencementList.ToArray();
                        string selectedCommencementVariant = commencementArray[commencementCounter];
                        if (selectedCommencementVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Rain);
                        }
                        else
                            switch (selectedCommencementVariant)
                            {
                                case "Night":
                                    Commencement.Night(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Thunder);
                                    break;

                                case "Crimson":
                                    Commencement.Crimson(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                case "Corruption":
                                    Commencement.Corruption(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;

                                case "Gray":
                                    Commencement.Gray(rampFog);
                                    StopSounds();
                                    PlaySound(SoundType.Wind);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Rain);
                                    break;
                            }
                        currentVariantName = selectedCommencementVariant;
                        commencementVariant = commencementCounter;

                        #endregion Commencement

                        break;

                    case "voidstage":

                        #region VoidLocus

                        int voidLocusCounter = rng.RangeInt(0, voidLocusList.Count);

                        if (voidLocusList.Count > 1 && voidLocusCounter == voidLocusVariant)
                            voidLocusCounter = (voidLocusCounter + 1) % voidLocusList.Count;

                        string[] voidLocusArray = voidLocusList.ToArray();
                        string selectedVoidLocusVariant = voidLocusArray[voidLocusCounter];
                        if (selectedVoidLocusVariant == "Vanilla")
                        {
                            currentVariantName = "Vanilla";
                            StopSounds();
                            PlaySound(SoundType.Void);
                        }
                        else
                            switch (selectedVoidLocusVariant)
                            {
                                case "Twilight":
                                    VoidLocus.Twilight(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                case "Pink":
                                    VoidLocus.Pink(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                case "Blue":
                                    VoidLocus.Blue(rampFog, colorGrading);
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    StopSounds();
                                    PlaySound(SoundType.Void);
                                    break;
                            }
                        currentVariantName = selectedVoidLocusVariant;
                        voidLocusVariant = voidLocusCounter;

                        #endregion VoidLocus

                        break;
                }

                volume.profile.name = "SA Profile" + " (" + currentVariantName + ")";
            }
        }

        public static PostProcessVolume volume;

        #region Jank

        public static int distantRoostVariant = -1;
        public static int distantRoostAltVariant = -1;
        public static int siphonedForestVariant = -1;
        public static int titanicPlainsVariant = -1;
        public static int titanicPlainsAltVariant = -1;
        public static int shatteredAbodesVariant = -1;
        public static int verdantFallsVariant = -1;

        public static int reformedAltarVariant = -1;
        public static int treebornColonyVariant = -1;

        public static int abandonedAqueductVariant = -1;
        public static int aphelianSanctuaryVariant = -1;
        public static int dryBasinVariant = -1;
        public static int wetlandAspectVariant = -1;

        public static int fogboundLagoonVariant = -1;
        public static int rallypointDeltaVariant = -1;
        public static int scorchedAcresVariant = -1;
        public static int sulfurPoolsVariant = -1;

        public static int abyssalDepthsVariant = -1;
        public static int sirensCallVariant = -1;
        public static int sunderedGroveVariant = -1;

        public static int skyMeadowVariant = -1;
        public static int helminthHatcheryVariant = -1;
        public static int slumberingSatelliteVariant = -1;

        public static int commencementVariant = -1;

        public static int voidLocusVariant = -1;

        public static int currentVariant;

        #endregion Jank

        public static GameObject rain;
        public static GameObject snow;
        public static GameObject sand;

        internal static BepInEx.Logging.ManualLogSource SALogger;

        #region VariantContainers

        public static List<string> distantRoostList = new();
        public static List<string> distantRoostAltList = new();
        public static List<string> siphonedForestList = new();
        public static List<string> titanicPlainsList = new();
        public static List<string> shatteredAbodesList = new();
        public static List<string> verdantFallsList = new();

        public static List<string> reformedAltarList = new();
        public static List<string> treebornColonyList = new();

        public static List<string> abandonedAqueductList = new();
        public static List<string> aphelianSanctuaryList = new();
        public static List<string> dryBasinList = new();
        public static List<string> wetlandAspectList = new();

        public static List<string> fogboundLagoonList = new();
        public static List<string> rallypointDeltaList = new();
        public static List<string> scorchedAcresList = new();
        public static List<string> sulfurPoolsList = new();

        public static List<string> abyssalDepthsList = new();
        public static List<string> sirensCallList = new();
        public static List<string> sunderedGroveList = new();

        public static List<string> skyMeadowList = new();
        public static List<string> helminthHatcheryList = new();
        public static List<string> slumberingSatelliteList = new();

        public static List<string> commencementList = new();
        public static List<string> voidLocusList = new();

        #endregion VariantContainers
    }

    /*
    public class ForgottenRelicsJ
    {
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void DryBasin(Xoroshiro128Plus rng, ColorGrading colorGrading)
        {
            if (Main.ForgottenRelicsLoaded)
            {
                var sandStorm = GameObject.Find("MapZones").transform.GetChild(1).Find("Sandstorm").GetComponent<PostProcessVolume>();

                int dryBasinCounter = rng.RangeInt(0, SwapVariants.dryBasinList.Count);

                var garbage = sandStorm.GetComponent<FRCSharp.TheCoolerRampFog>();
                var rampFog2 = sandStorm.profile.GetSetting<RampFog>();

                if (SwapVariants.dryBasinList.Count > 1 && dryBasinCounter == SwapVariants.dryBasinVariant)
                    dryBasinCounter = (dryBasinCounter + 1) % SwapVariants.dryBasinList.Count;

                string[] dryBasinArray = SwapVariants.dryBasinList.ToArray();
                string selectedDryBasinVariant = dryBasinArray[dryBasinCounter];
                if (selectedDryBasinVariant == "Vanilla")
                {
                    if (DryBasinChanges.Value)
                        Variants.Stage2.DryBasin.VanillaChanges();

                    SwapVariants.currentVariantName = "Vanilla";
                }
                else
                    switch (selectedDryBasinVariant)
                    {
                        case "Morning":
                            Variants.Stage2.DryBasin.Morning(garbage, rampFog2);

                            break;

                        case "Blue":
                            Variants.Stage2.DryBasin.Blue(garbage, colorGrading, rampFog2);

                            break;

                        case "Overcast":
                            Variants.Stage2.DryBasin.Overcast(garbage, colorGrading, rampFog2);

                            break;

                        default:
                            SwapVariants.SALogger.LogDebug("uwu I messed something up forgive me >w<");

                            break;
                    }
                SwapVariants.currentVariantName = selectedDryBasinVariant;
                SwapVariants.dryBasinVariant = dryBasinCounter;
                sandStorm.profile.name = "SA Profile" + " (" + SwapVariants.currentVariantName + ")";
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void SlumberingSatellite(Xoroshiro128Plus rng, PostProcessVolume volume)
        {
            if (Main.ForgottenRelicsLoaded)
            {
                int slumberingSatelliteCounter = rng.RangeInt(0, SwapVariants.slumberingSatelliteList.Count);

                var garbage = volume.GetComponent<FRCSharp.TheCoolerRampFog>();
                var rampFog2 = volume.profile.GetSetting<RampFog>();

                if (SwapVariants.slumberingSatelliteList.Count > 1 && slumberingSatelliteCounter == SwapVariants.slumberingSatelliteVariant)
                    slumberingSatelliteCounter = (slumberingSatelliteCounter + 1) % SwapVariants.slumberingSatelliteList.Count;

                string[] slumberingSatelliteArray = SwapVariants.slumberingSatelliteList.ToArray();
                string selectedSlumberingSatelliteVariant = slumberingSatelliteArray[slumberingSatelliteCounter];
                if (selectedSlumberingSatelliteVariant == "Vanilla")
                {
                    Variants.Stage5.SlumberingSatellite.Vanilla();
                    SwapVariants.currentVariantName = "Vanilla";
                }
                else
                    switch (selectedSlumberingSatelliteVariant)
                    {
                        case "Morning":
                            Variants.Stage5.SlumberingSatellite.Morning(garbage, rampFog2);

                            break;

                        case "Overcast":
                            Variants.Stage5.SlumberingSatellite.Overcast(garbage, rampFog2);

                            break;

                        case "Blue":
                            Variants.Stage5.SlumberingSatellite.Blue(garbage, rampFog2);

                            break;

                        default:
                            SwapVariants.SALogger.LogDebug("uwu I messed something up forgive me >w<");

                            break;
                    }
                SwapVariants.currentVariantName = selectedSlumberingSatelliteVariant;
                SwapVariants.slumberingSatelliteVariant = slumberingSatelliteCounter;
            }
        }
    }
    */
}