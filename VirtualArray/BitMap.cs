namespace VirtualArray
{
    public class BitMap : IBitMap
    {
        public int Length { get; private set; }
        public byte[] arr { get; private set; }
        private readonly int SizeOfElement = sizeof(byte) * 8;

        public BitMap(int Length)
        {
            if (Length >= 0)
                this.Length = Length;
            else
                throw new ArgumentException("Length must not be negative");

            arr = new byte[(int)Math.Ceiling((double)Length / SizeOfElement)];
        }

        public byte this[int i]
        {
            get
            {
                if (i < 0 || i >= Length)
                    throw new ArgumentOutOfRangeException(nameof(i));

                int shift = CalculateShift(i);
                return (byte)((arr[i / SizeOfElement] & ShiftValue(0b1, shift)) > 0 ? 1 : 0);
            }
            set
            {
                if (i < 0 || i >= Length)
                    throw new ArgumentOutOfRangeException(nameof(i));

                if (!(value == 0 || value == 1))
                    throw new ArgumentException("Value must be 0 or 1");
                
                int shift = SizeOfElement - i % SizeOfElement - 1;
                if (value == 1)
                    arr[i / SizeOfElement] |= ShiftValue(0b1, shift);
                else
                    arr[i / SizeOfElement] &= (byte)(~ShiftValue(0b1, shift));
            }
        }

        public void Initialize()
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = 0;
        }

        public void Read(Stream Stream, BinaryReader Reader, string Signature)
        {
            arr = arr.Select(x => Reader.ReadByte()).ToArray();
        }

        public void Write(Stream Stream, BinaryWriter Writer, string Signature)
        {
            Array.ForEach(arr, i => Writer.Write(i));
        }
        private byte ShiftValue(byte value, int shift)
        {
            return (byte)(value << shift);
        }

        private int CalculateShift(int index)
        {
            return SizeOfElement - index % SizeOfElement - 1;
        }
    }
}
