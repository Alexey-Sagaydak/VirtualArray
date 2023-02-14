namespace VirtualArray
{
    public interface IVirtualArray: IDisposable
    {
        int this[long i] { get; set; }
        long Length { get; }
        void Delete(long index);
        bool IsEmpty(long index);
        bool IsFilled(long index);
    }
}