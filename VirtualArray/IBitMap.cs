namespace VirtualArray
{
    public interface IBitMap
    {
        byte this[int i] { get; set; }
        int Length { get; }
        void Read(Stream Stream, BinaryReader Reader, string Signature);
        void Initialize();
        byte[] arr { get; }
        void Write(Stream Stream, BinaryWriter Writer, string Signature);
    }
}