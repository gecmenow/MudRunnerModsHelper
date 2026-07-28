namespace MudRunnerModsHelper;

internal static class Settings
{
    private const string ConfigPath = "Config.txt";
    public static string GamePath { get; private set; } = string.Empty;
    public static int TimeoutMs { get; private set; }

    public static bool Load(string configPath = ConfigPath)
    {
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Config file not found: {configPath}");
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

        if (!int.TryParse(lines[1].Trim(), out var seconds))
        {
            Console.WriteLine("Couldn't parse timeout.");
            return false;
        }

        TimeoutMs = seconds * 1000;
        return true;
    }
}
