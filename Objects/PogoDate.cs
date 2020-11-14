using System;

namespace PoGoEncTool
{
    public class PogoDate : IComparable
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

        public int Write() => (Y << 16) | (M << 8) | D;

        public int CompareTo(PogoDate p) => Write().CompareTo(p.Write());

        public int CompareTo(object? obj)
        {
            if (!(obj is PogoDate p))
                return 1;
            return CompareTo(p);
        }
    }
}
