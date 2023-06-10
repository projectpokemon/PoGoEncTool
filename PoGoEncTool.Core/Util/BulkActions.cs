using PKHeX.Core;
using System.Collections.Generic;
using System.Linq;
using static PKHeX.Core.Species;
using static PoGoEncTool.Core.PogoShiny;

#pragma warning disable RCS1041 // Remove empty initializer.
// ReSharper disable RedundantEmptyObjectOrCollectionInitializer
// ReSharper disable CollectionNeverUpdated.Local

namespace PoGoEncTool.Core;

public static class BulkActions
{
    public static void AddRaidBosses(PogoEncounterList list)
    {
        var bosses = new List<(ushort Species, byte Form, PogoShiny Shiny, byte Tier)>
        {
            new((int)Bulbasaur, 0, Random, 1),
        };

        bool shadow = false;

        foreach (var enc in bosses)
        {
            var pkm = list.GetDetails(enc.Species, enc.Form);
            var boss = shadow ? "Shadow Raid Boss" : "Raid Boss";
            var type = shadow ? PogoType.RaidS : PogoType.Raid;
            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                End   = new PogoDate(),
                Type = type,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = $"Tier {enc.Tier} {boss}",
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

    public static void AddMonthlyRaidBosses(PogoEncounterList list)
    {
        var bosses = new List<(ushort Species, byte Form, PogoShiny Shiny, PogoDate Start, PogoDate End, bool IsMega)>
        {
            // Five-Star
            new((int)Bulbasaur, 0, Random, new PogoDate(), new PogoDate(), false),

            // Mega
            new((int)Bulbasaur, 0, Random, new PogoDate(), new PogoDate(), true),
        };

        foreach (var enc in bosses)
        {
            var pkm = list.GetDetails(enc.Species, enc.Form);
            var comment = enc.IsMega ? "Mega Raid Boss" : "Tier 5 Raid Boss";
            var type = enc.Species switch
            {
                (int)Meltan or (int)Melmetal => PogoType.Raid, // only Mythicals that can be traded
                _ when SpeciesCategory.IsMythical(enc.Species) => PogoType.RaidM,
                _ when SpeciesCategory.IsUltraBeast(enc.Species) => PogoType.RaidUB,
                _ => PogoType.Raid,
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
            if ((!enc.IsMega) && !pkm.Data.Any(z => z.Type is PogoType.Wild or PogoType.Research or PogoType.ResearchM && z.Shiny == enc.Shiny && z.End == null))
                AddGBL(enc.Species, enc.Form, enc.Shiny, enc.Start);
        }

        void AddGBL(ushort species, byte form, PogoShiny shiny, PogoDate start)
        {
            var end = new PogoDate();
            var pkm = list.GetDetails(species, form);
            var type = SpeciesCategory.IsMythical(species) ? PogoType.GBLM : PogoType.GBL;
            var entry = new PogoEntry
            {
                Start = start,
                End = end,
                Type = type,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = "Reward Encounter",
                Shiny = shiny,
            };

            pkm.Add(entry); // add the GBL entry!
        }
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
                if (entry.Type == PogoType.Shadow && entry.End == null)
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
                Type = PogoType.Shadow,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = "Team GO Rocket Grunt",
            };

            pkm.Add(entry);
        }
    }
}
