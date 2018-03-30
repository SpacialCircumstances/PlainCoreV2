using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.System
{
    public class MemoryBuffer : IDisposable
    {
        public MemoryBuffer(int size)
        {
            buffer = new byte[size];
            writer = new BinaryWriter(stream = new MemoryStream(buffer));
        }

        protected byte[] buffer;
        protected int written;
        protected MemoryStream stream;
        protected BinaryWriter writer;

        public int BytesWritten => written;
        public int Size => buffer.Length;
        public byte[] Buffer => buffer;

        public void Clear()
        {
            written = 0;
            writer.Seek(0, SeekOrigin.Begin);
        }

        public void Write(int value)
        {
            writer.Write(value);
            written += 4;
        }

        public void Write(float value)
        {
            writer.Write(value);
            written += 4;
        }

        public void Write(byte value)
        {
            writer.Write(value);
            written++;
        }

        public void Dispose()
        {
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }
    }
}
