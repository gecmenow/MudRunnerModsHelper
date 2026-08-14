If you are already familiar with this helper, just download the archive you needed.

[x64.zip](https://github.com/user-attachments/files/31085957/x64.zip)

[x86.zip](https://github.com/user-attachments/files/31085991/x86.zip)

So, what does it do?

Imagine that you have files structure like this in the `Media` folder.

![image](https://github.com/user-attachments/assets/6abbc06e-e115-4c5a-94d7-7c715254df45)


And if you want to play multiplayer with your mates you need all time to move them from this folder before running the game and then return your files back. It's pretty annoying. This tool does it for you.

Download archive, based on your file system x64 or x86 Windows. **Place the helper `.exe` in the same folder as `Mudrunner.exe`.** The game folder must also contain `Config.xml` — if it is missing, the helper aborts with an error.

When you run the helper it will:

1. Ensure `Config.xml` contains `<MediaPath Path="Media" />` (adds it before `Media.zip` if missing).
2. Move mod folders out of `Media` into a `temp` folder **next to** `Media` (not inside it, so the game doesn't notice a change in `Media`).
3. Launch the game.
4. Wait for you to press **Space** at the game's start prompt (the one that says to press Space to continue). Mods are restored about one second after Space is detected.
5. If you don't press Space, mods are restored automatically after **30 seconds** as a fallback.

And voila, you can play it with your mates without manual file manipulation.

If you see this window - press `More info`.

![image](https://github.com/user-attachments/assets/8fcaba84-d333-4484-9f61-7eba3f9de9e3)

Then press `Run anyway`.

![image](https://github.com/user-attachments/assets/61bafe2f-4adf-4114-a206-d61bcc41c730)

it'll be just one-time prompt, because this .exe is not sertified as you might understand.

Data for this helper has been taken from there https://www.reddit.com/r/Spintires/comments/7z69kl/how_to_play_modded_maps_in_online_multiplayer_in/
