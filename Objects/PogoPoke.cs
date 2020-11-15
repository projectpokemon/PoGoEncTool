using System;
using System.Collections.Generic;

namespace PoGoEncTool
{
    public class PogoPoke : IComparable
    {
        public int Species { get; set; }
        public int Form { get; set; }
        public bool Available { get; set; }

        public List<PogoEntry> Data { get; set; } = new List<PogoEntry>();
        public PogoEntry this[int index]  { get => Data[index]; set => Data[index] = value; }
        public void Add(PogoEntry entry) => Data.Add(entry);

        public static PogoPoke CreateNew(int species, int form) => new PogoPoke
        {
            Species = species,
            Form = form,
        };

        public void Clean()
        {
            Data.RemoveAll(z => z.Type == 0);
            Data.Sort((x, y) => x.CompareTo(y));
        }

        public int CompareTo(PogoPoke p)
        {
            if (Species < p.Species)
                return -1;
            if (Species > p.Species)
                return 1;
            if (Form < p.Form)
                return -1;
            if (Form > p.Form)
                return 1;
            throw new Exception("Invalid sort index -- duplicate object detected"); // Shouldn't ever hit this.
        }

        public int CompareTo(object? obj)
        {
            if (!(obj is PogoPoke p))
                return 1;
            return CompareTo(p);
        }

        public void ModifyAll(Func<PogoEntry, bool> condition, Action<PogoEntry> action)
        {
            foreach (var appear in Data)
            {
                if (condition(appear))
                    action(appear);
            }
        }
    }
}
