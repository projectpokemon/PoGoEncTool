using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PKHeX.Core;
using static PKHeX.Core.Species;

namespace PoGoEncTool.Core;

public static class PogoPickler
{
    private const string identifier = "go";

    public static byte[] WritePickle(PogoEncounterList entries)
    {
        var data = GetEntries(entries.Data);

        return BinLinker.Pack(data, identifier);
    }

    private static ReadOnlyMemory<byte>[] GetEntries(IReadOnlyList<PogoPoke> entries)
    {
        var valid = entries.Where(z => GetCanTransfer(z.Species, z.Form)).ToArray();
        var result = new ReadOnlyMemory<byte>[valid.Length];
        for (int i = 0; i < result.Length; i++)
            result[i] = GetBinary(valid[i]);
        return result;
    }

    private static byte[] GetBinary(PogoPoke entry)
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);
        entry.Data.RemoveAll(z => z.Type.IsGigantamax);

        bw.Write(entry.Species);
        bw.Write(entry.Form);
        bw.Write((byte)GetGroup(entry.Species, entry.Form));
        foreach (var a in entry.Data)
            Write(a, bw);

        return ms.ToArray();
    }

    private static void Write(PogoEntry entry, BinaryWriter bw)
    {
        bw.Write(entry.Start?.Write(entry.LocalizedStart ? -1 : 0) ?? 0);
        bw.Write(entry.End?.Write(!entry.NoEndTolerance ? 1 : 0) ?? 0);

        byte sg = (byte) (PogoToHex(entry.Shiny) | (PogoToHex(entry.Gender) << 6));
        bw.Write(sg);
        bw.Write((byte)entry.Type);
    }

    public enum PogoImportFormat : byte
    {
        PK7 = 0,
        PB7 = 1,
        PK8 = 2,
        PA8 = 3,
        PK9 = 4,
    }

    private static PogoImportFormat GetGroup(ushort species, byte form) => species switch
    {
        // Transfer Rules:
        // If it can exist in LGP/E, it uses LGP/E's move data for the initial import.
        // Else, if it can exist in SW/SH, it uses SW/SH's move data for the initial import.
        // Else, if it can exist in PLA, it uses PLA move data for the initial import.
        // Else, if it can exist in S/V, it uses S/V move data for the initial import.
        // Else, it must exist in US/UM, thus it uses US/UM for the initial import.

        <= 0151 or 0808 or 0809 when form == 0 || PersonalTable.GG[species].HasForm(form) => PogoImportFormat.PB7,
        <= 0898 when PersonalTable.SWSH.IsPresentInGame(species, form) => PogoImportFormat.PK8,
        <= 0807 when form == 0 || PersonalTable.USUM[species].HasForm(form) => PogoImportFormat.PK7,
        <= 0905 when PersonalTable.LA.IsPresentInGame(species, form) => PogoImportFormat.PA8,
        <= 1025 when PersonalTable.SV.IsPresentInGame(species, form) => PogoImportFormat.PK9,
        <= 0807 => PogoImportFormat.PK7,
        _ => throw new ArgumentOutOfRangeException(nameof(species)),
    };

    private static byte PogoToHex(PogoShiny type) => type switch
    {
        PogoShiny.Random => 0,
        PogoShiny.Never => 1,
        PogoShiny.Always => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

    private static byte PogoToHex(PogoGender type) => type switch
    {
        PogoGender.Random => 2,
        PogoGender.MaleOnly => 0,
        PogoGender.FemaleOnly => 1,
        _ => throw new ArgumentOutOfRangeException(nameof(type)),
    };

    public static byte[] WritePickleLGPE(PogoEncounterList entries)
    {
        var data = GetPickleLGPE(entries);
        return BinLinker.Pack(data, identifier);
    }

    private static ReadOnlyMemory<byte>[] GetPickleLGPE(PogoEncounterList entries)
    {
        var noForm = Enumerable.Range(1, 150).Concat(Enumerable.Range(808, 2)).Select(z => (ushort)z); // count : 152
        var forms = new[]
        {
            (byte)Rattata,
            (byte)Raticate,
            (byte)Raichu,
            (byte)Sandshrew,
            (byte)Sandslash,
            (byte)Vulpix,
            (byte)Ninetales,
            (byte)Diglett,
            (byte)Dugtrio,
            (byte)Meowth,
            (byte)Persian,
            (byte)Geodude,
            (byte)Graveler,
            (byte)Golem,
            (byte)Grimer,
            (byte)Muk,
            (byte)Exeggutor,
            (byte)Marowak,
        };

        var regular = noForm.Select(z => entries.GetDetails(z, 0));
        var alolan = forms.Select(z => entries.GetDetails(z, 1));

        var all = regular.Concat(alolan).ToArray();
        var result = new ReadOnlyMemory<byte>[all.Length];
        for (var i = 0; i < all.Length; i++)
        {
            var entry = all[i];
            entry.Data.RemoveAll(z => z.Type.IsShadow);
            result[i] = GetBinary(entry);
        }

        return result;
    }

    private static bool GetCanTransfer(ushort species, byte form) => (Species)species switch
    {
        Spinda => false,
        Dialga when form is 1 => false,
        Palkia when form is 1 => false,
        Zygarde => false,
        Eternatus => false,
        _ => true,
    };
}
