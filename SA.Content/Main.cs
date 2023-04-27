using BepInEx;
using UnityEngine;
using System.Reflection;
using R2API;

namespace StageAesthetic
{
    [BepInPlugin("com.HIFU.StageAesthetic", "StageAesthetic", "0.7.3")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInDependency(PrefabAPI.PluginGUID)]
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