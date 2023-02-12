using System.Reflection.PortableExecutable;

namespace VirtualArray
{
    public interface IBitMap
    {
        byte this[int i] { get; set; }
        int Length { get; }
        void Read(FileStream FileStream, BinaryReader Reader, string Signature);
        void Initialize();
    }
}