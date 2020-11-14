namespace PoGoEncTool
{
    public enum PogoType : byte
    {
        None, // Don't use this.

        Wild,
        Egg,

        /// <summary> requires level 15 and IV=1 </summary>
        Raid15 = 10,
        /// <summary> requires level 20 and IV=10 </summary>
        Raid20,

        /// <summary> requires level 15 and IV=10 </summary>
        Field15 = 20,

        Shadow = 30,
    }
}
