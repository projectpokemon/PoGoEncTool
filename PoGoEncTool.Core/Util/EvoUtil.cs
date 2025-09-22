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
        var destSpecies = (Species) s;
        return (Species)species switch
        {
            // Alolan
            Pichu or Pikachu when destSpecies is Raichu && destForm is 1 => false,

            // Galarian
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
            Basculin when destSpecies is Basculegion => false,
            Girafarig when destSpecies is Farigiraf => false,
            Duraludon when destSpecies is Archaludon => false,

            _ => true,
        };
    }
}
