using MudRunnerModsHelper;

Folder.CheckFolderToExists();

Settings.SetMovingFoldersBackTimeout();

Folder.MoveFilders();

Game.Run();

Folder.RollbackAction();