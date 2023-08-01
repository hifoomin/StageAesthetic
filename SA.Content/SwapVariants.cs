using System;
using UnityEngine;
using RoR2;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using BepInEx.Configuration;
using System.Collections.Generic;
using StageAesthetic.Variants.Stage1;
using StageAesthetic.Variants.Stage2;
using StageAesthetic.Variants.Stage3;
using StageAesthetic.Variants.Stage4;
using StageAesthetic.Variants.Stage5;
using StageAesthetic.Variants.Special;
using RoR2.UI;
using static StageAesthetic.Config;

namespace StageAesthetic
{
    public class SwapVariants
    {
        public static string currentVariantName;

        public static void Initialize()
        {
            Config.SetConfig();
            // On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
            SceneManager.sceneLoaded += ChangeVariant;
            Run.onRunStartGlobal += Config.ApplyConfig;
            On.RoR2.UI.AssignStageToken.Start += AssignStageToken_Start;
        }

        private static void AssignStageToken_Start(On.RoR2.UI.AssignStageToken.orig_Start orig, AssignStageToken self)
        {
            orig(self);
            self.titleText.text += " (" + currentVariantName + ")";
        }

        private static void ChangeVariant(Scene scene, LoadSceneMode mode)
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

            var sceneName = scene.name;

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
                    alt = GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("Sandstorm")?.gameObject;
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
                // AesLog.LogError("found volume");

                var rampFog = volume.profile.GetSetting<RampFog>();

