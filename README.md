# PoGoEncTool (PGET)
Tracks Pokémon GO Legality data as json, and can output as a PKHeX Legality Binary (pkl), sometimes referred to as a pickle.

## Building
PGET is a Windows Forms application which requires [.NET 10.0](https://dotnet.microsoft.com/download/dotnet/9.0).

## Updating the JSON
1. Build the program.
2. Drag the json from the Resources folder onto the built executable.
3. The executable will launch, editing the json resource.
4. Future launches of the program will automatically edit the json resource, assuming you don't change paths.
5. Commit changes / submit pull request.

Periodically, the generated pkl will be merged into PKHeX for legality data.

## Screenshots
![Main Window](https://i.imgur.com/gWJVQDy.png)
