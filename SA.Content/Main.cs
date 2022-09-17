using R2API.Utils;
using BepInEx;
using UnityEngine;
using System.Reflection;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;

namespace StageAesthetic
{
    [BepInPlugin("com.HIFU.StageAesthetic", "StageAesthetic", "0.6.0")]
    [BepInDependency("com.rune580.riskofoptions")] 
    [R2APISubmoduleDependency(new string[]
    {
        "DirectorAPI",
        "ArtifactAPI",
        "NetworkingAPI",
        "PrefabAPI"
    })]
    internal class Main : BaseUnityPlugin
    {
        public static ConfigEntry<bool> Important;
        public static AssetBundle stageaesthetic;
        public void Awake()
        {
            Important = Config.Bind("_Important", "Multiplayer", true, "Make sure everyone's configs are the same for multiplayer!");
            SwapVariants.AesLog = this.Logger;
            stageaesthetic = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("StageAesthetic.dll", "stageaesthetic"));
            ModSettingsManager.AddOption(new GenericButtonOption("_Important", "Multiplayer", "Make sure everyone's configs are the same for multiplayer!", "                                      ", DoNothing));
            ModSettingsManager.SetModIcon(stageaesthetic.LoadAsset<Sprite>("texModIcon.png"));
            SwapVariants.Initialize();
        }
        void DoNothing()
        {

        }
    }
}