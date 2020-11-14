using System.Collections.Generic;

namespace PoGoEncTool
{
    public class PogoPoke : List<PogoEntry>
    {
        public int Species { get; set; }
        public int Form { get; set; }

        public static PogoPoke CreateNew(int species, int form) => new PogoPoke
        {
            Species = species,
            Form = form,
        };

        public void Clean() => RemoveAll(z => z.Type == 0);
    }
}
