using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
    public static class Program
    {
        public static void Main()
        {
            VirtualArray arr = new VirtualArray(9);

            //FileStream FileStream = new FileStream("1.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //BinaryReader Reader = new BinaryReader(FileStream);
            //BinaryWriter Writer = new BinaryWriter(FileStream);

            //VirtualArray arr = new VirtualArray(3);
            //arr.Delete(152);

            //BitMap bitMap = new BitMap(16);
            //FileStream.Seek(1, SeekOrigin.Begin);
            //bitMap.Read(FileStream, Reader, "");

            //Page page = new Page(FileStream, Reader, 9, 0);
            //page.BitMap.Print();
            //page.Values[2] = 0;
            //page.Write(FileStream, Writer, "VM");
            //Console.WriteLine($"{page.Values[0]} {page.Values[1]}");
            //FileStream.Close();

            //BitMap BitMap = new BitMap(50);
            //BitMap.Initialize();

            //Console.WriteLine(BitMap[0]);
            //Console.WriteLine(BitMap[3]);
            //Console.WriteLine(BitMap[49]);

            //BitMap[0] = 1;
            //BitMap[0] = 0;

            //BitMap[3] = 1;
            //BitMap[3] = 0;

            //BitMap[9] = 1;
            //BitMap[9] = 0;

            //BitMap[49] = 1;
            //BitMap[49] = 0;

            //bitMap.Print();

            //Console.WriteLine(BitMap[0]);
            //Console.WriteLine(BitMap[3]);
            //try
            //{
            //    Console.WriteLine(BitMap[499]);
            //}
            //catch (Exception ex) {
            //    Console.WriteLine(ex.Message);
            //}

            //try
            //{
            //    BitMap[49] = 5;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}