using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PKHeX.Core;
using static PKHeX.Core.Species;

namespace PoGoEncTool
{
    public static class EvoUtil
    {
        public static IEnumerable<int> GetEvoSpecForms(int species, int form)
        {
            var g8 = Get(PersonalTable.SWSH, 8, species, form);
            var g7 = Get(PersonalTable.USUM, 7, species, form);
            return g8.Concat(g7).Distinct();
        }

        private static IEnumerable<int> Get(PersonalTable table, in int gen, in int species, in int form)
        {
            if (species > table.MaxSpeciesID)
                return Array.Empty<int>();
            if (form >= table[species].FormeCount)
                return Array.Empty<int>();
            return GetEntriesAndEvolutions(table, species, form, gen);
        }

        private static IEnumerable<int> GetEntriesAndEvolutions(PersonalTable pt, in int species, in int form, in int gen)
        {
            var method = typeof(EvolutionTree).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .First(z => z.Name == "GetEvolutionTree" && z.GetParameters().Length == 1);
            var tree = (EvolutionTree) method.Invoke(null, new object[] {gen})!;

            var evoMethod = typeof(EvolutionTree).GetField("Entries", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var entries = (IReadOnlyList<EvolutionMethod[]>)evoMethod.GetValue(tree)!;

            return GetEvolutionsInner(pt, entries, species, form);
        }

        private static IEnumerable<int> GetEvolutionsInner(PersonalTable pt, IReadOnlyList<EvolutionMethod[]> entries, int species, int form)
        {
            int index = pt.GetFormeIndex(species, form);
            var evos = entries[index];
            foreach (var method in evos)
            {
                var s = method.Species;
                if (s == 0)
                    continue;
                var f = method.GetDestinationForm(form);
                yield return s | (f << 11);
                var nextEvolutions = GetEvolutionsInner(pt, entries, s, f);
                foreach (var next in nextEvolutions)
                    yield return next;
            }
        }

        public static bool IsAllowedEvolution(in int species, in int form, in int s, in int destForm)
        {
            // Alolan/Galar split-evolutions are not available.
            var destSpecies = (Species) s;
            return (Species)species switch
            {
                Pikachu when destSpecies == Raichu && destForm == 1 => false,
                Koffing when destSpecies == Weezing && destForm == 1 => false,
                MimeJr when destSpecies == MrMime && destForm == 1 => false,
                Exeggcute when destSpecies == Exeggutor && destForm == 1 => false,
                Cubone when destSpecies == Marowak && destForm == 1 => false,
                _ => true
            };
        }
    }
}
