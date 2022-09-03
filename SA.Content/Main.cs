using R2API.Utils;
using BepInEx;

namespace StageAesthetic
{
    [BepInPlugin("com.HIFU.StageAesthetic", "StageAesthetic", "0.4.1")]
    [R2APISubmoduleDependency(new string[]
    {
        "DirectorAPI",
        "ArtifactAPI",
        "NetworkingAPI",
        "PrefabAPI"
    })]
    internal class Main : BaseUnityPlugin
    {
        public void Awake()
        {
            SwapVariants.AesLog = this.Logger;
            SwapVariants.Initialize();
        }
    }
}