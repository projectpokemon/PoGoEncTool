using System;
using System.Collections.Generic;
using System.Linq;
using PKHeX.Core;

namespace PoGoEncTool.Core;

[Serializable]
public class PogoPoke : IComparable, ISpeciesForm
{
    public ushort Species { get; set; }
    public byte Form { get; set; }
    public bool Available { get; set; }
    public List<PogoEntry> Data { get; set; } = new();

    public PogoEntry this[int index]  { get => Data[index]; set => Data[index] = value; }
    public void Add(PogoEntry entry) => Data.Add(entry);

    public static PogoPoke CreateNew(ushort species, byte form) => new()
    {
        Species = species,
        Form = form,
    };

    public bool IsMatch(ushort species, byte form) => Species == species && Form == form;

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
        if (obj is not PogoPoke p)
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

    public IEnumerable<string> SanityCheck()
    {
        for (var i = 0; i < Data.Count; i++)
        {
            var app = Data[i];
            string fail(string msg) => $"{msg}: {(Species) Species}-{Form} -- appear[{i}] {app}";
            if (app.Comment.Any(char.IsWhiteSpace))
            {
                app.Comment = app.Comment.Trim();
            }

            if (app.Start == null)
            {
                yield return fail("No Start Date");
                continue;
            }

            if (app.End?.CompareTo(app.Start) == -1)
            {
                yield return fail("End Date before Start Date");
            }
        }
    }
}
