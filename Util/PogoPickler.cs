using System.Collections.Generic;
using System.IO;

namespace PoGoEncTool
{
    public static class PogoPickler
    {
        public static byte[] GetPickle(PogoEncounterList entries)
        {
            var data = GetEntries(entries);
            return BinLinker.Pack(data, "go");
        }

        private static byte[][] GetEntries(IReadOnlyList<PogoPoke> entries)
        {
            var result = new byte[entries.Count][];
            for (int i = 0; i < entries.Count; i++)
                result[i] = GetBinary(entries[i]);
            return result;
        }

        private static byte[] GetBinary(PogoPoke entry)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write(entry.Species | (entry.Form << 11));
            foreach (var a in entry.Available)
                Write(a, bw);

            return ms.ToArray();
        }

        private static void Write(PogoEntry entry, BinaryWriter bw)
        {
            bw.Write(entry.Start?.Write() ?? 0);
            bw.Write(entry.End?.Write() ?? 0);
            bw.Write((byte)(entry.Shiny ? 1 : 0));
            bw.Write((byte)entry.Type);
        }
    }
}
