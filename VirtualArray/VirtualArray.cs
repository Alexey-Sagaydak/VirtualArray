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

		public int this[int i]
		{
			get
			{
				return 1;
			}
			set
			{
				
			}
		}

		public void Delete(int index)
		{
			Console.WriteLine(Pages[0] == null);
		}

		private void FindPageNumber()
		{

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

		public VirtualArray(long length, int pageNumber = 3, int pageCapacity = 512, string fileName = "default.bin")
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
