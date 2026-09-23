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

    internal const int FirstDay = 736150 - 1; // 2016-07-06 (Launch Day)
    private ushort Write() => GetRelativeDay(new DateOnly(Year, Month, Day).DayNumber);
    private static ushort GetRelativeDay(int dayNumber) => (ushort)(dayNumber - FirstDay);
    public ushort Write(in int delta) => (ushort)(Write() + delta);

    public int CompareTo(PogoDate? p) => Write().CompareTo(p?.Write());
}
