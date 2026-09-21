using System;
using System.ComponentModel;

namespace PoGoEncTool.Core;

[Serializable]
[TypeConverter(typeof(PogoDateValueConverter))]
public sealed record PogoDate(int Year, int Month, int Day) : IComparable<PogoDate>
{
    public int Year { get; set; } = Year;
    public int Month { get; set; } = Month;
    public int Day { get; set; } = Day;

    public static PogoDate CreateNew() => new(DateTime.Now);
    public PogoDate(in DateTime value) : this(value.Year, value.Month, value.Day) { }
    public PogoDate(in int value) : this(value >> 16, (value >> 8) & 0xFF, value & 0xFF) { }
    public PogoDate() : this((2000 << 16) | (1 << 8) | 1) { }

    public override string ToString() => $"{Year:0000}.{Month:00}.{Day:00}";

    public static DateTime GetDateTime(PogoDate? date) => date == null ? DateTime.Now : new DateTime(date.Year, date.Month, date.Day);

    public int Write() => (Year << 16) | (Month << 8) | Day;

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
