using BepInEx.Configuration;
using BepInEx;
using RoR2;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;
using static StageAesthetic.SwapVariants;

namespace StageAesthetic
{
    public static class Config
    {
        public static ConfigFile SAConfig { get; set; }

        public static ConfigEntry<bool> Important;

        #region Enable/Disable Config

        // Roost
        public static ConfigEntry<bool> VanillaRoost { get; set; }

        public static ConfigEntry<bool> RoostChanges { get; set; }
        public static ConfigEntry<bool> SunnyRoost { get; set; }
        public static ConfigEntry<bool> NightRoost { get; set; }
        public static ConfigEntry<bool> StormRoost { get; set; }
        public static ConfigEntry<bool> VoidRoost { get; set; }
        public static ConfigEntry<bool> AbyssalRoost { get; set; }

        // Siphoned
        public static ConfigEntry<bool> NightForest { get; set; }

        public static ConfigEntry<bool> PurpleForest { get; set; }
        public static ConfigEntry<bool> CrimsonForest { get; set; }
        public static ConfigEntry<bool> MorningForest { get; set; }
        public static ConfigEntry<bool> VanillaForest { get; set; }
        public static ConfigEntry<bool> DesolateForest { get; set; }

        // Plains
        public static ConfigEntry<bool> VanillaPlains { get; set; }

        public static ConfigEntry<bool> SunsetPlains { get; set; }
        public static ConfigEntry<bool> OvercastPlains { get; set; }
        public static ConfigEntry<bool> NightPlains { get; set; }
        public static ConfigEntry<bool> NostalgicPlains { get; set; }
        public static ConfigEntry<bool> AbandonedPlains { get; set; }
        public static ConfigEntry<int> PlainsBridge { get; set; }

        // Aqueduct
        public static ConfigEntry<bool> VanillaAqueduct { get; set; }

        public static ConfigEntry<bool> AqueductChanges { get; set; }
        public static ConfigEntry<bool> DawnAqueduct { get; set; }
        public static ConfigEntry<bool> SunriseAqueduct { get; set; }
        public static ConfigEntry<bool> NightAqueduct { get; set; }
        public static ConfigEntry<bool> SunderedAqueduct { get; set; }

        // Aphelian

        public static ConfigEntry<bool> VanillaAphelian { get; set; }
        public static ConfigEntry<bool> TwilightAphelian { get; set; }
        public static ConfigEntry<bool> SunsetAphelian { get; set; }
        public static ConfigEntry<bool> SingularityAphelian { get; set; }
        public static ConfigEntry<bool> AbyssalAphelian { get; set; }

        // Dry Basin

        public static ConfigEntry<bool> OvercastBasin { get; set; }
        public static ConfigEntry<bool> BlueBasin { get; set; }
        public static ConfigEntry<bool> MorningBasin { get; set; }
        public static ConfigEntry<bool> VanillaBasin { get; set; }
        public static ConfigEntry<bool> BasinChanges { get; set; }

        // Wetland
        public static ConfigEntry<bool> VanillaWetland { get; set; }

        public static ConfigEntry<bool> SunsetWetland { get; set; }
        public static ConfigEntry<bool> BlueWetland { get; set; }
        public static ConfigEntry<bool> OvercastWetland { get; set; }
        public static ConfigEntry<bool> VoidWetland { get; set; }

        // Fogbound Lagoon

        public static ConfigEntry<bool> VanillaLagoon { get; set; }
        public static ConfigEntry<bool> ClearerLagoon { get; set; }
        public static ConfigEntry<bool> TwilightLagoon { get; set; }
        public static ConfigEntry<bool> OvercastLagoon { get; set; }

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

        // Siren
        public static ConfigEntry<bool> VanillaSiren { get; set; }

        public static ConfigEntry<bool> NightSiren { get; set; }
        public static ConfigEntry<bool> SunnySiren { get; set; }
        public static ConfigEntry<bool> MistySiren { get; set; }
        public static ConfigEntry<bool> AphelianSiren { get; set; }

        // Grove
        public static ConfigEntry<bool> VanillaGrove { get; set; }

        public static ConfigEntry<bool> GreenGrove { get; set; }
        public static ConfigEntry<bool> SunnyGrove { get; set; }
        public static ConfigEntry<bool> HannibalGrove { get; set; }
        public static ConfigEntry<bool> SandyGrove { get; set; }

        // Meadow
        public static ConfigEntry<bool> VanillaMeadow { get; set; }

