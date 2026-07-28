using MudRunnerModsHelper;

if (!Settings.Load())
{
    return;
}

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    Folder.RollbackAction();
    Environment.Exit(0);
};

if (!Game.ValidateExecutable())
{
    return;
}

if (!Folder.CheckFolderExists())
{
    return;
}

if (!Folder.MoveFolders())
{
    return;
}

Game.Run();

Folder.RollbackAction();
