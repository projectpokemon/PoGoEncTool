using System;
using System.Collections.Generic;
using System.Linq;
using PKHeX.Core;
using static PKHeX.Core.Species;

namespace PoGoEncTool.Core
{
    public static class EvoUtil
    {
        public static IEnumerable<int> GetEvoSpecForms(int species, int form)
        {
            if (species == (int) Meltan)
                return new[] {(int) Melmetal};

            var g8 = Get(PersonalTable.SWSH, 8, species, form);
            var g7 = Get(PersonalTable.USUM, 7, species, form);
            return g8.Concat(g7).Distinct();
        }

        private static IEnumerable<int> Get(PersonalTable table, in int gen, in int species, in int form)
        {
            if (species > table.MaxSpeciesID)
                return Array.Empty<int>();
            if (form >= table[species].FormCount)
                return Array.Empty<int>();
            var t = EvolutionTree.GetEvolutionTree(gen);
            return t.GetEvolutions(species, form);
        }

        public static bool IsAllowedEvolution(in int species, in int form, in int s, in int destForm)
        {
            // Alolan/Galar split-evolutions are not available.
            var destSpecies = (Species) s;
            return (Species)species switch
            {
                Pichu when destSpecies == Raichu && destForm == 1 => false,
                Pikachu when destSpecies == Raichu && destForm == 1 => false,
                Koffing when destSpecies == Weezing && destForm == 1 => false,
                MimeJr when destSpecies == MrMime && destForm == 1 => false,
                MimeJr when destSpecies == MrRime && destForm == 0 => false,
                Exeggcute when destSpecies == Exeggutor && destForm == 1 => false,
                Cubone when destSpecies == Marowak && destForm == 1 => false,
                _ => true
            };
        }
    }
}
