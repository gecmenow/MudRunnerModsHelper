using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MudRunnerModsHelper;

internal static class SpaceKeyWaiter
{
    private const int VkSpace = 0x20;
    private const int FallbackSeconds = 30;

    public static void WaitForSpace(Process game)
    {
        var wasDown = false;
        var deadline = DateTime.UtcNow.AddSeconds(FallbackSeconds);

        while (!game.HasExited && DateTime.UtcNow < deadline)
        {
            var isDown = (GetAsyncKeyState(VkSpace) & 0x8000) != 0;
            if (isDown && !wasDown)
            {
                Console.WriteLine("Space detected. Restoring mods...");
                return;
            }

            wasDown = isDown;
            Thread.Sleep(50);
        }

        Console.WriteLine(game.HasExited
            ? "Game closed. Restoring mods..."
            : $"No Space within {FallbackSeconds}s. Restoring mods anyway...");
    }

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);
}
