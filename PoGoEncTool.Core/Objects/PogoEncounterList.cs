using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PKHeX.Core;

namespace PoGoEncTool.Core;

[Serializable]
public class PogoEncounterList
{
    public List<PogoPoke> Data { get; set; } = [];

    public PogoEncounterList() { }
    public PogoEncounterList(IEnumerable<PogoPoke> seed) => Data.AddRange(seed);

    public PogoPoke GetDetails(ushort species, byte form)
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
            foreach ((ushort species, byte form) in evos)
            {
                if (!EvoUtil.IsAllowedEvolution(entry.Species, entry.Form, species, form))
                    continue;

                var dest = Data.Find(z => z.IsMatch(species, form));
                if (dest?.Available != true)
                    continue;

                // Mark any duplicate entry in the future evolutions
                foreach (var z in dest.Data)
                {
                    if (entry.Data.TrueForAll(p => p.CompareTo(z) != 0))
                        continue;
                    var speciesNames = GameInfo.Strings.Species;
                    var add = speciesNames[entry.Species];
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
            foreach ((ushort species, byte form) in evos)
            {
                if (!EvoUtil.IsAllowedEvolution(entry.Species, entry.Form, species, form))
                    continue;

                var dest = Data.Find(z => z.IsMatch(species, form));
                if (dest?.Available != true)
                    continue;

                AddToEvoIfAllowed(entry, dest);
            }
        }
    }

    private static void AddToEvoIfAllowed(PogoPoke entry, PogoPoke dest)
    {
        var destData = dest.Data;
        foreach (var z in entry.Data)
        {
            if (destData.Any(p => p.EqualsNoComment(z)))
                continue;

            if (!IsTimedEvolution(dest, out var end))
            {
                destData.Add(z);
                continue;
            }

            var timedChunks = GetTimedEvolutionEntry(z, end);
            destData.AddRange(timedChunks);
        }
    }

    private static IEnumerable<PogoEntry> GetTimedEvolutionEntry(PogoEntry entry, PogoDate evoEndDate)
    {
        bool isBefore = evoEndDate.CompareTo(entry.Start) < 0;
        if (isBefore) // Availability was before this entry existed. Can't evolve.
            yield break;

        bool isAfter = entry.End is not null && evoEndDate.CompareTo(entry.End) > 0;
        if (isAfter) // Availability ended before the evolution ended, so just return as is.
        {
            yield return entry;
            yield break;
        }

        // Get a copy with a truncated end date.
        yield return entry with { End = evoEndDate };
    }

    private static bool IsTimedEvolution(ISpeciesForm evo, [NotNullWhen(true)] out PogoDate? endDate)
    {
        return (endDate = GetEndDate(evo)) != null;
    }

    private static PogoDate? GetEndDate(ISpeciesForm evo) => evo.Species switch
    {
        (int)Species.Exeggutor when evo.Form == 1 => new PogoDate(2024, 03, 10), // Pokémon GO City Safari: Tainan 2024
        (int)Species.Marowak   when evo.Form == 1 => new PogoDate(2022, 11, 26), // Ultra Beast Arrival: London / Los Angeles
        _ => null,
    };

    public IEnumerable<string> SanityCheck()
    {
        foreach (var entry in Data)
        {
            foreach (var error in entry.SanityCheck())
                yield return error;
        }
    }
}
