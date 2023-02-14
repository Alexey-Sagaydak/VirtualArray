namespace VirtualArray
{
    public interface IBitMap
    {
        byte this[int i] { get; set; }
        int Length { get; }
        void Read(FileStream FileStream, BinaryReader Reader, string Signature);
        void Initialize();
        byte[] arr { get; }
        void Write(FileStream FileStream, BinaryWriter Writer, string Signature);
    }
}