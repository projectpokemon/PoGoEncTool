using System;
using System.Collections.Generic;
using System.Linq;
using static PKHeX.Core.Species;

namespace PoGoEncTool.Core
{
    public static class BulkActions
    {
        public static void AddRaidBosses(PogoEncounterList list)
        {
            var T1 = new[] { (int)Bulbasaur };
            var T3 = new[] { (int)Bulbasaur };
            var T5 = Array.Empty<int>(); // usually manually added
            var bosses = T1.Concat(T3).Concat(T5);

            foreach (var pkm in bosses)
            {
                var form = pkm switch
                {
                    (int)Rattata => 1,
                    _ => 0,
                };

                var species = list.GetDetails(pkm, form);
                var entry = new PogoEntry
                {
                    Start = new PogoDate { Y = 2000, M = 1, D = 1 },
                    End   = new PogoDate { Y = 2000, M = 1, D = 1 },
                    Type = PogoType.Raid,
                    LocalizedStart = true,
                    // NoEndTolerance = true,
                    // Gender = pkm is (int)Frillish ? PogoGender.MaleOnly : PogoGender.Random,
                };

                if (pkm is not ((int)Bulbasaur))
                    entry.Shiny = PogoShiny.Random;

                if (T1.Contains(pkm)) entry.Comment = "Tier 1 Raid Boss";
                if (T3.Contains(pkm)) entry.Comment = "Tier 3 Raid Boss";
                if (T5.Contains(pkm)) entry.Comment = "Tier 5 Raid Boss";

                // set debut species, and any of its evolutions, as available
                if (!species.Available)
                {
                    var evos = EvoUtil.GetEvoSpecForms(pkm, form)
                        .Select(z => new { Species = z & 0x7FF, Form = z >> 11 })
                        .Where(z => EvoUtil.IsAllowedEvolution(pkm, form, z.Species, z.Form)).ToArray();

                    foreach (var evo in evos)
                    {
                        var parent = list.GetDetails(evo.Species, evo.Form);
                        if (!parent.Available)
                            parent.Available = true;
                    }
                    species.Available = true;
                }
                species.Add(entry);
            }
        }

        public static void AddNewShadows(PogoEncounterList list)
        {
            const int form = 0;
            var removed = new List<int>()
            {

            };

            var added = new List<int>()
            {

            };

            // add end dates for Shadows that have been removed
            foreach (var pkm in removed)
            {
                var species = list.GetDetails(pkm, form);
                var entries = species.Data;

                foreach (var entry in entries)
                {
                    if (entry.Type == PogoType.Shadow && entry.End == null)
                        entry.End = new PogoDate { Y = 2000, M = 1, D = 1 };
                }
            }

            // add new Shadows
            foreach (var pkm in added)
            {
                var species = list.GetDetails(pkm, form);
                var entry = new PogoEntry
                {
                    Start = new PogoDate { Y = 2000, M = 1, D = 1 },
                    Shiny = PogoShiny.Never,
                    Type = PogoType.Shadow,
                    LocalizedStart = true,
                    // NoEndTolerance = true,
                    Comment = "Team GO Rocket Grunt",
                };

                species.Add(entry);
            }
        }
    }
}
