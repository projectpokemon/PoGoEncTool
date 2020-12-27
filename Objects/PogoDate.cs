using System;

namespace PoGoEncTool
{
    [Serializable]
    public class PogoDate : IComparable, IEquatable<PogoDate>
    {
        public int Y { get; set; }
        public int M { get; set; }
        public int D { get; set; }

        public PogoDate() { }

        public static PogoDate CreateNew()
        {
            var date = DateTime.Now;
            return new PogoDate { Y = date.Year, M = date.Month, D = date.Day };
        }

        public PogoDate(in DateTime value)
        {
            Y = value.Year;
            M = value.Month;
            D = value.Day;
        }

        public PogoDate(in int value)
        {
            Y = value >> 16;
            M = ((value >> 8) & 0xFF);
            D = value & 0xFF;
        }

        public override string ToString() => $"{Y:0000}.{M:00}.{D:00}";

        public static DateTime GetDateTime(PogoDate? date) => date == null ? DateTime.Now : new DateTime(date.Y, date.M, date.D);

        public int Write() => (Y << 16) | (M << 8) | D;

        public int Write(int delta)
        {
            if (delta == 0)
                return Write();

            var date = GetDateTime(this);
            var update = date.AddDays(delta);
            var obj = new PogoDate(update);
            return obj.Write();
        }

        public int CompareTo(PogoDate p) => Write().CompareTo(p.Write());

        public int CompareTo(object? obj)
        {
            if (obj is not PogoDate p)
                return 1;
            return CompareTo(p);
        }

        public bool Equals(PogoDate? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Y == other.Y && M == other.M && D == other.D;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not PogoDate p) return false;
            return Equals(p);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Y, M, D);
        }
    }
}
