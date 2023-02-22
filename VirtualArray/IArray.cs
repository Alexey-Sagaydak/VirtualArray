using System.Collections;

namespace VirtualArray
{
    public interface IArray: IDisposable, IEnumerable
    {
        int this[long i] { get; set; }
        long Length { get; }
        void Delete(long index);
        bool IsEmpty(long index);
        bool IsFilled(long index);
    }
}