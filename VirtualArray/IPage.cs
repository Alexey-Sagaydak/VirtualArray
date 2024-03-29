﻿namespace VirtualArray
{
    public interface IPage
    {
        int[] Values { get; set; }
        IBitMap BitMap { get; set; }
        bool IsModified { get; set; }
        DateTime LastCall { get; set; }
        long Number { get; set; }
        void Change(int index, int value);
        void Delete(int index);
        void Write(Stream Stream, BinaryWriter Writer, string Signature);
    }
}