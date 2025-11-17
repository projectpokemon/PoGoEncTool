using static PoGoEncTool.Core.PogoType;

namespace PoGoEncTool.Core;

public enum PogoType : byte
{
    None, // Don't use this.

    // Pokémon captured in the wild.
    Wild,

    // Pokémon hatched from Eggs.
    Egg,
    Egg12km,

    // Pokémon captured after completing Raid Battles. IV, Level, and Poké Ball permissions may vary depending on the Pokémon.
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

    // Pokémon captured after completing various types of Field Research.
    FieldResearch = 20,
    FieldResearchRange,
    ResearchBreakthrough,
    SpecialResearch,
    TimedResearch,
    CollectionChallenge,
    VivillonCollector,
    PartyPlay,
    StampRally,
    GOPass,
    ReferralBonus,

    // Pokémon captured after completing Special Research. IV, Level, and Poké Ball permissions may vary depending on how the Special Research was distributed.
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

    // Pokémon captured after completing Timed Research. IV, Level, and Poké Ball permissions may vary depending on how the Timed Research was distributed.
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

    // Pokémon captured after winning Trainer Battles in the GO Battle League.
    GBL = 70,
    GBLMythical,
    GBLEvent,

    // Shadow Pokémon captured after defeating members of Team GO Rocket.
    Shadow = 80,
    ShadowMythical,
    ShadowUltraBeast,

    // Pokémon captured after completing Max Battles.
    MaxBattle = 90,
    MaxBattleMythical,
    MaxBattleUltraBeast,
    MaxBattleGigantamax,
    MaxBattleGOWA,
    MaxBattleMythicalGOWA,
    MaxBattleUltraBeastGOWA,
    MaxBattleGigantamaxGOWA,

    // Pokémon captured through GO Pass. IV, Level, and Poké Ball permissions may vary depending on how the GO Pass was distributed.
    GOPassMythical = 100,
    GOPassMythicalPoke,
    GOPassUltraBeast,
    GOPassGigantamax,
    GOPassPoke,
    GOPassLevel10,
    GOPassLevel20,
    GOPassLevelRange,
    GOPassMythicalLevel10,
    GOPassMythicalLevel20,
    GOPassMythicalLevelRange,
    GOPassUltraBeastLevel10,
    GOPassUltraBeastLevel20,
    GOPassUltraBeastLevelRange,
    GOPassGigantamaxLevel10,
    GOPassGigantamaxLevel20,
    GOPassGigantamaxLevelRange,
    GOPassShadowLevel10,
    GOPassShadowLevel20,
    GOPassShadowLevelRange,
    GOPassShadowMythicalLevel10,
    GOPassShadowMythicalLevel20,
    GOPassShadowMythicalLevelRange,

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
    public static bool IsShadow(this PogoType t) => t is RaidShadow or RaidShadowMythical or RaidShadowGOWA or RaidShadowMythicalGOWA or Shadow or ShadowMythical or ShadowUltraBeast;
    public static bool IsGigantamax(this PogoType t) => t is SpecialGigantamax or SpecialGigantamaxLevel10 or SpecialGigantamaxLevel20 or SpecialGigantamaxLevelRange or TimedGigantamax or TimedGigantamaxLevel10 or TimedGigantamaxLevel20 or TimedGigantamaxLevelRange or GOPassGigantamax or GOPassGigantamaxLevel10 or GOPassGigantamaxLevel20 or GOPassGigantamaxLevelRange or MaxBattleGigantamax;
}
