using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine;
using RoR2;
using System;
using BepInEx;

namespace StageAesthetic
{
    internal class FindObjects : MonoBehaviour
    {
        public void Awake()
        {
            // Add this to your existing Awake() and transfer everything else in this class
            On.RoR2.SceneDirector.Start += new On.RoR2.SceneDirector.hook_Start(SceneDirector_Start);
        }

        public void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            // Getting PostProcessing and lighting
            SceneInfo currentScene = SceneInfo.instance;
            PostProcessVolume volume = new();
            string scenename = SceneManager.GetActiveScene().name;
            if (currentScene) volume = currentScene.GetComponent<PostProcessVolume>(); // What should normally have PostProcessing
            if (!volume)
            {
                GameObject alt = GameObject.Find("PP + Amb");
                if (!alt) alt = GameObject.Find("PP, Global");
                if (!alt) alt = GameObject.Find("GlobalPostProcessVolume, Base");
                if (alt) volume = alt.GetComponent<PostProcessVolume>();
                else volume = null;
            }
            RampFog fog;
            if (scenename == "moon2")
            {
                volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
                volume.enabled = true;
                volume.isGlobal = true;
                volume.priority = 9999f;
                volume.profile = commencementVolume;
                fog = volume.profile.GetSetting<RampFog>();
            }
            else fog = volume.GetComponent<RampFog>();
            Light sun = GameObject.Find("Directional Light (SUN)").GetComponent<Light>(); // You may need to do this on a per-stage basis
            SetValues(fog, sun);
            if (commencementVolume == null) commencementVolume = volume.profile; // For some reason Commencement doesn't actually have any of the above available for post-processing, so I borrowed stage 1's for it
            // Add in per-stage changes needed here. For example, Commencement having -0.15 fogZero instead of -0.04:
            if (scenename == "moon2") fog.fogZero.value = -0.15f;
            // Anything else you need to add to SceneDirector_Start here
            orig(self);
        }

        public void SetValues(RampFog fog, Light sun)
        {
            fog.fogColorStart.value = new Color32(45, 49, 75, 0);
            fog.fogColorMid.value = new Color32(103, 99, 97, 130);
            fog.fogColorEnd.value = new Color32(211, 107, 84, 255);
            fog.fogZero.value = -0.04f;
            fog.fogOne.value = 0.389f;
            fog.fogIntensity.value = 1;
            fog.fogPower.value = 0.6f;
            fog.skyboxStrength.value = 0.041f;
            sun.color = new Color32(255, 135, 0, 255);
            sun.intensity = 1.3f;
            sun.shadowStrength = 1;
        }

        private PostProcessProfile commencementVolume;
    }
}