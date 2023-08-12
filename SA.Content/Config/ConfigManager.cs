using BepInEx.Configuration;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using UnityEngine;

namespace StageAesthetic.Config
{
    public class ConfigManager
    {
        internal static bool ConfigChanged = false;
        internal static bool VersionChanged = false;

        public static T HandleConfig<T>(ConfigEntryBase entry, ConfigFile config, string name)
        {
            // SwapVariants.SALogger.LogError("typeof(T) in handle config is " + typeof(T));
            var method = typeof(ConfigFile).GetMethods().Where(x => x.Name == nameof(ConfigFile.Bind)).First();
            method = method.MakeGenericMethod(typeof(T));

            var newConfigEntry = new object[] { new ConfigDefinition(Regex.Replace(config.ConfigFilePath, "\\W", "") + " : " + entry.Definition.Section, name), entry.DefaultValue, new ConfigDescription(entry.Description.Description) };

            var backupVal = (ConfigEntryBase)method.Invoke(config, newConfigEntry);

            if (Config._preVersioning) entry.BoxedValue = entry.DefaultValue;

            if (!ConfigEqual(backupVal.DefaultValue, backupVal.BoxedValue))
            {
                if (VersionChanged)
                {
                    entry.BoxedValue = entry.DefaultValue;
                    backupVal.BoxedValue = backupVal.DefaultValue;
                }
            }
            return default;
        }

        private static bool ConfigEqual(object a, object b)
        {
            if (a.Equals(b)) return true;
            // if (a is bool || b is bool) return false;
            float fa, fb;
            if (float.TryParse(a.ToString(), out fa) && float.TryParse(b.ToString(), out fb) && Mathf.Abs(fa - fb) < 0.0001) return true;
            return false;
        }
    }
}