using System.Diagnostics;

namespace MudRunnerModsHelper;

internal sealed class Game
{
    public static void Run()
    {
        // Specify the path of the application you want to run
        var gameFolder = File.ReadAllLines(Settings.Config).First();

        // Create a new process start info
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = gameFolder,
            FileName = gameFolder + @"\Mudrunner.exe",
        };

        // Start the process
        using var process = new Process { StartInfo = startInfo };
        process.Start();

        Thread.Sleep(Settings.Timeout);
    }
}
