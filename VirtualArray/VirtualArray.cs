using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
	public class VirtualArray
	{
		public readonly string Signature = "VM";
		private readonly FileStream FileStream;
		private readonly BinaryReader Reader;
		private readonly BinaryWriter Writer;

		private IPage[] Pages;
		private int PageNumber;
		private int PageCapacity;

		private string FileName;
		public long Length { get; private set; }

		public int this[long i]
		{
			get
			{
				int index = FindPageNumber(i);
                Pages[index].LastCall = DateTime.Now;
                return Pages[index].Values[i % PageCapacity];
			}
			set
			{
                int index = FindPageNumber(i);
				Pages[index].Change((int)(i % PageCapacity), value);
				Pages[index].IsModified = true;
				Pages[index].LastCall = DateTime.Now;
				Pages[index].BitMap[(int)(i % PageCapacity)] = 1;
            }
		}

		public void Delete(int index)
		{
            int i = FindPageNumber(index);
            Pages[index].IsModified = true;
            Pages[index].LastCall = DateTime.Now;
			Pages[i].BitMap[(int)(index % PageCapacity)] = 0;
        }

		private int FindPageNumber(long index)
		{
			if (index < 0 || index >= Length)
				throw new ArgumentOutOfRangeException(nameof(index));

			long pageNumber = index / PageCapacity;
			int nullPageIndex = -1, oldestCallPageIndex = -1;
			DateTime oldestCallDate = DateTime.MaxValue;

			for (int i = 0; i < Pages.Length; i++)
			{
				if (Pages[i] == null) nullPageIndex = i;
				else if (Pages[i].Number == pageNumber) return i;
				else if (Pages[i].LastCall < oldestCallDate)
				{
					oldestCallDate = Pages[i].LastCall;
					oldestCallPageIndex = i;
				}
			}

			if (nullPageIndex != -1) return nullPageIndex;

			if (Pages[oldestCallPageIndex].IsModified)
				Pages[oldestCallPageIndex].Write(FileStream, Writer, Signature);

			Pages[oldestCallPageIndex] = new Page(FileStream, Reader, PageCapacity, pageNumber);

			return oldestCallPageIndex;
		}

		private void InitializeFile()
		{
			foreach (char c in Encoding.Unicode.GetBytes(Signature)) Writer.Write(c);

			for (int i = 0; i < (int)Math.Ceiling((double)Length / PageCapacity); i++)
			{
				for (int j = 0; j < (int)Math.Ceiling((double)Length / 8); j++) Writer.Write((byte)0b11111111);
				for (int j = 0; j < Length; j++) Writer.Write(0);
			}

			Writer.Flush();
		}

		private void CheckSignature()
		{
			foreach (char c in Signature)
				if (c != BitConverter.ToChar(Reader.ReadBytes(2))) throw new FileLoadException("Bad signature");
		}

		public VirtualArray(long length, int pageNumber = 3, int pageCapacity = 512, string fileName = "array.bin")
		{
			if (fileName == null || fileName == "") throw new ArgumentNullException("Bad name of file");
			FileName = fileName;

			if (pageNumber < 3) throw new ArgumentException("Number of pages must be >= 3");
			PageNumber = pageNumber;
			Pages = new Page[PageNumber];

			if (pageCapacity < 1) throw new ArgumentException("Page capacity must be >= 1");
			PageCapacity = pageCapacity;

			if (length <= 0) throw new ArgumentException("Length must not be negative");
			Length = length;

			bool IsFileExist = File.Exists(FileName);

			FileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			FileStream.Seek(0, SeekOrigin.Begin);

			Reader = new BinaryReader(FileStream);
			Writer = new BinaryWriter(FileStream);

			if (IsFileExist)
				CheckSignature();
			else
				InitializeFile();
		}
	}
}
