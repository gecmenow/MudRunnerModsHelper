namespace MudRunnerModsHelper;

internal static class Settings
{
    public static string GamePath { get; } =
        AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
}
