using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Volta
{
    public static class VoltaSettings
    {
        private static Dictionary<string, object> __settings;
        private const string SETTINGS_FILE = "volta.json";
        private static Logger log;

        static VoltaSettings() {
            log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            log.Information("Initializing events logger...");
        }

        private static object __Get(string key) {
            key = __ToCamelCase(key);

            if (__settings == null)
                __Load();

            try {
                return __settings[key];
            } catch (KeyNotFoundException) {
                return null;
            }
        }

        private static void __Set(string key, object value) {
            key = __ToCamelCase(key);

            if (__settings == null)
                __Load();

            if (__settings.ContainsKey(key))
                __settings[key] = value;
            else
                __settings.Add(key, value);

            __Save();
        }

        private static void __Load() {
            if (!File.Exists(SETTINGS_FILE)) {
                log.Information("Settings file was not found, it will be created.");

                File.WriteAllText(SETTINGS_FILE, "{ }");

                log.Information("Settings file was created.");
            }

            log.Information("Loading settings file...");

            var tmpSettingsFile = File.ReadAllText(SETTINGS_FILE);
            __settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmpSettingsFile);

            log.Information("Settings loaded.");
        }

        private static void __Save() {
            try {
                var jsonSettings = JsonConvert.SerializeObject(__settings);
                File.WriteAllBytes(SETTINGS_FILE, Encoding.UTF8.GetBytes(jsonSettings));

                log.Information("Settings saved.");
            } catch (Exception e) {
                log.Error($"An error occured while trying save settings: {e.Message}");
            }
        }

        private static string __ToCamelCase(string str) => $"{str.Substring(0, 1).ToLower()}{str.Substring(1)}";

        public static string LastDirectory {
            get => __Get(nameof(LastDirectory)) as string;
            set => __Set(nameof(LastDirectory), value);
        }

    }
}