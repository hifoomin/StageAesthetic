using UnityEngine;
using RoR2;
using System;

namespace StageAesthetic.Variants
{
    public static class Utils
    {
        public enum RainType
        {
            Drizzle,
            Rainstorm,
            Monsoon,
            Typhoon
        }

        public enum SnowType
        {
            Light,
            Moderate,
            Heavy,
            Gigachad
        }

        public enum SandType
        {
            Light,
            Moderate,
            Heavy,
            Gigachad
        }

        public enum SoundType
        {
            DayNature,
            Rain,
            Storm,
            NightNature,
            WaterStream,
            Wind
        }

        public static GameObject rain = SwapVariants.rain;
        public static GameObject snow = SwapVariants.snow;
        public static GameObject sand = SwapVariants.sand;

        public static void AddRain(RainType rainType, bool bloodRain = false)
        {
            if (!rain)
            {
                Debug.LogError("Rain gameobject not found");
                return;
            }
            if (!WeatherEffects.Value)
            {
                Debug.LogError("Weather effects turned off");
                return;
            }
            if (!Run.instance)
            {
                Debug.LogError("Run instance not found");
                return;
            }

            if (!rain.GetComponent<StageAestheticWeatherController>())
                rain.AddComponent<StageAestheticWeatherController>();

            var trans = rain.transform;
            var actualRain = trans.GetChild(0).gameObject;
            // var trans2 = actualRain.transform;
            // var rainSplash = trans2.GetChild(0).gameObject;
            // var rainSplashBig = trans2.GetChild(1).gameObject;

            var particleSystemRenderer = actualRain.GetComponent<ParticleSystemRenderer>();

            var rainMaterial = particleSystemRenderer.material;
            rainMaterial.shader = Main.cloudRemap;
            rainMaterial.EnableKeyword("DISABLEREMAP");
            rainMaterial.SetFloat("_DstBlend", 10);
            rainMaterial.SetFloat("_SrcBlend", 5);
            if (bloodRain)
                rainMaterial.SetColor("_TintColor", new Color32(72, 36, 36, 255));
            else
                rainMaterial.SetColor("_TintColor", new Color32(166, 166, 166, 255));
            /*
            var rainSplashMaterial = rainSplash.GetComponent<ParticleSystemRenderer>().material;
            rainSplashMaterial.shader = Main.cloudRemap;
            rainSplashMaterial.EnableKeyword("DISABLEREMAP");
            rainSplashMaterial.SetFloat("_DstBlend", 10);
            rainSplashMaterial.SetFloat("_SrcBlend", 5);

            var rainSplashBigMaterial = rainSplashBig.GetComponent<ParticleSystemRenderer>().material;
            rainSplashBigMaterial.shader = Main.cloudRemap;
            rainSplashBigMaterial.EnableKeyword("DISABLEREMAP");
            rainSplashBigMaterial.SetFloat("_DstBlend", 10);
            rainSplashBigMaterial.SetFloat("_SrcBlend", 5);
            */
            var difficultyDefScalingValue = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).scalingValue;
            var difficultyCoefficient = Run.instance.difficultyCoefficient;

            var intensity = 0f;
            var speed = 0f;
            var angleDev = 0f;
            var angleDev2 = 0f;

            switch (rainType)
            {
                case RainType.Drizzle:
                    intensity = Run.instance.treasureRng.RangeFloat(270f, 300f);

                    speed = Run.instance.treasureRng.RangeFloat(110f, 120f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-6f, 6f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-6f, 6f);
                    break;

                case RainType.Rainstorm:
                    intensity = Run.instance.treasureRng.RangeFloat(310f, 340f);

                    speed = Run.instance.treasureRng.RangeFloat(130f, 140f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-10f, 10f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-10f, 10f);
                    break;

                case RainType.Monsoon:
                    intensity = Run.instance.treasureRng.RangeFloat(350f, 380f);

                    speed = Run.instance.treasureRng.RangeFloat(150f, 160f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-14f, 14f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-14f, 14f);
                    break;

                case RainType.Typhoon:
                    intensity = Run.instance.treasureRng.RangeFloat(390f, 420f);

                    speed = Run.instance.treasureRng.RangeFloat(170f, 180f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-19f, 19f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-19f, 19f);
                    break;
            }

