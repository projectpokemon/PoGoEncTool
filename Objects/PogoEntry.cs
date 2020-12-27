using System;

namespace PoGoEncTool
{
    [Serializable]
    public class PogoEntry : IComparable<PogoEntry>, IEquatable<PogoEntry>
    {
        public PogoDate? Start { get; set; }
        public PogoDate? End { get; set; }
        public PogoShiny Shiny { get; set; } = PogoShiny.Never;
        public PogoType Type { get; set; }
        public bool LocalizedStart { get; set; }
        public bool NoEndTolerance { get; set; }
        public string Comment { get; set; } = string.Empty;

        public static PogoEntry CreateNew() => new()
        {
            Start = PogoDate.CreateNew(),
            End = null,
            Shiny = PogoShiny.Never,
            Type = PogoType.Wild,
        };

        public override string ToString() => $"[{Start?.ToString() ?? "X"}-{End?.ToString() ?? "X"}]: {Type} {{{Shiny}}} - {Comment}";

        public int CompareTo(PogoEntry? p)
        {
            if (p == null)
                return 1;

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

            if (Type != p.Type)
                return Type.CompareTo(p.Type);

            if (Shiny != p.Shiny)
                return Shiny.CompareTo(p.Shiny);

            return string.Compare(Comment, p.Comment, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(PogoEntry? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Start, other.Start) && Equals(End, other.End) && Shiny == other.Shiny && Type == other.Type && Comment == other.Comment;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PogoEntry) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End, (int) Shiny, (int) Type, Comment);
        }
    }
}
