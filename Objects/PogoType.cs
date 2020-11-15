namespace PoGoEncTool
{
    public enum PogoType : byte
    {
        None, // Don't use this.

        Wild,
        Egg,

        /// <summary> Raid Boss, requires Lv. 15 and IV=1 </summary>
        Raid15 = 10,
        /// <summary> Raid Boss, requires Lv. 20 and IV=10 </summary>
        Raid20,

        /// <summary> Field Research, requires Lv. 15 and IV=1 </summary>
        Field15 = 20,
        /// <summary> Field Research, requires Lv. 15 and IV=10 (Mythical) </summary>
        FieldM,
        /// <summary> Field Research, requires Lv. 15 and IV=10 (Mythical, Poké Ball only) </summary>
        FieldP,

        /// <summary> Purified, requires Lv. 8 and IV=1 (Premier Ball) </summary>
        Shadow = 30,
        /// <summary> Purified, requires Lv. 8 and IV=1 (No Premier Ball) </summary>
        ShadowPGU,
    }
}
