using System;
using System.Collections.Generic;
using System.Linq;
using PKHeX.Core;

namespace PoGoEncTool.Core
{
    [Serializable]
    public class PogoEncounterList
    {
        public List<PogoPoke> Data { get; set; } = new();

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
                    var evoSpecies = evo & 0x7FF;
                    var evoForm = evo >> 11;
                    if (!EvoUtil.IsAllowedEvolution(entry.Species, entry.Form, evoSpecies, evoForm))
                        continue;

                    var dest = Data.Find(z => z.IsMatch(evoSpecies, evoForm));
                    if (dest?.Available != true)
                        continue;

                    // Mark any duplicate entry in the future evolutions
                    foreach (var z in dest.Data)
                    {
                        if (entry.Data.TrueForAll(p => p.CompareTo(z) != 0))
                            continue;
                        var species = GameInfo.Strings.Species;
                        var add = species[entry.Species];
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
                    appearance.Comment = appearance.Comment[..(index - 1)];
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
                    var evoSpecies = evo & 0x7FF;
                    var evoForm = evo >> 11;
                    if (!EvoUtil.IsAllowedEvolution(entry.Species, entry.Form, evoSpecies, evoForm))
                        continue;

                    var dest = Data.Find(z => z.IsMatch(evoSpecies, evoForm));
                    if (dest?.Available != true)
                        continue;

                    AddToEvoIfAllowed(entry, dest);
                }
            }
        }

        private void AddToEvoIfAllowed(PogoPoke entry, PogoPoke dest)
        {
            var destData = dest.Data;
            foreach (var z in entry.Data)
            {
                if (destData.Any(p => p.EqualsNoComment(z)))
                    continue;

                if (!IsTimedEvolution(entry, dest))
                {
                    destData.Add(z);
                    continue;
                }

                var timedChunks = SplitIntoTimedEvolutions(entry, dest, z);
                destData.AddRange(timedChunks);
            }
        }

        private IEnumerable<PogoEntry> SplitIntoTimedEvolutions(PogoPoke entry, PogoPoke dest, PogoEntry pogoEntry)
        {
            yield return pogoEntry;
        }

        private bool IsTimedEvolution(PogoPoke entry, PogoPoke evoSpecies)
    {
            return false;
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
