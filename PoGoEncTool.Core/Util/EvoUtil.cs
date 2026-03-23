using System.Collections.Generic;
using System.Linq;
using PKHeX.Core;
using static PKHeX.Core.Species;

namespace PoGoEncTool.Core;

public static class EvoUtil
{
    public static IEnumerable<(ushort Species, byte Form)> GetEvoSpecForms(ushort species, byte form)
    {
        if (species == (int) Meltan)
        {
            (ushort, byte) melmetal = new((ushort)Melmetal, 0);
            return [melmetal];
        }

        var sv = Get(PersonalTable.SV,   EntityContext.Gen9,  species, form);
        var la = Get(PersonalTable.LA,   EntityContext.Gen8a, species, form);
        var ss = Get(PersonalTable.SWSH, EntityContext.Gen8,  species, form);
        var uu = Get(PersonalTable.USUM, EntityContext.Gen7,  species, form);
        return sv.Concat(la).Concat(ss).Concat(uu).Distinct();
    }

    private static IEnumerable<(ushort Species, byte Form)> Get(IPersonalTable table, EntityContext context, ushort species, byte form)
    {
        if (species > table.MaxSpeciesID)
            yield break;
        if (form >= table[species].FormCount)
            yield break;

        var tree = EvolutionTree.GetEvolutionTree(context);
        var evos = tree.Forward.GetEvolutions(species, form);
        foreach (var evo in evos)
            yield return evo;
    }

    public static bool IsAllowedEvolution(in ushort species, in byte form, in ushort s, in byte destForm)
    {
        // Outside of special events, regional form branched evolutions are not available.
        var destSpecies = (Species)s;
        return (Species)species switch
        {
            // Alolan Forms
            Pichu or Pikachu when destSpecies == Raichu && destForm == 1 => false,

            // Galarian Forms
            MimeJr when destSpecies == MrMime && destForm == 1 => false,
            MimeJr when destSpecies == MrRime && destForm == 0 => false,

            // Hisuian Forms
            Cyndaquil or Quilava when destSpecies == Typhlosion && destForm == 1 => false,
            Oshawott or Dewott when destSpecies == Samurott && destForm == 1 => false,
            Petilil when destSpecies == Lilligant && destForm == 1 => false,
            Rufflet when destSpecies == Braviary && destForm == 1 => false,
            Goomy when destSpecies is Sliggoo or Goodra && destForm == 1 => false,
            Bergmite when destSpecies == Avalugg && destForm == 1 => false,
            Rowlet or Dartrix when destSpecies == Decidueye && destForm == 1 => false,

            // Cross-Generation evolutions that are not available
            Stantler when destSpecies == Wyrdeer => false,
            Scyther when destSpecies == Kleavor => false,
            Basculin when destSpecies == Basculegion => false,
            Girafarig when destSpecies == Farigiraf => false,
            Duraludon when destSpecies == Archaludon => false,

            _ => true,
        };
    }
}
