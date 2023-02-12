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

        }

        private void FindPageNumber()
        {

        }

        private void InitializeFile()
        {

        }

        public VirtualArray(long length, string fileName = "default.bin", int pageNumber = 3, int pageCapacity = 512)
        {
            if (fileName == null || fileName == "") throw new ArgumentNullException("Bad name of file");
            FileName = fileName;

            if (pageNumber < 3) throw new ArgumentException("Number of pages must be >= 3");
            PageNumber = pageNumber;
            Pages = new Page[PageNumber];

            if (pageCapacity < 1) throw new ArgumentException("Page capacity must be >= 1");
            PageCapacity = pageCapacity;

            bool IsFileExist = File.Exists(FileName);

            FileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream.Seek(0, SeekOrigin.Begin);

            Reader = new BinaryReader(FileStream);
            Writer = new BinaryWriter(FileStream);

            if (IsFileExist)
            {

            }
            else
            {
                InitializeFile();
            }
        }
    }
}
