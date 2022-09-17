using System;
using R2API;
using UnityEngine;
using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using BepInEx.Configuration;
using System.Collections.Generic;
using StageAesthetic.Variants;

namespace StageAesthetic
{
    public class SwapVariants
    {
        // materials sometimes break due to timing i believe, caching them here to prevent that sorta stuff

        public static void Initialize()
        {
            // Setting up config and hooks before the game is actually loaded
            Config.SetConfig();
            On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
            SceneManager.sceneLoaded += TitlePicker;
            SceneCamera.onSceneCameraPreRender += RainCamera;
            Run.onRunStartGlobal += Config.ApplyConfig;
            AesLog.LogMessage("Welcome to the latest update of StageAesthetics!");
            AesLog.LogMessage("Note that most of the code is run during the game itself, so just because no errors popped up here doesn't mean the mod will work.");
            AesLog.LogMessage("This has NOT been tested for cross-mod compatibility - if you're experiencing bugs that are disruptive enough to warrant a fix, sending BepInEx logs along with the bug report will make things easier. Also, note that Starstorm 2's weather and void effects will likely not mix well with this mod.");
        }

        private static void TitlePicker(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "title")
            {
                // Doing this since the title sequence isn't covered by SceneDirector.Start
                rainCheck = false;
                var menuBase = GameObject.Find("MainMenu").transform;
                // Pulling weather effects if they're enabled (this used to be part of the Weather Effects bool, but with InLobbyConfig being a thing now it'd NRE if this was disabled on load and then enabled in lobby without returning to the title screen)
                if (!rainEffect)
                {
                    rainEffect = PrefabAPI.InstantiateClone(menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject, "rainT", true);
                    rainEffect.transform.eulerAngles = new Vector3(90, 0, 0);
                }

                // Title screen changes
                if (TitleScene.Value)
                {
                    var graphicBase = GameObject.Find("HOLDER: Title Background").transform;
                    graphicBase.Find("Terrain").gameObject.SetActive(true);
                    graphicBase.Find("CamDust").gameObject.SetActive(true);
                    graphicBase.Find("Misc Props").Find("DeadCommando").localPosition = new Vector3(16, -2f, 27);
                    ParticleSystem menuRain = menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").gameObject.GetComponent<ParticleSystem>();
                    var epic = menuRain.emission;
                    var epic2 = epic.rateOverTime; // 30 constant, 30 constantmax, 0 constantmin, 0 curvemultiplier
                    epic.rateOverTime = new ParticleSystem.MinMaxCurve()
                    {
                        constant = 100,
                        constantMax = 100,
                        constantMin = 60,
                        curve = epic2.curve,
                        curveMax = epic2.curveMax,
                        curveMin = epic2.curveMax,
                        curveMultiplier = epic2.curveMultiplier,
                        mode = epic2.mode
                    };
                    var epic3 = menuRain.colorOverLifetime;
                    epic3.enabled = false;
                    menuBase.Find("MENU: Title").Find("World Position").Find("CameraPositionMarker").Find("Rain").eulerAngles = new Vector3(80, 90, 0);
                    WindZone menuWind = GameObject.Find("HOLDER: Title Background").transform.Find("FX").Find("WindZone").gameObject.GetComponent<WindZone>();
                    menuWind.windMain = 0.5f;
                    menuWind.windTurbulence = 1;
                }
            }
        }

        private static void RainCamera(SceneCamera sceneCamera)
        {
            if (sceneCamera.cameraRigController && WeatherEffects.Value)
            {
                // Grabbing the scene camera's controller
                SetRain(sceneCamera.cameraRigController, true, false);
            }
        }

        private static void SetRain(CameraRigController cameraRigController, bool lockPosition, bool lockRotation)
        {
            if (rainCheck || emberCheck || purpleCheck)
            {
                // Getting the two needed objects set up
                Transform transform = cameraRigController.transform;
                if (rainCheck)
                {
                    rainObj = GameObject.Find("rainT(Clone)");
                    // Using the camera's xyz position to set the rain effect
                    if (rainObj) rainObj.transform.SetPositionAndRotation(lockPosition ? transform.position : rain.transform.position, lockRotation ? transform.rotation : rain.transform.rotation);
                }
            }
        }

