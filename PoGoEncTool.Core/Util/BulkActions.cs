using System.Collections.Generic;
using System.Linq;
using static PKHeX.Core.Species;

#pragma warning disable RCS1041 // Remove empty initializer.
// ReSharper disable RedundantEmptyObjectOrCollectionInitializer
// ReSharper disable CollectionNeverUpdated.Local

namespace PoGoEncTool.Core;

public static class BulkActions
{
    public static void AddRaidBosses(PogoEncounterList list)
    {
        var T1 = new (ushort Species, byte Form)[] { new((int)Bulbasaur, 0) }; // Tier 1
        var T3 = new (ushort Species, byte Form)[] { new((int)Bulbasaur, 0) }; // Tier 3
        var SE = new (ushort Species, byte Form)[] { new((int)Bulbasaur, 0) }; // Shiny eligible
        var bosses = T1.Concat(T3);

        foreach (var enc in bosses)
        {
            byte form = enc.Species switch
            {
                (ushort)Rattata => 1,
                _ => 0,
            };

            var tier = T1.Contains(enc) ? "1" : "3";
            var pkm = list.GetDetails(enc.Species, form);
            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                End   = new PogoDate(),
                Type = PogoType.Raid,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = $"Tier {tier} Raid Boss",
                Shiny = SE.Contains(enc) ? PogoShiny.Random : PogoShiny.Never,
            };

            // set species as available if this encounter is its debut
            if (!pkm.Available)
                pkm.Available = true;

            // set its evolutions as available as well
            var evos = EvoUtil.GetEvoSpecForms(enc.Species, form)
                .Where(z => EvoUtil.IsAllowedEvolution(enc.Species, form, z.Species, z.Form)).ToArray();

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
        const int form = 0;
        var removed = new List<int>
        {

        };

        var added = new List<int>
        {

        };

        // add end dates for Shadows that have been removed
        foreach (var species in removed)
        {
            var pkm = list.GetDetails((ushort)species, form);
            var entries = pkm.Data;

            foreach (var entry in entries)
            {
                if (entry.Type == PogoType.Shadow && entry.End == null)
                    entry.End = new PogoDate();
            }
        }

        // add new Shadows
        foreach (var species in added)
        {
            var pkm = list.GetDetails((ushort)species, form);
            var entry = new PogoEntry
            {
                Start = new PogoDate(),
                Shiny = PogoShiny.Never,
                Type = PogoType.Shadow,
                LocalizedStart = true,
                NoEndTolerance = false,
                Comment = "Team GO Rocket Grunt",
            };

            pkm.Add(entry);
        }
    }
}
