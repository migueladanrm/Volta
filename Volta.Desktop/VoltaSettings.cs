using System;
using System.Collections.Generic;
using System.IO;
using Volta.Common;

namespace Volta
{
    public static class VoltaSettings
    {
        private static Dictionary<string, object> __settings;
        private const string SETTINGS_FILE = "volta.cfg";

        static VoltaSettings() {
            __Load();
        }

        private static object __Get(string key) {
            if (__settings == null)
                __Load();

            try {
                return __settings[key];
            } catch (KeyNotFoundException) {
                return null;
            }
        }

        private static void __Set(string key, object value) {
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
                Console.WriteLine("Settings file was not found, it will be created.");

                using var emptySettings = SerializationUtils.Serialize(new Dictionary<string, object>()) as MemoryStream;
                File.WriteAllBytes(SETTINGS_FILE, emptySettings.ToArray());                

                Console.WriteLine("Settings file was created.");
            }

            Console.WriteLine("Loading settings file...");

            var tmpSettings = File.ReadAllBytes(SETTINGS_FILE);
            __settings = SerializationUtils.Deserialize<Dictionary<string, object>>(tmpSettings);

            Console.WriteLine("Settings loaded.");
        }

        private static void __Save() {
            try {
                var serializedSettings = SerializationUtils.Serialize(__settings) as MemoryStream;
                File.WriteAllBytes(SETTINGS_FILE, serializedSettings.ToArray());

                Console.WriteLine("Settings saved.");
            } catch (Exception e) {
                Console.WriteLine($"An error occured while trying save settings: {e.Message}");
            }
        }

        public static string LastDirectory {
            get => __Get(nameof(LastDirectory)) as string;
            set => __Set(nameof(LastDirectory), value);
        }

        public static byte MaxRecentFiles => 10;

        public static List<string> RecentFiles {
            get => __Get(nameof(RecentFiles)) as List<string>;
            set => __Set(nameof(RecentFiles), value);
        }

    }
}