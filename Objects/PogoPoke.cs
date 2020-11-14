namespace PoGoEncTool
{
    public class PogoPoke
    {
        public int Species { get; set; }
        public int Form { get; set; }

        public PogoAppearanceList Available { get; set; } = new PogoAppearanceList();

        public static PogoPoke CreateNew(int species, int form) => new PogoPoke
        {
            Species = species,
            Form = form,
            Available = new PogoAppearanceList(),
        };
    }
}
