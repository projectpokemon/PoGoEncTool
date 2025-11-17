using static PoGoEncTool.Core.PogoType;

namespace PoGoEncTool.Core;

public enum PogoType : byte
{
    None, // Don't use this.

    /// <summary>
    /// Pokémon captured in the wild.
    /// </summary>
    Wild,

    /// <summary>
    /// Pokémon hatched from Eggs.
    /// </summary>
    Egg,
    Egg12km,

    /// <summary>
    /// Pokémon captured after completing Raid Battles. IV, Level, and Poké Ball permissions may vary depending on the Pokémon.
    /// </summary>
    Raid = 10,
    RaidMythical,
    RaidUltraBeast,
    RaidShadow,
    RaidShadowMythical,
    RaidGOWA,
    RaidMythicalGOWA,
    RaidUltraBeastGOWA,
    RaidShadowGOWA,
    RaidShadowMythicalGOWA,

    /// <summary>
    /// Pokémon captured after completing various types of Field Research.
    /// </summary>
    FieldResearch = 20,
    FieldResearchRange,
    ResearchBreakthrough,
    SpecialResearch,
    TimedResearch,
    CollectionChallenge,
    VivillonCollector,
    PartyPlay,
    StampRally,
    EventPass,
    ReferralBonus,

    /// <summary>
    /// Pokémon captured after completing Special Research. IV, Level, and Poké Ball permissions may vary depending on how the Special Research was distributed.
    /// </summary>
    SpecialMythical = 31,
    SpecialMythicalPoke,
    SpecialUltraBeast,
    SpecialGigantamax,
    SpecialPoke,
    SpecialLastBall,
    SpecialNoHUD,
    SpecialLevel10,
    SpecialLevel20,
    SpecialLevelRange,
    SpecialMythicalLevel10,
    SpecialMythicalLevel20,
    SpecialMythicalLevelRange,
    SpecialUltraBeastLevel10,
    SpecialUltraBeastLevel20,
    SpecialUltraBeastLevelRange,
    SpecialGigantamaxLevel10,
    SpecialGigantamaxLevel20,
    SpecialGigantamaxLevelRange,

    /// <summary>
    /// Pokémon captured after completing Timed Research. IV, Level, and Poké Ball permissions may vary depending on how the Timed Research was distributed.
    /// </summary>
    TimedMythical = 51,
    TimedMythicalPoke,
    TimedUltraBeast,
    TimedGigantamax,
    TimedPoke,
    TimedLastBall,
    TimedNoHUD,
    TimedLevel10,
    TimedLevel20,
    TimedLevelRange,
    TimedMythicalLevel10,
    TimedMythicalLevel20,
    TimedMythicalLevelRange,
    TimedUltraBeastLevel10,
    TimedUltraBeastLevel20,
    TimedUltraBeastLevelRange,
    TimedGigantamaxLevel10,
    TimedGigantamaxLevel20,
    TimedGigantamaxLevelRange,

    /// <summary>
    /// Pokémon captured after winning Trainer Battles in the GO Battle League.
    /// </summary>
    GBL = 70,
    GBLMythical,
    GBLEvent,

    /// <summary>
    /// Shadow Pokémon captured after defeating members of Team GO Rocket.
    /// </summary>
    /// <remarks>
    /// Pokémon with these <see cref="PogoType"/> can not be moved to <see cref="PKHeX.Core.GameVersion.GG"/>.
    /// </remarks>
    Shadow = 80,
    ShadowMythical,
    ShadowUltraBeast,

    /// <summary>
    /// Pokémon captured after completing Max Battles.
    /// </summary>
    MaxBattle = 90,
    MaxBattleMythical,
    MaxBattleUltraBeast,
    MaxBattleGigantamax,

    /// <summary> Pokémon captured from Special Research or Timed Research with a Premier Ball. </summary>
    /// <remarks>
    /// Niantic released version 0.269.0 on April 22, 2023, which contained an issue with the Remember Last-Used Poké Ball setting.
    /// This allowed for Premier Balls obtained from Raid Battles to be remembered on all future encounters.
    /// The moment the Premier Ball touched the floor or a wild Pokémon, the encounter would end, except if it was from a Special Research, Timed Research, or Collection Challenge encounter.
    /// This made it possible for over 300 species of Pokémon to be obtainable in a Poké Ball they were never meant to be captured in.
    /// This bug was fixed with the release of version 0.269.2.
    /// </remarks>
    PremierBallBug = 254,
    PremierBallBugMythical,
}

public static class PogoTypeExtensions
{
    public static bool IsShadow(this PogoType t) => t is Shadow or ShadowMythical or ShadowUltraBeast or RaidShadow or RaidShadowMythical;
    public static bool IsGigantamax(this PogoType t) => t is SpecialGigantamax or SpecialGigantamaxLevel10 or SpecialGigantamaxLevel20 or SpecialGigantamaxLevelRange or TimedGigantamax or TimedGigantamaxLevel10 or TimedGigantamaxLevel20 or TimedGigantamaxLevelRange or MaxBattleGigantamax;
}
