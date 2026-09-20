using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;

namespace PoGoEncTool.Core;

[Serializable]
[TypeConverter(typeof(PogoDateConverter))]
public sealed record PogoDate(int Year, int Month, int Day) : IComparable<PogoDate>
{
    [JsonPropertyName("Y")] public int Year { get; set; } = Year;
    [JsonPropertyName("M")] public int Month { get; set; } = Month;
    [JsonPropertyName("D")] public int Day { get; set; } = Day;

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
public sealed class PogoDateConverter : ExpandableObjectConverter
{
    private const string Format = "yyyy.MM.dd";

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
    {
        if (value is null)
            return null;
        if (value is not string text)
            return base.ConvertFrom(context, culture, value);
        if (string.IsNullOrWhiteSpace(text))
            return null;

        return DateTime.TryParseExact(
            text,
            Format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var date) ? new PogoDate(date) : throw new FormatException($"Invalid PogoDate '{text}'. Expected format {Format}.");
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is PogoDate date)
            return date.ToString();

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
