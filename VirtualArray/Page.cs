using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
    public class Page : IPage
    {
        public int Length { get; private set; }

        private long number;
        public long Number
        {
            get { return number; }
            set
            {
                if (value < 0) throw new ArgumentException("Number must be positive");
                number = value;
            }
        }

        private byte isModified;
        public byte IsModified
        {
            get { return isModified; }
            set
            {
                if (!(value == 0 || value == 1)) throw new ArgumentException("Value must be 0 ot 1");
                isModified = value;
            }
        }

        public DateTime LastCall { get; set; }
        public IBitMap BitMap { get; set; }
        public int[] Array { get; set; }

        private void Read(FileStream FileStream, BinaryReader Reader, string Signature)
        {
            FileStream.Seek(sizeof(char) * Signature.Length + Number * (Length * (sizeof(int) + sizeof(bool))), SeekOrigin.Begin);
            BitMap.Read(FileStream, Reader, Signature);

            Array = Array.Select(x => Reader.ReadInt32()).ToArray();
        }

        public void Write(FileStream FileStream, BinaryReader Reader, string Signature)
        {
            FileStream.Seek(sizeof(char) * Signature.Length + Number * (Length * (sizeof(int) + sizeof(bool))), SeekOrigin.Begin);
            BitMap.Read(FileStream, Reader, Signature);

            Array = Array.Select(x => Reader.ReadInt32()).ToArray();
        }

        public Page(FileStream FileStream, BinaryReader Reader, int Length, int Number, string Signature = "VM", DateTime LastCall = new DateTime(), byte IsModified = 0)
        {
            if (Length <= 0) throw new ArgumentException("Length must be positive");
            if (Signature == null) throw new ArgumentException("Signature must not be null");
            if (FileStream == null) throw new ArgumentException("FileStream must not be null");

            Array = new int[Length];
            BitMap = new BitMap(Length);
            this.Length = Length;
            this.Number = Number;
            this.IsModified = IsModified;
            this.LastCall = LastCall;

            Read(FileStream, Reader, Signature);
        }
    }
}
