using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class DataBuffer<T>
    {
        public DataBuffer(uint initialSize, IDeviceBuffer<T> buffer)
        {
            this.buffer = buffer;
            this.buffer.Bind();
            this.buffer.CopyRawData(IntPtr.Zero, initialSize);
            this.buffer.Unbind();
            Clear();
        }

        private IDeviceBuffer<T> buffer;
        public IDeviceBuffer<T> DeviceBuffer => buffer;

        private MemoryStream stream;
        private BinaryWriter writer;

        private int written;
        public int WrittenBytes => written;

        public void Flush()
        {
            writer.Flush();
            var data = stream.ToArray();
            buffer.ReplaceData(data, IntPtr.Zero);
        }

        public void Clear()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
            written = 0;
        }

        public void Write(int value)
        {
            writer.Write(value);
        }

        public void Write(float value)
        {
            writer.Write(value);
        }

        public void Write(byte value)
        {
            writer.Write(value);
        }

        public void Write(byte[] values)
        {
            writer.Write(values);
        }
    }
}
