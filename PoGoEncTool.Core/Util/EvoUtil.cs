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

            var la = Get(PersonalTable.LA,   8, species, form);
            var ss = Get(PersonalTable.SWSH, 8, species, form);
            var uu = Get(PersonalTable.USUM, 7, species, form);
            return la.Concat(ss).Concat(uu).Distinct();
        }

        private static IEnumerable<int> Get(PersonalTable table, int gen, int species, int form)
        {
            if (species > table.MaxSpeciesID)
                yield break;
            if (form >= table[species].FormCount)
                yield break;

            EvolutionTree t;
            var pt = PersonalTable.LA;
            if (pt.IsPresentInGame(species, form) && !HisuiOnlyEvos.Contains(species))
                t = EvolutionTree.GetEvolutionTree(EntityContext.Gen8a);
            else
                t = EvolutionTree.GetEvolutionTree((EntityContext)gen);

            var tableEvos = t.GetEvolutions(species, form);
            foreach (var evo in tableEvos)
                yield return evo;
        }

        public static bool IsAllowedEvolution(in int species, in int form, in int s, in int destForm)
        {
            // Outside of special events, regional form branched evolutions are not available.
            var destSpecies = (Species) s;
            return (Species)species switch
            {
                // Alolan
                Pichu or Pikachu when destSpecies is Raichu && destForm is 1 => false,

                // Galarian
                Koffing when destSpecies is Weezing && destForm is 1 => false,
                MimeJr when destSpecies is MrMime && destForm is 1 => false,
                MimeJr when destSpecies is MrRime && destForm is 0 => false,

                // Hisuian
                Cyndaquil or Quilava when destSpecies is Typhlosion && destForm is 1 => false,
                Oshawott or Dewott when destSpecies is Samurott && destForm is 1 => false,
                Petilil when destSpecies is Lilligant && destForm is 1 => false,
                Rufflet when destSpecies is Braviary && destForm is 1 => false,
                Goomy when destSpecies is Sliggoo or Goodra && destForm is 1 => false,
                Bergmite when destSpecies is Avalugg && destForm is 1 => false,
                Rowlet or Dartrix when destSpecies is Decidueye && destForm is 1 => false,

                // Future evolutions (temporary, to be removed when they debut in GO)
                Stantler when destSpecies is Wyrdeer => false,
                Scyther when destSpecies is Kleavor => false,
                Teddiursa or Ursaring when destSpecies is Ursaluna => false,

                _ => true,
            };
        }

        /// <summary>
        /// Pokémon that can only evolve into their Hisuian Forms in <see cref="PKHeX.Core.GameVersion.PLA"/>.
        /// </summary>
        private static readonly HashSet<int> HisuiOnlyEvos = new()
        {
            (int)Cyndaquil,
            (int)Quilava,
            (int)Oshawott,
            (int)Dewott,
            (int)Petilil,
            (int)Rufflet,
            (int)Goomy,
            (int)Bergmite,
            (int)Rowlet,
            (int)Dartrix,
        };
    }
}
