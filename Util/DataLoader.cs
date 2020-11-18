using System;
using System.IO;
using Newtonsoft.Json;

namespace PoGoEncTool
{
    public static class DataLoader
    {
        private const string SettingsPath = "settings.json";

        private static JsonSerializerSettings getSettings() => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented, 
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static PogoEncounterList GetData(string exePath, out ProgramSettings settings)
        {
            var settingsPath = Path.Combine(exePath, SettingsPath);
            settings = File.Exists(SettingsPath) ? JsonConvert.DeserializeObject<ProgramSettings>(File.ReadAllText(settingsPath)) : new ProgramSettings();
            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
                settings.DataPath = args[1];
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings));

            var fn = settings.DataPath;
            var str = File.Exists(fn) ? File.ReadAllText(fn) : string.Empty;
            return JsonConvert.DeserializeObject<PogoEncounterList?>(str, getSettings()) ?? new PogoEncounterList();
        }

        public static void SaveData(string exePath, PogoEncounterList entries, string jsonPath)
        {
            var clone = JsonConvert.DeserializeObject<PogoEncounterList>(JsonConvert.SerializeObject(entries));
            clone.Clean();
            var settings = getSettings();
            settings.Converters.Add(new FlatConverter<PogoEntry>());
            var contents = JsonConvert.SerializeObject(clone, settings);
            File.WriteAllText(jsonPath, contents);

            clone.Propagate();

            var data = PogoPickler.WritePickle(clone);
            File.WriteAllBytes(Path.Combine(exePath, "encounter_go_home.pkl"), data);

            var lgpe = PogoPickler.WritePickleLGPE(clone);
            File.WriteAllBytes(Path.Combine(exePath, "encounter_go_lgpe.pkl"), lgpe);
        }
    }
}
