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
using System.Runtime.CompilerServices;
using System;
using System.Collections;

namespace StageAesthetic
{
    public class SwapVariants
    {
        public static string currentVariantName;

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
                rain = Main.stageaesthetic.LoadAsset<GameObject>("Assets/StageAesthetic/Stage Aesthetic Rain.prefab");
                rain.transform.eulerAngles = new Vector3(90, 0, 0);
            }

            if (!snow)
            {
                snow = Main.stageaesthetic.LoadAsset<GameObject>("Assets/StageAesthetic/Stage Aesthetic Snow.prefab");
                snow.transform.eulerAngles = new Vector3(90, 0, 0);
            }

            if (!sand)
            {
                sand = Main.stageaesthetic.LoadAsset<GameObject>("Assets/StageAesthetic/Stage Aesthetic Sand.prefab");
                // sand.transform.eulerAngles = new Vector3(90, 0, 0);
            }

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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedDistantRoostVariant)
                            {
                                case "Sunny":
                                    DistantRoost.Sunny(rampFog, sceneName, colorGrading);
                                    break;

                                case "Overcast":
                                    DistantRoost.Overcast(rampFog, sceneName);
                                    break;

                                case "Void":
                                    DistantRoost.Void(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedDistantRoostAltVariant)
                            {
                                case "Sunny":
                                    DistantRoost.Sunny(rampFog, sceneName, colorGrading);
                                    break;

                                case "Overcast":
                                    DistantRoost.Overcast(rampFog, sceneName);
                                    break;

                                case "Night":
                                    DistantRoost.Night(rampFog, sceneName, colorGrading);
                                    break;

                                case "Abyssal":
                                    DistantRoost.Abyssal(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedSiphonedForestVariant)
                            {
                                case "Night":
                                    SiphonedForest.Night(rampFog, colorGrading);
                                    break;

                                case "Morning":
                                    SiphonedForest.Morning(rampFog, colorGrading);
                                    break;

                                case "Purple":
                                    SiphonedForest.Purple(rampFog, colorGrading);
                                    break;

                                case "Crimson":
                                    SiphonedForest.Crimson(rampFog, colorGrading);
                                    break;

                                case "Desolate":
                                    SiphonedForest.Desolate(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                        }
                        else
                            switch (selectedTitanicPlainsVariant)
                            {
                                case "Nostalgic":
                                    TitanicPlains.Nostalgic(rampFog);
                                    break;

                                case "Sunset":
                                    TitanicPlains.Sunset(rampFog);
                                    break;

                                case "Overcast":
                                    TitanicPlains.Overcast(rampFog, sceneName);
                                    break;

                                case "Night":
                                    TitanicPlains.Night(rampFog, colorGrading);
                                    break;

                                case "Abandoned":
                                    TitanicPlains.Abandoned(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    break;
                            }
                        currentVariantName = selectedTitanicPlainsVariant;
                        titanicPlainsVariant = titanicPlainsCounter;

                        #endregion TitanicPlainsAndAlt

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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedAbandonedAqueductVariant)
                            {
                                case "Dawn":
                                    AbandonedAqueduct.Dawn(rampFog);
                                    break;

                                case "Sunrise":
                                    AbandonedAqueduct.Sunrise(rampFog);
                                    break;

                                case "Night":
                                    AbandonedAqueduct.Night(rampFog, colorGrading);
                                    break;

                                case "Sundered":
                                    AbandonedAqueduct.Sundered(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedAphelianSanctuaryVariant)
                            {
                                case "Singularity":
                                    AphelianSanctuary.Singularity(rampFog, colorGrading);
                                    break;

                                case "Twilight":
                                    AphelianSanctuary.Twilight(rampFog, colorGrading);
                                    break;

                                case "Sunset":
                                    AphelianSanctuary.Sunset(rampFog, colorGrading);
                                    break;

                                case "Abyssal":
                                    AphelianSanctuary.Abyssal(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    break;
                            }
                        currentVariantName = selectedAphelianSanctuaryVariant;
                        aphelianSanctuaryVariant = aphelianSanctuaryCounter;

                        #endregion AphelianSanctuary

                        break;

                    case "drybasin":

                        #region DryBasin

                        SALogger.LogError("Loading Dry Basin, Forgotten Relics Loaded: " + Main.ForgottenRelicsLoaded);
                        if (Main.ForgottenRelicsLoaded)
                        {
                            int dryBasinCounter = rng.RangeInt(0, dryBasinList.Count);

                            var garbage = volume.GetComponent<FRCSharp.TheCoolerRampFog>();
                            if (dryBasinList.Count > 1 && dryBasinCounter == dryBasinVariant)
                                dryBasinCounter = (dryBasinCounter + 1) % dryBasinList.Count;

                            string[] dryBasinArray = dryBasinList.ToArray();
                            string selectedDryBasinVariant = dryBasinArray[dryBasinCounter];
                            if (selectedDryBasinVariant == "Vanilla")
                            {
                                if (DryBasinChanges.Value)
                                    DryBasin.VanillaChanges();
                                currentVariantName = "Vanilla";
                            }
                            else
                                switch (selectedDryBasinVariant)
                                {
                                    case "Morning":
                                        DryBasin.Morning(garbage, rampFog);
                                        break;

                                    case "Blue":
                                        DryBasin.Blue(garbage, colorGrading, rampFog);
                                        break;

                                    case "Overcast":
                                        DryBasin.Overcast(garbage, colorGrading, rampFog);
                                        break;

                                    default:
                                        SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                        break;
                                }
                            currentVariantName = selectedDryBasinVariant;
                            dryBasinVariant = dryBasinCounter;

                            #endregion DryBasin
                        }
                        break;

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
                        }
                        else
                            switch (selectedWetlandAspectVariant)
                            {
                                case "Sunset":
                                    WetlandAspect.Sunset(rampFog, colorGrading);
                                    break;

                                case "Morning":
                                    WetlandAspect.Morning(rampFog, colorGrading);
                                    break;

                                case "Night":
                                    WetlandAspect.Night(rampFog);
                                    break;

                                case "Void":
                                    WetlandAspect.Void(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                        }
                        else
                            switch (selectedRallypointDeltaVariant)
                            {
                                case "Night":
                                    RallypointDelta.Night(rampFog, colorGrading);
                                    break;

                                case "Overcast":
                                    RallypointDelta.Overcast(rampFog, volume);
                                    break;

                                case "Sunset":
                                    RallypointDelta.Sunset(rampFog, volume);
                                    break;

                                case "Titanic":
                                    RallypointDelta.Titanic(rampFog, colorGrading, volume);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedScorchedAcresVariant)
                            {
                                case "Sunset":
                                    ScorchedAcres.Sunset(rampFog, colorGrading);
                                    break;

                                case "Night":
                                    ScorchedAcres.Night(rampFog);
                                    break;

                                case "Jade":
                                    ScorchedAcres.Jade(rampFog);
                                    break;

                                case "SunnyBeta":
                                    ScorchedAcres.SunnyBeta(rampFog);
                                    break;

                                case "CrimsonBeta":
                                    ScorchedAcres.CrimsonBeta(rampFog);
                                    break;

                                case "Twilight":
                                    ScorchedAcres.Twilight(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                        }
                        else
                            switch (selectedSulfurPoolsVariant)
                            {
                                case "Coral":
                                    SulfurPools.Coral(rampFog);
                                    break;

                                case "Hell":
                                    SulfurPools.Hell(rampFog);
                                    break;

                                case "Void":
                                    SulfurPools.Void(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                        }
                        else
                            switch (selectedFogboundLagoonVariant)
                            {
                                case "Clear":
                                    FogboundLagoon.Clear(rampFog);
                                    break;

                                case "Twilight":
                                    FogboundLagoon.Twilight(rampFog);
                                    break;

                                case "Overcast":
                                    FogboundLagoon.Overcast(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedAbyssalDepthsVariant)
                            {
                                case "Blue":
                                    AbyssalDepths.Blue(rampFog, colorGrading);
                                    break;

                                case "Night":
                                    AbyssalDepths.Night(rampFog, colorGrading);
                                    break;

                                case "Orange":
                                    AbyssalDepths.Orange(rampFog);
                                    break;

                                case "Coral":
                                    AbyssalDepths.Coral(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    break;
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
                        }
                        else
                            switch (selectedSirensCallVariant)
                            {
                                case "Night":
                                    SirensCall.Night(rampFog, colorGrading);
                                    break;

                                case "Sunny":
                                    SirensCall.Sunny(rampFog);
                                    break;

                                case "Overcast":
                                    SirensCall.Overcast(rampFog);
                                    break;

                                case "Aphelian":
                                    SirensCall.Aphelian(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                        }
                        else
                            switch (selectedSunderedGroveVariant)
                            {
                                case "Jade":
                                    SunderedGrove.Jade(rampFog, colorGrading);
                                    break;

                                case "Sunny":
                                    SunderedGrove.Sunny(rampFog, colorGrading);
                                    break;

                                case "Overcast":
                                    SunderedGrove.Overcast(rampFog, colorGrading);
                                    break;

                                case "Abandoned":
                                    SunderedGrove.Abandoned(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    break;
                            }
                        currentVariantName = selectedSunderedGroveVariant;
                        sunderedGroveVariant = sunderedGroveCounter;

                        #endregion SunderedGrove

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
                            currentVariantName = "Vanilla";
                        }
                        else
                            switch (selectedSkyMeadowVariant)
                            {
                                case "Night":
                                    SkyMeadow.Night(rampFog);
                                    break;

                                case "Overcast":
                                    SkyMeadow.Overcast(rampFog);
                                    break;

                                case "Abyssal":
                                    SkyMeadow.Abyssal(rampFog, colorGrading);
                                    break;

                                case "Titanic":
                                    SkyMeadow.Titanic(rampFog);
                                    break;

                                case "Abandoned":
                                    SkyMeadow.Abandoned(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
                                    break;
                            }
                        currentVariantName = selectedSkyMeadowVariant;
                        skyMeadowVariant = skyMeadowCounter;

                        #endregion SkyMeadow

                        break;

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
                        }
                        else
                            switch (selectedCommencementVariant)
                            {
                                case "Night":
                                    Commencement.Night(rampFog);
                                    break;

                                case "Crimson":
                                    Commencement.Crimson(rampFog);
                                    break;

                                case "Corruption":
                                    Commencement.Corruption(rampFog);
                                    break;

                                case "Gray":
                                    Commencement.Gray(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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
                        }
                        else
                            switch (selectedVoidLocusVariant)
                            {
                                case "Twilight":
                                    VoidLocus.Twilight(rampFog, colorGrading);
                                    break;

                                case "Pink":
                                    VoidLocus.Pink(rampFog, colorGrading);
                                    break;

                                case "Blue":
                                    VoidLocus.Blue(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu I messed something up forgive me >w<");
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

        public static List<string> commencementList = new();
        public static List<string> voidLocusList = new();

        #endregion VariantContainers
    }
}