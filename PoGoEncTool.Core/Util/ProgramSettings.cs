namespace PoGoEncTool.Core;

public class ProgramSettings
{
    private const string FileName = "data.json";
    public string DataPath { get; set; } = FileName;
    public bool IsDarkMode { get; set; } // opt-in
}
