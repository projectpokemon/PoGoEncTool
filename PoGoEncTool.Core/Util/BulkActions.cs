using System;
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
                    End = new PogoDate { Y = 2000, M = 1, D = 1 },
                    Type = PogoType.Raid,
                    LocalizedStart = true,
                    // NoEndTolerance = true,
                    // Gender = pkm is (int)Frillish ? PogoGender.MaleOnly : PogoGender.Random,
                };

                if (pkm is not ((int)Bulbasaur))
                    entry.Shiny = PogoShiny.Random;

                if (T1.Contains(pkm)) entry.Comment = "T1 Raid Boss";
                if (T3.Contains(pkm)) entry.Comment = "T3 Raid Boss";
                if (T5.Contains(pkm)) entry.Comment = "T5 Raid Boss";

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
    }
}
