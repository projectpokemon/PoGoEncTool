using System;

namespace PoGoEncTool
{
    public class PogoEntry : IComparable
    {
        public PogoDate? Start { get; set; }
        public PogoDate? End { get; set; }
        public PogoShiny Shiny { get; set; } = PogoShiny.Never;
        public PogoType Type { get; set; }
        public string Comment { get; set; } = string.Empty;

        public static PogoEntry CreateNew() => new PogoEntry
        {
            Start = PogoDate.CreateNew(),
            End = null,
            Shiny = PogoShiny.Never,
            Type = PogoType.Wild,
        };

        public override string ToString() => $"[{Start?.ToString() ?? "X"}-{End?.ToString() ?? "X"}]: {Type} {{{Shiny}}} - {Comment}";

        public int CompareTo(PogoEntry p)
        {
            if (p.End != null)
            {
                if (End == null)
                    return 1;
                var date = End.CompareTo(p.End);
                if (date != 0)
                    return date;
            }
            else
            {
                if (End != null)
                    return -1;
            }

            if (p.Start != null)
            {
                if (Start == null)
                    return 1;
                var date = Start.CompareTo(p.Start);
                if (date != 0)
                    return date;
            }
            else
            {
                if (Start != null)
                    return -1;
            }

            if (Type != p.Type)
                return Type.CompareTo(p.Type);

            if (Shiny != p.Shiny)
                return Shiny.CompareTo(p.Shiny);

            return string.Compare(Comment, p.Comment, StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(object? obj)
        {
            if (!(obj is PogoEntry p))
                return 1;
            return CompareTo(p);
        }
    }
}
