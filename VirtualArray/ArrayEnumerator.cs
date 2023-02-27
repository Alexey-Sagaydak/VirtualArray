using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArray
{
    public class ArrayEnumerator : IEnumerator 
    {
        public VirtualArray array;

        int position = -1;

        public ArrayEnumerator(VirtualArray _array)
        {
            array = _array;
        }

        public bool MoveNext()
        {
            position++;
            return (position < array.Length - 1);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public int Current
        {
            get
            {
                try
                {
                    while (array.IsEmpty(position))
                        if (!MoveNext())
                            throw new IndexOutOfRangeException(); 
                           
                    if (array.IsFilled(position))
                        return array[position];
                    return 0;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
