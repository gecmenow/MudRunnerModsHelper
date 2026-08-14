using System.Diagnostics;

namespace MudRunnerModsHelper;

internal static class Game
{
    private const string MudrunnerExe = "Mudrunner.exe";

    public static bool ValidateExecutable()
    {
        var exePath = Path.Combine(Settings.GamePath, MudrunnerExe);
        if (File.Exists(exePath))
        {
            return true;
        }

        Console.WriteLine($"Game executable not found: {exePath}");
        Console.WriteLine("Place the helper in the same folder as Mudrunner.exe.");
        return false;
    }

    public static Process Launch()
    {
        var exePath = Path.Combine(Settings.GamePath, MudrunnerExe);

        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = Settings.GamePath,
            FileName = exePath,
        };

        var process = new Process { StartInfo = startInfo };
        process.Start();
        Console.WriteLine("Game launched. Press Space at the start prompt to restore mods.");
        return process;
    }
}
