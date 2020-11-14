using System.Collections.Generic;

namespace PoGoEncTool
{
    public class PogoPoke
    {
        public int Species { get; set; }
        public int Form { get; set; }

        public List<PogoEntry> Data { get; set; } = new List<PogoEntry>();
        public PogoEntry this[int index]  { get => Data[index]; set => Data[index] = value; }
        public void Add(PogoEntry entry) => Data.Add(entry);

        public static PogoPoke CreateNew(int species, int form) => new PogoPoke
        {
            Species = species,
            Form = form,
        };

        public void Clean() => Data.RemoveAll(z => z.Type == 0);
    }
}
