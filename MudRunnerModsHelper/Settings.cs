namespace MudRunnerModsHelper;

internal static class Settings
{
    private const string ConfigFileName = "Config.txt";
    private const string MudrunnerExe = "Mudrunner.exe";
    private const int DefaultTimeoutSeconds = 10;
    private const string PlaceholderGamePath = @"D:\Games\MudRunner";

    public static string GamePath { get; private set; } = string.Empty;
    public static int TimeoutMs { get; private set; }

    public static bool Load()
    {
        var baseDir = AppContext.BaseDirectory;
        var configPath = Path.Combine(baseDir, ConfigFileName);

        if (!File.Exists(configPath) && !CreateConfig(baseDir, configPath))
        {
            Console.WriteLine($"Could not find {MudrunnerExe} next to the helper, so the game folder is unknown.");
            Console.WriteLine($"A config file was created at: {configPath}");
            Console.WriteLine("Please set the game folder path on the first line, then run again.");
            return false;
        }

        var lines = File.ReadAllLines(configPath);
        if (lines.Length < 2)
        {
            Console.WriteLine("Config file must contain game path and timeout (seconds).");
            return false;
        }

        GamePath = lines[0].Trim();
        if (string.IsNullOrWhiteSpace(GamePath))
        {
            Console.WriteLine("Game path is empty.");
            return false;
        }

        if (!int.TryParse(lines[1].Trim(), out var timeoutSeconds))
        {
            Console.WriteLine("Couldn't parse timeout.");
            return false;
        }

        if (timeoutSeconds <= 0)
        {
            Console.WriteLine($"Timeout must be positive. Using default of {DefaultTimeoutSeconds}s.");
            timeoutSeconds = DefaultTimeoutSeconds;
        }

        TimeoutMs = timeoutSeconds * 1000;
        return true;
    }

    // When no config exists yet, always create one with a default timeout.
    // If the helper exe sits inside the game folder (next to Mudrunner.exe),
    // auto-populate the real path and return true; otherwise write a placeholder
    // path for the user to edit and return false.
    private static bool CreateConfig(string baseDir, string configPath)
    {
        var gameExeAlongside = File.Exists(Path.Combine(baseDir, MudrunnerExe));

        var gamePath = gameExeAlongside
            ? baseDir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            : PlaceholderGamePath;

        WriteConfig(configPath, gamePath, DefaultTimeoutSeconds);

        if (gameExeAlongside)
        {
            Console.WriteLine($"Detected game folder. Created config with path: {gamePath}");
        }

        return gameExeAlongside;
    }

    private static void WriteConfig(string configPath, string gamePath, int timeoutSeconds)
    {
        try
        {
            File.WriteAllLines(configPath, [gamePath, timeoutSeconds.ToString()]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Warning: couldn't update config file: {ex.Message}");
        }
    }
}
