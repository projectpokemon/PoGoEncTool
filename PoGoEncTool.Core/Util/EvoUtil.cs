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

            var hisui = new[] { 155, 156, 501, 502, 548, 627, 704, 712, 722, 723 }; // pre-evos with branched evo paths only possible in LA

            EvolutionTree t;
            var pt = PersonalTable.LA;
            if (((PersonalInfoLA)pt.GetFormEntry(species, form)).IsPresentInGame && !hisui.Contains(species))
                t = EvolutionTree.GetEvolutionTree(new PA8 { Version = (int)GameVersion.PLA }, 8);
            else
                t = EvolutionTree.GetEvolutionTree(gen);

            var tableEvos = t.GetEvolutions(species, form);
            foreach (var evo in tableEvos)
                yield return evo;
        }

        public static bool IsAllowedEvolution(in int species, in int form, in int s, in int destForm)
        {
            // Regional Form split-evolutions are not available.
            var destSpecies = (Species) s;
            return (Species)species switch
            {
                // Alolan
                Pichu or Pikachu when destSpecies is Raichu && destForm is 1 => false,
                Exeggcute when destSpecies is Exeggutor && destForm is 1 => false,
                Cubone when destSpecies is Marowak && destForm is 1 => false,

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

                _ => true,
            };
        }
    }
}
