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

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Main());
    }
}

internal static class Updater
{
    public static void RunUpdater()
    {
        var path = Application.StartupPath;
        var entries = DataLoader.GetData(path, out var settings);
        DataLoader.SaveAllData(path, entries, settings.DataPath);
    }
}
