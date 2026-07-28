using System.Diagnostics;

namespace MudRunnerModsHelper;

internal static class Game
{
    private const string MudrunnerExe = "Mudrunner.exe";
    
    public static bool ValidateExecutable()
    {
        var exePath = Path.Combine(Settings.GamePath, MudrunnerExe);
        if (File.Exists(exePath))
            return true;

        Console.WriteLine($"Game executable not found: {exePath}");
        return false;
    }

    public static void Run()
    {
        var exePath = Path.Combine(Settings.GamePath, MudrunnerExe);

        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = Settings.GamePath,
            FileName = exePath,
        };

        using var process = new Process { StartInfo = startInfo };
        process.Start();

        var secondsRemaining = Settings.TimeoutMs / 1000;
        for (var i = secondsRemaining; i > 0; i--)
        {
            Console.Write($"\rRestoring mods in {i}s...  ");
            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }
}
