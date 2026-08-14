using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MudRunnerModsHelper;

internal static class SpaceKeyWaiter
{
    private const int VkSpace = 0x20;

    public static void WaitForSpace(Process game)
    {
        var wasDown = false;

        while (!game.HasExited)
        {
            var isDown = (GetAsyncKeyState(VkSpace) & 0x8000) != 0;
            if (isDown && !wasDown)
            {
                break;
            }

            wasDown = isDown;
            Thread.Sleep(50);
        }
    }

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);
}