                var colorGrading = volume.profile.GetSetting<ColorGrading>() ?? volume.profile.AddSettings<ColorGrading>();
                switch (sceneName)
                {
                    case "blackbeach":

                        #region DistantRoost

                        int distantRoostCounter = rng.RangeInt(0, distantRoostList.Count);
                        if (distantRoostList.Count > 1) do distantRoostCounter = rng.RangeInt(0, distantRoostList.Count); while (distantRoostCounter == distantRoostVariant);
                        string[] distantRoostArray = distantRoostList.ToArray();
                        if (distantRoostCounter == distantRoostList.Count) { }
                        else
                        {
                            switch (distantRoostArray[distantRoostCounter])
                            {
                                case "Vanilla":
                                    if (RoostChanges.Value)
                                        DistantRoost.Vanilla();
                                    DistantRoost.VanillaFoliage();
                                    break;

                                case "Sunny":
                                    DistantRoost.Sunny(rampFog, sceneName, colorGrading);
                                    break;

                                case "Storm":
                                    DistantRoost.Storm(rampFog, sceneName);
                                    break;

                                case "Void":
                                    DistantRoost.Void(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = distantRoostArray[distantRoostCounter];
                        }
                        distantRoostVariant = distantRoostCounter;

                        #endregion DistantRoost

                        break;

                    case "blackbeach2":

                        #region DistantRoostAlt

                        int distantRoostAltCounter = rng.RangeInt(0, distantRoostList.Count);
                        if (distantRoostList.Count > 1) do distantRoostAltCounter = rng.RangeInt(0, distantRoostList.Count); while (distantRoostAltCounter == distantRoostVariant);
                        string[] distantRoostAltArray = distantRoostList.ToArray();
                        if (distantRoostAltCounter == distantRoostList.Count) { }
                        else
                        {
                            switch (distantRoostAltArray[distantRoostAltCounter])
                            {
                                case "Vanilla":
                                    if (RoostChanges.Value)
                                        DistantRoost.Vanilla();
                                    DistantRoost.VanillaFoliage();
                                    break;

                                case "Night":
                                    DistantRoost.Night(rampFog, sceneName, colorGrading);
                                    break;

                                case "Sunny":
                                    DistantRoost.Sunny(rampFog, sceneName, colorGrading);
                                    break;

                                case "Storm":
                                    DistantRoost.Storm(rampFog, sceneName);
                                    break;

                                case "Abyssal":
                                    DistantRoost.Abyssal(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = distantRoostAltArray[distantRoostAltCounter];
                        }
                        distantRoostVariant = distantRoostAltCounter;

                        #endregion DistantRoostAlt

                        break;

                    case "snowyforest":

                        #region SiphonedForest

                        int siphonedForestCounter = rng.RangeInt(0, siphonedForestList.Count);
                        if (siphonedForestList.Count > 1) do siphonedForestCounter = rng.RangeInt(0, siphonedForestList.Count); while (siphonedForestCounter == siphonedForestVariant);
                        string[] siphonedForestArray = siphonedForestList.ToArray();
                        if (siphonedForestCounter == siphonedForestList.Count) { }
                        else
                        {
                            switch (siphonedForestArray[siphonedForestCounter])
                            {
                                case "Vanilla":
                                    SiphonedForest.Vanilla();
                                    break;

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
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = siphonedForestArray[siphonedForestCounter];
                        }
                        siphonedForestVariant = siphonedForestCounter;

                        #endregion SiphonedForest

                        break;

                    case string n when (n == "golemplains" || n == "golemplains2"):

                        #region TitanicPlainsAndAlt

                        int titanicPlainsCounter = rng.RangeInt(0, titanicPlainsList.Count);

                        if (titanicPlainsList.Count > 1) do titanicPlainsCounter = rng.RangeInt(0, titanicPlainsList.Count); while (titanicPlainsCounter == titanicPlainsVariant);
                        // Converting the list to a string array so I can pull values based off of index. There's probably a better way to do this, but...
                        string[] titanicPlainsArray = titanicPlainsList.ToArray();
                        if (titanicPlainsCounter == titanicPlainsList.Count) { }
                        else
                        {
                            switch (titanicPlainsArray[titanicPlainsCounter])
                            {
                                case "Vanilla":
                                    break;

                                case "Mostalgic":
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
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = titanicPlainsArray[titanicPlainsCounter];
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
                        titanicPlainsVariant = titanicPlainsCounter;

                        #endregion TitanicPlainsAndAlt

                        break;

                    case "goolake":

                        #region AbandonedAqueduct

                        int abandonedAqueductCounter = rng.RangeInt(0, abandonedAqueductList.Count);
                        if (abandonedAqueductList.Count > 1) do abandonedAqueductCounter = rng.RangeInt(0, abandonedAqueductList.Count); while (abandonedAqueductCounter == abandonedAqueductVariant);
                        string[] abandonedAqueductArray = abandonedAqueductList.ToArray();
                        if (abandonedAqueductCounter == abandonedAqueductList.Count) { }
                        else
                        {
                            switch (abandonedAqueductArray[abandonedAqueductCounter])
                            {
                                case "Vanilla":
                                    if (AqueductChanges.Value) AbandonedAqueduct.VanillaChanges();
                                    break;

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
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = abandonedAqueductArray[abandonedAqueductCounter];
                        }
                        abandonedAqueductVariant = abandonedAqueductCounter;

                        #endregion AbandonedAqueduct

                        break;

                    case "ancientloft":

                        #region AphelianSanctuary

                        int aphelianSanctuaryCounter = rng.RangeInt(0, aphelianSanctuaryList.Count);

                        if (aphelianSanctuaryList.Count > 1 && aphelianSanctuaryCounter == aphelianSanctuaryVariant)
                            aphelianSanctuaryCounter = (aphelianSanctuaryCounter + 1) % aphelianSanctuaryList.Count;

                        string[] aphelianSanctuaryArray = aphelianSanctuaryList.ToArray();
                        string selectedVariant = aphelianSanctuaryArray[aphelianSanctuaryCounter];
                        if (selectedVariant == "Vanilla") currentVariantName = "Vanilla";
                        else
                            switch (selectedVariant)
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
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                        currentVariantName = selectedVariant;
                        aphelianSanctuaryVariant = aphelianSanctuaryCounter;

                        #endregion AphelianSanctuary

                        break;

                    case "drybasin":

                        #region DryBasin

                        int dryBasinCounter = rng.RangeInt(0, dryBasinList.Count);
                        if (dryBasinList.Count > 1) do dryBasinCounter = rng.RangeInt(0, dryBasinList.Count); while (dryBasinCounter == dryBasinVariant);
                        string[] dryBasinArray = dryBasinList.ToArray();
                        if (dryBasinCounter == dryBasinList.Count) { }
                        else
                        {
                            basin();
                            void basin()
                            {
                                FRCSharp.TheCoolerRampFog stupidAssFog = volume.GetComponent<FRCSharp.TheCoolerRampFog>();
                                switch (dryBasinArray[dryBasinCounter])
                                {
                                    case "Vanilla":
                                        if (BasinChanges.Value) DryBasin.VanillaChanges();
                                        break;

                                    case "Morning":
                                        DryBasin.Morning(stupidAssFog, colorGrading);
                                        break;

                                    case "Blue":
                                        DryBasin.Blue(stupidAssFog, colorGrading);
                                        break;

                                    case "Overcast":
                                        DryBasin.Overcast(stupidAssFog, colorGrading);
                                        break;

                                    default:
                                        SALogger.LogDebug("uwu");
                                        break;
                                }
                                currentVariantName = dryBasinArray[dryBasinCounter];
                            }
                        }
                        dryBasinVariant = dryBasinCounter;

                        #endregion DryBasin

                        break;

                    case "foggyswamp":

                        #region WetlandAspect

                        int wetlandAspectCounter = rng.RangeInt(0, wetlandAspectList.Count);
                        if (wetlandAspectList.Count > 1) do wetlandAspectCounter = rng.RangeInt(0, wetlandAspectList.Count); while (wetlandAspectCounter == wetlandAspectVariant);
                        string[] wetlandAspectArray = wetlandAspectList.ToArray();
                        if (wetlandAspectCounter == wetlandAspectList.Count) { }
                        else
                        {
                            switch (wetlandAspectArray[wetlandAspectCounter])
                            {
                                case "Vanilla":
                                    break;

                                case "Sunset":

                                    WetlandAspect.GoldSwamp(rampFog, colorGrading);
                                    break;

                                case "Sky":

                                    WetlandAspect.PinkSwamp(rampFog, colorGrading);
                                    break;

                                case "Dark":

                                    WetlandAspect.MoreSwamp(rampFog);
                                    break;

                                case "Void":

                                    WetlandAspect.VoidSwamp(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = wetlandAspectArray[wetlandAspectCounter];
                        }
                        wetlandAspectVariant = wetlandAspectCounter;

                        #endregion WetlandAspect

                        break;

                    case "frozenwall":

                        #region RallypointDelta

                        int rallypointDeltaCounter = rng.RangeInt(0, rallypointDeltaList.Count);
                        if (rallypointDeltaList.Count > 1) do rallypointDeltaCounter = rng.RangeInt(0, rallypointDeltaList.Count); while (rallypointDeltaCounter == rallypointDeltaVariant);
                        string[] rallypointDeltaArray = rallypointDeltaList.ToArray();
                        if (rallypointDeltaCounter == rallypointDeltaList.Count) { }
                        else
                        {
                            switch (rallypointDeltaArray[rallypointDeltaCounter])
                            {
                                case "vanilla":
                                    RallypointDelta.VanillaChanges();
                                    break;

                                case "night":
                                    RallypointDelta.NightWall(rampFog, colorGrading);
                                    break;

                                case "foggy":

                                    RallypointDelta.OceanWall(rampFog);
                                    break;

                                case "green":
                                    RallypointDelta.GreenWall(rampFog);
                                    break;

                                case "titanic":
                                    RallypointDelta.TitanicWall(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = rallypointDeltaArray[rallypointDeltaCounter];
                        }
                        rallypointDeltaVariant = rallypointDeltaCounter;
                        // obligatory covid joke

                        #endregion RallypointDelta

                        break;

                    case "wispgraveyard":

                        #region ScorchedAcres

                        int scorchedAcresCounter = rng.RangeInt(0, scorchedAcresList.Count);
                        if (scorchedAcresList.Count > 1) do scorchedAcresCounter = rng.RangeInt(0, scorchedAcresList.Count); while (scorchedAcresCounter == scorchedAcresVariant);
                        string[] scorchedAcresArray = scorchedAcresList.ToArray();
                        if (scorchedAcresCounter == scorchedAcresList.Count) { }
                        else
                        {
                            switch (scorchedAcresArray[scorchedAcresCounter])
                            {
                                case "vanilla":
                                    if (AcresChanges.Value) ScorchedAcres.VanillaChanges();
                                    break;

                                case "sunset":

                                    ScorchedAcres.SunsetAcres(rampFog, colorGrading);
                                    break;

                                case "night":
                                    ScorchedAcres.MoonAcres(rampFog);
                                    break;

                                case "nothing":
                                    ScorchedAcres.OddAcres(rampFog);
                                    break;

                                case "beta":
                                    ScorchedAcres.BetaAcres(rampFog);
                                    break;

                                case "beta2":
                                    ScorchedAcres.BetaAcres2(rampFog);
                                    break;

                                case "twilight":
                                    ScorchedAcres.TwilightAcres(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = scorchedAcresArray[scorchedAcresCounter];
                        }
                        scorchedAcresVariant = scorchedAcresCounter;

                        #endregion ScorchedAcres

                        break;

                    case "sulfurpools":

                        #region SulfurPools

                        int sulfurPoolsCounter = rng.RangeInt(0, sulfurPoolsList.Count);
                        if (sulfurPoolsList.Count > 1) do sulfurPoolsCounter = rng.RangeInt(0, sulfurPoolsList.Count); while (sulfurPoolsCounter == sulfurPoolsVariant);
                        string[] sulfurPoolsArray = sulfurPoolsList.ToArray();
                        if (sulfurPoolsCounter == sulfurPoolsList.Count) { }
                        else
                        {
                            switch (sulfurPoolsArray[sulfurPoolsCounter])
                            {
                                case "vanilla":
                                    SulfurPools.VanillaPools();
                                    break;

                                case "coralblue":

                                    SulfurPools.CoralBluePools(rampFog);
                                    break;

                                case "hell":

                                    SulfurPools.HellOnEarthPools(rampFog);
                                    break;

                                case "void":

                                    SulfurPools.VoidPools(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = sulfurPoolsArray[sulfurPoolsCounter];
                        }
                        sulfurPoolsVariant = sulfurPoolsCounter;

                        #endregion SulfurPools

                        break;

                    case "FBLScene":
                        int fogboundLagoonCounter = rng.RangeInt(0, fogboundLagoonList.Count);
                        if (fogboundLagoonList.Count > 1) do fogboundLagoonCounter = rng.RangeInt(0, fogboundLagoonList.Count); while (fogboundLagoonCounter == fogboundLagoonVariant);
                        string[] fogboundLagoonArray = fogboundLagoonList.ToArray();
                        if (fogboundLagoonCounter == fogboundLagoonList.Count) { }
                        else
                        {
                            switch (fogboundLagoonArray[fogboundLagoonCounter])
                            {
                                case "vanilla":
                                    break;

                                case "clearer":
                                    FogboundLagoon.ClearerLagoon(rampFog);
                                    break;

                                case "twilight":
                                    FogboundLagoon.TwilightLagoon(rampFog);
                                    break;

                                case "overcast":
                                    FogboundLagoon.OvercastLagoon(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = fogboundLagoonArray[fogboundLagoonCounter];
                        }
                        fogboundLagoonVariant = fogboundLagoonCounter;
                        break;

                    case "dampcavesimple":

                        #region AbyssalDepths

                        int abyssalDepthsCounter = rng.RangeInt(0, abyssalDepthsList.Count);
                        if (abyssalDepthsList.Count > 1) do abyssalDepthsCounter = rng.RangeInt(0, abyssalDepthsList.Count); while (abyssalDepthsCounter == abyssalDepthsVariant);
                        string[] abyssalDepthsArray = abyssalDepthsList.ToArray();
                        if (abyssalDepthsCounter == abyssalDepthsList.Count) { }
                        else
                        {
                            switch (abyssalDepthsArray[abyssalDepthsCounter])
                            {
                                case "vanilla":
                                    if (DepthsChanges.Value) AbyssalDepths.VanillaChanges();
                                    break;

                                case "hive":
                                    AbyssalDepths.HiveCave(rampFog, colorGrading);
                                    break;

                                case "dark":
                                    AbyssalDepths.DarkCave(rampFog, colorGrading);
                                    break;

                                case "orange":

                                    AbyssalDepths.OrangeCave(rampFog);
                                    break;

                                case "coral":

                                    AbyssalDepths.CoralCave(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = abyssalDepthsArray[abyssalDepthsCounter];
                        }
                        abyssalDepthsVariant = abyssalDepthsCounter;

                        #endregion AbyssalDepths

                        break;

                    case "shipgraveyard":

                        #region SirensCall

                        int sirensCallCounter = rng.RangeInt(0, sirensCallList.Count);
                        if (sirensCallList.Count > 1) do sirensCallCounter = rng.RangeInt(0, sirensCallList.Count); while (sirensCallCounter == sirensCallVariant);
                        string[] sirensCallArray = sirensCallList.ToArray();
                        if (sirensCallCounter == sirensCallList.Count) { }
                        else
                        {
                            switch (sirensCallArray[sirensCallCounter])
                            {
                                case "vanilla":
                                    break;

                                case "night":
                                    SirensCall.ShipNight(rampFog, colorGrading);
                                    break;

                                case "sunny":
                                    SirensCall.ShipSkies(rampFog);
                                    break;

                                case "storm":

                                    SirensCall.ShipDeluge(rampFog);
                                    break;

                                case "aphelian":

                                    SirensCall.ShipAphelian(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = sirensCallArray[sirensCallCounter];
                        }
                        sirensCallVariant = sirensCallCounter;

                        #endregion SirensCall

                        break;

                    case "rootjungle":

                        #region SunderedGrove

                        int sunderedGroveCounter = rng.RangeInt(0, sunderedGroveList.Count);
                        if (sunderedGroveList.Count > 1) do sunderedGroveCounter = rng.RangeInt(0, sunderedGroveList.Count); while (sunderedGroveCounter == sunderedGroveVariant);
                        string[] sunderedGroveArray = sunderedGroveList.ToArray();
                        if (sunderedGroveCounter == sunderedGroveList.Count) { }
                        else
                        {
                            switch (sunderedGroveArray[sunderedGroveCounter])
                            {
                                case "vanilla":
                                    SunderedGrove.VanillaJungle();
                                    break;

                                case "green":
                                    SunderedGrove.GreenJungle(rampFog, colorGrading);
                                    break;

                                case "sunny":
                                    SunderedGrove.SunJungle(rampFog, colorGrading);
                                    break;

                                case "storm":

                                    SunderedGrove.StormJungle(rampFog, colorGrading);
                                    break;

                                case "sandy":
                                    SunderedGrove.SandyJungle(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = sunderedGroveArray[sunderedGroveCounter];
                        }
                        sunderedGroveVariant = sunderedGroveCounter;

                        #endregion SunderedGrove

                        break;

                    case "skymeadow":

                        #region SkyMeadow

                        int skyMeadowCounter = rng.RangeInt(0, skyMeadowList.Count);
                        if (skyMeadowList.Count > 1) do skyMeadowCounter = rng.RangeInt(0, skyMeadowList.Count); while (skyMeadowCounter == skyMeadowVariant);
                        string[] skyMeadowArray = skyMeadowList.ToArray();
                        if (skyMeadowCounter == skyMeadowList.Count) { }
                        else
                        {
                            switch (skyMeadowArray[skyMeadowCounter])
                            {
                                case "vanilla":

                                    if (MeadowChanges.Value) SkyMeadow.VanillaChanges();
                                    break;

                                case "night":

                                    SkyMeadow.NightMeadow(rampFog);
                                    break;

                                case "storm":

                                    SkyMeadow.StormyMeadow(rampFog);
                                    break;

                                case "abyss":

                                    SkyMeadow.AbyssalMeadow(rampFog, colorGrading);
                                    break;

                                case "titanic":

                                    SkyMeadow.TitanicMeadow(rampFog);
                                    break;

                                case "sandy":

                                    SkyMeadow.SandyMeadow(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = skyMeadowArray[skyMeadowCounter];
                        }
                        skyMeadowVariant = skyMeadowCounter;

                        #endregion SkyMeadow

                        break;

                    case "moon2":

                        #region Commencement

                        int commencementCounter = rng.RangeInt(0, commencementList.Count);
                        if (commencementList.Count > 1) do commencementCounter = rng.RangeInt(0, commencementList.Count); while (commencementCounter == commencementVariant);
                        string[] commencementArray = commencementList.ToArray();
                        if (commencementCounter == commencementList.Count) { }
                        else
                        {
                            switch (commencementArray[commencementCounter])
                            {
                                case "vanilla":
                                    break;

                                case "dark":
                                    Commencement.DarkCommencement(rampFog);
                                    break;

                                case "crimson":
                                    Commencement.CrimsonCommencement(rampFog);
                                    break;

                                case "corruption":
                                    Commencement.CorruptionCommencement(rampFog);
                                    break;

                                case "gray":
                                    Commencement.GrayCommencement(rampFog);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = commencementArray[commencementCounter];
                        }
                        commencementVariant = commencementCounter;

                        #endregion Commencement

                        break;

                    case "voidstage":

                        #region VoidLocus

                        int voidLocusCounter = rng.RangeInt(0, voidLocusList.Count);
                        if (voidLocusList.Count > 1) do voidLocusCounter = rng.RangeInt(0, voidLocusList.Count); while (voidLocusCounter == voidLocusVariant);
                        string[] voidLocusArray = voidLocusList.ToArray();
                        if (voidLocusCounter == voidLocusList.Count) { }
                        else
                        {
                            switch (voidLocusArray[voidLocusCounter])
                            {
                                case "vanilla":
                                    break;

                                case "blue":

                                    VoidLocus.BlueLocus(rampFog, colorGrading);
                                    break;

                                case "pink":

                                    VoidLocus.PinkLocus(rampFog, colorGrading);
                                    break;

                                case "green":

                                    VoidLocus.GreenLocus(rampFog, colorGrading);
                                    break;

                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = voidLocusArray[voidLocusCounter];
                        }
                        voidLocusVariant = voidLocusCounter;

                        #endregion VoidLocus

                        break;

                    case "voidraid":

                        #region ThePlanetarium

                        int thePlanetariumCounter = rng.RangeInt(0, thePlanetariumList.Count);
                        if (thePlanetariumList.Count > 1) do thePlanetariumCounter = rng.RangeInt(0, thePlanetariumList.Count); while (thePlanetariumCounter == thePlanetariumVariant);
                        string[] thePlanetariumArray = thePlanetariumList.ToArray();
                        if (thePlanetariumCounter == thePlanetariumList.Count) { }
                        else
                        {
                            switch (thePlanetariumArray[thePlanetariumCounter])
                            {
                                case "vanilla":
                                    break;
                                /*
                            case "purple":

                                Planetarium.PurplePlanetarium(fog, cgrade);
                                break;

                            case "twilight":

                                Planetarium.TwilightPlanetarium();
                                break;

                                */
                                default:
                                    SALogger.LogDebug("uwu");
                                    break;
                            }
                            currentVariantName = thePlanetariumArray[thePlanetariumCounter];
                        }
                        thePlanetariumVariant = thePlanetariumCounter;

                        #endregion ThePlanetarium

                        break;
                }

                if (scene.name == "title")
                {
                    var menuBase = GameObject.Find("MainMenu").transform;

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
                volume.profile.name = "SA Profile" + " (" + currentVariantName + ")";
            }
        }

        private static void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            ChangeProfile(SceneManager.GetActiveScene().name);
            orig(self);
        }

        private static void ChangeProfile(string scenename)
        {
        }

        public static PostProcessVolume volume;

        #region Jank

        public static int distantRoostVariant = -1;
        public static int siphonedForestVariant = -1;
        public static int titanicPlainsVariant = -1;

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

        public static int thePlanetariumVariant = -1;

        public static int currentVariant;

        #endregion Jank

        public static GameObject rain;
        public static GameObject snow;
        public static GameObject sand;

        internal static BepInEx.Logging.ManualLogSource SALogger;

        public static ConfigEntry<bool> TitleScene { get; set; }
        public static ConfigEntry<bool> WeatherEffects { get; set; }

        #region VariantContainers

        public static List<string> titanicPlainsList = new();
        public static List<string> distantRoostList = new();
        public static List<string> siphonedForestList = new();

        public static List<string> wetlandAspectList = new();
        public static List<string> abandonedAqueductList = new();
        public static List<string> aphelianSanctuaryList = new();
        public static List<string> dryBasinList = new();

        public static List<string> rallypointDeltaList = new();
        public static List<string> scorchedAcresList = new();
        public static List<string> sulfurPoolsList = new();
        public static List<string> fogboundLagoonList = new();

        public static List<string> abyssalDepthsList = new();
        public static List<string> sunderedGroveList = new();
        public static List<string> sirensCallList = new();

        public static List<string> skyMeadowList = new();

        public static List<string> commencementList = new();
        public static List<string> voidLocusList = new();
        public static List<string> thePlanetariumList = new();

        #endregion VariantContainers
    }
}