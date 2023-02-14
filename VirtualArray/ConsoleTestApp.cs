using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
    public static class ConsoleTestApp
    {
        public static void Run()
        {
            IVirtualArray arr = null;
            try
            {
                string filename;
                int numberOfPages, pageCapacity;

                Console.Write("Array length: ");
                int length = int.Parse(Console.ReadLine());

                Console.Write("Use default settings? (1, 0): ");
                if (Console.ReadLine() == "1")
                {
                    arr = new VirtualArray(length);
                }
                else
                {
                    Console.Write("Filename: ");
                    filename = Console.ReadLine();
                    Console.Write("Number of pages: ");
                    numberOfPages = int.Parse(Console.ReadLine());
                    Console.Write("Page capacity: ");
                    pageCapacity = int.Parse(Console.ReadLine());

                    arr = new VirtualArray(length, numberOfPages, pageCapacity, filename);
                }

                Console.Clear();

                Console.WriteLine("MENU\n1 - Set value\n2 - Get value\n3 - Delete value\n4 - Save and exit");
                bool flag = true;
                while (flag)
                {
                    int index, value;

                    Console.Write("Option: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Write("Index: ");
                            index = int.Parse(Console.ReadLine());
                            Console.Write("Value: ");
                            value = int.Parse(Console.ReadLine());
                            arr[index] = value;
                            break;

                        case "2":
                            Console.Write("Index: ");
                            Console.WriteLine(arr[int.Parse(Console.ReadLine())]);
                            break;

                        case "3":
                            Console.Write("Index: ");
                            index = int.Parse(Console.ReadLine());
                            arr.Delete(index);
                            break;

                        case "4":
                            arr.Dispose();
                            flag = false;
                            break;
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
