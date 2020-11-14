namespace PoGoEncTool
{
    public class PogoEntry
    {
        public PogoDate? Start { get; set; }
        public PogoDate? End { get; set; }

        public bool Shiny { get; set; }

        public PogoType Type { get; set; }

        public string Comment { get; set; } = string.Empty;

        public static PogoEntry CreateNew() => new PogoEntry
        {
            Start = PogoDate.CreateNew(),
            End = null,
            Shiny = false,
            Type = PogoType.Wild,
        };

        public override string ToString() => $"[{Start?.ToString() ?? "X"}-{End?.ToString() ?? "X"}]: {(Shiny ? "*" : "")}{Type} - {Comment}";
    }
}
