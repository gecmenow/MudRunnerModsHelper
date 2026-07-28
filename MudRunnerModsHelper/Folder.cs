namespace MudRunnerModsHelper;

internal static class Folder
{
    private static readonly List<(string SourcePath, string DestinationPath)> MovedFolders = [];
    private static string _tempPath = string.Empty;
    private static string[] _subdirectories = [];

    private const string TempFolder = "temp";

    public static bool CheckFolderExists()
    {
        var mediaPath = Path.Combine(Settings.GamePath, "Media");
        _tempPath = Path.Combine(mediaPath, TempFolder);

        if (!Directory.Exists(mediaPath))
        {
            Console.WriteLine("Media folder is missing.");
            return false;
        }

        RecoverFromPreviousCrash(mediaPath);

        _subdirectories = Directory.GetDirectories(mediaPath)
            .Where(d => !string.Equals(Path.GetFileName(d), TempFolder, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (_subdirectories.Length == 0)
        {
            Console.WriteLine("Nothing to move. Exiting.");
            return false;
        }

        return true;
    }

    private static void RecoverFromPreviousCrash(string mediaPath)
    {
        if (!Directory.Exists(_tempPath))
        {
            return;
        }

        var leftoverDirs = Directory.GetDirectories(_tempPath);
        if (leftoverDirs.Length == 0)
        {
            Directory.Delete(_tempPath);
            {
                return;
            }
        }

        Console.WriteLine("Detected leftover temp folder from a previous crash. Recovering...");

        foreach (var dir in leftoverDirs)
        {
            var folderName = Path.GetFileName(dir);
            var destinationPath = Path.Combine(mediaPath, folderName);
            Directory.Move(dir, destinationPath);
            Console.WriteLine($"Recovered {folderName}.");
        }

        Directory.Delete(_tempPath);
        Console.WriteLine("Recovery complete.");
    }

    public static bool MoveFolders()
    {
        Directory.CreateDirectory(_tempPath);

        foreach (var subdir in _subdirectories)
        {
            var folderName = Path.GetFileName(subdir);
            var destinationPath = Path.Combine(_tempPath, folderName);

            try
            {
                Directory.Move(subdir, destinationPath);
                MovedFolders.Add((subdir, destinationPath));
                Console.WriteLine($"Moved {folderName} to temp.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to move {folderName}: {ex.Message}");
                Console.WriteLine("Rolling back...");
                RollbackAction();
                return false;
            }
        }

        return true;
    }

    public static void RollbackAction()
    {
        foreach (var (sourcePath, destinationPath) in MovedFolders)
        {
            if (!Directory.Exists(destinationPath))
            {
                continue;
            }

            Directory.Move(destinationPath, sourcePath);
            Console.WriteLine($"Rolled back {Path.GetFileName(sourcePath)} to original location.");
        }

        MovedFolders.Clear();

        if (Directory.Exists(_tempPath))
            Directory.Delete(_tempPath, recursive: Directory.GetFileSystemEntries(_tempPath).Length > 0);

        Console.WriteLine("Rollback completed.");
    }
}
