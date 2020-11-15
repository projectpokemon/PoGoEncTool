using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PKHeX.Core;

namespace PoGoEncTool
{
    public static class EvoUtil
    {
        public static IEnumerable<int> Get(int species, int form)
        {
            var g8 = Get(species, form, 8);
            var g7 = Get(species, form, 7);
            return g8.Concat(g7).Distinct();
        }

        private static IEnumerable<int> Get(int species, int form, int gen)
        {
            var method = typeof(EvolutionTree).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .First(z => z.Name == "GetEvolutionTree" && z.GetParameters().Length == 1);
            var tree = (EvolutionTree) method.Invoke(null, new object[] {gen})!;

            var evoMethod = typeof(EvolutionTree).GetMethod("GetEvolutions", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var result = evoMethod.Invoke(tree, new object[] {species, form})!;
            return (IEnumerable<int>) result;
        }
    }
}
