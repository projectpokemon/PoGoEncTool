namespace PoGoEncTool.Core;

public enum PogoType : byte
{
    None, // Don't use this.

    /// <summary> Pokémon captured in the wild. </summary>
    Wild,

    /// <summary> Pokémon hatched from 2km, 5km, 7km, or 10km Eggs. </summary>
    Egg,
    /// <summary> Pokémon hatched from Strange Eggs received from the Leaders of Team GO Rocket. </summary>
    EggS,

    /// <summary> Pokémon captured after completing Raid Battles. </summary>
    Raid = 10,
    /// <summary> Mythical Pokémon captured after completing Raid Battles. </summary>
    RaidM,
    /// <summary> Ultra Beasts captured after completing Raid Battles. Only Beast Balls can be used. </summary>
    RaidUB,
    /// <summary> Shadow Pokémon captured after completing Shadow Raid Battles. Must be Purified before transferring to Pokémon HOME. </summary>
    /// <remarks> Pokémon with this <see cref="PogoType"/> can not be moved to <see cref="PKHeX.Core.GameVersion.GG"/>. </remarks>
    RaidS,

    /// <summary> Pokémon captured after completing Field Research. </summary>
    Research = 20,
    /// <summary> Mythical Pokémon captured after completing Field Research. </summary>
    ResearchM,
    /// <summary> Mythical Pokémon captured after completing Field Research. Only Poké Balls can be used. </summary>
    ResearchMP,
    /// <summary> Ultra Beasts captured after completing Field Research. Only Beast Balls can be used. </summary>
    ResearchUB,

    /// <summary> Mythical Pokémon captured after completing Field Research. No HUD is visible during these encounters. </summary>
    /// <remarks>
    /// Under normal circumstances, only Poké Balls can be used, but Great Balls and Ultra Balls can be used with the Remember Last-Used Poké Ball setting.
    /// This was rendered unusable as of version 0.277.3.
    /// </remarks>
    ResearchMH,

    /// <summary> Pokémon captured after completing Field Research. No HUD is visible during these encounters. </summary>
    /// <remarks>
    /// The encounter defaults to the player's stock of Poké Balls. If they have none, it falls back to Great Balls, and then to Ultra Balls.
    /// If the player has no Poké Balls, Great Balls, or Ultra Balls, the HUD fails to load in any Poké Ball at all, even if they have a Master Ball.
    /// </remarks>
    ResearchNH,

    /// <summary> Pokémon captured after completing Field Research. </summary>
    /// <remarks> Unlike standard Field Research encounters, these are lowered to Level 10. </remarks>
    Research10,

    /// <summary> Pokémon captured after completing Field Research. </summary>
    /// <remarks> Unlike standard Field Research encounters, these are boosted to Level 20. </remarks>
    Research20,

    /// <summary> Pokémon captured after completing Field Research. Only Beast Balls can be used. </summary>
    /// <remarks> Unlike standard Field Research encounters, these are boosted to Level 20. </remarks>
    ResearchUB20,

    /// <summary> Pokémon captured from the GO Battle League. </summary>
    GBL = 40,
    /// <summary> Mythical Pokémon captured from the GO Battle League. </summary>
    GBLM,
    /// <summary> Pokémon captured from the GO Battle League during GO Battle Day events. Excludes Legendary Pokémon, Mythical Pokémon, and Ultra Beasts. </summary>
    GBLD,

    /// <summary> Pokémon captured after defeating members of Team GO Rocket. Must be Purified before transferring to Pokémon HOME. </summary>
    /// <remarks> Pokémon with this <see cref="PogoType"/> can not be moved to <see cref="PKHeX.Core.GameVersion.GG"/>. </remarks>
    Shadow = 50,

    /// <summary> Pokémon captured from Special Research or Timed Research with a Premier Ball. </summary>
    /// <remarks>
    /// Niantic released version 0.269.0 on April 22, 2023, which contained an issue with the Remember Last-Used Poké Ball setting.
    /// This allowed for Premier Balls obtained from Raid Battles to be remembered on all future encounters.
    /// The moment the Premier Ball touched the floor or a wild Pokémon, the encounter would end, except if it was from a Special Research, Timed Research, or Collection Challenge encounter.
    /// This made it possible for over 300 species of Pokémon to be obtainable in a Poké Ball they were never meant to be captured in.
    /// This bug was fixed with the release of version 0.269.2.
    /// </remarks>
    Research269 = 200,
    Research269M,
}

public static class PogoTypeExtensions
{
    public static bool IsShadow(this PogoType t) => t is PogoType.Shadow or PogoType.RaidS;
}