        private static void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            ChangeProfile(SceneManager.GetActiveScene().name);
            orig(self);
        }

        // TODO:
        /*
         * sky aphelian
         *
         * aphelian aqueduct
         *
         * titanic rallypoint
         * snowy acres
         * two new sulfur pools
         *
         * roost abyssal, simulacrum abyssal
         * distant grove, void grove
        */

        private static void ChangeProfile(string scenename)
        {
            ulong seed = (ulong)(Run.instance.GetStartTimeUtc().Ticks ^ (Run.instance.stageClearCount << 16));
            Xoroshiro128Plus rng = new(seed);
            // Disabling weather checks
            rainCheck = false;
            emberCheck = false;
            purpleCheck = false;
            // Resetting rain object between stages
            rain = rainEffect;
            // Loading in the current PostProcessVolume from SceneInfo
            SceneInfo currentScene = SceneInfo.instance;
            if (currentScene) volume = currentScene.GetComponent<PostProcessVolume>();
            // Some stages keep post-processing in a dedicated "Weather" folder because ??? inconsistent code moment
            // The following checks for three options used by various stages, which should leave Commencement as the only null value
            if (!volume)
            {
                GameObject alt = GameObject.Find("PP + Amb");
                if (!alt) alt = GameObject.Find("PP, Global");
                if (!alt) alt = GameObject.Find("GlobalPostProcessVolume, Base");
                if (alt) volume = alt.GetComponent<PostProcessVolume>();
                else volume = null;
            }
            if (volume && scenename != "moon2")
            {
                // Pretty much every variant uses RampFog, and it always shows up in the post-processing volume so it's put into an easy variable for transferring to other files.
                RampFog fog = volume.profile.GetSetting<RampFog>();
                // As of 0.1.2, I'm reintroducing color grading to help make some alts look better
                ColorGrading cgrade = volume.profile.GetSetting<ColorGrading>();
                if (cgrade == null) cgrade = volume.profile.AddSettings<ColorGrading>();
                // Commencement does not natively have a profile, so I'm borrowing it from the first stage in memory.
                if (Run.instance.stageClearCount == 0 && CommencementAlt.Value)
                {
                    commencementVolume = volume.profile;
                }
                // Moving onto the big list of stage changes. I'll comment the Titanic Plains one for context, but the rest won't be.
                // Due to Titanic Plains and Distant Roost having two different variations, the if statement for them is an OR check for both.

                switch (scenename)
                {
                    case string n when (n == "golemplains" || n == "golemplains2"):

                        #region TitanicPlainsAndAlt

                        // Setting up a random number between 0 and the total number of strings in the stage's list. This sets the maximum number to
                        int plainsCounter = rng.RangeInt(0, plainsList.Count);
                        // Implementing a do-while loop to force a new variant whenever a stage is reloaded:
                        if (plainsList.Count > 1) do plainsCounter = rng.RangeInt(0, plainsList.Count); while (plainsCounter == plainsVariant);
                        // Converting the list to a string array so I can pull values based off of index. There's probably a better way to do this, but...
                        string[] plainsArray = plainsList.ToArray();
                        if (plainsCounter == plainsList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (plainsArray[plainsCounter])
                            {
                                case "vanilla":
                                    break;

                                case "sunset":
                                    TitanicPlains.SunsetPlains(fog);
                                    break;

                                case "rain":
                                    rainCheck = true;
                                    TitanicPlains.RainyPlains(fog, rain, scenename);
                                    break;

                                case "night":
                                    rainCheck = true;
                                    TitanicPlains.NightPlains(fog, rain, cgrade);
                                    break;

                                case "nostalgia":
                                    rainCheck = true;
                                    TitanicPlains.NostalgiaPlains(fog);
                                    break;

                                case "sunrise":
                                    TitanicPlains.SunrisePlains(fog);
                                    break;

                                case "sandy":
                                    TitanicPlains.SandyPlains(fog);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        if (PlainsBridge.Value > 0)
                        {
                            int bridgeValue = Math.Min(PlainsBridge.Value - 1, 99);
                            if (rng.RangeInt(bridgeValue, 100) <= bridgeValue)
                            {
                                try
                                {
                                    Transform bridgeObject = GameObject.Find("HOLDER: Ruined Pieces").transform.Find("MiniBridge");
                                    bridgeObject.gameObject.SetActive(true);
                                    bridgeObject.position = new Vector3(264.8f, -117.1f, -148.6f);
                                    bridgeObject.eulerAngles = new Vector3(270, 277, 0);
                                    bridgeObject.localScale = new Vector3(3.64f, 3.64f, 3.64f);
                                }
                                catch { }
                            }
                        }
                        // Finally, the active variant is stored for the next time this stage is loaded.
                        plainsVariant = plainsCounter;

                        #endregion TitanicPlainsAndAlt

                        break;

                    case "blackbeach":

                        #region DistantRoost

                        int roostCounter = rng.RangeInt(0, roostList.Count);
                        if (roostList.Count > 1) do roostCounter = rng.RangeInt(0, roostList.Count); while (roostCounter == roostVariant);
                        string[] roostArray = roostList.ToArray();
                        if (roostCounter == roostList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (roostArray[roostCounter])
                            {
                                case "vanilla":
                                    if (scenename == "blackbeach2" && RoostChanges.Value) rainCheck = true;
                                    if (RoostChanges.Value) DistantRoost.VanillaBeach(rain, scenename);
                                    DistantRoost.VanillaFoliage();
                                    break;

                                case "night":
                                    if (scenename == "blackbeach2" && RoostChanges.Value) rainCheck = true;
                                    DistantRoost.DarkBeach(fog, scenename, cgrade);
                                    break;

                                case "sunny":
                                    DistantRoost.LightBeach(fog, scenename, cgrade);
                                    break;

                                case "foggy":
                                    rainCheck = true;
                                    DistantRoost.FoggyBeach(fog, scenename, rain);
                                    break;

                                case "void":
                                    rainCheck = true;
                                    DistantRoost.VoidBeach(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        roostVariant = roostCounter;

                        #endregion DistantRoost

                        break;

                    case "blackbeach2":

                        #region DistantRoostAlt

                        int roostAltCounter = rng.RangeInt(0, roostList.Count);
                        if (roostList.Count > 1) do roostAltCounter = rng.RangeInt(0, roostList.Count); while (roostAltCounter == roostVariant);
                        string[] roostAltArray = roostList.ToArray();
                        if (roostAltCounter == roostList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (roostAltArray[roostAltCounter])
                            {
                                case "vanilla":
                                    if (RoostChanges.Value) rainCheck = true;
                                    if (RoostChanges.Value) DistantRoost.VanillaBeach(rain, scenename);
                                    DistantRoost.VanillaFoliage();
                                    break;

                                case "night":
                                    if (RoostChanges.Value) rainCheck = true;
                                    DistantRoost.DarkBeach(fog, scenename, cgrade);
                                    break;

                                case "sunny":
                                    DistantRoost.LightBeach(fog, scenename, cgrade);
                                    break;

                                case "foggy":
                                    rainCheck = true;
                                    DistantRoost.FoggyBeach(fog, scenename, rain);
                                    break;

                                case "gold":
                                    rainCheck = true;
                                    DistantRoost.GoldBeach(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        roostVariant = roostAltCounter;

                        #endregion DistantRoostAlt

                        break;

                    case "snowyforest":

                        #region SiphonedForest

                        int forestCounter = rng.RangeInt(0, forestList.Count);
                        if (forestList.Count > 1) do forestCounter = rng.RangeInt(0, forestList.Count); while (forestCounter == forestVariant);
                        string[] forestArray = forestList.ToArray();
                        if (forestCounter == forestList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (forestArray[forestCounter])
                            {
                                case "vanilla":
                                    break;

                                case "night":
                                    purpleCheck = true;
                                    SiphonedForest.NightForest(fog, cgrade);
                                    break;

                                case "extrasnowy":
                                    purpleCheck = true;
                                    SiphonedForest.ExtraSnowyForest(fog, cgrade);
                                    break;

                                case "crimson":
                                    purpleCheck = true;
                                    SiphonedForest.CrimsonForest(fog, cgrade);
                                    break;

                                case "morning":
                                    purpleCheck = true;
                                    SiphonedForest.MorningForest(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        forestVariant = forestCounter;

                        #endregion SiphonedForest

                        break;

                    case "foggyswamp":

                        #region WetlandAspect

                        int wetlandCounter = rng.RangeInt(0, wetlandList.Count);
                        if (wetlandList.Count > 1) do wetlandCounter = rng.RangeInt(0, wetlandList.Count); while (wetlandCounter == wetlandVariant);
                        string[] wetlandArray = wetlandList.ToArray();
                        if (wetlandCounter == wetlandList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (wetlandArray[wetlandCounter])
                            {
                                case "vanilla":
                                    break;

                                case "sunset":
                                    emberCheck = true;
                                    WetlandAspect.GoldSwamp(fog, cgrade);
                                    break;

                                case "sky":
                                    purpleCheck = true;
                                    WetlandAspect.PinkSwamp(fog, cgrade);
                                    break;

                                case "dark":
                                    rainCheck = true;
                                    WetlandAspect.MoreSwamp(fog, rain);
                                    break;

                                case "void":
                                    rainCheck = true;
                                    WetlandAspect.VoidSwamp(fog);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        wetlandVariant = wetlandCounter;

                        #endregion WetlandAspect

                        break;

                    case "goolake":

                        #region AbandonedAqueduct

                        int aqueductCounter = rng.RangeInt(0, aqueductList.Count);
                        if (aqueductList.Count > 1) do aqueductCounter = rng.RangeInt(0, aqueductList.Count); while (aqueductCounter == aqueductVariant);
                        string[] aqueductArray = aqueductList.ToArray();
                        if (aqueductCounter == aqueductList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (aqueductArray[aqueductCounter])
                            {
                                case "vanilla":
                                    if (AqueductChanges.Value) AbandonedAqueduct.VanillaChanges();
                                    break;

                                case "night":
                                    AbandonedAqueduct.DarkAqueduct(fog);
                                    break;

                                case "rain":
                                    rainCheck = true;
                                    AbandonedAqueduct.BlueAqueduct(fog, rain);
                                    break;

                                case "nightrain":
                                    rainCheck = true;
                                    AbandonedAqueduct.NightAqueduct(fog, rain, cgrade);
                                    break;

                                case "sundered":
                                    rainCheck = true;
                                    AbandonedAqueduct.SunderedAqueduct(fog, rain, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        aqueductVariant = aqueductCounter;

                        #endregion AbandonedAqueduct

                        break;

                    case "ancientloft":

                        #region AphelianSanctuary

                        int aphelianCounter = rng.RangeInt(0, aphelianList.Count);
                        if (aphelianList.Count > 1) do aphelianCounter = rng.RangeInt(0, aphelianList.Count); while (aphelianCounter == aphelianVariant);
                        string[] aphelianArray = aphelianList.ToArray();
                        if (aphelianCounter == aphelianList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (aphelianArray[aphelianCounter])
                            {
                                case "vanilla":
                                    break;

                                case "nearrain":
                                    purpleCheck = true;
                                    AphelianSanctuary.NearRainSanctuary(fog, cgrade);
                                    break;

                                case "sunrise":
                                    purpleCheck = true;
                                    AphelianSanctuary.SunriseSanctuary(fog, cgrade);
                                    break;

                                case "night":
                                    purpleCheck = true;
                                    AphelianSanctuary.NightSanctuary(fog, cgrade);
                                    break;

                                case "abyss":
                                    purpleCheck = true;
                                    AphelianSanctuary.AbyssalSanctuary(fog);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        aphelianVariant = aphelianCounter;

                        #endregion AphelianSanctuary

                        break;

                    case "frozenwall":

                        #region RallypointDelta

                        int deltaCounter = rng.RangeInt(0, deltaList.Count);
                        if (deltaList.Count > 1) do deltaCounter = rng.RangeInt(0, deltaList.Count); while (deltaCounter == deltaVariant);
                        string[] deltaArray = deltaList.ToArray();
                        if (deltaCounter == deltaList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (deltaArray[deltaCounter])
                            {
                                case "vanilla":
                                    break;

                                case "night":
                                    RallypointDelta.NightWall(fog, cgrade);
                                    break;

                                case "foggy":
                                    rainCheck = true;
                                    RallypointDelta.OceanWall(fog, rain);
                                    break;

                                case "green":
                                    RallypointDelta.GreenWall(fog);
                                    break;

                                case "titanic":
                                    RallypointDelta.TitanicWall(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        deltaVariant = deltaCounter;
                        // obligatory covid joke

                        #endregion RallypointDelta

                        break;

                    case "wispgraveyard":

                        #region ScorchedAcres

                        int acresCounter = rng.RangeInt(0, acresList.Count);
                        if (acresList.Count > 1) do acresCounter = rng.RangeInt(0, acresList.Count); while (acresCounter == acresVariant);
                        string[] acresArray = acresList.ToArray();
                        if (acresCounter == acresList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (acresArray[acresCounter])
                            {
                                case "vanilla":
                                    if (AcresChanges.Value) ScorchedAcres.VanillaChanges();
                                    break;

                                case "sunset":
                                    purpleCheck = true;
                                    ScorchedAcres.SunsetAcres(fog, cgrade);
                                    break;

                                case "night":
                                    ScorchedAcres.MoonAcres(fog);
                                    break;

                                case "nothing":
                                    ScorchedAcres.OddAcres(fog);
                                    break;

                                case "beta":
                                    ScorchedAcres.BetaAcres(fog);
                                    break;

                                case "beta2":
                                    ScorchedAcres.BetaAcres2(fog);
                                    break;

                                case "twilight":
                                    ScorchedAcres.TwilightAcres(fog);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        acresVariant = acresCounter;

                        #endregion ScorchedAcres

                        break;

                    case "sulfurpools":

                        #region SulfurPools

                        int sulfurCounter = rng.RangeInt(0, sulfurList.Count);
                        if (sulfurList.Count > 1) do sulfurCounter = rng.RangeInt(0, sulfurList.Count); while (sulfurCounter == sulfurVariant);
                        string[] sulfurArray = sulfurList.ToArray();
                        if (sulfurCounter == sulfurList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (sulfurArray[sulfurCounter])
                            {
                                case "vanilla":
                                    SulfurPools.VanillaPools();
                                    break;

                                case "coralblue":
                                    purpleCheck = true;
                                    SulfurPools.CoralBluePools(fog);
                                    break;

                                case "hell":
                                    purpleCheck = true;
                                    SulfurPools.HellOnEarthPools(fog);
                                    break;

                                case "void":
                                    purpleCheck = true;
                                    SulfurPools.VoidPools(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        sulfurVariant = sulfurCounter;

                        #endregion SulfurPools

                        break;

                    case "dampcavesimple":

                        #region AbyssalDepths

                        int depthsCounter = rng.RangeInt(0, depthsList.Count);
                        if (depthsList.Count > 1) do depthsCounter = rng.RangeInt(0, depthsList.Count); while (depthsCounter == depthsVariant);
                        string[] depthsArray = depthsList.ToArray();
                        if (depthsCounter == depthsList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (depthsArray[depthsCounter])
                            {
                                case "vanilla":
                                    if (DepthsChanges.Value) AbyssalDepths.VanillaChanges();
                                    break;

                                case "hive":
                                    AbyssalDepths.HiveCave(fog, cgrade);
                                    break;

                                case "gold":
                                    AbyssalDepths.DarkCave(fog, cgrade);
                                    break;

                                case "sky":
                                    purpleCheck = true;
                                    AbyssalDepths.MeadowCave(fog);
                                    break;

                                case "coral":
                                    purpleCheck = true;
                                    AbyssalDepths.CoralCave(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        depthsVariant = depthsCounter;

                        #endregion AbyssalDepths

                        break;

                    case "shipgraveyard":

                        #region SirensCall

                        int sirenCounter = rng.RangeInt(0, sirenList.Count);
                        if (sirenList.Count > 1) do sirenCounter = rng.RangeInt(0, sirenList.Count); while (sirenCounter == sirenVariant);
                        string[] sirenArray = sirenList.ToArray();
                        if (sirenCounter == sirenList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (sirenArray[sirenCounter])
                            {
                                case "vanilla":
                                    break;

                                case "night":
                                    SirensCall.ShipNight(fog, cgrade);
                                    break;

                                case "sunny":
                                    SirensCall.ShipSkies(fog);
                                    break;

                                case "storm":
                                    rainCheck = true;
                                    SirensCall.ShipDeluge(fog, rain);
                                    break;

                                case "aphelian":
                                    rainCheck = true;
                                    SirensCall.ShipAphelian(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        sirenVariant = sirenCounter;

                        #endregion SirensCall

                        break;

                    case "rootjungle":

                        #region SunderedGrove

                        int groveCounter = rng.RangeInt(0, groveList.Count);
                        if (groveList.Count > 1) do groveCounter = rng.RangeInt(0, groveList.Count); while (groveCounter == groveVariant);
                        string[] groveArray = groveList.ToArray();
                        if (groveCounter == groveList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (groveArray[groveCounter])
                            {
                                case "vanilla":
                                    break;

                                case "green":
                                    SunderedGrove.GreenJungle(fog, cgrade);
                                    break;

                                case "sunny":
                                    SunderedGrove.SunJungle(fog, cgrade);
                                    break;

                                case "storm":
                                    rainCheck = true;
                                    SunderedGrove.StormJungle(fog, rain, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        groveVariant = groveCounter;

                        #endregion SunderedGrove

                        break;

                    case "skymeadow":

                        #region SkyMeadow

                        int meadowCounter = rng.RangeInt(0, meadowList.Count);
                        if (meadowList.Count > 1) do meadowCounter = rng.RangeInt(0, meadowList.Count); while (meadowCounter == meadowVariant);
                        string[] meadowArray = meadowList.ToArray();
                        if (meadowCounter == meadowList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (meadowArray[meadowCounter])
                            {
                                case "vanilla":
                                    purpleCheck = true;
                                    if (MeadowChanges.Value) SkyMeadow.VanillaChanges();
                                    break;

                                case "night":
                                    purpleCheck = true;
                                    SkyMeadow.NightMeadow(fog);
                                    break;

                                case "storm":
                                    rainCheck = true;
                                    SkyMeadow.StormyMeadow(fog, rain);
                                    break;

                                case "abyss":
                                    emberCheck = true;
                                    SkyMeadow.EpicMeadow(fog, cgrade);
                                    break;

                                case "titanic":
                                    emberCheck = true;
                                    SkyMeadow.TitanicMeadow(fog);
                                    break;

                                case "sandy":
                                    emberCheck = true;
                                    SkyMeadow.SandyMeadow(fog);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        meadowVariant = meadowCounter;

                        #endregion SkyMeadow

                        break;

                    case "voidstage":

                        #region VoidLocus

                        int locusCounter = rng.RangeInt(0, locusList.Count);
                        if (locusList.Count > 1) do locusCounter = rng.RangeInt(0, locusList.Count); while (locusCounter == locusVariant);
                        string[] locusArray = locusList.ToArray();
                        if (locusCounter == locusList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (locusArray[locusCounter])
                            {
                                case "vanilla":
                                    break;

                                case "blue":
                                    purpleCheck = true;
                                    VoidLocus.BlueLocus(fog, cgrade);
                                    break;

                                case "pink":
                                    purpleCheck = true;
                                    VoidLocus.PinkLocus(fog, cgrade);
                                    break;

                                case "green":
                                    purpleCheck = true;
                                    VoidLocus.GreenLocus(fog, cgrade);
                                    break;

                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        locusVariant = locusCounter;

                        #endregion VoidLocus

                        break;

                    case "voidraid":

                        #region ThePlanetarium

                        int planetariumCounter = rng.RangeInt(0, planetariumList.Count);
                        if (planetariumList.Count > 1) do planetariumCounter = rng.RangeInt(0, planetariumList.Count); while (planetariumCounter == planetariumVariant);
                        string[] planetariumArray = planetariumList.ToArray();
                        if (planetariumCounter == planetariumList.Count) AesLog.LogError("Number generated is above the maximum value of the array. Please report this error if you see it!");
                        else
                        {
                            switch (planetariumArray[planetariumCounter])
                            {
                                case "vanilla":
                                    break;
                                /*
                            case "purple":
                                purpleCheck = true;
                                Planetarium.PurplePlanetarium(fog, cgrade);
                                break;

                            case "twilight":
                                purpleCheck = true;
                                Planetarium.TwilightPlanetarium();
                                break;

                                */
                                default:
                                    AesLog.LogError("Value selected does not line up with any existing variants. Please report this error if you see it!");
                                    break;
                            }
                        }
                        planetariumVariant = planetariumCounter;

                        #endregion ThePlanetarium

                        break;
                }
            }
            else if (scenename == "moon2" && CommencementAlt.Value)
            {
                volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                volume.enabled = true;
                volume.isGlobal = true;
                volume.priority = 9999f;
                volume.profile = commencementVolume;
                RampFog fog = volume.profile.GetSetting<RampFog>();
                fog.fogColorStart.value = new Color(0.08f, 0.05f, 0.12f, 0.4f);
                fog.fogColorMid.value = new Color(0.13f, 0.14f, 0.19f, 0.625f);
                fog.fogColorEnd.value = new Color(0f, 0f, 0f, 1f);
                fog.skyboxStrength.value = 0f;
                var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
                sun.color = new Color32(178, 238, 238, 255);
                sun.intensity = 1.9f;
                var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
                es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
                // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
                es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
                es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;
            }
            else AesLog.LogWarning("Post process volume could not be found.");

            // lemme know if there's a better way of doing this
        }

        // Used for uh everything
        public static PostProcessVolume volume;

        #region Jank

        public static int roostVariant = -1;
        public static int forestVariant = -1;
        public static int plainsVariant = -1;

        public static int aqueductVariant = -1;
        public static int aphelianVariant = -1;
        public static int wetlandVariant = -1;

        public static int deltaVariant = -1;
        public static int acresVariant = -1;
        public static int sulfurVariant = -1;

        public static int depthsVariant = -1;
        public static int sirenVariant = -1;
        public static int groveVariant = -1;

        public static int meadowVariant = -1;

        public static int commencementVariant = -1;

        public static int locusVariant = -1;

        public static int planetariumVariant = -1;

        public static int currentVariant;

        #endregion Jank

        // Used to store a dummy volume to use in Commencement
        public static PostProcessProfile commencementVolume;

        // Used for rain effect
        public static GameObject rainEffect;

        public static GameObject rain;
        public static GameObject rainObj;
        public static GameObject quad;

        // Used during SceneCamera hook
        public static bool rainCheck;

        public static bool emberCheck;
        public static bool purpleCheck;

        // Custom log
        internal static BepInEx.Logging.ManualLogSource AesLog;

        #region Enable/Disable Config

        // Plains
        public static ConfigEntry<bool> VanillaPlains { get; set; }

        public static ConfigEntry<bool> SunrisePlains { get; set; }
        public static ConfigEntry<bool> SunsetPlains { get; set; }
        public static ConfigEntry<bool> RainyPlains { get; set; }
        public static ConfigEntry<bool> NightPlains { get; set; }
        public static ConfigEntry<bool> NostalgiaPlains { get; set; }
        public static ConfigEntry<bool> SandyPlains { get; set; }
        public static ConfigEntry<int> PlainsBridge { get; set; }

        // Roost
        public static ConfigEntry<bool> VanillaRoost { get; set; }

        public static ConfigEntry<bool> RoostChanges { get; set; }
        public static ConfigEntry<bool> SunnyRoost { get; set; }
        public static ConfigEntry<bool> NightRoost { get; set; }
        public static ConfigEntry<bool> FoggyRoost { get; set; }
        public static ConfigEntry<bool> VoidRoost { get; set; }
        public static ConfigEntry<bool> GoldRoost { get; set; }

        // Siphoned
        public static ConfigEntry<bool> NightForest { get; set; }

        public static ConfigEntry<bool> ExtraSnowyForest { get; set; }
        public static ConfigEntry<bool> CrimsonForest { get; set; }
        public static ConfigEntry<bool> MorningForest { get; set; }
        public static ConfigEntry<bool> VanillaForest { get; set; }

        // Wetland
        public static ConfigEntry<bool> VanillaWetland { get; set; }

        public static ConfigEntry<bool> SunsetWetland { get; set; }
        public static ConfigEntry<bool> SkyWetland { get; set; }
        public static ConfigEntry<bool> EveningWetland { get; set; }
        public static ConfigEntry<bool> VoidWetland { get; set; }

        // Aqueduct
        public static ConfigEntry<bool> VanillaAqueduct { get; set; }

        public static ConfigEntry<bool> AqueductChanges { get; set; }
        public static ConfigEntry<bool> NightAqueduct { get; set; }
        public static ConfigEntry<bool> RainyAqueduct { get; set; }
        public static ConfigEntry<bool> MistyAqueduct { get; set; }
        public static ConfigEntry<bool> SunderedAqueduct { get; set; }

        // Aphelian

        public static ConfigEntry<bool> VanillaAphelian { get; set; }
        public static ConfigEntry<bool> NearRainAphelian { get; set; }
        public static ConfigEntry<bool> SunsetterAphelian { get; set; }
        public static ConfigEntry<bool> NightAphelian { get; set; }
        public static ConfigEntry<bool> AbyssalAphelian { get; set; }

        // Delta
        public static ConfigEntry<bool> VanillaDelta { get; set; }

        public static ConfigEntry<bool> NightDelta { get; set; }
        public static ConfigEntry<bool> FoggyDelta { get; set; }
        public static ConfigEntry<bool> PurpleDelta { get; set; }
        public static ConfigEntry<bool> TitanicDelta { get; set; }

        // Acres
        public static ConfigEntry<bool> VanillaAcres { get; set; }

        public static ConfigEntry<bool> AcresChanges { get; set; }
        public static ConfigEntry<bool> SunsetAcres { get; set; }
        public static ConfigEntry<bool> NightAcres { get; set; }
        public static ConfigEntry<bool> BlueAcres { get; set; }
        public static ConfigEntry<bool> BetaAcres { get; set; }
        public static ConfigEntry<bool> BetaAcres2 { get; set; }
        public static ConfigEntry<bool> TwilightAcres { get; set; }

        // Sulfur

        public static ConfigEntry<bool> VanillaSulfur { get; set; }
        public static ConfigEntry<bool> CoralBlueSulfur { get; set; }
        public static ConfigEntry<bool> HellSulfur { get; set; }
        public static ConfigEntry<bool> VoidSulfur { get; set; }

        // Depths
        public static ConfigEntry<bool> VanillaDepths { get; set; }

        public static ConfigEntry<bool> DepthsChanges { get; set; }
        public static ConfigEntry<bool> DarkDepths { get; set; }
        public static ConfigEntry<bool> SkyDepths { get; set; }
        public static ConfigEntry<bool> BlueDepths { get; set; }
        public static ConfigEntry<bool> CoralDepths { get; set; }

        // Grove
        public static ConfigEntry<bool> VanillaGrove { get; set; }

        public static ConfigEntry<bool> GreenGrove { get; set; }
        public static ConfigEntry<bool> SunnyGrove { get; set; }
        public static ConfigEntry<bool> HannibalGrove { get; set; }

        // Siren
        public static ConfigEntry<bool> VanillaSiren { get; set; }

        public static ConfigEntry<bool> NightSiren { get; set; }
        public static ConfigEntry<bool> SunnySiren { get; set; }
        public static ConfigEntry<bool> MistySiren { get; set; }
        public static ConfigEntry<bool> AphelianSiren { get; set; }

        // Meadow
        public static ConfigEntry<bool> VanillaMeadow { get; set; }

        public static ConfigEntry<bool> MeadowChanges { get; set; }
        public static ConfigEntry<bool> NightMeadow { get; set; }
        public static ConfigEntry<bool> StormyMeadow { get; set; }
        public static ConfigEntry<bool> CrimsonMeadow { get; set; }
        public static ConfigEntry<bool> TitanicMeadow { get; set; }
        public static ConfigEntry<bool> SandyMeadow { get; set; }

        // Void Locus
        public static ConfigEntry<bool> VanillaLocus { get; set; }

        public static ConfigEntry<bool> BlueLocus { get; set; }
        public static ConfigEntry<bool> PurpleLocus { get; set; }
        public static ConfigEntry<bool> RedLocus { get; set; }

        // Planetarium

        public static ConfigEntry<bool> VanillaPlanetarium { get; set; }
        // public static ConfigEntry<bool> TwilightPlanetarium { get; set; }
        // public static ConfigEntry<bool> PurplePlanetarium { get; set; }

        #endregion Enable/Disable Config

        // Base Config
        public static ConfigFile AesConfig { get; set; }

        public static ConfigEntry<bool> CommencementAlt { get; set; }
        public static ConfigEntry<bool> TitleScene { get; set; }
        public static ConfigEntry<bool> WeatherEffects { get; set; }

        #region VariantContainers

        public static List<string> plainsList = new();
        public static List<string> roostList = new();
        public static List<string> forestList = new();

        public static List<string> wetlandList = new();
        public static List<string> aqueductList = new();
        public static List<string> aphelianList = new();

        public static List<string> deltaList = new();
        public static List<string> acresList = new();
        public static List<string> sulfurList = new();

        public static List<string> depthsList = new();
        public static List<string> groveList = new();
        public static List<string> sirenList = new();

        public static List<string> meadowList = new();

        public static List<string> commencementList = new();
        public static List<string> locusList = new();
        public static List<string> planetariumList = new();

        #endregion VariantContainers
    }
}