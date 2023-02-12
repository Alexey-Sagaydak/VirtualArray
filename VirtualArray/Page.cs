using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
    public class Page : IPage
    {
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

        public Page(int Length, int Number, DateTime LastCall = new DateTime(), byte IsModified = 0)
        {
            if (Length < 0) throw new ArgumentException("Length must not be negative");

            Array = new int[Length];
            BitMap = new BitMap(Length);
            this.Number = Number;
            this.IsModified = IsModified;
            this.LastCall = LastCall;
        }
    }
}
