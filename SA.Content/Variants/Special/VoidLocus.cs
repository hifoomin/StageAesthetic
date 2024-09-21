using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants.Special
{
    internal class VoidLocus
    {
        public static void Blue(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(81, 81, 142, 20);
            fog.fogColorMid.value = new Color32(76, 96, 150, 35);
            fog.fogColorEnd.value = new Color32(55, 46, 117, 111);
            fog.fogIntensity.value = 1f;
            fog.fogPower.value = 0.3f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.182f;

            cgrade.colorFilter.value = new Color32(102, 115, 176, 255);
            cgrade.colorFilter.overrideState = true;

            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(30, 28, 99, 255);
            sunLight.intensity = 2.6f;

            var theLightCantSeemToUnderstandItCantSeemToKnow = GameObject.Find("Weather, Void Stage").transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>();

            var newMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Bright Top.mat").WaitForCompletion());
            newMat.SetColor("_TintColor", new Color32(2, 0, 255, 255));
            newMat.SetFloat("_AlphaBias", 0.2f);

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + newMat);

            theLightCantSeemToUnderstandItCantSeemToKnow.sharedMaterial = newMat;
        }

        public static void Twilight(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(227, 26, 150, 15);
            fog.fogColorMid.value = new Color32(80, 51, 113, 100);
            fog.fogColorEnd.value = new Color32(68, 201, 133, 50);
            fog.fogIntensity.value = 0.95f;
            fog.fogPower.value = 0.5f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.13f;

            cgrade.colorFilter.value = new Color32(35, 46, 99, 70);
            cgrade.colorFilter.overrideState = true;

            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(255, 255, 255, 255);
            sunLight.intensity = 4f;
            sunLight.transform.eulerAngles = Vector3.zero;

            var theLightCantSeemToUnderstandItCantSeemToKnow = GameObject.Find("Weather, Void Stage").transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>();

            var newRamp = Addressables.LoadAssetAsync<Texture2D>("RoR2/Base/Common/ColorRamps/texRampBombOrb.png").WaitForCompletion();

            var newMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Bright Top.mat").WaitForCompletion());
            newMat.SetColor("_TintColor", new Color32(70, 15, 27, 255));
            newMat.SetFloat("_AlphaBias", 0.2423056f);
            newMat.SetFloat("_AlphaBoost", 20f);
            newMat.SetTexture("_RemapTex", newRamp);

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + newMat);

            theLightCantSeemToUnderstandItCantSeemToKnow.sharedMaterial = newMat;
        }

        public static void Pink(RampFog fog, ColorGrading cgrade)
        {
            fog.fogIntensity.value = 0.95f;
            fog.fogPower.value = 0.6f;
            fog.fogZero.value = 0f;
            fog.fogOne.value = 0.1f;

            fog.fogColorStart.value = new Color32(197, 108, 119, 10);
            fog.fogColorMid.value = new Color32(93, 29, 28, 50);
            fog.fogColorEnd.value = new Color32(0, 0, 0, 170);

            cgrade.colorFilter.value = new Color32(117, 69, 90, 255);
            cgrade.colorFilter.overrideState = true;

            var sunLight = GameObject.Find("Directional Light").GetComponent<Light>();
            sunLight.color = new Color32(255, 200, 189, 255);
            sunLight.intensity = 4f;
            sunLight.transform.eulerAngles = Vector3.zero;

            var theLightCantSeemToUnderstandItCantSeemToKnow = GameObject.Find("Weather, Void Stage").transform.GetChild(6).GetChild(0).GetComponent<MeshRenderer>();

            var newMat = Object.Instantiate(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Bright Top.mat").WaitForCompletion());
            newMat.SetColor("_TintColor", new Color32(66, 0, 34, 255));
            newMat.SetFloat("_AlphaBias", 0.2f);
            newMat.SetFloat("_AlphaBoost", 11.9882f);

            SwapVariants.SALogger.LogInfo("Initializing material, if this is null then guhhh... " + newMat);

            theLightCantSeemToUnderstandItCantSeemToKnow.sharedMaterial = newMat;
        }
    }
}