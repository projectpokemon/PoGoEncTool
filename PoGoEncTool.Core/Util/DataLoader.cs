using System;
using System.IO;
using Newtonsoft.Json;
using PoGoEncTool.WinForms;

namespace PoGoEncTool.Core
{
    public static class DataLoader
    {
        private const string SettingsPath = "settings.json";

        private static JsonSerializerSettings GetSettings() => new()
        {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public static PogoEncounterList GetData(string exePath, out ProgramSettings settings)
        {
            var settingsPath = Path.Combine(exePath, SettingsPath);
            settings = GetSettings(settingsPath);
            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
                settings.DataPath = args[1];
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings));

            return GetList(settings.DataPath);
        }

        private static PogoEncounterList GetList(string fn)
        {
            var str = File.Exists(fn) ? File.ReadAllText(fn) : string.Empty;
            return JsonConvert.DeserializeObject<PogoEncounterList?>(str, GetSettings()) ?? new PogoEncounterList();
        }

        private static ProgramSettings GetSettings(string settingsPath)
        {
            if (!File.Exists(SettingsPath))
                return new ProgramSettings();
            var text = File.ReadAllText(settingsPath);
            var result = JsonConvert.DeserializeObject<ProgramSettings>(text);
            return result ?? new ProgramSettings();
        }

        public static void SavePickles(string binDestinationPath, string listJsonPath)
        {
            var list = GetList(listJsonPath);
            SaveAllData(binDestinationPath, list);
        }

        /// <summary>
        /// Saves all data for the program.
        /// </summary>
        /// <param name="binDestinationPath">Path to save the pickles</param>
        /// <param name="entries">Object containing all encounter data objects</param>
        /// <param name="listJsonPath">Encounter json path to save to. If left null, will not be saved.</param>
        public static void SaveAllData(string binDestinationPath, PogoEncounterList entries, string? listJsonPath = null)
        {
            var clone = JsonConvert.DeserializeObject<PogoEncounterList>(JsonConvert.SerializeObject(entries));
            if (clone is null)
                throw new NullReferenceException("Should have been able to create a clone from original object.");

            clone.Clean();
            if (listJsonPath != null)
                SaveList(listJsonPath, clone);

            clone.Propagate();

            var data = PogoPickler.WritePickle(clone);
            File.WriteAllBytes(Path.Combine(binDestinationPath, "encounter_go_home.pkl"), data);

            var lgpe = PogoPickler.WritePickleLGPE(clone);
            File.WriteAllBytes(Path.Combine(binDestinationPath, "encounter_go_lgpe.pkl"), lgpe);
        }

        private static void SaveList(string jsonPath, PogoEncounterList clone)
        {
            var settings = GetSettings();
            settings.Converters.Add(new FlatConverter<PogoEntry>());
            var contents = JsonConvert.SerializeObject(clone, settings);
            File.WriteAllText(jsonPath, contents);
        }
    }
}
