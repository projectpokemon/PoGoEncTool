using System.Collections.Generic;

namespace PoGoEncTool
{
    public class PogoEncounterList
    {
        public List<PogoPoke> Data { get; set; } = new List<PogoPoke>();
        public PogoPoke this[int index] { get => Data[index]; set => Data[index] = value; }
        public void Add(PogoPoke entry) => Data.Add(entry);

        public PogoEncounterList() { }
        public PogoEncounterList(IEnumerable<PogoPoke> seed) => Data.AddRange(seed);

        public PogoPoke GetDetails(int species, int form)
        {
            var exist = Data.Find(z => z.Species == species && z.Form == form);
            if (exist != null)
                return exist;

            var created = PogoPoke.CreateNew(species, form);
            Data.Add(created);
            return created;
        }

        public void Clean()
        {
            foreach (var d in Data)
                d.Clean();
            Data.RemoveAll(z => z.Data.Count == 0);
            Data.Sort((x, y) => x.CompareTo(y));
        }
    }
}
