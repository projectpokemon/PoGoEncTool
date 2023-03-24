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
        var T1 = new List<(ushort Species, byte Form, PogoShiny Shiny)>
        {
            new((int)Bulbasaur, 0, Random),
        };

        var T3 = new List<(ushort Species, byte Form, PogoShiny Shiny)>
        {
            new((int)Bulbasaur, 0, Random),
        };

        var bosses = T1.Concat(T3);

        foreach (var enc in bosses)
        {
            var tier = T1.Contains(enc) ? "1" : "3";
            var pkm = list.GetDetails(enc.Species, enc.Form);
            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                End   = new PogoDate(),
                Type = PogoType.Raid,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = $"Tier {tier} Raid Boss",
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
