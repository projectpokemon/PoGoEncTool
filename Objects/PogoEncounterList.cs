using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            // CleanDuplicatesForEvolutions();
            foreach (var d in Data)
                d.Clean();
            Data.RemoveAll(z => !z.Available);
            Data.Sort((x, y) => x.CompareTo(y));
        }

        public void ModifyAll(Func<PogoEntry, bool> condition, Action<PogoEntry> action)
        {
            foreach (var entry in Data)
                entry.ModifyAll(condition, action);
        }

        public void ModifyAll(Func<PogoPoke, bool> condition, Action<PogoPoke> action)
        {
            foreach (var entry in Data)
            {
                if (condition(entry))
                    action(entry);
            }
        }

        private void CleanDuplicatesForEvolutions()
        {
            foreach (var entry in Data)
            {
                var evos = EvoUtil.GetEvoSpecForms(entry.Species, entry.Form);
                foreach (var evo in evos)
                {
                    var s = evo & 0x7FF;
                    var f = evo >> 11;
                    if (!EvoUtil.IsAllowedEvolution(entry.Species, entry.Form, s, f))
                        continue;

                    var dest = Data.Find(z => z.Species == s && z.Form == f);
                    if (dest?.Available != true)
                        continue;

                    // Remove any duplicate entry in the future evolutions
                    int count = dest.Data.RemoveAll(z => !entry.Data.TrueForAll(p => p.CompareTo(z) != 0));
                    if (count != 0)
                        Debug.WriteLine($"Removed {count} entries from {(PKHeX.Core.Species)dest.Species}-{dest.Form}");
                }
            }
        }

        public void Propagate()
        {
            foreach (var entry in Data)
            {
                var evos = EvoUtil.GetEvoSpecForms(entry.Species, entry.Form);
                foreach (var evo in evos)
                {
                    var s = evo & 0x7FF;
                    var f = evo >> 11;
                    if (!EvoUtil.IsAllowedEvolution(entry.Species, entry.Form, s, f))
                        continue;

                    var dest = Data.Find(z => z.Species == s && z.Form == f);
                    if (dest?.Available != true)
                        continue;

                    dest.Data.AddRange(entry.Data);
                }
            }
        }
    }
}
