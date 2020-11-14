using System;

namespace PoGoEncTool
{
    public class PogoDate
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

        public override string ToString() => $"{Y:0000}.{M:00}.{D:00}";
    }
}
