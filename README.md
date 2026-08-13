If you are already familiar with this helper, just download the archive you needed.

[x64.zip](https://github.com/user-attachments/files/18706401/x64.zip)

[x86.zip](https://github.com/user-attachments/files/18706402/x86.zip)

So, what does it do?

Imagine that you have files structure like this in the `Media` folder.

![image](https://github.com/user-attachments/assets/6abbc06e-e115-4c5a-94d7-7c715254df45)


And if you want to play multiplayer with your mates you need all time to move them from this folder before running the game and then return your files back. It's pretty annoying. This tool does it for you. But your input is required in the begining.

Download archive, based on your file system x64 or x86 Winodws. Extract it, where it is convinient to you. Both arhive files contain `Config.txt`. First row is a path that should lead to the game folder only (there is already an example path). Second row is a timeout, if your PC runs game pretty slow and it takes more than 10 seconds - replace the timeout to yours.

**Tip: you can just drop the `.exe` right into the game's folder (next to `Mudrunner.exe`).** If there's no `Config.txt` next to the exe on startup, it is created automatically with a default 10 second timeout. When the exe sits in the game folder, the path is auto-populated too, so it's ready to run. When it sits elsewhere, a placeholder path is written and the tool asks you to set the real game folder path on the first line, then run again. If a `Config.txt` is already present, it is used as-is.

The timeout (second row of `Config.txt`, in seconds) is the window you have after the game launches to create your lobby before the mods are restored. It starts counting the moment the game is launched, so make sure it covers the game's full load time **plus** the time you need to create the lobby.

When you are done and run the `.exe` file it will do the magic for you. It moves the folders out of `Media` into a `temp` folder located **next to** `Media` (not inside it, so the game doesn't notice a change in `Media`), launches the game, then moves them back. And voila, you can play it with your mates without manual file manupulation.

If you see this window - press `More info`.

![image](https://github.com/user-attachments/assets/8fcaba84-d333-4484-9f61-7eba3f9de9e3)

Then press `Run anyway`.

![image](https://github.com/user-attachments/assets/61bafe2f-4adf-4114-a206-d61bcc41c730)

it'll be just one-time prompt, because this .exe is not sertified as you might understand.
