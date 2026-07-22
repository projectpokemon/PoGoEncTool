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
    public static string Season { get; set; } = "Forever Forward";
    public static PogoDate SeasonEnd { get; set; } = new(2026, 09, 08);

    public static void AddBossEncounters(PogoEncounterList list)
    {
        var bosses = new List<(ushort Species, byte Form, PogoShiny Shiny, byte Tier)>
        {
            new((int)Bulbasaur, 0, Random, 1),
        };

        foreach (var enc in bosses)
        {
            var pk = list.GetDetails(enc.Species, enc.Form);
            var boss = Type switch
            {
                BossType.ShadowRaid => "Shadow Raid Boss",
                BossType.MaxBattleDynamax or BossType.MaxBattleGigantamax => "Power Spot Boss",
                _ => "Raid Boss"
            };

            var type = Type switch
            {
                BossType.ShadowRaid => RaidShadow,
                BossType.MaxBattleDynamax => MaxBattle,
                BossType.MaxBattleGigantamax => MaxBattleGigantamax,
                _ => Raid,
            };

            var tier = Type switch
            {
                BossType.MaxBattleDynamax => GetPowerSpotTier(enc.Species),
                BossType.MaxBattleGigantamax => (byte)6,
                _ => enc.Tier,
            };

            if (type is Raid or RaidShadow or MaxBattle && SpeciesCategory.IsMythical(enc.Species))
                type++;

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
            if (!pk.Available)
                pk.Available = true;

            // set its evolutions as available as well
            var evos = EvoUtil.GetEvoSpecForms(enc.Species, enc.Form)
                .Where(z => EvoUtil.IsAllowedEvolution(enc.Species, enc.Form, z.Species, z.Form)).ToArray();

            foreach ((ushort s, byte f) in evos)
            {
                var parent = list.GetDetails(s, f);
                if (!parent.Available)
                    parent.Available = true;
            }

            pk.Add(entry); // add the entry!
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
        var bosses = new List<(ushort Species, byte Form, PogoShiny Shiny, PogoDate Start, PogoDate End, MegaType Mega)>
        {
            // Five-Star
            new((int)Bulbasaur, 0, Random, new PogoDate(), new PogoDate(), MegaType.None),

            // Mega
            new((int)Bulbasaur, 0, Random, new PogoDate(), new PogoDate(), MegaType.Normal),
        };

        foreach (var enc in bosses)
        {
            bool mega = enc.Mega != MegaType.None;
            var pk = list.GetDetails(enc.Species, enc.Form);
            var comment = mega ? "Mega Raid Boss" : "Five-Star Raid Boss";

            // specify which Mega-Evolved form it is
            if (mega && enc.Mega != MegaType.Normal)
                comment += $" (Mega {(Species)enc.Species} {enc.Mega})";

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
            if (!pk.Available)
                pk.Available = true;

            pk.Add(entry); // add the raid entry!

            // add an accompanying GBL encounter if it has not appeared in research before, or continues to appear in the wild
            if ((!mega) && !pk.Data.Any(z => IsLessRestrictiveEncounter(z.Type) && z.Shiny == enc.Shiny && z.End == null))
            {
                // some Special Pokémon are exempt because one of their other forms have been in research, and their form is reverted when sent to HOME
                if (enc.Species is (int)Giratina or (int)Genesect)
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
        var pk = list.GetDetails(species, form);
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

        pk.Add(entry); // add the GBL entry!
    }

    public static void AddNewShadows(PogoEncounterList list)
    {
        var removed = new List<(ushort Species, byte Form)>
        {
            new((int)Bulbasaur, 0),
        };

        var added = new List<(ushort Species, byte Form, PogoShiny Shiny)>
        {
            new((int)Bulbasaur, 0, Random),
        };

        // add end dates for Shadows that have been removed
        foreach ((ushort s, byte f) in removed)
        {
            var pk = list.GetDetails(s, f);
            var entries = pk.Data;

            foreach (var entry in entries)
            {
                if (entry is { Type: Shadow, End: null })
                    entry.End = new PogoDate();
            }
        }

        // add new Shadows
        foreach ((ushort s, byte f, PogoShiny shiny) in added)
        {
            var pk = list.GetDetails(s, f);
            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                Shiny = shiny,
                Type = Shadow,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = "Team GO Rocket Grunt",
            };

            pk.Add(entry);
        }
    }

    private static byte GetPowerSpotTier(ushort species)
    {
        if (SpeciesCategory.IsSpecialPokemon(species))
            return 5;

        return (Species)species switch
        {
            Bulbasaur => 1,
            Charmander => 1,
            Squirtle => 1,
            Caterpie => 1,
            Pikachu => 1,
            Growlithe => 1,
            Abra => 1,
            Machop => 2,
            Gastly => 1,
            Krabby => 1,
            Hitmonlee => 3,
            Hitmonchan => 3,
            Chansey => 3,
            Electabuzz => 3,
            Magmar => 3, // verify
            Magikarp => 1, // verify
            Eevee => 2,
            Omanyte => 1,
            Kabuto => 1,
            Hoothoot => 1,
            Shuckle => 2,
            Hitmontop => 3, // verify
            Ralts => 1,
            Sableye => 3,
            Wailmer => 2,
            Trapinch => 1,
            Feebas => 1, // verify
            Spheal => 1,
            Beldum => 3,
            Combee => 1,
            Pidove => 1,
            Roggenrola => 1,
            Woobat => 1,
            Drilbur => 1,
            Cottonee => 1,
            Darumaka => 2,
            Trubbish => 1,
            Cryogonal => 3,
            Deino => 3,
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
            _ => throw new System.Exception("Pokémon has not yet been released as a Dynamax Pokémon."),
        };
    }

    public enum BossType : byte
    {
        Raid = 0,
        ShadowRaid = 1,
        MaxBattleDynamax = 2,
        MaxBattleGigantamax = 3,
    }

    public enum MegaType : byte
    {
        None = 0,
        Normal = 1,
        X = 2,
        Y = 3,
        Z = 4,
    }
}
#endif
