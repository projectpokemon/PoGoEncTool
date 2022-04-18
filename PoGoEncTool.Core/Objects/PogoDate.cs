using System;

namespace PoGoEncTool.Core;

[Serializable]
public sealed record PogoDate(int Y, int M, int D) : IComparable<PogoDate>
{
    public static PogoDate CreateNew() => new(DateTime.Now);
    public PogoDate(in DateTime value) : this(value.Year, value.Month, value.Day) { }
    public PogoDate(in int value) : this(value >> 16, (value >> 8) & 0xFF, value & 0xFF) { }
    public PogoDate() : this((2000 << 16) | (1 << 8) | 1) { }

    public override string ToString() => $"{Y:0000}.{M:00}.{D:00}";

    public static DateTime GetDateTime(PogoDate? date) => date == null ? DateTime.Now : new DateTime(date.Y, date.M, date.D);

    public int Write() => (Y << 16) | (M << 8) | D;

    public int Write(in int delta)
    {
        if (delta == 0)
            return Write();

        var date = GetDateTime(this);
        var update = date.AddDays(delta);
        var obj = new PogoDate(update);
        return obj.Write();
    }

    public int CompareTo(PogoDate? p) => Write().CompareTo(p?.Write());
}
