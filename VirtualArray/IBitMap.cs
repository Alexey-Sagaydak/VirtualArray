namespace VirtualArray
{
    public interface IBitMap
    {
        byte this[int i] { get; set; }
        int Length { get; }
        void Initialize();
    }
}