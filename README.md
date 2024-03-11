# PoGoEncTool (PGET)
Tracks Pokémon GO Legality data as json, and can output as a PKHeX Legality Binary (pkl), sometimes referred to as a pickle.

PGET is a Windows only project (WinForms).

Updating the json:
1. Build the program (requires .NET 8.0, yay!).
2. Drag the json from the Resources folder onto the built executable.
3. The executable will launch, editing the json resource.
4. Future launches of the program will automatically edit the json resource, assuming you don't change paths.
5. Commit changes / submit pull request.

Periodically, the generated pkl will be merged into PKHeX for legality data.