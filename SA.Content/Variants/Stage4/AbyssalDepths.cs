using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace StageAesthetic.Variants
{
    internal class AbyssalDepths
    {
        public static void VanillaChanges()
        {
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.intensity = 2f;
            GameObject.Find("Directional Light (SUN)").transform.localEulerAngles = new Vector3(35, 15, 351);
        }

        public static void HiveCave(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(63, 38, 70, 84);
            fog.fogColorMid.value = new Color32(53, 86, 65, 205);
            fog.fogColorEnd.value = new Color32(51, 96, 46, 255);
            fog.fogOne.value = 0.129f;
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(202, 161, 201, 255);
            sunLight.intensity = 0.5f;
            sunLight.shadowStrength = 0.3f;
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(58, 35, 60, 80);
            caveFog.fogColorMid.value = new Color32(44, 74, 55, 184);
            caveFog.fogColorEnd.value = new Color32(40, 68, 53, 255);
            cgrade.colorFilter.value = new Color32(192, 100, 208, 255);
            cgrade.colorFilter.overrideState = true;
            // Lighting: Magenta coral, orange otherwise
            LightChange("hive");
        }

        public static void DarkCave(RampFog fog, ColorGrading cgrade)
        {
            fog.fogColorStart.value = new Color32(76, 71, 119, 76);
            fog.fogColorMid.value = new Color32(73, 90, 104, 175);
            fog.fogColorEnd.value = new Color32(78, 94, 103, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(69, 201, 215, 255);
            sunLight.intensity = 1.2f;
            GameObject.Find("Directional Light (SUN)").transform.localEulerAngles = new Vector3(43, 78, 351);
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(67, 65, 109, 76);
            caveFog.fogColorMid.value = new Color32(40, 68, 123, 161);
            caveFog.fogColorEnd.value = new Color32(46, 128, 148, 255);
            cgrade.colorFilter.value = new Color32(119, 207, 181, 255);
            cgrade.colorFilter.overrideState = true;
            // Lighting: Blue coral, cyan or green lighting otherwise
            LightChange("azure");
        }

        public static void MeadowCave(RampFog fog)
        {
            fog.fogColorStart.value = new Color32(96, 67, 103, 33);
            fog.fogColorMid.value = new Color32(102, 66, 109, 148);
            fog.fogColorEnd.value = new Color32(148, 85, 166, 255);
            var sunLight = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sunLight.color = new Color32(205, 129, 255, 255);
            sunLight.intensity = 1f;
            RampFog caveFog = GameObject.Find("HOLDER: Lighting, PP, Wind, Misc").transform.Find("DCPPInTunnels").gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<RampFog>();
            caveFog.fogColorStart.value = new Color32(85, 57, 91, 33);
            caveFog.fogColorMid.value = new Color32(90, 55, 97, 148);
            caveFog.fogColorEnd.value = new Color32(135, 76, 149, 255);
            // Lighting: Pink coral, orange otherwise
            LightChange("meadow");
        }

        public static void LightChange(string variant)
        {
            if (variant == "meadow")
            {
                coral = new Color(0.64f, 0.343f, 0.924f, 1);
                chain = new Color(0.981f, 0.521f, 0.065f);
                crystal = new Color(0.598f, 0.117f, 0.355f);
            }
            else if (variant == "azure")
            {
                coral = new Color(0.188f, 0.444f, 0, 1);
                chain = new Color(0.181f, 0.921f, 0.945f);
                crystal = new Color(0f, 0.837f, 0.14f);
            }
            else if (variant == "hive")
            {
                coral = new Color32(30, 209, 27, 255);
                chain = new Color(0.981f, 0.521f, 0.065f);
                crystal = new Color(0.718f, 0, 0.515f);
            }
            var lightList = UnityEngine.Object.FindObjectsOfType(typeof(Light)) as Light[];
            foreach (Light light in lightList)
            {
                var lightBase = light.gameObject;
                if (lightBase != null)
                {
                    var lightParent = lightBase.transform.parent;
                    if (lightParent != null)
                    {
                        if (light.gameObject.transform.parent.gameObject.name.Equals("DCCoralPropMediumActive"))
                        {
                            light.color = coral;
                            var lightLP = light.transform.localPosition;
                            lightLP.z = 4;
                        }
                        else if (light.gameObject.transform.parent.gameObject.name.Equals("DCCrystalCluster Variant")) light.color = crystal;
                    }
                    if (light.gameObject.name.Equals("CrystalLight")) light.color = chain;
                }
            }
        }

        private static Color coral;
        private static Color chain;
        private static Color crystal;
    }
}