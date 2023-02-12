﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
    public static class Program
    {
        public static void Main()
        {
            BitMap BitMap = new BitMap(50);
            BitMap.Initialize();

            Console.WriteLine(BitMap[0]);
            Console.WriteLine(BitMap[3]);
            Console.WriteLine(BitMap[49]);

            BitMap[0] = 1;
            //BitMap[0] = 0;

            BitMap[3] = 1;
            BitMap[3] = 0;

            BitMap[9] = 1;
            //BitMap[9] = 0;

            BitMap[49] = 1;
            //BitMap[49] = 0;

            BitMap.Print();

            Console.WriteLine(BitMap[0]);
            Console.WriteLine(BitMap[3]);
            try
            {
                Console.WriteLine(BitMap[499]);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            try
            {
                BitMap[49] = 5;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}