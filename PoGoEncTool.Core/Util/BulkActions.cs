using PKHeX.Core;
using System.Collections.Generic;
using System.Linq;
using static PKHeX.Core.Species;
using static PoGoEncTool.Core.PogoShiny;
using static PoGoEncTool.Core.PogoType;

// ReSharper disable RedundantEmptyObjectOrCollectionInitializer
// ReSharper disable CollectionNeverUpdated.Local

namespace PoGoEncTool.Core;

#if DEBUG
public static class BulkActions
{
    public static BossType Type { get; set; } = BossType.Raid;
    public static string Season { get; set; } = "Precious Paths";
    public static PogoDate SeasonEnd { get; set; } = new(2026, 03, 03);

    public static void AddBossEncounters(PogoEncounterList list)
    {
        var bosses = new List<(ushort Species, byte Form, PogoShiny Shiny, byte Tier)>
        {
            new((int)Bulbasaur, 0, Random, 1),
        };

        foreach (var enc in bosses)
        {
            var pkm = list.GetDetails(enc.Species, enc.Form);
            var boss = Type switch
            {
                BossType.Shadow => "Shadow Raid Boss",
                BossType.Dynamax or BossType.Gigantamax => "Power Spot Boss",
                _ => "Raid Boss"
            };

            var type = Type switch
            {
                BossType.Shadow => RaidShadow,
                BossType.Dynamax => MaxBattle,
                BossType.Gigantamax => MaxBattleGigantamax,
                _ => Raid,
            };

            var tier = Type switch
            {
                BossType.Dynamax => GetPowerSpotTier(enc.Species),
                BossType.Gigantamax => (byte)6,
                _ => enc.Tier,

            };

            var stars = GetRaidBossTier(tier);
            var eventName = "";
            var descriptor = eventName is "" ? "" : $" ({eventName})";
            var comment = $"{stars}-Star {boss}{descriptor}";

            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                End   = new PogoDate(),
                Type = type,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = comment,
                Shiny = enc.Shiny,
            };

            // set species as available if this encounter is its debut
            if (!pkm.Available)
                pkm.Available = true;

            // set its evolutions as available as well
            var evos = EvoUtil.GetEvoSpecForms(enc.Species, enc.Form)
                .Where(z => EvoUtil.IsAllowedEvolution(enc.Species, enc.Form, z.Species, z.Form)).ToArray();

            foreach ((ushort s, byte f) in evos)
            {
                var parent = list.GetDetails(s, f);
                if (!parent.Available)
                    parent.Available = true;
            }

            pkm.Add(entry); // add the entry!
        }
    }

    private static string GetRaidBossTier(byte tier) => tier switch
    {
        1 => "One",
        2 => "Two",
        3 => "Three",
        4 => "Four",
        5 => "Five",
        6 => "Six",
        _ => string.Empty,
    };

    public static void AddMonthlyRaidBosses(PogoEncounterList list)
    {
        var bosses = new List<(ushort Species, byte Form, PogoShiny Shiny, PogoDate Start, PogoDate End, bool IsMega, byte MegaForm)>
        {
            // Five-Star
            new((int)Bulbasaur, 0, Random, new PogoDate(), new PogoDate(), false, 0),

            // Mega
            new((int)Bulbasaur, 0, Random, new PogoDate(), new PogoDate(), true, 0),
        };

        foreach (var enc in bosses)
        {
            var pkm = list.GetDetails(enc.Species, enc.Form);
            var comment = "Five-Star Raid Boss";
            if (enc.IsMega)
            {
                comment = enc.MegaForm switch
                {
                    0 when enc.Species is (int)Charizard or (int)Raichu or (int)Mewtwo => $"Mega Raid Boss (Mega {(Species)enc.Species} X)",
                    1 when enc.Species is (int)Charizard or (int)Raichu or (int)Mewtwo => $"Mega Raid Boss (Mega {(Species)enc.Species} Y)",
                    _ => "Mega Raid Boss",
                };
            }

            var type = enc.Species switch
            {
                (int)Meltan or (int)Melmetal => Raid, // unlike other Mythical Pokémon, Meltan and Melmetal can be traded
                _ when SpeciesCategory.IsMythical(enc.Species) => RaidMythical,
                _ => Raid,
            };

            var entry = new PogoEntry
            {
                Start = enc.Start,
                End = enc.End,
                Type = type,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = comment,
                Shiny = enc.Shiny,
            };

            // set species as available if this encounter is its debut
            if (!pkm.Available)
                pkm.Available = true;

            pkm.Add(entry); // add the raid entry!

            // add an accompanying GBL encounter if it has not appeared in research before, or continues to appear in the wild
            if ((!enc.IsMega) && !pkm.Data.Any(z => IsLessRestrictiveEncounter(z.Type) && z.Shiny == enc.Shiny && z.End == null))
            {
                // some Legendary and Mythical Pokémon are exempt because one of their forms or pre-evolutions have been in research, and they revert or can be changed upon transfer to HOME
                if (enc.Species is (int)Giratina or (int)Genesect or (int)Cosmoem or (int)Solgaleo or (int)Lunala)
                    continue;
                AddEncounterGBL(list, enc.Species, enc.Form, enc.Shiny, enc.Start);
            }
        }

        static bool IsLessRestrictiveEncounter(PogoType type) => type is Wild or ResearchBreakthrough or SpecialResearch or TimedResearch or CollectionChallenge or
                                                                                 SpecialMythical or SpecialLevel10 or SpecialLevel20 or SpecialLevelRange or SpecialMythicalLevel10 or SpecialMythicalLevel20 or SpecialMythicalLevelRange or
                                                                                 TimedMythical or TimedLevel10 or TimedLevel20 or TimedLevelRange or TimedMythicalLevel10 or TimedMythicalLevel20 or TimedMythicalLevelRange;
    }

    private static void AddEncounterGBL(PogoEncounterList list, ushort species, byte form, PogoShiny shiny, PogoDate start)
    {
        var pkm = list.GetDetails(species, form);
        var type = SpeciesCategory.IsMythical(species) ? GBLMythical : GBL;
        var entry = new PogoEntry
        {
            Start = start,
            End = SeasonEnd,
            Type = type,
            LocalizedStart = true,
            NoEndTolerance = false,
            Comment = $"Reward Encounter (Pokémon GO: {Season})",
            Shiny = shiny,
        };

        pkm.Add(entry); // add the GBL entry!
    }

    public static void AddNewShadows(PogoEncounterList list)
    {
        var removed = new List<(ushort Species, byte Form)>
        {
            new((int)Bulbasaur, 0),
        };

        var added = new List<(ushort Species, byte Form, PogoShiny Shiny)>
        {
            new((int)Bulbasaur, 0, Never),
        };

        // add end dates for Shadows that have been removed
        foreach ((ushort s, byte f) in removed)
        {
            var pkm = list.GetDetails(s, f);
            var entries = pkm.Data;

            foreach (var entry in entries)
            {
                if (entry is { Type: Shadow, End: null })
                    entry.End = new PogoDate();
            }
        }

        // add new Shadows
        foreach ((ushort s, byte f, PogoShiny shiny) in added)
        {
            var pkm = list.GetDetails(s, f);
            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                Shiny = shiny,
                Type = Shadow,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = "Team GO Rocket Grunt",
            };

            pkm.Add(entry);
        }
    }

    private static byte GetPowerSpotTier(ushort species) => (Species)species switch
    {
        Bulbasaur => 1,
        Charmander => 1,
        Squirtle => 1,
        Caterpie => 1,
        Growlithe => 1, // verify
        Abra => 1,
        Machop => 2,
        Gastly => 1,
        Krabby => 1,
        Hitmonlee => 3,
        Hitmonchan => 3,
        Chansey => 3,
        Eevee => 2,
        Omanyte => 1,
        Kabuto => 1,
        Shuckle => 2,
        Ralts => 1,
        Sableye => 3,
        Wailmer => 2,
        Spheal => 1,
        Beldum => 3,
        Pidove => 1,
        Roggenrola => 1,
        Woobat => 1,
        Drilbur => 1,
        Darumaka => 2,
        Trubbish => 1,
        Cryogonal => 3,
        Inkay => 1,
        Bounsweet => 1,
        Passimian => 3,
        Drampa => 3,
        Grookey => 1,
        Scorbunny => 1,
        Sobble => 1,
        Skwovet => 1,
        Rookidee => 1,
        Wooloo => 1,
        Toxtricity => 4,
        Hatenna => 1,
        Falinks => 3,
        Duraludon => 4,
        _ when SpeciesCategory.IsSpecialPokemon(species) => 5,
        _ => throw new System.Exception("Species has not been released as a Dynamax Pokémon yet."),
    };

    public enum BossType : byte
    {
        Raid = 0,
        Shadow = 1,
        Dynamax = 2,
        Gigantamax = 3,
    }
}
#endif
