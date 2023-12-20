using System.Text.Json.Serialization;

namespace PoGoEncTool.Core;

[JsonSerializable(typeof(ProgramSettings))]
[JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
public sealed partial class ProgramSettingsContext : JsonSerializerContext;

[JsonSerializable(typeof(PogoEncounterList))]
[JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
public sealed partial class PogoEncounterListContext : JsonSerializerContext;
