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
        public int[] Values { get; set; }

        private void Read(FileStream FileStream, BinaryReader Reader, string Signature)
        { // ПРОБЛЕМА С CHAR
            FileStream.Seek(sizeof(char) * Signature.Length + Number * (Length * sizeof(int) + BitMap.arr.Length * sizeof(bool)), SeekOrigin.Begin);
            BitMap.Read(FileStream, Reader, Signature);

            Values = Values.Select(x => Reader.ReadInt32()).ToArray();
        }

        public void Write(FileStream FileStream, BinaryWriter Writer, string Signature)
        {
            FileStream.Seek(sizeof(char) * Signature.Length + Number * (Length * sizeof(int) + BitMap.arr.Length * sizeof(bool)), SeekOrigin.Begin);
            BitMap.Write(FileStream, Writer, Signature);
            Array.ForEach(Values, i => Writer.Write(i));
            Writer.Flush();
        }

        public void Change(int index, int value)
        {
            if (index >= Length) throw new ArgumentOutOfRangeException(nameof(index));

            Values[index] = value;
            BitMap[index] = 1;
        }

        public void Delete(int index)
        {
            if (index >= Length) throw new ArgumentOutOfRangeException(nameof(index));

            BitMap[index] = 0;
        }

        public Page(FileStream FileStream, BinaryReader Reader, int Length, int Number, string Signature = "VM", DateTime LastCall = new DateTime(), byte IsModified = 0)
        {
            if (Length <= 0) throw new ArgumentException("Length must be positive");
            if (Signature == null) throw new ArgumentException("Signature must not be null");
            if (FileStream == null) throw new ArgumentException("FileStream must not be null");

            Values = new int[Length];
            BitMap = new BitMap(Length);
            this.Length = Length;
            this.Number = Number;
            this.IsModified = IsModified;
            this.LastCall = LastCall;

            Read(FileStream, Reader, Signature);
        }
    }
}
