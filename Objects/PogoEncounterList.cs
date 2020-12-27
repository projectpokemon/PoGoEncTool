using System;
using System.Collections.Generic;
using System.Linq;

namespace PoGoEncTool
{
    [Serializable]
    public class PogoEncounterList
    {
        public List<PogoPoke> Data { get; set; } = new();
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
            CleanDuplicatesForEvolutions();
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

                    // Mark any duplicate entry in the future evolutions
                    foreach (var z in dest.Data)
                    {
                        if (entry.Data.TrueForAll(p => p.CompareTo(z) != 0))
                            continue;
                        var add = ((PKHeX.Core.Species)entry.Species).ToString();
                        if (entry.Form != 0)
                            add += $"-{entry.Form}";
                        z.Comment += $" {{{add}}}";
                    }
                }
            }
        }

        public void ReapplyDuplicates()
        {
            foreach (var entry in Data)
            {
                foreach (var appearance in entry.Data)
                {
                    var index = appearance.Comment.IndexOf('{');
                    if (index < 0)
                        continue;
                    appearance.Comment = appearance.Comment.Substring(0, index - 1);
                }
            }
            CleanDuplicatesForEvolutions();
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

                    foreach (var z in entry.Data)
                    {
                        if (dest.Data.Any(p => p.Equals(z)))
                            continue;
                        dest.Data.Add(z);
                    }
                }
            }
        }

        public IEnumerable<string> SanityCheck()
        {
            foreach (var entry in Data)
            {
                foreach (var error in entry.SanityCheck())
                    yield return error;
            }
        }
    }
}
