using System.Collections;
using System.Text;

namespace VirtualArray
{
	public class VirtualArray : IArray
	{
        private readonly int SizeOfElement = sizeof(byte) * 8;
        private readonly byte defaultByteValue = 0b00000000;
		private readonly string Signature = "VM";
		private readonly Stream Stream;
		private readonly BinaryReader Reader;
		private readonly BinaryWriter Writer;

		private IPage[] Pages;
		private int PageNumber;
		private int PageCapacity;

		public long Length { get; private set; }

		public VirtualArray(Stream stream, long length, bool isFileExist, int pageNumber = 3, int pageCapacity = 512)
		{
			if (pageNumber < 3)
				throw new ArgumentException("Number of pages must be >= 3");
			PageNumber = pageNumber;
			Pages = new Page[PageNumber];

			if (pageCapacity < 1)
				throw new ArgumentException("Page capacity must be >= 1");
			PageCapacity = pageCapacity;

			if (length <= 0)
				throw new ArgumentException("Length must not be negative");
			Length = length;
			Stream = stream;
			Stream.Seek(0, SeekOrigin.Begin);

			Reader = new BinaryReader(Stream);
			Writer = new BinaryWriter(Stream);

			if (isFileExist)
			{
				ReadFileProperties();
				CheckSignature();
			}
			else
			{
				InitializeFile();
			}	
		}

        private void ReadFileProperties()
        {
			Stream.Seek(sizeof(char) * Signature.Length, SeekOrigin.Begin);
			Length = Reader.ReadInt64();
			PageCapacity = Reader.ReadInt32();
        }

        public int this[long i]
		{
			get
			{
				int index = GetPageIndex(i);

				if (Pages[index].BitMap[(int)(i % PageCapacity)] == 0)
					throw new ArgumentException("The element is empty");

				Pages[index].LastCall = DateTime.Now;
				return Pages[index].Values[i % PageCapacity];
			}
			set
			{
				int index = GetPageIndex(i);
				Pages[index].Change((int)(i % PageCapacity), value);
				Pages[index].IsModified = true;
				Pages[index].LastCall = DateTime.Now;
				Pages[index].BitMap[(int)(i % PageCapacity)] = 1;
			}
		}

		public void Delete(long index)
		{
			int i = GetPageIndex(index);
			Pages[i].IsModified = true;
			Pages[i].LastCall = DateTime.Now;
			Pages[i].BitMap[(int)(index % PageCapacity)] = 0;
		}

		public bool IsEmpty(long index)
		{
			int i = GetPageIndex(index);
			return Pages[i].BitMap[(int)(index % PageCapacity)] == 0 ? true : false;
		}

		public bool IsFilled(long index)
		{
			return !IsEmpty(index);
		}

		public void Dispose()
		{
			foreach (Page page in Pages)
				if (page != null && page.IsModified)
					page.Write(Stream, Writer, Signature);
			Writer.Flush();
			Stream.Close();
		}

        private int GetPageIndex(long elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= Length)
                throw new ArgumentOutOfRangeException(nameof(elementIndex));

            long globalPageIndex = elementIndex / PageCapacity;
            int nullPageIndex = -1, oldestCallPageIndex = -1;
            DateTime oldestCallDate = DateTime.MaxValue;

            for (int i = 0; i < Pages.Length; i++)
            {
                if (Pages[i] == null)
                    nullPageIndex = i;
                else if (Pages[i].Number == globalPageIndex)
                    return i;
                else if (Pages[i].LastCall < oldestCallDate)
                {
                    oldestCallDate = Pages[i].LastCall;
                    oldestCallPageIndex = i;
                }
            }

            if (nullPageIndex != -1)
            {
				LoadPage(nullPageIndex, globalPageIndex);
                return nullPageIndex;
            }

            LoadPage(oldestCallPageIndex, globalPageIndex);

            return oldestCallPageIndex;
        }

        private void LoadPage(int index, long globalPageNumber)
		{
			if (Pages[index] != null && Pages[index].IsModified)
				Pages[index].Write(Stream, Writer, Signature);

			Pages[index] = new Page(Stream, Reader, PageCapacity, globalPageNumber);
		}

		private void InitializeFile()
		{
			int numberOfPages = (int)Math.Ceiling((double)Length / PageCapacity);

            foreach (char c in Encoding.Unicode.GetBytes(Signature))
				Writer.Write(c);

            Writer.Write(Length);
            Writer.Write(PageCapacity);

            for (int i = 0; i < numberOfPages; i++)
			{
				InitializeBitmap();
				InitializePageValues();
			}

			Writer.Flush();
		}

		private void InitializePageValues()
		{
            for (int j = 0; j < PageCapacity; j++)
                Writer.Write(0);
        }

		private void InitializeBitmap()
		{
			int numberOfBytes = (int)Math.Ceiling((double)PageCapacity / SizeOfElement);
            for (int j = 0; j < numberOfBytes; j++)
                Writer.Write((byte)defaultByteValue);
        }

		private void CheckSignature()
		{
			Stream.Seek(0, SeekOrigin.Begin);
			foreach (char c in Signature)
				if (c != BitConverter.ToChar(Reader.ReadBytes(2)))
					throw new FileLoadException("Bad signature");
		}

		public ArrayEnumerator GetEnumerator() 
		{
			return new ArrayEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)GetEnumerator();
		}
	}
}
