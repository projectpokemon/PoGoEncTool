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
    FieldResearchLevelRange,
    ResearchBreakthrough,
    SpecialResearch,
    TimedResearch,
    CollectionChallenge,
    VivillonCollector,
    PartyPlay,
    StampRally,
    GOPass,
    ReferralBonus,

    // Pokémon captured after completing Special Research. IV, Level, and Poké Ball permissions may vary depending on the Pokémon.
    SpecialMythical = 40,
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

    // Pokémon captured after completing Timed Research or GO Passes. IV, Level, and Poké Ball permissions may vary depending on the Pokémon.
    TimedMythical = 60,
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
    TimedShadow,
    TimedShadowLevel10,
    TimedShadowLevel20,
    TimedShadowLevelRange,
    TimedShadowMythical,
    TimedShadowMythicalLevel10,
    TimedShadowMythicalLevel20,
    TimedShadowMythicalLevelRange,

    // Pokémon captured after winning Trainer Battles in the GO Battle League.
    GBL = 90,
    GBLMythical,
    GBLEvent,

    // Shadow Pokémon captured after defeating members of Team GO Rocket.
    Shadow = 100,
    ShadowMythical,
    ShadowUltraBeast,

    // Pokémon captured after completing Max Battles.
    MaxBattle = 110,
    MaxBattleMythical,
    MaxBattleUltraBeast,
    MaxBattleGigantamax,
    MaxBattleGOWA,
    MaxBattleMythicalGOWA,
    MaxBattleUltraBeastGOWA,
    MaxBattleGigantamaxGOWA,

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
    extension(PogoType t)
    {
        public bool IsShadow => t is RaidShadow or RaidShadowMythical or RaidShadowGOWA or RaidShadowMythicalGOWA or Shadow or ShadowMythical or ShadowUltraBeast;
        public bool IsGigantamax => t is SpecialGigantamax or SpecialGigantamaxLevel10 or SpecialGigantamaxLevel20 or SpecialGigantamaxLevelRange or TimedGigantamax or TimedGigantamaxLevel10 or TimedGigantamaxLevel20 or TimedGigantamaxLevelRange or MaxBattleGigantamax;
    }
}
