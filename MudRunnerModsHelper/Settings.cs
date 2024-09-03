namespace MudRunnerModsHelper;

internal static class Settings
{
    public static string Config = "Config.txt";
    public static int Timeout;

    public static void SetMovingFoldersBackTimeout()
    {
        var time = File.ReadAllLines(Config).Last();

        if (!int.TryParse(time, out Timeout))
        {
            Console.WriteLine("Couldn't parse timeout");

            Environment.Exit(0);
        }

        Timeout *= 1000;
    }
}
