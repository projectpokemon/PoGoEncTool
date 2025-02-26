using System;
using System.IO;
using System.Text.Json;

namespace PoGoEncTool.Core;

public static class DataLoader
{
    private const string SettingsPath = "settings.json";

    public static PogoEncounterList GetData(string exePath, out ProgramSettings settings)
    {
        var settingsPath = Path.Combine(exePath, SettingsPath);
        settings = GetSettings(settingsPath);
        var args = Environment.GetCommandLineArgs();

        if (args.Length > 1 && File.Exists(args[1]))
            settings.DataPath = args[1];

        var context = ProgramSettingsContext.Default;
        var json = JsonSerializer.Serialize(settings, context.ProgramSettings);
        File.WriteAllText(settingsPath, json);

        return GetList(settings.DataPath);
    }

    private static PogoEncounterList GetList(string fn)
    {
        if (!File.Exists(fn))
            return new PogoEncounterList();

        var json = File.ReadAllText(fn);
        var context = PogoEncounterListContext.Default;
        var list = JsonSerializer.Deserialize(json, context.PogoEncounterList);
        return list ?? new PogoEncounterList();
    }

    private static ProgramSettings GetSettings(string settingsPath)
    {
        if (!File.Exists(SettingsPath))
            return new ProgramSettings();
        var text = File.ReadAllText(settingsPath);
        var context = ProgramSettingsContext.Default;
        var result = JsonSerializer.Deserialize(text, context.ProgramSettings);
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
        var context = PogoEncounterListContext.Default;
        var json = JsonSerializer.Serialize(entries, context.PogoEncounterList);
        var clone = JsonSerializer.Deserialize(json, context.PogoEncounterList);
        if (clone is null || clone.Data.Count != entries.Data.Count)
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
        var context = PogoEncounterListContext.Default;
        var json = JsonSerializer.Serialize(clone, context.PogoEncounterList);
        File.WriteAllText(jsonPath, json);
    }
}