            intensity = intensity + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);
            speed = speed + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);

            var particleSystem = actualRain.GetComponent<ParticleSystem>();

            var main = particleSystem.main;
            main.startSpeed = Mathf.Min(1000f, speed);
            main.maxParticles = Mathf.Min(5000000, 100000 + (int)(intensity * 30f));

            var emission = particleSystem.emission;
            var rateOverTime = emission.rateOverTime;
            rateOverTime.mode = ParticleSystemCurveMode.Constant;
            rateOverTime.constant = Mathf.Min(10000f, 800f + intensity);

            var shape = particleSystem.shape;
            shape.rotation = new Vector3(angleDev, 0f, angleDev2);

            // Debug.LogErrorFormat("Rain Type {0} Rain Intensity {1} Rain Speed {2} Max Particles {3} Rate Over Time Constant {4} Difficulty Def Scaling Value {5} Difficulty Coefficient {6}", rainType, intensity, speed, main.maxParticles, rateOverTime.constant, difficultyDefScalingValue, difficultyCoefficient);

            UnityEngine.Object.Instantiate(rain, Vector3.zero, Quaternion.identity);
        }

        public static void AddSnow(SnowType snowType, float heightOverride = 150f)
        {
            if (!snow)
            {
                return;
            }
            if (!WeatherEffects.Value)
            {
                return;
            }
            if (!Run.instance)
            {
                return;
            }

            if (!snow.GetComponent<StageAestheticWeatherController>())
                snow.AddComponent<StageAestheticWeatherController>();

            var trans = snow.transform;
            var actualSnow = trans.GetChild(0).gameObject;
            actualSnow.transform.localPosition = new Vector3(0f, heightOverride, 0f);
            var goofySnow = actualSnow.transform.GetChild(0).gameObject;

            var particleSystemRenderer = actualSnow.GetComponent<ParticleSystemRenderer>();

            var snowMaterial = particleSystemRenderer.material;
            snowMaterial.shader = Main.cloudRemap;
            snowMaterial.EnableKeyword("DISABLEREMAP");
            snowMaterial.SetFloat("_DstBlend", 10);
            snowMaterial.SetFloat("_SrcBlend", 5);

            var goofyParticleSystemRenderer = goofySnow.GetComponent<ParticleSystemRenderer>();

            var goofySnowMaterial = goofyParticleSystemRenderer.material;
            goofySnowMaterial.shader = Main.cloudRemap;
            goofySnowMaterial.EnableKeyword("DISABLEREMAP");
            goofySnowMaterial.SetFloat("_DstBlend", 10);
            goofySnowMaterial.SetFloat("_SrcBlend", 5);

            var difficultyDefScalingValue = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).scalingValue;
            var difficultyCoefficient = Run.instance.difficultyCoefficient;

            var intensity = 0f;
            var speed = 0f;
            var angleDev = 0f;
            var angleDev2 = 0f;

            switch (snowType)
            {
                case SnowType.Light:
                    intensity = Run.instance.treasureRng.RangeFloat(600f, 800f);

                    speed = Run.instance.treasureRng.RangeFloat(12f, 16f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-7f, 7f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-7f, 7f);
                    break;

                case SnowType.Moderate:
                    intensity = Run.instance.treasureRng.RangeFloat(1400f, 1700f);

                    speed = Run.instance.treasureRng.RangeFloat(17f, 21f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    break;

                case SnowType.Heavy:
                    intensity = Run.instance.treasureRng.RangeFloat(2300f, 2800f);

                    speed = Run.instance.treasureRng.RangeFloat(22f, 28f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-17f, 17f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-17f, 17f);
                    break;

                case SnowType.Gigachad:
                    intensity = Run.instance.treasureRng.RangeFloat(3400f, 3800f);

                    speed = Run.instance.treasureRng.RangeFloat(29f, 33f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-25f, 25f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-25f, 25f);
                    break;
            }

            intensity = intensity + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);
            speed = speed + Mathf.Sqrt(difficultyDefScalingValue) + Mathf.Sqrt(difficultyCoefficient);

            var particleSystem = actualSnow.GetComponent<ParticleSystem>();

            var main = particleSystem.main;
            main.startSpeed = Mathf.Min(1000f, speed);
            main.maxParticles = Mathf.Min(5000000, 100000 + (int)(intensity * 5f));

            var emission = particleSystem.emission;
            var rateOverTime = emission.rateOverTime;
            rateOverTime.mode = ParticleSystemCurveMode.Constant;
            rateOverTime.constant = Mathf.Min(10000f, intensity);

            var shape = particleSystem.shape;
            shape.rotation = new Vector3(angleDev, 0f, angleDev2);

            // Debug.LogErrorFormat("Rain Type {0} Rain Intensity {1} Rain Speed {2} Max Particles {3} Rate Over Time Constant {4} Difficulty Def Scaling Value {5} Difficulty Coefficient {6}", rainType, intensity, speed, main.maxParticles, rateOverTime.constant, difficultyDefScalingValue, difficultyCoefficient);

            UnityEngine.Object.Instantiate(snow, Vector3.zero, Quaternion.identity);
        }

        public static void AddSand(SandType sandType)
        {
            if (!sand)
            {
                return;
            }
            if (!WeatherEffects.Value)
            {
                return;
            }
            if (!Run.instance)
            {
                return;
            }

            if (!sand.GetComponent<StageAestheticWeatherController>())
                sand.AddComponent<StageAestheticWeatherController>();

            var trans = sand.transform;
            var actualSand = trans.GetChild(0).gameObject;

            var particleSystemRenderer = actualSand.GetComponent<ParticleSystemRenderer>();

            var sandMaterial = particleSystemRenderer.material;
            sandMaterial.shader = Main.cloudRemap;
            sandMaterial.EnableKeyword("DISABLEREMAP");
            sandMaterial.SetFloat("_DstBlend", 10);
            sandMaterial.SetFloat("_SrcBlend", 5);

            var difficultyDefScalingValue = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty).scalingValue;
            var difficultyCoefficient = Run.instance.difficultyCoefficient;

            var intensity = 0f;
            var speed = 0f;
            var angleDev = 0f;
            var angleDev2 = 0f;

            switch (sandType)
            {
                case SandType.Light:
                    intensity = Run.instance.treasureRng.RangeFloat(2500f, 3000f);

                    speed = Run.instance.treasureRng.RangeFloat(20f, 25f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-7f, 7f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-7f, 7f);
                    break;

                case SandType.Moderate:
                    intensity = Run.instance.treasureRng.RangeFloat(3000f, 3500f);

                    speed = Run.instance.treasureRng.RangeFloat(25f, 30f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    break;

                case SandType.Heavy:
                    intensity = Run.instance.treasureRng.RangeFloat(3500f, 4000f);

                    speed = Run.instance.treasureRng.RangeFloat(25f, 30f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-17f, 17f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-17f, 17f);
                    break;

                case SandType.Gigachad:
                    intensity = Run.instance.treasureRng.RangeFloat(4500f, 5000f);

                    speed = Run.instance.treasureRng.RangeFloat(25f, 30f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-25f, 25f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-25f, 25f);
                    break;
            }

            intensity = intensity + Mathf.Sqrt(difficultyDefScalingValue * 2500f) + Mathf.Sqrt(difficultyCoefficient * 20f);
            speed = speed + Mathf.Sqrt(difficultyDefScalingValue * 5f) + Mathf.Sqrt(difficultyCoefficient / 2f);

            var particleSystem = actualSand.GetComponent<ParticleSystem>();

            var main = particleSystem.main;
            main.startSpeed = Mathf.Min(1100f, speed);
            main.maxParticles = Mathf.Min(5000000, 100000 + (int)(intensity * 5f));

            var emission = particleSystem.emission;
            var rateOverTime = emission.rateOverTime;
            rateOverTime.mode = ParticleSystemCurveMode.Constant;
            rateOverTime.constant = Mathf.Min(10000f, intensity);

            var shape = particleSystem.shape;
            shape.rotation = new Vector3(angleDev, 0f, angleDev2);

            // Debug.LogErrorFormat("Rain Type {0} Rain Intensity {1} Rain Speed {2} Max Particles {3} Rate Over Time Constant {4} Difficulty Def Scaling Value {5} Difficulty Coefficient {6}", rainType, intensity, speed, main.maxParticles, rateOverTime.constant, difficultyDefScalingValue, difficultyCoefficient);

            UnityEngine.Object.Instantiate(sand, Vector3.zero, Quaternion.identity);
        }

        public static void PlaySound(SoundType soundType)
        {
            if (!WeatherSounds.Value)
            {
                return;
            }

            var soundToPlay = soundType switch
            {
                SoundType.DayNature => "Play_SA_birds",
                SoundType.Rain => "Play_SA_rain",
                SoundType.Storm => "Play_SA_storm",
                SoundType.NightNature => "Play_SA_night",
                SoundType.WaterStream => "Play_SA_water",
                SoundType.Wind => "Play_SA_wind",
                _ => "Play_SA_wind"
            };

            Util.PlaySound(soundToPlay, RoR2Application.instance.gameObject);
        }

        public static void StopSounds()
        {
            Util.PlaySound("Stop_SA_birds", RoR2Application.instance.gameObject);
            Util.PlaySound("Stop_SA_rain", RoR2Application.instance.gameObject);
            Util.PlaySound("Stop_SA_storm", RoR2Application.instance.gameObject);
            Util.PlaySound("Stop_SA_night", RoR2Application.instance.gameObject);
            Util.PlaySound("Stop_SA_water", RoR2Application.instance.gameObject);
            Util.PlaySound("Stop_SA_wind", RoR2Application.instance.gameObject);
        }
    }

    // for compat
    public class StageAestheticWeatherController : MonoBehaviour
    {
        public GameObject particleSystem;
        public bool disable = false;
        public float timer = 0f;
        public float interval = 0.1f;

        public void Start()
        {
            particleSystem = transform.GetChild(0).gameObject;
        }

        public void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer >= interval)
            {
                if (disable)
                    particleSystem.SetActive(false);
                else
                    particleSystem.SetActive(true);
                timer = 0f;
            }
        }
    }
}