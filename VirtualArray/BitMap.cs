namespace VirtualArray
{
    public class BitMap : IBitMap
    {
        public int Length { get; private set; }
        public byte[] arr { get; private set; }
        private readonly int SizeOfElement = sizeof(byte) * 8;
        private int shift;

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

                CalculateShift(i);
                return (byte)((arr[i / SizeOfElement] & ShiftValue(0b1)) > 0 ? 1 : 0);
            }
            set
            {
                if (i < 0 || i >= Length)
                    throw new ArgumentOutOfRangeException(nameof(i));

                if (!(value == 0 || value == 1))
                    throw new ArgumentException("Value must be 0 or 1");

                CalculateShift(shift);
                if (value == 1)
                    arr[i / SizeOfElement] |= ShiftValue(0b1);
                else
                    arr[i / SizeOfElement] &= (byte)~ShiftValue(0b1);
            }
        }

        public void Initialize()
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = 0;
        }

        public void Read(FileStream FileStream, BinaryReader Reader, string Signature)
        {
            arr = arr.Select(x => Reader.ReadByte()).ToArray();
        }

        public void Write(FileStream FileStream, BinaryWriter Writer, string Signature)
        {
            Array.ForEach(arr, i => Writer.Write(i));
        }
        private byte ShiftValue(byte value)
        {
            return (byte)(value << shift);
        }

        private void CalculateShift(int index)
        {
            shift =  SizeOfElement - index % SizeOfElement - 1;
        }
    }
}
