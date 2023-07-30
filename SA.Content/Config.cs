using System;
using System.Collections.Generic;
using System.Text;
using BepInEx.Configuration;
using BepInEx;
using RoR2;
using RiskOfOptions;
using RiskOfOptions.Options;
using RiskOfOptions.OptionConfigs;
using UnityEngine;
using StageAesthetic.Variants;

namespace StageAesthetic
{
    public class Config : SwapVariants
    {
        public static ConfigEntry<bool> Important;

        public static void SetConfig()
        {
            AesConfig = new ConfigFile(Paths.ConfigPath + "\\HIFU.StageAesthetic.cfg", true);
            Important = AesConfig.Bind("! Important !", "Config", true, "Make sure everyone's configs are the same for multiplayer!");
            VanillaPlains = AesConfig.Bind("Stages : Titanic Plains", "Enable Vanilla?", false, "Disabling removes vanilla from getting picked");
            NostalgiaPlains = AesConfig.Bind("Stages : Titanic Plains", "Enable Nostalgia Plains?", true, "Early Access look.");
            SunsetPlains = AesConfig.Bind("Stages : Titanic Plains", "Enable Sunset Plains?", true, "Orange sun over greenery.");
            RainyPlains = AesConfig.Bind("Stages : Titanic Plains", "Enable Rainy Plains?", true, "Rainy with more fog.");
            NightPlains = AesConfig.Bind("Stages : Titanic Plains", "Enable Night Plains?", true, "Blue and dark.");
            SandyPlains = AesConfig.Bind("Stages : Titanic Plains", "Enable Abandoned Plains?", true, "Texture swap to Yellow Abandoned Aqueduct.");
            PlainsBridge = AesConfig.Bind<int>("Stages : Titanic Plains", "Bridge % Chance", 40, "How often the unused bridge in Titanic Plains should appear.");

            VanillaRoost = AesConfig.Bind("Stages : Distant Roost", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightRoost = AesConfig.Bind("Stages : Distant Roost", "Enable Night Roost?", true, "Dark with lights.");
            SunnyRoost = AesConfig.Bind("Stages : Distant Roost", "Enable Sunny Roost?", true, "Yellow sun over greenery.");
            FoggyRoost = AesConfig.Bind("Stages : Distant Roost", "Enable Storm Roost?", true, "Rainy with more fog.");
            VoidRoost = AesConfig.Bind("Stages : Distant Roost", "Enable Void Roost?", true, "Texture swap to Purple Void Fields.");
            GoldRoost = AesConfig.Bind("Stages : Distant Roost", "Enable Abyssal Roost?", true, "Texture swap to Red Abyssal Depths.");
            RoostChanges = AesConfig.Bind("Stages : Distant Roost", "Add rain in alt version?", true, "");

            NightForest = AesConfig.Bind("Stages : Siphoned Forest", "Enable Night Forest?", true, "Blue and dark.");
            ExtraSnowyForest = AesConfig.Bind("Stages : Siphoned Forest", "Enable Extra Snowy Forest?", true, "Extra snowy with purple hints.");
            CrimsonForest = AesConfig.Bind("Stages : Siphoned Forest", "Enable Crimson Forest?", true, "Red fog with Doom vibes.");
            MorningForest = AesConfig.Bind("Stages : Siphoned Forest", "Enable Morning Forest?", true, "Yellow sun with blue shadows.");
            VanillaForest = AesConfig.Bind("Stages : Siphoned Forest", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            DesolateForest = AesConfig.Bind("Stages : Siphoned Forest", "Enable Desolate Forest?", true, "Green ground with a purple contrast.");

            VanillaAphelian = AesConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NearRainAphelian = AesConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Twilight Sanctuary?", true, "Strong purple and orange fog.");
            SunsetterAphelian = AesConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Sunrise Sanctuary?", true, "Very strong orange sun.");
            NightAphelian = AesConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Singularity Sanctuary?", true, "Very blue and dark.");
            AbyssalAphelian = AesConfig.Bind("Stages :: Aphelian Sanctuary", "Enable Abyssal Sanctuary?", true, "Texture swap to Red Abyssal Depths.");

            VanillaWetland = AesConfig.Bind("Stages :: Wetland Aspect", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            SunsetWetland = AesConfig.Bind("Stages :: Wetland Aspect", "Enable Sunset Aspect?", true, "Orange sun and fog.");
            SkyWetland = AesConfig.Bind("Stages :: Wetland Aspect", "Enable Sky Aspect?", true, "Purple.");
            EveningWetland = AesConfig.Bind("Stages :: Wetland Aspect", "Enable Dark Aspect?", true, "Green and dark.");
            VoidWetland = AesConfig.Bind("Stages :: Wetland Aspect", "Enable Void Aspect?", true, "Texture swap to Purple Void Fields.");

            VanillaAqueduct = AesConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightAqueduct = AesConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Dark Aqueduct?", true, "Honestly no idea lol");
            RainyAqueduct = AesConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Rainy Aqueduct?", true, "Rainy with more fog.");
            MistyAqueduct = AesConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Night Aqueduct?", true, "Honestly no idea lol");
            SunderedAqueduct = AesConfig.Bind("Stages :: Abandoned Aqueduct", "Enable Sundered Aqueduct?", true, "Texture swap to Pink Sundered Grove.");
            AqueductChanges = AesConfig.Bind("Stages :: Abandoned Aqueduct", "Alter vanilla Abandoned Aqueduct?", true, "Makes the sun a slightly more intense yellow-orange, and changes its angle.");

            VanillaBasin = AesConfig.Bind("Stages :: Dry Basin", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            MorningBasin = AesConfig.Bind("Stages :: Dry Basin", "Enable Morning Basin?", true, "Yellow sun with blue shadows.");
            PurpleBasin = AesConfig.Bind("Stages :: Dry Basin", "Enable Purple Basin?", true, "Purple.");
            RainyBasin = AesConfig.Bind("Stages :: Dry Basin", "Enable Rainy Basin?", true, "Overcast and rainy.");

            VanillaDelta = AesConfig.Bind("Stages ::: Rallypoint Delta", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightDelta = AesConfig.Bind("Stages ::: Rallypoint Delta", "Enable Night Delta?", true, "Blue and dark with extra snow.");
            FoggyDelta = AesConfig.Bind("Stages ::: Rallypoint Delta", "Enable Foggy Delta?", true, "Rainy with more fog.");
            PurpleDelta = AesConfig.Bind("Stages ::: Rallypoint Delta", "Enable Emerald Delta?", true, "Green fog.");
            TitanicDelta = AesConfig.Bind("Stages ::: Rallypoint Delta", "Enable Titanic Delta?", true, "Texture swap to Titanic Plains.");

            VanillaAcres = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            SunsetAcres = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Sunset Acres?", true, "Orange fog.");
            NightAcres = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Night Acres?", true, "Dark with stars!");
            BlueAcres = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Emerald Acres?", true, "Green fog.");
            BetaAcres = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Sunny Beta Acres?", false, "Brings back the unreleased Scorched Acres' look");
            BetaAcres2 = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Crimson Beta Acres?", false, "Brings back the unreleased Scorched Acres' look (alt ver)");
            TwilightAcres = AesConfig.Bind("Stages ::: Scorched Acres", "Enable Twilight Acres?", true, "Purple, blue and orange fog.");
            AcresChanges = AesConfig.Bind("Stages ::: Scorched Acres", "Alter vanilla Scorched Acres?", true, "Greatly increases the sunlight intensity, and alters the light angle and sun position towards a different corner of the map.");

            VanillaSulfur = AesConfig.Bind("Stages ::: Sulfur Pools", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            CoralBlueSulfur = AesConfig.Bind("Stages ::: Sulfur Pools", "Enable Coral Blue Pools?", true, "Blue fog.");
            HellSulfur = AesConfig.Bind("Stages ::: Sulfur Pools", "Enable Hell Pools?", true, "Texture swap to Red Aphelian Sanctuary.");
            VoidSulfur = AesConfig.Bind("Stages ::: Sulfur Pools", "Enable Void Pools?", true, "Texture swap to Blue Void Fields.");

            VanillaLagoon = AesConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            ClearerLagoon = AesConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Clear Lagoon?", true, "Yellow sun over greenery with less fog.");
            TwilightLagoon = AesConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Twilight Lagoon?", true, "Purple and orange fog.");
            OvercastLagoon = AesConfig.Bind("Stages ::: Fogbound Lagoon", "Enable Overcast Lagoon?", true, "Very foggy with rain. Actually no rain yet, I'm waiting for a potential bugfix lol");

            VanillaDepths = AesConfig.Bind("Stages :::: Abyssal Depths", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            DarkDepths = AesConfig.Bind("Stages :::: Abyssal Depths", "Enable Indigo Depths?", true, "Dark blue with light fog.");
            BlueDepths = AesConfig.Bind("Stages :::: Abyssal Depths", "Enable Hive Depths?", true, "Orange-ish and pink-ish");
            SkyDepths = AesConfig.Bind("Stages :::: Abyssal Depths", "Enable Pink Depths?", true, "Pink with some orange and blue.");
            CoralDepths = AesConfig.Bind("Stages :::: Abyssal Depths", "Enable Coral Depths?", true, "Texture swap to Blue/Purple/Pink Sundered Grove.");
            DepthsChanges = AesConfig.Bind("Stages :::: Abyssal Depths", "Alter vanilla Abyssal Depths?", true, "Greatly increases the sunlight intensity, and alters the light angle.");

            VanillaGrove = AesConfig.Bind("Stages :::: Sundered Grove", "Enable Vanilla?", false, "Disabling removes vanilla from getting picked");
            GreenGrove = AesConfig.Bind("Stages :::: Sundered Grove", "Enable Olive Grove?", true, "GREEN.");
            SunnyGrove = AesConfig.Bind("Stages :::: Sundered Grove", "Enable Sunny Grove?", true, "Yellow sun over greenery.");
            HannibalGrove = AesConfig.Bind("Stages :::: Sundered Grove", "Enable Overcast Grove?", true, "Rainy with extra fog.");
            SandyGrove = AesConfig.Bind("Stages :::: Sundered Grove", "Enable Abandoned Grove?", true, "Texture swap to Orange Abandoned Aqueduct.");

            VanillaSiren = AesConfig.Bind("Stages :::: Sirens Call", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightSiren = AesConfig.Bind("Stages :::: Sirens Call", "Enable Night Call?", true, "Dark and green.");
            SunnySiren = AesConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Sun?", true, "Yellow sun over greenery.");
            MistySiren = AesConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Storm?", true, "Rainy with more fog.");
            AphelianSiren = AesConfig.Bind("Stages :::: Sirens Call", "Enable Sirens Sanctuary?", true, "Texture swap to Blue/Yellow/Orange Aphelian Sanctuary.");

            VanillaMeadow = AesConfig.Bind("Stages ::::: Sky Meadow", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            NightMeadow = AesConfig.Bind("Stages ::::: Sky Meadow", "Enable Night Meadow?", true, "Blue and dark.");
            StormyMeadow = AesConfig.Bind("Stages ::::: Sky Meadow", "Enable Stormy Meadow?", true, "Rainy with more fog.");
            AbyssalMeadow = AesConfig.Bind("Stages ::::: Sky Meadow", "Enable Abyssal Meadow?", true, "Texture swap to Red Abyssal Depths.");
            TitanicMeadow = AesConfig.Bind("Stages ::::: Sky Meadow", "Enable Titanic Meadow?", true, "Texture swap to Titanic Plains.");
            SandyMeadow = AesConfig.Bind("Stages ::::: Sky Meadow", "Enable Abandoned Meadow?", true, "Texture swap to Yellow Abandoned Aqueduct.");
            MeadowChanges = AesConfig.Bind("Stages ::::: Sky Meadow", "Alter vanilla Sky Meadow?", true, "Makes the sun a slightly more intense yellow-orange.");

            VanillaCommencement = AesConfig.Bind("Stages :::::: Commencement", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            DarkCommencement = AesConfig.Bind("Stages :::::: Commencement", "Enable Dark Commencement?", true, "Very dark with a great view.");
            CrimsonCommencement = AesConfig.Bind("Stages :::::: Commencement", "Enable Crimson Commencement?", true, "Bloody and threatening.");
            CorruptionCommencement = AesConfig.Bind("Stages :::::: Commencement", "Enable Corruption Commencement?", true, "Purple.");
            GrayCommencement = AesConfig.Bind("Stages :::::: Commencement", "Enable Gray Commencement?", true, "Gray!");

            VanillaLocus = AesConfig.Bind("Stages ::::::: Void Locus", "Enable Vanilla?", true, "Disabling removes vanilla from getting picked");
            BlueLocus = AesConfig.Bind("Stages ::::::: Void Locus", "Enable Blue Locus?", true, "Blue fog. Yeah that's it.");
            RedLocus = AesConfig.Bind("Stages ::::::: Void Locus", "Enable Pink Locus?", true, "Pink.");
            PurpleLocus = AesConfig.Bind("Stages ::::::: Void Locus", "Enable Green Locus?", true, "Green..");

            VanillaPlanetarium = AesConfig.Bind("Stages :::::::: The Planetarium", "Enable Vanilla?", true, "Do not disable, currently bugged!");
            // PurplePlanetarium = AesConfig.Bind("Stages :::::::: The Planetarium", "Enable Purple Planetarium?", true, "");
            // TwilightPlanetarium = AesConfig.Bind("Stages :::::::: The Planetarium", "Enable Twilight Planetarium?", true, "");

            TitleScene = AesConfig.Bind("Stages Title", "Alter title screen?", true, "Adds rain, patches of grass, particles and brings a Commando closer to focus.");
            WeatherEffects = AesConfig.Bind("Stages Weather", "Import weather effects?", true, "Adds/swaps rain/snow for stages. Disabling this is recommended if performance is an issue. Starstorm 2 compatibility coming soon.");

            var tabID = 0;
            foreach (ConfigEntryBase ceb in AesConfig.GetConfigEntries())
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

        public static void ApplyConfig(Run obj)
        {
            plainsList = new List<string>();
            roostList = new List<string>();
            forestList = new List<string>();

            wetlandList = new List<string>();
            aqueductList = new List<string>();
            aphelianList = new List<string>();
            basinList = new List<string>();

            deltaList = new List<string>();
            acresList = new List<string>();
            sulfurList = new List<string>();
            fogboundList = new List<string>();

            depthsList = new List<string>();
            sirenList = new List<string>();
            groveList = new List<string>();

            meadowList = new List<string>();

            commencementList = new List<string>();

            locusList = new List<string>();
            planetariumList = new List<string>();

            if (VanillaPlains.Value) plainsList.Add("vanilla");
            if (SunsetPlains.Value) plainsList.Add("sunset");
            if (RainyPlains.Value) plainsList.Add("rain");
            if (NightPlains.Value) plainsList.Add("night");
            if (NostalgiaPlains.Value) plainsList.Add("nostalgia");
            if (SandyPlains.Value) plainsList.Add("sandy");
            if (plainsList.Count == 0)
            {
                AesLog.LogWarning("Titanic Plains list empty - adding vanilla...");
                plainsList.Add("vanilla");
            }

            if (VanillaRoost.Value) roostList.Add("vanilla");
            if (NightRoost.Value) roostList.Add("night");
            if (SunnyRoost.Value) roostList.Add("sunny");
            if (FoggyRoost.Value) roostList.Add("foggy");
            if (VoidRoost.Value) roostList.Add("void");
            if (GoldRoost.Value) roostList.Add("gold");
            if (roostList.Count == 0)
            {
                AesLog.LogWarning("Distant Roost list empty - adding vanilla...");
                roostList.Add("vanilla");
            }

            if (NightForest.Value) forestList.Add("night");
            if (ExtraSnowyForest.Value) forestList.Add("extrasnowy");
            if (CrimsonForest.Value) forestList.Add("crimson");
            if (MorningForest.Value) forestList.Add("morning");
            if (VanillaForest.Value) forestList.Add("vanilla");
            if (DesolateForest.Value) forestList.Add("desolate");
            if (forestList.Count == 0)
            {
                AesLog.LogWarning("Siphoned Forest list empty - adding vanilla...");
                forestList.Add("vanilla");
            }

            //

            if (VanillaWetland.Value) wetlandList.Add("vanilla");
            if (SunsetWetland.Value) wetlandList.Add("sunset");
            if (SkyWetland.Value) wetlandList.Add("sky");
            if (EveningWetland.Value) wetlandList.Add("dark");
            if (VoidWetland.Value) wetlandList.Add("void");
            if (wetlandList.Count == 0)
            {
                AesLog.LogWarning("Wetland Aspect list empty - adding vanilla...");
                wetlandList.Add("vanilla");
            }

            if (VanillaAqueduct.Value) aqueductList.Add("vanilla");
            if (NightAqueduct.Value) aqueductList.Add("night");
            if (RainyAqueduct.Value) aqueductList.Add("rain");
            if (MistyAqueduct.Value) aqueductList.Add("nightrain");
            if (SunderedAqueduct.Value) aqueductList.Add("sundered");
            if (aqueductList.Count == 0)
            {
                AesLog.LogWarning("Abandoned Aqueduct list empty - adding vanilla...");
                aqueductList.Add("vanilla");
            }
            if (MorningBasin.Value) basinList.Add("morning");
            if (RainyBasin.Value) basinList.Add("rainy");
            if (PurpleBasin.Value) basinList.Add("purple");
            if (VanillaBasin.Value) basinList.Add("vanilla");
            if (basinList.Count == 0)
            {
                AesLog.LogWarning("Dry Basin list empty - adding vanilla...");
                basinList.Add("vanilla");
            }
            if (NearRainAphelian.Value) aphelianList.Add("nearrain");
            if (SunsetterAphelian.Value) aphelianList.Add("sunrise");
            if (NightAphelian.Value) aphelianList.Add("night");
            if (VanillaAphelian.Value) aphelianList.Add("vanilla");
            if (AbyssalAphelian.Value) aphelianList.Add("abyss");
            if (aphelianList.Count == 0)
            {
                AesLog.LogWarning("Aphelian Sanctuary list empty - adding vanilla...");
                aphelianList.Add("vanilla");
            }

            //

            if (VanillaDelta.Value) deltaList.Add("vanilla");
            if (NightDelta.Value) deltaList.Add("night");
            if (FoggyDelta.Value) deltaList.Add("foggy");
            if (PurpleDelta.Value) deltaList.Add("green");
            if (TitanicDelta.Value) deltaList.Add("titanic");
            if (deltaList.Count == 0)
            {
                AesLog.LogWarning("Rallypoint Delta list empty - adding vanilla...");
                deltaList.Add("vanilla");
            }

            if (VanillaAcres.Value) acresList.Add("vanilla");
            if (SunsetAcres.Value) acresList.Add("sunset");
            if (NightAcres.Value) acresList.Add("night");
            if (BlueAcres.Value) acresList.Add("nothing");
            if (BetaAcres.Value) acresList.Add("beta");
            if (BetaAcres2.Value) acresList.Add("beta2");
            if (TwilightAcres.Value) acresList.Add("twilight");
            if (acresList.Count == 0)
            {
                AesLog.LogWarning("Scorched Acres list empty - adding vanilla...");
                acresList.Add("vanilla");
            }

            if (VanillaSulfur.Value) sulfurList.Add("vanilla");
            if (CoralBlueSulfur.Value) sulfurList.Add("coralblue");
            if (HellSulfur.Value) sulfurList.Add("hell");
            if (VoidSulfur.Value) sulfurList.Add("void");
            if (sulfurList.Count == 0)
            {
                AesLog.LogWarning("Sulfur Pools list empty - adding vanilla...");
                sulfurList.Add("vanilla");
            }

            if (VanillaLagoon.Value) fogboundList.Add("vanilla");
            if (ClearerLagoon.Value) fogboundList.Add("clearer");
            if (TwilightLagoon.Value) fogboundList.Add("twilight");
            if (OvercastLagoon.Value) fogboundList.Add("overcast");
            if (fogboundList.Count == 0)
            {
                AesLog.LogWarning("Fogbound Lagoon list empty - adding vanilla...");
                fogboundList.Add("vanilla");
            }

            //

            if (VanillaDepths.Value) depthsList.Add("vanilla");
            if (DarkDepths.Value) depthsList.Add("dark");
            if (BlueDepths.Value) depthsList.Add("hive");
            if (SkyDepths.Value) depthsList.Add("orange");
            if (CoralDepths.Value) depthsList.Add("coral");
            if (depthsList.Count == 0)
            {
                AesLog.LogWarning("Abyssal Depths list empty - adding vanilla...");
                depthsList.Add("vanilla");
            }

            if (VanillaGrove.Value) groveList.Add("vanilla");
            if (GreenGrove.Value) groveList.Add("green");
            if (SunnyGrove.Value) groveList.Add("sunny");
            if (HannibalGrove.Value) groveList.Add("storm");
            if (SandyGrove.Value) groveList.Add("sandy");
            if (groveList.Count == 0)
            {
                AesLog.LogWarning("Sundered Grove list empty - adding vanilla...");
                groveList.Add("vanilla");
            }

            if (VanillaSiren.Value) sirenList.Add("vanilla");
            if (NightSiren.Value) sirenList.Add("night");
            if (SunnySiren.Value) sirenList.Add("sunny");
            if (MistySiren.Value) sirenList.Add("storm");
            if (AphelianSiren.Value) sirenList.Add("aphelian");
            if (sirenList.Count == 0)
            {
                AesLog.LogWarning("Siren's Call list empty - adding vanilla...");
                sirenList.Add("vanilla");
            }

            //

            if (VanillaMeadow.Value) meadowList.Add("vanilla");
            if (NightMeadow.Value) meadowList.Add("night");
            if (StormyMeadow.Value) meadowList.Add("storm");
            if (AbyssalMeadow.Value) meadowList.Add("abyss");
            if (TitanicMeadow.Value) meadowList.Add("titanic");
            if (SandyMeadow.Value) meadowList.Add("sandy");
            if (meadowList.Count == 0)
            {
                AesLog.LogWarning("Sky Meadow list empty - adding vanilla...");
                meadowList.Add("vanilla");
            }

            //

            if (VanillaCommencement.Value) commencementList.Add("vanilla");
            if (DarkCommencement.Value) commencementList.Add("dark");
            if (CrimsonCommencement.Value) commencementList.Add("crimson");
            if (CorruptionCommencement.Value) commencementList.Add("corruption");
            if (GrayCommencement.Value) commencementList.Add("gray");
            if (commencementList.Count == 0)
            {
                AesLog.LogWarning("Commencement list empty - adding vanilla...");
                commencementList.Add("vanilla");
            }

            //

            if (VanillaLocus.Value) locusList.Add("vanilla");
            if (BlueLocus.Value) locusList.Add("blue");
            if (RedLocus.Value) locusList.Add("pink");
            if (PurpleLocus.Value) locusList.Add("green");
            if (locusList.Count == 0)
            {
                AesLog.LogWarning("Void Locus list empty - adding vanilla...");
                locusList.Add("vanilla");
            }

            //

            if (VanillaPlanetarium.Value) planetariumList.Add("vanilla");
            // if (PurplePlanetarium.Value) planetariumList.Add("purple");
            // if (TwilightPlanetarium.Value) planetariumList.Add("twilight");
        }
    }
}