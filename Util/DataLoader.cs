using System;
using System.IO;
using Newtonsoft.Json;

namespace PoGoEncTool
{
    public static class DataLoader
    {
        private const string SettingsPath = "settings.json";

        public static PogoEncounterList GetData(out ProgramSettings settings)
        {
            settings = File.Exists(SettingsPath) ? JsonConvert.DeserializeObject<ProgramSettings>(File.ReadAllText(SettingsPath)) : new ProgramSettings();
            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
                settings.DataPath = args[1];
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(settings));

            var fn = settings.DataPath;
            var str = File.Exists(fn) ? File.ReadAllText(fn) : string.Empty;
            return JsonConvert.DeserializeObject<PogoEncounterList?>(str) ?? new PogoEncounterList();
        }

        public static void SaveData(PogoEncounterList entries, string path)
        {
            var clone = JsonConvert.DeserializeObject<PogoEncounterList>(JsonConvert.SerializeObject(entries));
            clone.Clean();
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore };
            var str = JsonConvert.SerializeObject(clone, settings);
            File.WriteAllText(path, str);

            var data = PogoPickler.WritePickle(entries);
            File.WriteAllBytes("pgo_home.pkl", data);
        }
    }
}
