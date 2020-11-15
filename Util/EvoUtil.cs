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
            var method = typeof(EvolutionTree).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).First(z => z.Name == "GetEvolutionTree" && z.GetParameters().Length == 1);
            var tree = (EvolutionTree)method.Invoke(null, new object[] {8})!;

            var evoMethod = typeof(EvolutionTree).GetMethod("GetEvolutions", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var result = evoMethod.Invoke(tree, new object[] {species, form})!;
            return (IEnumerable<int>) result;
        }
    }
}
