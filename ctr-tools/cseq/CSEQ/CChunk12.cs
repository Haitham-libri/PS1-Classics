﻿using System.Text;
using System.IO;

namespace cseq
{
    public struct CChunk12
    {
        public byte[] data;

        public void Read(BinaryReader br)
        {
            data = br.ReadBytes(12);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");

            return sb.ToString() + "\r\n";
        }

    }
}