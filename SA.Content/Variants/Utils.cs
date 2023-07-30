using UnityEngine;
using RoR2;
using static StageAesthetic.Variants.Utils;

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

        public static GameObject rain = SwapVariants.rain;
        public static GameObject snow = SwapVariants.snow;

        public static void AddRain(RainType rainType, bool bloodRain = false)
        {
            if (!rain)
            {
                return;
            }
            if (!SwapVariants.WeatherEffects.Value)
            {
                return;
            }
            if (!Run.instance)
            {
                return;
            }

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
                rainMaterial.SetColor("_TintColor", new Color32(94, 16, 16, 255));
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

            var minRotation1 = 0f;
            var minRotation2 = 0f;

            switch (rainType)
            {
                case RainType.Drizzle:
                    intensity = Run.instance.treasureRng.RangeFloat(270f, 300f);

                    speed = Run.instance.treasureRng.RangeFloat(110f, 120f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-3f, 3f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-3f, 3f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-6f, 6f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-6f, 6f);
                    break;

                case RainType.Rainstorm:
                    intensity = Run.instance.treasureRng.RangeFloat(310f, 340f);

                    speed = Run.instance.treasureRng.RangeFloat(130f, 140f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-5f, 5f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-5f, 5f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-10f, 10f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-10f, 10f);
                    break;

                case RainType.Monsoon:
                    intensity = Run.instance.treasureRng.RangeFloat(350f, 380f);

                    speed = Run.instance.treasureRng.RangeFloat(150f, 160f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-8f, 8f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-8f, 8f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-14f, 14f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-14f, 14f);
                    break;

                case RainType.Typhoon:
                    intensity = Run.instance.treasureRng.RangeFloat(390f, 420f);

                    speed = Run.instance.treasureRng.RangeFloat(170f, 180f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-12f, 12f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-19f, 19f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-19f, 19f);
                    break;
            }

            angleDev += minRotation1;
            angleDev2 += minRotation2;

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

        public static void AddSnow(SnowType snowType)
        {
            if (!snow)
            {
                return;
            }
            if (!SwapVariants.WeatherEffects.Value)
            {
                return;
            }
            if (!Run.instance)
            {
                return;
            }

            snow.AddComponent<StageAestheticWeatherController>();

            var trans = snow.transform;
            var actualSnow = trans.GetChild(0).gameObject;
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

            var minRotation1 = 0f;
            var minRotation2 = 0f;

            switch (snowType)
            {
                case SnowType.Light:
                    intensity = Run.instance.treasureRng.RangeFloat(600f, 800f);

                    speed = Run.instance.treasureRng.RangeFloat(12f, 16f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-4f, 4f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-4f, 4f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-7f, 7f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-7f, 7f);
                    break;

                case SnowType.Moderate:
                    intensity = Run.instance.treasureRng.RangeFloat(1400f, 1700f);

                    speed = Run.instance.treasureRng.RangeFloat(17f, 21f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-6f, 6f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-6f, 6f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-12f, 12f);
                    break;

                case SnowType.Heavy:
                    intensity = Run.instance.treasureRng.RangeFloat(2300f, 2800f);

                    speed = Run.instance.treasureRng.RangeFloat(22f, 28f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-11f, 11f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-11f, 11f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-17f, 17f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-17f, 17f);
                    break;

                case SnowType.Gigachad:
                    intensity = Run.instance.treasureRng.RangeFloat(3400f, 3800f);

                    speed = Run.instance.treasureRng.RangeFloat(29f, 33f);

                    minRotation1 = Run.instance.treasureRng.RangeFloat(-15f, 15f);
                    minRotation2 = Run.instance.treasureRng.RangeFloat(-15f, 15f);

                    angleDev = Run.instance.treasureRng.RangeFloat(-25f, 25f);
                    angleDev2 = Run.instance.treasureRng.RangeFloat(-25f, 25f);
                    break;
            }

            angleDev += minRotation1;
            angleDev2 += minRotation2;

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