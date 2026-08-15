using MudRunnerModsHelper;

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    Folder.RollbackAction();
    Environment.Exit(0);
};

if (!Game.ValidateExecutable()
    || !GameConfig.EnsureMediaPath()
    || !Folder.VerifyFolderExists()
    || !Folder.MoveFolders())
{
    return;
}

var game = Game.Launch();
SpaceKeyWaiter.WaitForSpace(game);
Thread.Sleep(1000);
Folder.RollbackAction();
