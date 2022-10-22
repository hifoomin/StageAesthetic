using R2API.Utils;
using BepInEx;
using UnityEngine;
using System.Reflection;

namespace StageAesthetic
{
    [BepInPlugin("com.HIFU.StageAesthetic", "StageAesthetic", "0.7.0")]
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
        public static AssetBundle stageaesthetic;
        public void Awake()
        {
            // increasing fogone increases the fog distance
            SwapVariants.AesLog = this.Logger;
            stageaesthetic = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("StageAesthetic.dll", "stageaesthetic"));
            SwapVariants.Initialize();
        }
    }
}