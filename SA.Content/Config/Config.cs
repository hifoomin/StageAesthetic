using BepInEx.Configuration;
using BepInEx;
using RoR2;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;
using static StageAesthetic.SwapVariants;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StageAesthetic.Config
{
    public static class Config
    {
        public static ConfigFile SAConfig;

        public static ConfigFile SABackupConfig;

        public static ConfigEntry<bool> enableAutoConfig { get; set; }
        public static ConfigEntry<string> latestVersion { get; set; }

        public static bool _preVersioning = false;

        public static ConfigEntry<bool> Important;

        #region Enable/Disable Config

        public static ConfigEntry<bool> TitleScene { get; set; }
        public static ConfigEntry<bool> WeatherEffects { get; set; }
        public static ConfigEntry<bool> WeatherSounds { get; set; }
        public static ConfigEntry<bool> DisplayVariantName { get; set; }

        // Roost
        public static ConfigEntry<bool> DistantRoostVanilla { get; set; }

        public static ConfigEntry<bool> DistantRoostChanges { get; set; }
        public static ConfigEntry<bool> DistantRoostSunny { get; set; }
        public static ConfigEntry<bool> DistantRoostNight { get; set; }
        public static ConfigEntry<bool> DistantRoostOvercast { get; set; }
        public static ConfigEntry<bool> DistantRoostVoid { get; set; }
        public static ConfigEntry<bool> DistantRoostAbyssal { get; set; }

        // Siphoned
        public static ConfigEntry<bool> SiphonedForestNight { get; set; }

        public static ConfigEntry<bool> SiphonedForestPurple { get; set; }
        public static ConfigEntry<bool> SiphonedForestCrimson { get; set; }
        public static ConfigEntry<bool> SiphonedForestMorning { get; set; }
        public static ConfigEntry<bool> SiphonedForestVanilla { get; set; }
        public static ConfigEntry<bool> SiphonedForestDesolate { get; set; }

        // Plains
        public static ConfigEntry<bool> TitanicPlainsVanilla { get; set; }

        public static ConfigEntry<bool> TitanicPlainsSunset { get; set; }
        public static ConfigEntry<bool> TitanicPlainsOvercast { get; set; }
        public static ConfigEntry<bool> TitanicPlainsNight { get; set; }
        public static ConfigEntry<bool> TitanicPlainsNostalgic { get; set; }
        public static ConfigEntry<bool> TitanicPlainsAbandoned { get; set; }

        // Abodes
        public static ConfigEntry<bool> ShatteredAbodesVanilla { get; set; }
        public static ConfigEntry<bool> ShatteredAbodesSunny { get; set; }
        public static ConfigEntry<bool> ShatteredAbodesAbandoned { get; set; }

        // Reformed Altar
        public static ConfigEntry<bool> ReformedAltarVanilla { get; set; }
        public static ConfigEntry<bool> ReformedAltarVerdant { get; set; }
        public static ConfigEntry<bool> ReformedAltarHelminth { get; set; }

        // Treeborn Colony
        public static ConfigEntry<bool> TreebornColonyVanilla { get; set; }
        public static ConfigEntry<bool> TreebornColonyOvercast { get; set; }
        public static ConfigEntry<bool> TreebornColonySunny { get; set; }
        public static ConfigEntry<bool> TreebornColonyNight { get; set; }

        // Aqueduct
        public static ConfigEntry<bool> AbandonedAqueductVanilla { get; set; }

        public static ConfigEntry<bool> AbandonedAqueductChanges { get; set; }
        public static ConfigEntry<bool> AbandonedAqueductDawn { get; set; }
        public static ConfigEntry<bool> AbandonedAqueductSunrise { get; set; }
        public static ConfigEntry<bool> AbandonedAqueductNight { get; set; }
        public static ConfigEntry<bool> AbandonedAqueductSundered { get; set; }

        // Aphelian

        public static ConfigEntry<bool> AphelianSanctuaryVanilla { get; set; }
        public static ConfigEntry<bool> AphelianSanctuaryTwilight { get; set; }
        public static ConfigEntry<bool> AphelianSanctuarySunset { get; set; }
        public static ConfigEntry<bool> AphelianSanctuarySingularity { get; set; }
        public static ConfigEntry<bool> AphelianSanctuaryAbyssal { get; set; }

        // Dry Basin

        public static ConfigEntry<bool> DryBasinOvercast { get; set; }
        public static ConfigEntry<bool> DryBasinBlue { get; set; }
        public static ConfigEntry<bool> DryBasinMorning { get; set; }
        public static ConfigEntry<bool> DryBasinVanilla { get; set; }
        public static ConfigEntry<bool> DryBasinChanges { get; set; }

        // Wetland
        public static ConfigEntry<bool> WetlandAspectVanilla { get; set; }

        public static ConfigEntry<bool> WetlandAspectSunset { get; set; }
        public static ConfigEntry<bool> WetlandAspectMorning { get; set; }
        public static ConfigEntry<bool> WetlandAspectNight { get; set; }
        public static ConfigEntry<bool> WetlandAspectVoid { get; set; }

        // Fogbound Lagoon

        public static ConfigEntry<bool> FogboundLagoonVanilla { get; set; }
        public static ConfigEntry<bool> FogboundLagoonClear { get; set; }
        public static ConfigEntry<bool> FogboundLagoonTwilight { get; set; }
        public static ConfigEntry<bool> FogboundLagoonOvercast { get; set; }

        // Delta
        public static ConfigEntry<bool> RallypointDeltaVanilla { get; set; }

        public static ConfigEntry<bool> RallypointDeltaNight { get; set; }
        public static ConfigEntry<bool> RallypointDeltaOvercast { get; set; }
        public static ConfigEntry<bool> RallypointDeltaSunset { get; set; }
        public static ConfigEntry<bool> RallypointDeltaTitanic { get; set; }

        // Acres
        public static ConfigEntry<bool> ScorchedAcresVanilla { get; set; }

        public static ConfigEntry<bool> ScorchedAcresChanges { get; set; }
        public static ConfigEntry<bool> ScorchedAcresSunset { get; set; }
        public static ConfigEntry<bool> ScorchedAcresNight { get; set; }
        public static ConfigEntry<bool> ScorchedAcresJade { get; set; }
        public static ConfigEntry<bool> ScorchedAcresSunnyBeta { get; set; }
        public static ConfigEntry<bool> ScorchedAcresCrimsonBeta { get; set; }
        public static ConfigEntry<bool> ScorchedAcresTwilight { get; set; }

        // Sulfur

        public static ConfigEntry<bool> SulfurPoolsVanilla { get; set; }
        public static ConfigEntry<bool> SulfurPoolsCoral { get; set; }
        public static ConfigEntry<bool> SulfurPoolsHell { get; set; }
        public static ConfigEntry<bool> SulfurPoolsVoid { get; set; }

        // Depths
        public static ConfigEntry<bool> AbyssalDepthsVanilla { get; set; }

        public static ConfigEntry<bool> AbyssalDepthsChanges { get; set; }
        public static ConfigEntry<bool> AbyssalDepthsNight { get; set; }
        public static ConfigEntry<bool> AbyssalDepthsOrange { get; set; }
        public static ConfigEntry<bool> AbyssalDepthsBlue { get; set; }
        public static ConfigEntry<bool> AbyssalDepthsCoral { get; set; }

        // Siren
        public static ConfigEntry<bool> SirensCallVanilla { get; set; }

        public static ConfigEntry<bool> SirensCallNight { get; set; }
        public static ConfigEntry<bool> SirensCallSunny { get; set; }
        public static ConfigEntry<bool> SirensCallOvercast { get; set; }
        public static ConfigEntry<bool> SirensCallAphelian { get; set; }

        // Grove
        public static ConfigEntry<bool> SunderedGroveVanilla { get; set; }

        public static ConfigEntry<bool> SunderedGroveJade { get; set; }
        public static ConfigEntry<bool> SunderedGroveSunny { get; set; }
        public static ConfigEntry<bool> SunderedGroveOvercast { get; set; }
        public static ConfigEntry<bool> SunderedGroveAbandoned { get; set; }

        // Meadow
        public static ConfigEntry<bool> SkyMeadowVanilla { get; set; }

        public static ConfigEntry<bool> SkyMeadowChanges { get; set; }
        public static ConfigEntry<bool> SkyMeadowNight { get; set; }
        public static ConfigEntry<bool> SkyMeadowOvercast { get; set; }
        public static ConfigEntry<bool> SkyMeadowAbyssal { get; set; }
        public static ConfigEntry<bool> SkyMeadowTitanic { get; set; }
        public static ConfigEntry<bool> SkyMeadowAbandoned { get; set; }

        // Helminth Hatchery
        public static ConfigEntry<bool> HelminthHatcheryVanilla { get; set; }
        public static ConfigEntry<bool> HelminthHatcheryVanillaChanges { get; set; }
        public static ConfigEntry<bool> HelminthHatcheryLunar { get; set; }

        // Satellite
        public static ConfigEntry<bool> SlumberingSatelliteVanilla { get; set; }

        public static ConfigEntry<bool> SlumberingSatelliteMorning { get; set; }
        public static ConfigEntry<bool> SlumberingSatelliteOvercast { get; set; }
        public static ConfigEntry<bool> SlumberingSatelliteBlue { get; set; }

        // Commencement

        public static ConfigEntry<bool> CommencementNight { get; set; }
        public static ConfigEntry<bool> CommencementVanilla { get; set; }
        public static ConfigEntry<bool> CommencementCrimson { get; set; }
        public static ConfigEntry<bool> CommencementCorruption { get; set; }
        public static ConfigEntry<bool> CommencementGray { get; set; }

        // Void Locus
        public static ConfigEntry<bool> VoidLocusVanilla { get; set; }

        public static ConfigEntry<bool> VoidLocusTwilight { get; set; }
        public static ConfigEntry<bool> VoidLocusBlue { get; set; }
        public static ConfigEntry<bool> VoidLocusPink { get; set; }

        #endregion Enable/Disable Config

        public static void SetConfig()
        {
            SAConfig = new ConfigFile(Paths.ConfigPath + "\\HIFU.StageAesthetic.cfg", true);
            Important = SAConfig.Bind("! Important !", "Config", true, "Make sure everyone's configs are the same for multiplayer!");

            SABackupConfig = new(Paths.ConfigPath + "\\" + Main.PluginAuthor + "." + Main.PluginName + ".Backup.cfg", true);
            SABackupConfig.Bind(": DO NOT MODIFY THIS FILES CONTENTS :", ": DO NOT MODIFY THIS FILES CONTENTS :", ": DO NOT MODIFY THIS FILES CONTENTS :", ": DO NOT MODIFY THIS FILES CONTENTS :");

            enableAutoConfig = SAConfig.Bind("Config", "Enable Auto Config Sync", true, "Disabling this would stop StageAesthetic from syncing config whenever a new version is found.");
            _preVersioning = !((Dictionary<ConfigDefinition, string>)AccessTools.DeclaredPropertyGetter(typeof(ConfigFile), "OrphanedEntries").Invoke(SAConfig, null)).Keys.Any(x => x.Key == "Latest Version");
            latestVersion = SAConfig.Bind("Config", "Latest Version", Main.PluginVersion, "DO NOT CHANGE THIS");
            if (enableAutoConfig.Value && (_preVersioning || latestVersion.Value != Main.PluginVersion))
            {
                latestVersion.Value = Main.PluginVersion;
                ConfigManager.VersionChanged = true;
                SALogger.LogInfo("Config Autosync Enabled.");
            }

            DisplayVariantName = SAConfig.Bind("Stages Misc", "Enable variant display name?", true, "Shows the variant name next to the map title UI");

            DistantRoostVanilla = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            DistantRoostNight = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Night)?", true, "Dark and blue with green lights.");
            DistantRoostSunny = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Sunny)?", true, "Yellow sun over greenery.");
            DistantRoostOvercast = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Overcast)?", true, "Rainy with more fog.");
            DistantRoostVoid = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Void)?", true, "Texture swap to Purple Void Fields.");
            DistantRoostAbyssal = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Abyssal)?", true, "Texture swap to Red Abyssal Depths.");

            DistantRoostChanges = SAConfig.Bind("Stages : Distant Roost", "Alter Distant Roost (Vanilla)?", true, "Adds rain to the alt version.");

            ShatteredAbodesVanilla = SAConfig.Bind("Stages : Shattered Abodes", "Enable Shattered Abodes (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            ShatteredAbodesSunny = SAConfig.Bind("Stages : Shattered Abodes", "Enable Shattered Abodes (Verdant)?", true, "Sunny and bright green.");
            ShatteredAbodesAbandoned = SAConfig.Bind("Stages : Shattered Abodes", "Enable Shattered Abodes (Abandoned)?", true, "Scorching Desert.");

            ReformedAltarVanilla = SAConfig.Bind("Stages : Reformed Altar", "Enable Reformed Altar (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            ReformedAltarVerdant = SAConfig.Bind("Stages : Reformed Altar", "Enable Reformed Altar (Verdant)?", true, "Sunny and bright green.");
            ReformedAltarHelminth = SAConfig.Bind("Stages : Reformed Altar", "Enable Reformed Altar (Helminth)?", true, "Dark and red.");

            TreebornColonyVanilla = SAConfig.Bind("Stages : Treeborn Colony", "Enable Treeborn Colony (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            TreebornColonyOvercast = SAConfig.Bind("Stages : Treeborn Colony", "Enable Treeborn Colony (Overcast)?", true, "Foggy storm.");
            TreebornColonySunny = SAConfig.Bind("Stages : Treeborn Colony", "Enable Treeborn Colony (Sunny)?", true, "Sunny, blue sky, and changes sun's angle.");
            TreebornColonyNight = SAConfig.Bind("Stages : Treeborn Colony", "Enable Treeborn Colony (Night)?", true, "Dark and blue with a starry sky.");

            SiphonedForestVanilla = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SiphonedForestNight = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Night)?", true, "Blue and dark.");
            SiphonedForestMorning = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Morning)?", true, "Yellow sun with blue shadows.");
            SiphonedForestPurple = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Purple)?", true, "Extra snowy with purple hints.");
            SiphonedForestCrimson = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Crimson)?", true, "Red fog with Doom vibes.");
            SiphonedForestDesolate = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Desolate)?", true, "Green ground with a purple contrast.");

            TitanicPlainsVanilla = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Vanilla)?", false, "Disabling removes vanilla from getting picked");
            TitanicPlainsNostalgic = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Nostalgic)?", true, "Early Access look.");
            TitanicPlainsSunset = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Sunset)?", true, "Orange sun over greenery.");
            TitanicPlainsOvercast = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Overcast)?", true, "Rainy with more fog.");
            TitanicPlainsNight = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Night)?", true, "Blue and dark.");
            TitanicPlainsAbandoned = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Abandoned)?", true, "Texture swap to Yellow Abandoned Aqueduct.");

            AbandonedAqueductVanilla = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            AbandonedAqueductDawn = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Dawn)?", true, "Dark orange.");
            AbandonedAqueductSunrise = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Sunrise)?", true, "Rainy blue sky with more fog.");
            AbandonedAqueductNight = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Night)?", true, "Dark blue.");
            AbandonedAqueductSundered = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Sundered)?", true, "Texture swap to Pink Sundered Grove.");

            AbandonedAqueductChanges = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Alter Abandoned Aqueduct (Vanilla)?", true, "Makes the sun a slightly more intense yellow-orange, and changes its angle.");

            AphelianSanctuaryVanilla = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            AphelianSanctuarySingularity = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Singularity)?", true, "Very blue and dark.");
            AphelianSanctuaryTwilight = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Twilight)?", true, "Strong purple and orange fog.");
            AphelianSanctuarySunset = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Sunset)?", true, "Very strong orange sun.");
            AphelianSanctuaryAbyssal = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Abyssal)?", false, "Texture swap to Red Abyssal Depths. Kinda sucks right now.");

            DryBasinVanilla = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            DryBasinMorning = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Morning)?", true, "Yellow sun with blue shadows.");
            DryBasinBlue = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Blue)?", true, "Blue.");
            DryBasinOvercast = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Overcast)?", true, "Overcast and rainy.");

            DryBasinChanges = SAConfig.Bind("Stages :: Dry Basin", "Alter Dry Basin (Vanilla)?", true, "Adds a SandStorm 2.");

            WetlandAspectVanilla = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            WetlandAspectSunset = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Sunset)?", true, "Orange sun and fog.");
            WetlandAspectMorning = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Morning)?", true, "Blue and yellow.");
            WetlandAspectNight = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Night)?", true, "Green and dark.");
            WetlandAspectVoid = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Void)?", true, "Texture swap to Purple Void Fields.");

            FogboundLagoonVanilla = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Fogbound Lagoon (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            FogboundLagoonClear = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Fogbound Lagoon (Clear)?", true, "Yellow sun over greenery with less fog.");
            FogboundLagoonTwilight = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Fogbound Lagoon (Twilight)?", true, "Purple and orange fog.");
            FogboundLagoonOvercast = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Fogbound Lagoon (Overcast)?", true, "Very foggy with rain.");

            RallypointDeltaVanilla = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Rallypoint Delta (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            RallypointDeltaNight = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Rallypoint Delta (Night)?", true, "Blue and dark with extra snow.");
            RallypointDeltaOvercast = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Rallypoint Delta (Overcast)?", true, "Rainy and snowy with more fog.");
            RallypointDeltaSunset = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Rallypoint Delta (Sunset)?", true, "Orange fog.");
            RallypointDeltaTitanic = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Rallypoint Delta (Titanic)??", true, "Texture swap to Titanic Plains.");

            ScorchedAcresVanilla = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            ScorchedAcresSunset = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Sunset)?", true, "Orange....");
            ScorchedAcresNight = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Night)?", true, "Dark purple with stars!");
            ScorchedAcresJade = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Jade)?", true, "Green fog with stars!");
            ScorchedAcresSunnyBeta = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Sunny Beta)?", true, "Brings back the unreleased Scorched Acres' look.");
            ScorchedAcresCrimsonBeta = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Crimson Beta)?", true, "Brings back the unreleased Scorched Acres' look (alt ver)");
            ScorchedAcresTwilight = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Scorched Acres (Twilight)?", true, "Blue and pinkish.");

            ScorchedAcresChanges = SAConfig.Bind("Stages ::: Scorched Acres", "Alter Scorched Acres (Vanilla)?", true, "Greatly increases the sunlight intensity, and alters the light angle and sun position towards a different corner of the map.");

            SulfurPoolsVanilla = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Sulfur Pools (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SulfurPoolsCoral = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Sulfur Pools (Coral)?", true, "Blue sky.");
            SulfurPoolsHell = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Sulfur Pools (Hell)?", true, "Texture swap to Red Aphelian Sanctuary.");
            SulfurPoolsVoid = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Sulfur Pools (Void)?", true, "Texture swap to Blue Void Fields.");

            AbyssalDepthsVanilla = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Abyssal Depths (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            AbyssalDepthsNight = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Abyssal Depths (Night)?", true, "Dark blue with light fog.");
            AbyssalDepthsBlue = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Abyssal Depths (Blue)?", true, "Orange-ish and pink-ish");
            AbyssalDepthsOrange = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Abyssal Depths (Orange)?", true, "Pink with some orange and blue.");
            AbyssalDepthsCoral = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Abyssal Depths (Coral)?", true, "Texture swap to Blue/Purple/Pink Sundered Grove.");

            AbyssalDepthsChanges = SAConfig.Bind("Stages :::: Abyssal Depths", "Alter Abyssal Depths (Vanilla)?", true, "Greatly increases the sunlight intensity, and alters the light angle.");

            SirensCallVanilla = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Call (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SirensCallNight = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Call (Night)?", true, "Dark and green.");
            SirensCallSunny = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Call (Sunny)?", true, "Yellow sun over greenery.");
            SirensCallOvercast = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Call (Overcast)?", true, "Rainy with more fog.");
            SirensCallAphelian = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Call (Aphelian)?", true, "Texture swap to Blue/Yellow/Orange Aphelian Sanctuary.");

            SunderedGroveVanilla = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Sundered Grove (Vanilla)?", false, "Disabling removes vanilla from getting picked");
            SunderedGroveJade = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Sundered Grove (Jade)?", true, "GREEN.");
            SunderedGroveSunny = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Sundered Grove (Sunny)?", true, "Yellow sun over greenery.");
            SunderedGroveOvercast = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Sundered Grove (Overcast)?", true, "Rainy with extra fog.");
            SunderedGroveAbandoned = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Sundered Grove (Abandoned)?", true, "Texture swap to Orange Abandoned Aqueduct.");

            SkyMeadowVanilla = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Sky Meadow (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SkyMeadowNight = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Sky Meadow (Night)?", true, "Blue and dark.");
            SkyMeadowOvercast = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Sky Meadow (Overcast)?", true, "Rainy with more fog.");
            SkyMeadowAbyssal = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Sky Meadow (Abyssal)?", true, "Texture swap to Red Abyssal Depths.");
            SkyMeadowTitanic = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Sky Meadow (Titanic)?", true, "Texture swap to Titanic Plains.");
            SkyMeadowAbandoned = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Sky Meadow (Abandoned)?", true, "Texture swap to Yellow Abandoned Aqueduct.");

            SkyMeadowChanges = SAConfig.Bind("Stages ::::: Sky Meadow", "Alter Sky Meadow (Vanilla)?", true, "Makes the sun a slightly more intense yellow-orange.");

            HelminthHatcheryVanilla = SAConfig.Bind("Stages ::::: Helminth Hatchery", "Enable Helminth Hatchery (Vanilla)?", true, "Disabling removes vanilla from getting picked.");
            HelminthHatcheryVanillaChanges = SAConfig.Bind("Stages ::::: Helminth Hatchery", "Alter Helminth Hatchery (Vanilla)?", true, "Removes smoke from screen and brightens up the map.");
            HelminthHatcheryLunar = SAConfig.Bind("Stages ::::: Helminth Hatchery", "Enable Helminth Hatchery (Lunar)?", true, "Bleu and ashy terrain.");

            SlumberingSatelliteVanilla = SAConfig.Bind("Stages ::::: Slumbering Satellite", "Enable Slumbering Satellite (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SlumberingSatelliteMorning = SAConfig.Bind("Stages ::::: Slumbering Satellite", "Enable Slumbering Satellite (Morning)?", true, "Blue and yellow.");
            SlumberingSatelliteOvercast = SAConfig.Bind("Stages ::::: Slumbering Satellite", "Enable Slumbering Satellite (Overcast)?", true, "Rainy with more fog.");
            SlumberingSatelliteBlue = SAConfig.Bind("Stages ::::: Slumbering Satellite", "Enable Slumbering Satellite (Blue)?", true, "BLUE!");

            CommencementVanilla = SAConfig.Bind("Stages :::::: Commencement", "Enable Commencement (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            CommencementNight = SAConfig.Bind("Stages :::::: Commencement", "Enable Commencement (Night)?", true, "Very dark with a great view.");
            CommencementCrimson = SAConfig.Bind("Stages :::::: Commencement", "Enable Commencement (Crimson)?", true, "Bloody and threatening.");
            CommencementCorruption = SAConfig.Bind("Stages :::::: Commencement", "Enable Commencement (Corruption)?", true, "Purple.");
            CommencementGray = SAConfig.Bind("Stages :::::: Commencement", "Enable Commencement (Gray)?", true, "Gr*y!");

            VoidLocusVanilla = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Void Locus (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            VoidLocusTwilight = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Void Locus (Twilight)?", true, "Red, blue and cyan gradient with an amazing void.");
            VoidLocusPink = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Void Locus (Pink)?", true, "Pink..");
            VoidLocusBlue = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Void Locus (Blue)?", true, "Dark Blue..");

            TitleScene = SAConfig.Bind("Stages Misc", "Alter title screen?", true, "Adds rain, patches of grass, particles and brings a Commando closer to focus.");
            WeatherEffects = SAConfig.Bind("Stages Misc", "Import weather effects?", true, "Adds/swaps rain/snow/sand for stages. Disabling this is recommended if performance is a big issue. Starstorm 2 compatibility coming soon.");
            WeatherSounds = SAConfig.Bind("Stages Misc", "Use weather sound effects?", true, "Adds sound effects for weather.");

            var tabID = 0;
            foreach (ConfigEntryBase ceb in SAConfig.GetConfigEntries())
            {
                var Name = ceb.Definition.Section;
                // SALogger.LogError("ceb key is " + ceb.Definition.Key);

                if (ceb.SettingType == typeof(bool))
                    ConfigManager.HandleConfig<bool>(ceb, SABackupConfig, Name);

                if (Name.Contains("Important"))
                {
                    tabID = 0;
                    Name = "StageAesthetic";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, Name);
                }
                if (Name.Contains("Plains") || Name.Contains("Roost") || Name.Contains("Forest"))
                {
                    tabID = 1;
                    Name = "Stage 1";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name.Contains("Aphelian") || Name.Contains("Wetland") || Name.Contains("Aqueduct") || Name.Contains("Basin"))
                {
                    tabID = 2;
                    Name = "Stage 2";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name.Contains("Delta") || Name.Contains("Acres") || Name.Contains("Pools") || Name.Contains("Fogbound"))
                {
                    tabID = 3;
                    Name = "Stage 3";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name.Contains("Depths") || Name.Contains("Grove") || Name.Contains("Call"))
                {
                    tabID = 4;
                    Name = "Stage 4";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name.Contains("Meadow") || Name.Contains("Slumbering"))
                {
                    tabID = 5;
                    Name = "Stage 5";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name.Contains("Commencement") || Name.Contains("Locus") || Name.Contains("Planetarium") || Name.Contains("Title") || Name.Contains("Weather"))
                {
                    tabID = 6;
                    Name = "Special";
                    ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (ceb.DefaultValue.GetType() == typeof(bool))
                {
                    ModSettingsManager.AddOption(new CheckBoxOption((ConfigEntry<bool>)ceb, new CheckBoxConfig() { restartRequired = true }), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
            }
        }

        public static void ApplyConfig(Run run)
        {
            distantRoostList = new();
            distantRoostAltList = new();
            siphonedForestList = new();
            titanicPlainsList = new();
            shatteredAbodesList = new();

            reformedAltarList = new();
            treebornColonyList = new();

            abandonedAqueductList = new();
            aphelianSanctuaryList = new();
            dryBasinList = new();
            wetlandAspectList = new();

            fogboundLagoonList = new();
            rallypointDeltaList = new();
            scorchedAcresList = new();
            sulfurPoolsList = new();

            abyssalDepthsList = new();
            sirensCallList = new();
            sunderedGroveList = new();

            skyMeadowList = new();
            slumberingSatelliteList = new();
            helminthHatcheryList = new();

            commencementList = new();
            voidLocusList = new();
            // non-alt is blackbeach and alt is blackbeach2
            if (DistantRoostVanilla.Value)
            {
                distantRoostList.Add("Vanilla");
                distantRoostAltList.Add("Vanilla");
            }
            if (DistantRoostSunny.Value)
            {
                distantRoostList.Add("Sunny");
                distantRoostAltList.Add("Sunny");
            }
            if (DistantRoostOvercast.Value)
            {
                distantRoostList.Add("Overcast");
                distantRoostAltList.Add("Overcast");
            }
            if (DistantRoostVoid.Value) distantRoostList.Add("Void");
            if (DistantRoostNight.Value) distantRoostAltList.Add("Night");
            if (DistantRoostAbyssal.Value) distantRoostAltList.Add("Abyssal");

            if (distantRoostList.Count == 0)
            {
                SALogger.LogWarning("Distant Roost list empty - adding vanilla...");
                distantRoostList.Add("Vanilla");
            }
            if (distantRoostAltList.Count == 0)
            {
                SALogger.LogWarning("Distant Roost Alt list empty - adding vanilla...");
                distantRoostAltList.Add("Vanilla");
            }

            if (ShatteredAbodesVanilla.Value) shatteredAbodesList.Add("Vanilla");
            if (ShatteredAbodesSunny.Value) shatteredAbodesList.Add("Verdant");
            if (ShatteredAbodesAbandoned.Value) shatteredAbodesList.Add("Abandoned");

            if (ReformedAltarVanilla.Value) reformedAltarList.Add("Vanilla");
            if (ReformedAltarVerdant.Value) reformedAltarList.Add("Verdant");
            if (ReformedAltarHelminth.Value) reformedAltarList.Add("Helminth");

            if (TreebornColonyVanilla.Value) treebornColonyList.Add("Vanilla");
            if (TreebornColonyOvercast.Value) treebornColonyList.Add("Overcast");
            if (TreebornColonySunny.Value) treebornColonyList.Add("Sunny");
            if (TreebornColonyNight.Value) treebornColonyList.Add("Night");

            if (SiphonedForestVanilla.Value) siphonedForestList.Add("Vanilla");
            if (SiphonedForestNight.Value) siphonedForestList.Add("Night");
            if (SiphonedForestMorning.Value) siphonedForestList.Add("Morning");
            if (SiphonedForestPurple.Value) siphonedForestList.Add("Purple");
            if (SiphonedForestCrimson.Value) siphonedForestList.Add("Crimson");
            if (SiphonedForestDesolate.Value) siphonedForestList.Add("Desolate");
            if (siphonedForestList.Count == 0)
            {
                SALogger.LogWarning("Siphoned Forest list empty - adding vanilla...");
                siphonedForestList.Add("Vanilla");
            }

            if (TitanicPlainsVanilla.Value) titanicPlainsList.Add("Vanilla");
            if (TitanicPlainsNostalgic.Value) titanicPlainsList.Add("Nostalgic");
            if (TitanicPlainsSunset.Value) titanicPlainsList.Add("Sunset");
            if (TitanicPlainsOvercast.Value) titanicPlainsList.Add("Overcast");
            if (TitanicPlainsNight.Value) titanicPlainsList.Add("Night");
            if (TitanicPlainsAbandoned.Value) titanicPlainsList.Add("Abandoned");
            if (titanicPlainsList.Count == 0)
            {
                SALogger.LogWarning("Titanic Plains list empty - adding vanilla...");
                titanicPlainsList.Add("Vanilla");
            }

            if (AbandonedAqueductVanilla.Value) abandonedAqueductList.Add("Vanilla");
            if (AbandonedAqueductDawn.Value) abandonedAqueductList.Add("Dawn");
            if (AbandonedAqueductSunrise.Value) abandonedAqueductList.Add("Sunrise");
            if (AbandonedAqueductNight.Value) abandonedAqueductList.Add("Night");
            if (AbandonedAqueductSundered.Value) abandonedAqueductList.Add("Sundered");
            if (abandonedAqueductList.Count == 0)
            {
                SALogger.LogWarning("Abandoned Aqueduct list empty - adding vanilla...");
                abandonedAqueductList.Add("Vanilla");
            }

            if (AphelianSanctuaryVanilla.Value) aphelianSanctuaryList.Add("Vanilla");
            if (AphelianSanctuarySingularity.Value) aphelianSanctuaryList.Add("Singularity");
            if (AphelianSanctuaryTwilight.Value) aphelianSanctuaryList.Add("Twilight");
            if (AphelianSanctuarySunset.Value) aphelianSanctuaryList.Add("Sunset");
            if (AphelianSanctuaryAbyssal.Value) aphelianSanctuaryList.Add("Abyssal");
            if (aphelianSanctuaryList.Count == 0)
            {
                SALogger.LogWarning("Aphelian Sanctuary list empty - adding vanilla...");
                aphelianSanctuaryList.Add("Vanilla");
            }

            if (DryBasinMorning.Value) dryBasinList.Add("Morning");
            if (DryBasinOvercast.Value) dryBasinList.Add("Overcast");
            if (DryBasinBlue.Value) dryBasinList.Add("Blue");
            if (DryBasinVanilla.Value) dryBasinList.Add("Vanilla");
            if (dryBasinList.Count == 0)
            {
                SALogger.LogWarning("Dry Basin list empty - adding vanilla...");
                dryBasinList.Add("Vanilla");
            }

            if (WetlandAspectVanilla.Value) wetlandAspectList.Add("Vanilla");
            if (WetlandAspectSunset.Value) wetlandAspectList.Add("Sunset");
            if (WetlandAspectMorning.Value) wetlandAspectList.Add("Morning");
            if (WetlandAspectNight.Value) wetlandAspectList.Add("Night");
            if (WetlandAspectVoid.Value) wetlandAspectList.Add("Void");
            if (wetlandAspectList.Count == 0)
            {
                SALogger.LogWarning("Wetland Aspect list empty - adding vanilla...");
                wetlandAspectList.Add("Vanilla");
            }

            //

            if (RallypointDeltaVanilla.Value) rallypointDeltaList.Add("Vanilla");
            if (RallypointDeltaNight.Value) rallypointDeltaList.Add("Night");
            if (RallypointDeltaOvercast.Value) rallypointDeltaList.Add("Overcast");
            if (RallypointDeltaSunset.Value) rallypointDeltaList.Add("Sunset");
            if (RallypointDeltaTitanic.Value) rallypointDeltaList.Add("Titanic");
            if (rallypointDeltaList.Count == 0)
            {
                SALogger.LogWarning("Rallypoint Delta list empty - adding vanilla...");
                rallypointDeltaList.Add("Vanilla");
            }

            if (ScorchedAcresVanilla.Value) scorchedAcresList.Add("Vanilla");
            if (ScorchedAcresSunset.Value) scorchedAcresList.Add("Sunset");
            if (ScorchedAcresNight.Value) scorchedAcresList.Add("Night");
            if (ScorchedAcresJade.Value) scorchedAcresList.Add("Jade");
            if (ScorchedAcresSunnyBeta.Value) scorchedAcresList.Add("Sunny Beta");
            if (ScorchedAcresCrimsonBeta.Value) scorchedAcresList.Add("Crimson Beta");
            if (ScorchedAcresTwilight.Value) scorchedAcresList.Add("Twilight");
            if (scorchedAcresList.Count == 0)
            {
                SALogger.LogWarning("Scorched Acres list empty - adding vanilla...");
                scorchedAcresList.Add("Vanilla");
            }

            if (SulfurPoolsVanilla.Value) sulfurPoolsList.Add("Vanilla");
            if (SulfurPoolsCoral.Value) sulfurPoolsList.Add("Coral");
            if (SulfurPoolsHell.Value) sulfurPoolsList.Add("Hell");
            if (SulfurPoolsVoid.Value) sulfurPoolsList.Add("Void");
            if (sulfurPoolsList.Count == 0)
            {
                SALogger.LogWarning("Sulfur Pools list empty - adding vanilla...");
                sulfurPoolsList.Add("Vanilla");
            }

            if (FogboundLagoonVanilla.Value) fogboundLagoonList.Add("Vanilla");
            if (FogboundLagoonClear.Value) fogboundLagoonList.Add("Clear");
            if (FogboundLagoonTwilight.Value) fogboundLagoonList.Add("Twilight");
            if (FogboundLagoonOvercast.Value) fogboundLagoonList.Add("Overcast");
            if (fogboundLagoonList.Count == 0)
            {
                SALogger.LogWarning("Fogbound Lagoon list empty - adding vanilla...");
                fogboundLagoonList.Add("Vanilla");
            }

            //

            if (AbyssalDepthsVanilla.Value) abyssalDepthsList.Add("Vanilla");
            if (AbyssalDepthsNight.Value) abyssalDepthsList.Add("Night");
            if (AbyssalDepthsBlue.Value) abyssalDepthsList.Add("Blue");
            if (AbyssalDepthsOrange.Value) abyssalDepthsList.Add("Orange");
            if (AbyssalDepthsCoral.Value) abyssalDepthsList.Add("Coral");
            if (abyssalDepthsList.Count == 0)
            {
                SALogger.LogWarning("Abyssal Depths list empty - adding vanilla...");
                abyssalDepthsList.Add("Vanilla");
            }

            if (SirensCallVanilla.Value) sirensCallList.Add("Vanilla");
            if (SirensCallNight.Value) sirensCallList.Add("Night");
            if (SirensCallSunny.Value) sirensCallList.Add("Sunny");
            if (SirensCallOvercast.Value) sirensCallList.Add("Overcast");
            if (SirensCallAphelian.Value) sirensCallList.Add("Aphelian");
            if (sirensCallList.Count == 0)
            {
                SALogger.LogWarning("Siren's Call list empty - adding vanilla...");
                sirensCallList.Add("Vanilla");
            }

            if (SunderedGroveVanilla.Value) sunderedGroveList.Add("Vanilla");
            if (SunderedGroveJade.Value) sunderedGroveList.Add("Jade");
            if (SunderedGroveSunny.Value) sunderedGroveList.Add("Sunny");
            if (SunderedGroveOvercast.Value) sunderedGroveList.Add("Overcast");
            if (SunderedGroveAbandoned.Value) sunderedGroveList.Add("Abandoned");
            if (sunderedGroveList.Count == 0)
            {
                SALogger.LogWarning("Sundered Grove list empty - adding vanilla...");
                sunderedGroveList.Add("Vanilla");
            }

            //

            if (HelminthHatcheryVanilla.Value) helminthHatcheryList.Add("Vanilla");
            if (HelminthHatcheryLunar.Value) helminthHatcheryList.Add("Lunar");

            if (SkyMeadowVanilla.Value) skyMeadowList.Add("Vanilla");
            if (SkyMeadowNight.Value) skyMeadowList.Add("Night");
            if (SkyMeadowOvercast.Value) skyMeadowList.Add("Overcast");
            if (SkyMeadowAbyssal.Value) skyMeadowList.Add("Abyssal");
            if (SkyMeadowTitanic.Value) skyMeadowList.Add("Titanic");
            if (SkyMeadowAbandoned.Value) skyMeadowList.Add("Abandoned");
            if (skyMeadowList.Count == 0)
            {
                SALogger.LogWarning("Sky Meadow list empty - adding vanilla...");
                skyMeadowList.Add("Vanilla");
            }

            //

            if (SlumberingSatelliteVanilla.Value) slumberingSatelliteList.Add("Vanilla");
            if (SlumberingSatelliteMorning.Value) slumberingSatelliteList.Add("Morning");
            if (SlumberingSatelliteOvercast.Value) slumberingSatelliteList.Add("Overcast");
            if (SlumberingSatelliteBlue.Value) slumberingSatelliteList.Add("Blue");
            if (slumberingSatelliteList.Count == 0)
            {
                SALogger.LogWarning("Slumbering Satellite list empty - adding vanilla...");
                slumberingSatelliteList.Add("Vanilla");
            }

            //

            if (CommencementVanilla.Value) commencementList.Add("Vanilla");
            if (CommencementNight.Value) commencementList.Add("Night");
            if (CommencementCrimson.Value) commencementList.Add("Crimson");
            if (CommencementCorruption.Value) commencementList.Add("Corruption");
            if (CommencementGray.Value) commencementList.Add("Gray");
            if (commencementList.Count == 0)
            {
                SALogger.LogWarning("Commencement list empty - adding vanilla...");
                commencementList.Add("Vanilla");
            }

            //

            if (VoidLocusVanilla.Value) voidLocusList.Add("Vanilla");
            if (VoidLocusTwilight.Value) voidLocusList.Add("Twilight");
            if (VoidLocusPink.Value) voidLocusList.Add("Pink");
            if (VoidLocusBlue.Value) voidLocusList.Add("Blue");
            if (voidLocusList.Count == 0)
            {
                SALogger.LogWarning("Void Locus list empty - adding vanilla...");
                voidLocusList.Add("Vanilla");
            }
        }
    }
}