        public static ConfigEntry<bool> MeadowChanges { get; set; }
        public static ConfigEntry<bool> NightMeadow { get; set; }
        public static ConfigEntry<bool> StormyMeadow { get; set; }
        public static ConfigEntry<bool> AbyssalMeadow { get; set; }
        public static ConfigEntry<bool> TitanicMeadow { get; set; }
        public static ConfigEntry<bool> SandyMeadow { get; set; }

        // Commencement

        public static ConfigEntry<bool> DarkCommencement { get; set; }
        public static ConfigEntry<bool> VanillaCommencement { get; set; }
        public static ConfigEntry<bool> CrimsonCommencement { get; set; }
        public static ConfigEntry<bool> CorruptionCommencement { get; set; }
        public static ConfigEntry<bool> GrayCommencement { get; set; }

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

        public static void SetConfig()
        {
            SAConfig = new ConfigFile(Paths.ConfigPath + "\\HIFU.StageAesthetic.cfg", true);
            Important = SAConfig.Bind("! Important !", "Config", true, "Make sure everyone's configs are the same for multiplayer!");

            VanillaRoost = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            NightRoost = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Night)?", true, "Dark with lights.");
            SunnyRoost = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Sunny)?", true, "Yellow sun over greenery.");
            StormRoost = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Storm)?", true, "Rainy with more fog.");
            VoidRoost = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Void)?", true, "Texture swap to Purple Void Fields.");
            AbyssalRoost = SAConfig.Bind("Stages : Distant Roost", "Enable Distant Roost (Abyssal)?", true, "Texture swap to Red Abyssal Depths.");

            RoostChanges = SAConfig.Bind("Stages : Distant Roost", "Alter Distant Roost (Vanilla)?", true, "Adds rain to the alt version.");

            VanillaForest = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            NightForest = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Night)?", true, "Blue and dark.");
            MorningForest = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Morning)?", true, "Yellow sun with blue shadows.");
            PurpleForest = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Purple)?", true, "Extra snowy with purple hints.");
            CrimsonForest = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Crimson)?", true, "Red fog with Doom vibes.");
            DesolateForest = SAConfig.Bind("Stages : Siphoned Forest", "Enable Siphoned Forest (Desolate)?", true, "Green ground with a purple contrast.");

            VanillaPlains = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Vanilla)?", false, "Disabling removes vanilla from getting picked");
            NostalgicPlains = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Nostalgic)?", true, "Early Access look.");
            SunsetPlains = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Sunset)?", true, "Orange sun over greenery.");
            OvercastPlains = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Overcast)?", true, "Rainy with more fog.");
            NightPlains = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Night)?", true, "Blue and dark.");
            AbandonedPlains = SAConfig.Bind("Stages : Titanic Plains", "Enable Titanic Plains (Abandoned)?", true, "Texture swap to Yellow Abandoned Aqueduct.");

            PlainsBridge = SAConfig.Bind<int>("Stages : Titanic Plains", "Bridge % Chance", 40, "How often the unused bridge in Titanic Plains should appear.");

            VanillaAqueduct = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            DawnAqueduct = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Dawn)?", true, "Dark orange.");
            SunriseAqueduct = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Sunrise)?", true, "Rainy blue sky with more fog.");
            NightAqueduct = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Night)?", true, "Dark blue.");
            SunderedAqueduct = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Abandoned Aqueduct (Sundered)?", true, "Texture swap to Pink Sundered Grove.");

            AqueductChanges = SAConfig.Bind("Stages :: Abandoned Aqueduct", "Alter Abandoned Aqueduct (Vanilla)?", true, "Makes the sun a slightly more intense yellow-orange, and changes its angle.");

            VanillaAphelian = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SingularityAphelian = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Singularity)?", true, "Very blue and dark.");
            TwilightAphelian = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Twilight)?", true, "Strong purple and orange fog.");
            SunsetAphelian = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Sunset)?", true, "Very strong orange sun.");
            AbyssalAphelian = SAConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Aphelian Sanctuary (Abyssal)?", true, "Texture swap to Red Abyssal Depths.");

            VanillaWetland = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            SunsetWetland = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Sunset)?", true, "Orange sun and fog.");
            BlueWetland = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Blue)?", true, "Blue.");
            OvercastWetland = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Overcast)?", true, "Green and dark.");
            VoidWetland = SAConfig.Bind("Stages :: Wetland Aspect", "Enable Wetland Aspect (Void)?", true, "Texture swap to Purple Void Fields.");

            VanillaBasin = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Vanilla)?", true, "Disabling removes vanilla from getting picked");
            MorningBasin = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Morning)?", true, "Yellow sun with blue shadows.");
            BlueBasin = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Purple)?", true, "Purple.");
            OvercastBasin = SAConfig.Bind("Stages :: Dry Basin", "Enable Dry Basin (Overcast)?", true, "Overcast and rainy.");

            BasinChanges = SAConfig.Bind("Stages :: Dry Basin", "Alter Dry Basin (Vanilla)?", true, "Adds a SandStorm 2.");

            VanillaDelta = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightDelta = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Night Delta?", true, "Blue and dark with extra snow.");
            FoggyDelta = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Foggy Delta?", true, "Rainy with more fog.");
            PurpleDelta = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Emerald Delta?", true, "Green fog.");
            TitanicDelta = SAConfig.Bind("Stages ::: Rallypoint Delta", "Enable Titanic Delta?", true, "Texture swap to Titanic Plains.");

            VanillaAcres = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            SunsetAcres = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Sunset Acres?", true, "Orange fog.");
            NightAcres = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Night Acres?", true, "Dark with stars!");
            BlueAcres = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Emerald Acres?", true, "Green fog.");
            BetaAcres = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Sunny Beta Acres?", false, "Brings back the unreleased Scorched Acres' look");
            BetaAcres2 = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Crimson Beta Acres?", false, "Brings back the unreleased Scorched Acres' look (alt ver)");
            TwilightAcres = SAConfig.Bind("Stages ::: Scorched Acres", "Enable Twilight Acres?", true, "Purple, blue and orange fog.");
            AcresChanges = SAConfig.Bind("Stages ::: Scorched Acres", "Alter vanilla Scorched Acres?", true, "Greatly increases the sunlight intensity, and alters the light angle and sun position towards a different corner of the map.");

            VanillaSulfur = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            CoralBlueSulfur = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Coral Blue Pools?", true, "Blue fog.");
            HellSulfur = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Hell Pools?", true, "Texture swap to Red Aphelian Sanctuary.");
            VoidSulfur = SAConfig.Bind("Stages ::: Sulfur Pools", "Enable Void Pools?", true, "Texture swap to Blue Void Fields.");

            VanillaLagoon = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            ClearerLagoon = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Clear Lagoon?", true, "Yellow sun over greenery with less fog.");
            TwilightLagoon = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Twilight Lagoon?", true, "Purple and orange fog.");
            OvercastLagoon = SAConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Overcast Lagoon?", true, "Very foggy with rain. Actually no rain yet, I'm waiting for a potential bugfix lol");

            VanillaDepths = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            DarkDepths = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Indigo Depths?", true, "Dark blue with light fog.");
            BlueDepths = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Hive Depths?", true, "Orange-ish and pink-ish");
            SkyDepths = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Pink Depths?", true, "Pink with some orange and blue.");
            CoralDepths = SAConfig.Bind("Stages :::: Abyssal Depths", "Enable Coral Depths?", true, "Texture swap to Blue/Purple/Pink Sundered Grove.");
            DepthsChanges = SAConfig.Bind("Stages :::: Abyssal Depths", "Alter vanilla Abyssal Depths?", true, "Greatly increases the sunlight intensity, and alters the light angle.");

            VanillaGrove = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Vanilla?", false, "Disabling removes vanilla from getting picked");
            GreenGrove = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Olive Grove?", true, "GREEN.");
            SunnyGrove = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Sunny Grove?", true, "Yellow sun over greenery.");
            HannibalGrove = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Overcast Grove?", true, "Rainy with extra fog.");
            SandyGrove = SAConfig.Bind("Stages :::: Sundered Grove", "Enable Abandoned Grove?", true, "Texture swap to Orange Abandoned Aqueduct.");

            VanillaSiren = SAConfig.Bind("Stages :::: Sirens Call", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightSiren = SAConfig.Bind("Stages :::: Sirens Call", "Enable Night Call?", true, "Dark and green.");
            SunnySiren = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Sun?", true, "Yellow sun over greenery.");
            MistySiren = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Storm?", true, "Rainy with more fog.");
            AphelianSiren = SAConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Sanctuary?", true, "Texture swap to Blue/Yellow/Orange Aphelian Sanctuary.");

            VanillaMeadow = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightMeadow = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Night Meadow?", true, "Blue and dark.");
            StormyMeadow = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Stormy Meadow?", true, "Rainy with more fog.");
            AbyssalMeadow = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Abyssal Meadow?", true, "Texture swap to Red Abyssal Depths.");
            TitanicMeadow = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Titanic Meadow?", true, "Texture swap to Titanic Plains.");
            SandyMeadow = SAConfig.Bind("Stages ::::: Sky Meadow", "Enable Abandoned Meadow?", true, "Texture swap to Yellow Abandoned Aqueduct.");
            MeadowChanges = SAConfig.Bind("Stages ::::: Sky Meadow", "Alter vanilla Sky Meadow?", true, "Makes the sun a slightly more intense yellow-orange.");

            VanillaCommencement = SAConfig.Bind("Stages :::::: Commencement", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            DarkCommencement = SAConfig.Bind("Stages :::::: Commencement", "Enable Dark Commencement?", true, "Very dark with a great view.");
            CrimsonCommencement = SAConfig.Bind("Stages :::::: Commencement", "Enable Crimson Commencement?", true, "Bloody and threatening.");
            CorruptionCommencement = SAConfig.Bind("Stages :::::: Commencement", "Enable Corruption Commencement?", true, "Purple.");
            GrayCommencement = SAConfig.Bind("Stages :::::: Commencement", "Enable Gray Commencement?", true, "Gray!");

            VanillaLocus = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            BlueLocus = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Blue Locus?", true, "Blue fog. Yeah that's it.");
            RedLocus = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Pink Locus?", true, "Pink.");
            PurpleLocus = SAConfig.Bind("Stages ::::::: Void Locus", "Enable Green Locus?", true, "Green..");

            VanillaPlanetarium = SAConfig.Bind("Stages :::::::: The Planetarium", "Enable Vanilla?", true, "Do not disable, currently bugged!");
            // PurplePlanetarium = AesConfig.Bind("Stages :::::::: The Planetarium", "Enable Purple Planetarium?", true, "");
            // TwilightPlanetarium = AesConfig.Bind("Stages :::::::: The Planetarium", "Enable Twilight Planetarium?", true, "");

            TitleScene = SAConfig.Bind("Stages Title", "Alter title screen?", true, "Adds rain, patches of grass, particles and brings a Commando closer to focus.");
            WeatherEffects = SAConfig.Bind("Stages Weather", "Import weather effects?", true, "Adds/swaps rain/snow for stages. Disabling this is recommended if performance is an issue. Starstorm 2 compatibility coming soon.");

            var tabID = 0;
            foreach (ConfigEntryBase ceb in SAConfig.GetConfigEntries())
            {
                var Name = ceb.Definition.Section;
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
            siphonedForestList = new();
            titanicPlainsList = new();

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

            commencementList = new();

            voidLocusList = new();
            thePlanetariumList = new();

            if (VanillaRoost.Value) distantRoostList.Add("Vanilla");
            if (NightRoost.Value) distantRoostList.Add("Night");
            if (SunnyRoost.Value) distantRoostList.Add("Sunny");
            if (StormRoost.Value) distantRoostList.Add("Storm");
            if (VoidRoost.Value) distantRoostList.Add("Void");
            if (AbyssalRoost.Value) distantRoostList.Add("Abyssal");
            if (distantRoostList.Count == 0)
            {
                SALogger.LogWarning("Distant Roost list empty - adding vanilla...");
                distantRoostList.Add("Vanilla");
            }

            if (VanillaForest.Value) siphonedForestList.Add("Vanilla");
            if (NightForest.Value) siphonedForestList.Add("Night");
            if (MorningForest.Value) siphonedForestList.Add("Morning");
            if (PurpleForest.Value) siphonedForestList.Add("Purple");
            if (CrimsonForest.Value) siphonedForestList.Add("Crimson");
            if (DesolateForest.Value) siphonedForestList.Add("Desolate");
            if (siphonedForestList.Count == 0)
            {
                SALogger.LogWarning("Siphoned Forest list empty - adding vanilla...");
                siphonedForestList.Add("Vanilla");
            }

            if (VanillaPlains.Value) titanicPlainsList.Add("Vanilla");
            if (NostalgicPlains.Value) titanicPlainsList.Add("Nostalgic");
            if (SunsetPlains.Value) titanicPlainsList.Add("Sunset");
            if (OvercastPlains.Value) titanicPlainsList.Add("Overcast");
            if (NightPlains.Value) titanicPlainsList.Add("Night");
            if (AbandonedPlains.Value) titanicPlainsList.Add("Abandoned");
            if (titanicPlainsList.Count == 0)
            {
                SALogger.LogWarning("Titanic Plains list empty - adding vanilla...");
                titanicPlainsList.Add("Vanilla");
            }

            //

            if (VanillaAqueduct.Value) abandonedAqueductList.Add("Vanilla");
            if (DawnAqueduct.Value) abandonedAqueductList.Add("Night");
            if (SunriseAqueduct.Value) abandonedAqueductList.Add("Rain");
            if (NightAqueduct.Value) abandonedAqueductList.Add("Nightrain");
            if (SunderedAqueduct.Value) abandonedAqueductList.Add("Sundered");
            if (abandonedAqueductList.Count == 0)
            {
                SALogger.LogWarning("Abandoned Aqueduct list empty - adding vanilla...");
                abandonedAqueductList.Add("Vanilla");
            }

            if (VanillaAphelian.Value) aphelianSanctuaryList.Add("Vanilla");
            if (SingularityAphelian.Value) aphelianSanctuaryList.Add("Singularity");
            if (TwilightAphelian.Value) aphelianSanctuaryList.Add("Twilight");
            if (SunsetAphelian.Value) aphelianSanctuaryList.Add("Sunset");
            if (AbyssalAphelian.Value) aphelianSanctuaryList.Add("Abyssal");
            if (aphelianSanctuaryList.Count == 0)
            {
                SALogger.LogWarning("Aphelian Sanctuary list empty - adding vanilla...");
                aphelianSanctuaryList.Add("Vanilla");
            }

            if (MorningBasin.Value) dryBasinList.Add("Morning");
            if (OvercastBasin.Value) dryBasinList.Add("Overcast");
            if (BlueBasin.Value) dryBasinList.Add("Blue");
            if (VanillaBasin.Value) dryBasinList.Add("Vanilla");
            if (dryBasinList.Count == 0)
            {
                SALogger.LogWarning("Dry Basin list empty - adding vanilla...");
                dryBasinList.Add("Vanilla");
            }

            if (VanillaWetland.Value) wetlandAspectList.Add("Vanilla");
            if (SunsetWetland.Value) wetlandAspectList.Add("Sunset");
            if (BlueWetland.Value) wetlandAspectList.Add("Sky");
            if (OvercastWetland.Value) wetlandAspectList.Add("Dark");
            if (VoidWetland.Value) wetlandAspectList.Add("Void");
            if (wetlandAspectList.Count == 0)
            {
                SALogger.LogWarning("Wetland Aspect list empty - adding vanilla...");
                wetlandAspectList.Add("Vanilla");
            }

            //

            if (VanillaDelta.Value) rallypointDeltaList.Add("Vanilla");
            if (NightDelta.Value) rallypointDeltaList.Add("Night");
            if (FoggyDelta.Value) rallypointDeltaList.Add("Foggy");
            if (PurpleDelta.Value) rallypointDeltaList.Add("Green");
            if (TitanicDelta.Value) rallypointDeltaList.Add("Titanic");
            if (rallypointDeltaList.Count == 0)
            {
                SALogger.LogWarning("Rallypoint Delta list empty - adding vanilla...");
                rallypointDeltaList.Add("Vanilla");
            }

            if (VanillaAcres.Value) scorchedAcresList.Add("Vanilla");
            if (SunsetAcres.Value) scorchedAcresList.Add("Sunset");
            if (NightAcres.Value) scorchedAcresList.Add("Night");
            if (BlueAcres.Value) scorchedAcresList.Add("Nothing");
            if (BetaAcres.Value) scorchedAcresList.Add("Beta");
            if (BetaAcres2.Value) scorchedAcresList.Add("Beta2");
            if (TwilightAcres.Value) scorchedAcresList.Add("Twilight");
            if (scorchedAcresList.Count == 0)
            {
                SALogger.LogWarning("Scorched Acres list empty - adding vanilla...");
                scorchedAcresList.Add("Vanilla");
            }

            if (VanillaSulfur.Value) sulfurPoolsList.Add("Vanilla");
            if (CoralBlueSulfur.Value) sulfurPoolsList.Add("Coralblue");
            if (HellSulfur.Value) sulfurPoolsList.Add("Hell");
            if (VoidSulfur.Value) sulfurPoolsList.Add("Void");
            if (sulfurPoolsList.Count == 0)
            {
                SALogger.LogWarning("Sulfur Pools list empty - adding vanilla...");
                sulfurPoolsList.Add("Vanilla");
            }

            if (VanillaLagoon.Value) fogboundLagoonList.Add("Vanilla");
            if (ClearerLagoon.Value) fogboundLagoonList.Add("Clearer");
            if (TwilightLagoon.Value) fogboundLagoonList.Add("Twilight");
            if (OvercastLagoon.Value) fogboundLagoonList.Add("Overcast");
            if (fogboundLagoonList.Count == 0)
            {
                SALogger.LogWarning("Fogbound Lagoon list empty - adding vanilla...");
                fogboundLagoonList.Add("Vanilla");
            }

            //

            if (VanillaDepths.Value) abyssalDepthsList.Add("Vanilla");
            if (DarkDepths.Value) abyssalDepthsList.Add("Dark");
            if (BlueDepths.Value) abyssalDepthsList.Add("Hive");
            if (SkyDepths.Value) abyssalDepthsList.Add("Orange");
            if (CoralDepths.Value) abyssalDepthsList.Add("Coral");
            if (abyssalDepthsList.Count == 0)
            {
                SALogger.LogWarning("Abyssal Depths list empty - adding vanilla...");
                abyssalDepthsList.Add("Vanilla");
            }

            if (VanillaGrove.Value) sunderedGroveList.Add("Vanilla");
            if (GreenGrove.Value) sunderedGroveList.Add("Green");
            if (SunnyGrove.Value) sunderedGroveList.Add("Sunny");
            if (HannibalGrove.Value) sunderedGroveList.Add("Storm");
            if (SandyGrove.Value) sunderedGroveList.Add("Sandy");
            if (sunderedGroveList.Count == 0)
            {
                SALogger.LogWarning("Sundered Grove list empty - adding vanilla...");
                sunderedGroveList.Add("Vanilla");
            }

            if (VanillaSiren.Value) sirensCallList.Add("Vanilla");
            if (NightSiren.Value) sirensCallList.Add("Night");
            if (SunnySiren.Value) sirensCallList.Add("Sunny");
            if (MistySiren.Value) sirensCallList.Add("Storm");
            if (AphelianSiren.Value) sirensCallList.Add("Aphelian");
            if (sirensCallList.Count == 0)
            {
                SALogger.LogWarning("Siren's Call list empty - adding vanilla...");
                sirensCallList.Add("Vanilla");
            }

            //

            if (VanillaMeadow.Value) skyMeadowList.Add("Vanilla");
            if (NightMeadow.Value) skyMeadowList.Add("Night");
            if (StormyMeadow.Value) skyMeadowList.Add("Storm");
            if (AbyssalMeadow.Value) skyMeadowList.Add("Abyss");
            if (TitanicMeadow.Value) skyMeadowList.Add("Titanic");
            if (SandyMeadow.Value) skyMeadowList.Add("Sandy");
            if (skyMeadowList.Count == 0)
            {
                SALogger.LogWarning("Sky Meadow list empty - adding vanilla...");
                skyMeadowList.Add("Vanilla");
            }

            //

            if (VanillaCommencement.Value) commencementList.Add("Vanilla");
            if (DarkCommencement.Value) commencementList.Add("Dark");
            if (CrimsonCommencement.Value) commencementList.Add("Crimson");
            if (CorruptionCommencement.Value) commencementList.Add("Corruption");
            if (GrayCommencement.Value) commencementList.Add("Gray");
            if (commencementList.Count == 0)
            {
                SALogger.LogWarning("Commencement list empty - adding vanilla...");
                commencementList.Add("Vanilla");
            }

            //

            if (VanillaLocus.Value) voidLocusList.Add("Vanilla");
            if (BlueLocus.Value) voidLocusList.Add("Blue");
            if (RedLocus.Value) voidLocusList.Add("Pink");
            if (PurpleLocus.Value) voidLocusList.Add("Green");
            if (voidLocusList.Count == 0)
            {
                SALogger.LogWarning("Void Locus list empty - adding vanilla...");
                voidLocusList.Add("Vanilla");
            }

            //

            if (VanillaPlanetarium.Value) thePlanetariumList.Add("Vanilla");
            // if (PurplePlanetarium.Value) planetariumList.Add("purple");
            // if (TwilightPlanetarium.Value) planetariumList.Add("twilight");
        }
    }
}