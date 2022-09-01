using System;
using System.Linq;

namespace PoGoEncTool.Core;

public static class BinLinker
{
    /// <summary>
    /// Creates a single output binary with serialized information.
    /// </summary>
    /// <param name="fileData">Serialized data entries</param>
    /// <param name="identifier">Magic 2 character prefix to indicate the file's signature.</param>
    /// <returns>Single binary packed together.</returns>
    public static byte[] Pack(ReadOnlyMemory<byte>[] fileData, string identifier)
    {
        // Create new Binary with the relevant header bytes
        int ofs = 4 + ((fileData.Length + 1) * sizeof(int));
        int length = ofs + fileData.Sum(f => (f.Length + 3) & ~3);
        byte[] data = new byte[length];
        var m = new Memory<byte>(data);
        var s = m.Span;
        data[0] = (byte)identifier[0];
        data[1] = (byte)identifier[1];
        BitConverter.TryWriteBytes(s[2..], (ushort)fileData.Length);

        // For each file...
        for (int i = 0; i < fileData.Length; i++)
        {
            var f = fileData[i];
            BitConverter.TryWriteBytes(s[(4 + (i * sizeof(int)))..], ofs);
            f.CopyTo(m[ofs..]);
            ofs += (f.Length + 3) & ~3;
        }
        // Cap the File
        BitConverter.TryWriteBytes(s[(4 + (fileData.Length * sizeof(int)))..], ofs);

        return data;
    }
}
