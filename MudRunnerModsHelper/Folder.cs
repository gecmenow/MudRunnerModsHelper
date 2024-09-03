namespace MudRunnerModsHelper;

internal static class Folder
{
    private static List<(string SourcePath, string DestinationPath)> _movedFolders = [];
    private static string _levelsPath = string.Empty;
    private static string[] _subdirectories = [];

    private const string levelsFolder = "levels";

    public static void CheckFolderToExists()
    {
        var mediaPath = File.ReadAllLines(Settings.Config).First() + @"\Media";
        _levelsPath = Path.Combine(mediaPath, levelsFolder);

        if (!Directory.Exists(mediaPath) || !Directory.Exists(_levelsPath))
        {
            Console.WriteLine("Media and/or levels folder is missing.");
            return;
        }

        // Get all subdirectories in the Media folder
        _subdirectories = Directory.GetDirectories(mediaPath);

        if (_subdirectories.Length is 0)
        {
            Console.WriteLine("Nothing to move. Exiting.");
            return;
        }
    }

    public static void MoveFilders()
    {
        // Move subdirectories except "levels" into the "levels" folder
        foreach (var subdir in _subdirectories)
        {
            var folderName = Path.GetFileName(subdir);

            if (folderName is levelsFolder)
            {
                continue;
            }

            var destinationPath = Path.Combine(_levelsPath, folderName);

            // Move the folder and keep track of the move
            Directory.Move(subdir, destinationPath);
            _movedFolders.Add((subdir, destinationPath));
            Console.WriteLine($"Moved {folderName} to levels.");
        }
    }

    public static void RollbackAction()
    {
        var movedFolders = _movedFolders;

        foreach (var (sourcePath, destinationPath) in movedFolders)
        {
            // Move folders back to their original locations
            Directory.Move(destinationPath, sourcePath);
            Console.WriteLine($"Rolled back {Path.GetFileName(sourcePath)} to original location.");
        }

        movedFolders.Clear();
        Console.WriteLine("Rollback completed.");
    }
}
