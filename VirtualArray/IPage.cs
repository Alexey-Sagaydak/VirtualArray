namespace VirtualArray
{
    public interface IPage
    {
        int[] Array { get; set; }
        IBitMap BitMap { get; set; }
        byte IsModified { get; set; }
        DateTime LastCall { get; set; }
        long Number { get; set; }
    }
}