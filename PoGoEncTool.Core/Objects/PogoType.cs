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

    /// <summary> Pokémon captured after completing Field Research. </summary>
    Research = 20,
    /// <summary> Mythical Pokémon captured after completing Field Research. </summary>
    ResearchM,
    /// <summary> Mythical Pokémon captured after completing Field Research. Only Poké Balls can be used. </summary>
    ResearchP,
    /// <summary> Ultra Beasts captured after completing Field Research. Only Beast Balls can be used. </summary>
    ResearchUB,

    /// <summary> Pokémon captured from the GO Battle League. </summary>
    GBL = 30,
    /// <summary> Mythical Pokémon captured from the GO Battle League. </summary>
    GBLM,
    /// <summary> Pokémon captured from the GO Battle League during GO Battle Day, excluding Legendary and Mythical Pokémon. </summary>
    GBLD,

    /// <summary> Pokémon captured after defeating members of Team GO Rocket. Must become Purified before transferring to Pokémon HOME. </summary>
    /// <remarks> Pokémon with this <see cref="PogoType"/> can not be moved to <see cref="PKHeX.Core.GameVersion.GG"/>. </remarks>
    Shadow = 40,
}

public static class PogoTypeExtensions
{
    public static bool IsShadow(this PogoType t) => t == PogoType.Shadow;
}
