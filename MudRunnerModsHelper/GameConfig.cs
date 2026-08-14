using System.Xml.Linq;

namespace MudRunnerModsHelper;

internal static class GameConfig
{
    private const string ConfigFileName = "Config.xml";
    private const string MediaPathElement = "MediaPath";
    private const string MediaFolderPath = "Media";
    private const string MediaZipPath = "Media.zip";

    public static bool EnsureMediaPath()
    {
        var configPath = Path.Combine(Settings.GamePath, ConfigFileName);

        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Config.xml not found: {configPath}");
            return false;
        }

        XDocument doc;
        try
        {
            doc = XDocument.Load(configPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read Config.xml: {ex.Message}");
            return false;
        }

        var root = doc.Root;
        if (root is null)
        {
            Console.WriteLine("Config.xml has no root element.");
            return false;
        }

        var mediaPaths = root.Elements(MediaPathElement).ToList();
        var hasMediaFolder = mediaPaths.Any(element =>
            string.Equals((string?)element.Attribute("Path"), MediaFolderPath, StringComparison.OrdinalIgnoreCase));

        if (hasMediaFolder)
        {
            return true;
        }

        var newElement = new XElement(MediaPathElement, new XAttribute("Path", MediaFolderPath));
        var mediaZipElement = mediaPaths.FirstOrDefault(element =>
            string.Equals((string?)element.Attribute("Path"), MediaZipPath, StringComparison.OrdinalIgnoreCase));

        if (mediaZipElement is not null)
        {
            mediaZipElement.AddBeforeSelf(newElement);
        }
        else
        {
            Console.WriteLine("Media zip path not found in the config, check the game sum.");
            
            return false;
        }

        try
        {
            doc.Save(configPath);
            Console.WriteLine($"Added <MediaPath Path=\"{MediaFolderPath}\" /> to Config.xml.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write Config.xml: {ex.Message}");
            
            return false;
        }

        return true;
    }
}
