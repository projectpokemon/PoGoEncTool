namespace PoGoEncTool
{
    public enum PogoType : byte
    {
        None, // Don't use this.

        /// <summary> Wild encounter, no special requirements </summary>
        Wild,

        /// <summary> Pokémon Egg, requires Lv. 1 and IV = 1 </summary>
        Egg,
        /// <summary> Strange Egg, requires Lv. 8 and IV = 1 </summary>
        EggS,

        /// <summary> Raid Boss, requires Lv. 20 and IV = 1 </summary>
        Raid = 10,
        /// <summary> Raid Boss (Mythical), requires Lv. 20 and IV = 10 </summary>
        RaidM,

        /// <summary> Field Research Reward, requires Lv. 15 and IV = 1 </summary>
        Research = 20,
        /// <summary> Field Research Reward (Mythical), requires Lv. 15 and IV = 10 </summary>
        ResearchM,
        /// <summary> Field Research Reward, requires Lv. 15 and IV = 10 (Poké Ball only) </summary>
        ResearchP,

        /// <summary> GO Battle League Reward, requires Lv. 20 and IV = 1 </summary>
        GBL,
        /// <summary> GO Battle League Reward (Mythical), requires Lv. 20 and IV = 10 </summary>
        GBLM,

        /// <summary> Purified, requires Lv. 8 and IV = 1 (Premier Ball only) </summary>
        Shadow = 30,
    }

    public static class PogoTypeExtensions
    {
        public static bool IsShadow(this PogoType t) => t == PogoType.Shadow;
    }
}
