using PoGoEncTool.Core;
using System;
using System.Windows.Forms;

namespace PoGoEncTool.WinForms;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Check for command line arguments -- if externally requested to update the pkl files, do only that.
        var args = Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            foreach (var arg in args)
                Console.WriteLine(arg);
            // if --update is passed, run the updater
            if (args[1].Equals("--update", StringComparison.OrdinalIgnoreCase))
            {
                Updater.RunUpdater();
                return;
            }
        }
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Main());
    }
}

/// <summary>
/// General updater class to update pkl files.
/// </summary>
internal static class Updater
{
    public static void RunUpdater()
    {
        var path = Application.StartupPath;
        var entries = DataLoader.GetData(path, out var settings);
        DataLoader.SaveAllData(path, entries, settings.DataPath);
    }
}
