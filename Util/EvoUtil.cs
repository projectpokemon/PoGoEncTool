using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PKHeX.Core;

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
                yield return s | (form << 11);
                var nextEvolutions = GetEvolutionsInner(pt, entries, s, f);
                foreach (var next in nextEvolutions)
                    yield return next;
            }
        }
    }
}
