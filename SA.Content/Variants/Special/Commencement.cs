using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace StageAesthetic.Variants
{
    internal class Commencement
    {
        public static void DarkCommencement(RampFog fog)
        {
            fog.fogColorStart.value = new Color(0.08f, 0.05f, 0.12f, 0.4f);
            fog.fogColorMid.value = new Color(0.13f, 0.14f, 0.19f, 0.625f);
            fog.fogColorEnd.value = new Color(0f, 0f, 0f, 1f);
            fog.skyboxStrength.value = 0f;
            var sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>();
            sun.color = new Color32(178, 238, 238, 255);
            sun.intensity = 1.9f;
            var es = GameObject.Find("EscapeSequenceController").transform.GetChild(0);
            es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
            // es.GetChild(6).GetComponent<PostProcessDuration>().enabled = false;
            es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
            es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;
        }
    }
}