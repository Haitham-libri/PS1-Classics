﻿using CTRFramework.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CTRFramework.Sound
{
    public class Xinf : IRead
    {
        public string magic = "XINF";
        private int version = 0x66;

        private int numGroups = 0;
        private int numSkipInts = 0;

        private int[] numXaFiles = new int[3];
        private int[] unk2 = new int[3];
        private int[] numEntries = new int[3];
        private int[] startIndex = new int[3];

        public List<uint> Entries = new List<uint>();

        public Xinf()
        {
        }

        public Xinf(BinaryReaderEx br)
        {
            Read(br);
        }

        public static Xinf FromReader(BinaryReaderEx br)
        {
            return new Xinf(br);
        }

        /// <summary>
        /// Creates XINF instance from file.
        /// </summary>
        /// <param name="filename">Source file name.</param>
        /// <returns></returns>
        public static Xinf FromFile(string filename)
        {
            using (BinaryReaderEx br = new BinaryReaderEx(File.OpenRead(filename)))
            {
                return Xinf.FromReader(br);
            }
        }

        /// <summary>
        /// Read VAG data from stream using binary reader.
        /// </summary>
        /// <param name="br">BinaryReaderEx instance.</param>
        public void Read(BinaryReaderEx br)
        {
            magic = new string(br.ReadChars(4));

            if (magic != "XINF")
                Helpers.Panic(this, PanicType.Error, $"No XINF found. Not a XINF file: magic = {magic}");

            version = br.ReadInt32();
            numGroups = br.ReadInt32();
            numSkipInts = br.ReadInt32();
            int numTotalEntries = br.ReadInt32();

            for (int i = 0; i < numGroups; i++)
                numXaFiles[i] = br.ReadInt32();

            for (int i = 0; i < numGroups; i++)
                unk2[i] = br.ReadInt32();

            for (int i = 0; i < numGroups; i++)
                numEntries[i] = br.ReadInt32();

            for (int i = 0; i < numGroups; i++)
                startIndex[i] = br.ReadInt32();

            br.Seek(numSkipInts * 4);

            for (int i = 0; i < numTotalEntries; i++)
                Entries.Add(br.ReadUInt32());
        }

        /// <summary>
        /// Saves XINF to file.
        /// </summary>
        /// <param name="filename">Target file name.</param>
        public void Save(string filename)
        {
            using (BinaryWriterEx bw = new BinaryWriterEx(File.Create(filename)))
            {
                Write(bw);
            }
        }

        /// <summary>
        /// Writes XINF data to stream using binary writer.
        /// </summary>
        /// <param name="bw">BinaryWriterEx object.</param>
        public void Write(BinaryWriterEx bw)
        {
            throw new NotImplementedException("Unimplemented.");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(magic);
            sb.AppendLine($"Version: {version}");
            sb.AppendLine($"numGroups: {numGroups}");
            sb.AppendLine("---");

            foreach (var en in Entries)
                sb.AppendLine(en.ToString("X8"));

            return sb.ToString();
        }
    }
}