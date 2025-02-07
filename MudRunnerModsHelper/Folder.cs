namespace MudRunnerModsHelper;

internal static class Folder
{
    private static List<(string SourcePath, string DestinationPath)> _movedFolders = [];
    private static string TempPath = string.Empty;
    private static string[] _subdirectories = [];

    private const string tempFolder = "temp";

    public static void CheckFolderToExists()
    {
        var mediaPath = File.ReadAllLines(Settings.Config).First() + @"\Media";
        TempPath = Path.Combine(mediaPath, tempFolder);

        if (!Directory.Exists(mediaPath))
        {
            Console.WriteLine("Media folder is missing.");
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
        Directory.CreateDirectory(TempPath);

        // Move subdirectories into the "temp" folder
        foreach (var subdir in _subdirectories)
        {
            var folderName = Path.GetFileName(subdir);

            var destinationPath = Path.Combine(TempPath, folderName);

            // Move the folder and keep track of the move
            Directory.Move(subdir, destinationPath);
            _movedFolders.Add((subdir, destinationPath));
            Console.WriteLine($"Moved {folderName} to temp.");
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

        Directory.Delete(TempPath);

        Console.WriteLine("Rollback completed.");
    }
}