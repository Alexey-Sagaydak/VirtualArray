namespace VirtualArray
{
    public interface IPage
    {
        int[] Values { get; set; }
        IBitMap BitMap { get; set; }
        byte IsModified { get; set; }
        DateTime LastCall { get; set; }
        long Number { get; set; }
        void Change(int index, int value);
    }
}