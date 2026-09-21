using System;
using System.ComponentModel;
using System.Globalization;

namespace PoGoEncTool.Core;

public sealed class PogoDateValueConverter : ExpandableObjectConverter
{
    private const string Format = "yyyy.M.d";

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
