using System.Collections.Generic;
using System.IO;

namespace PoGoEncTool
{
    public static class PogoPickler
    {
        private const string identifier = "go";

        public static PogoEncounterList ReadPickle(byte[] pickle)
        {
            var data = BinLinker.Unpack(pickle, identifier);
            var result = new PogoEncounterList();
            foreach (var entry in data)
                result.Add(GetPogoPoke(entry));
            return result;
        }

        private static PogoPoke GetPogoPoke(byte[] entry)
        {
            using var ms = new MemoryStream(entry);
            using var br = new BinaryReader(ms);

            var sf = br.ReadInt32();
            var result = new PogoPoke
            {
                Species = sf & 0x7FFF,
                Form = sf >> 11,
            };

            var count = (entry.Length - 2) / WriteSize;
            for (int i = 0; i < count; i++)
                result.Add(ReadAppearance(br));
            return result;
        }

        public static byte[] WritePickle(PogoEncounterList entries)
        {
            var data = GetEntries(entries);
            return BinLinker.Pack(data, identifier);
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
            foreach (var a in entry)
                Write(a, bw);

            return ms.ToArray();
        }

        private const int WriteSize = (2 * sizeof(int)) + 2;

        private static PogoEntry ReadAppearance(BinaryReader br)
        {
            var start = br.ReadInt32();
            var end = br.ReadInt32();
            var shiny = br.ReadByte();
            var type = br.ReadByte();

            return new PogoEntry
            {
                Shiny = shiny == 1,
                Type = (PogoType) type,
                Start = start == 0 ? null : new PogoDate(start),
                End = end == 0 ? null : new PogoDate(end),
            };
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
