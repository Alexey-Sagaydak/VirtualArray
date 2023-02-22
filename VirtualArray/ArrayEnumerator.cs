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
            return (position < array.Length);
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
                        MoveNext();
                    return array[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
