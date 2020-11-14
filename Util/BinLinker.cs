using System;
using System.IO;

namespace PoGoEncTool
{
    public static class BinLinker
    {
        /// <summary>
        /// Unpacks a BinLinkerAccessor generated file container into individual arrays.
        /// </summary>
        /// <param name="fileData">Packed data</param>
        /// <param name="identifier">Signature expected in the first two bytes (ASCII)</param>
        /// <returns>Unpacked array containing all files that were packed.</returns>
        public static byte[][] Unpack(byte[] fileData, string identifier)
        {
            if (fileData.Length < 4)
                throw new ArgumentException("Bad length", nameof(fileData));

            if (identifier[0] != fileData[0] || identifier[1] != fileData[1])
                throw new ArgumentException("Bad identifier", nameof(identifier));

            int count = BitConverter.ToUInt16(fileData, 2); int ctr = 4;
            int start = BitConverter.ToInt32(fileData, ctr); ctr += 4;
            var result = new byte[count][];
            for (int i = 0; i < count; i++)
            {
                int end = BitConverter.ToInt32(fileData, ctr); ctr += 4;
                int len = end - start;
                byte[] data = new byte[len];
                Buffer.BlockCopy(fileData, start, data, 0, len);
                result[i] = data;
                start = end;
            }
            return result;
        }

        public static byte[] Pack(byte[][] fileData, string identifier)
        {
            // Create new Binary with the relevant header bytes
            byte[] data = new byte[4];
            data[0] = (byte)identifier[0];
            data[1] = (byte)identifier[1];
            Array.Copy(BitConverter.GetBytes((ushort)fileData.Length), 0, data, 2, 2);

            int count = fileData.Length;
            int dataOffset = 4 + 4 + (count * 4);

            // Start the data filling.
            using var dataout = new MemoryStream();
            using var offsetMap = new MemoryStream();
            using var bd = new BinaryWriter(dataout);
            using var bo = new BinaryWriter(offsetMap);
            // For each file...
            for (int i = 0; i < count; i++)
            {
                // Write File Offset
                uint fileOffset = (uint)(dataout.Position + dataOffset);
                bo.Write(fileOffset);

                // Write File to Stream
                bd.Write(fileData[i]);

                // Pad the Data MemoryStream with Zeroes until len%4=0;
                while (dataout.Length % 4 != 0)
                    bd.Write((byte)0);
                // File Offset will be updated as the offset is based off of the Data length.
            }
            // Cap the File
            bo.Write((uint)(dataout.Position + dataOffset));

            using var newPack = new MemoryStream();
            using var header = new MemoryStream(data);
            header.WriteTo(newPack);
            offsetMap.WriteTo(newPack);
            dataout.WriteTo(newPack);
            return newPack.ToArray();
        }
    }
}