using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A class for writing primitives to a GPU buffer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>Does not support texture buffers at the moment.</remarks>
    public class DataBuffer<T>
    {
        /// <summary>
        /// Create a new data buffer.
        /// </summary>
        /// <param name="initialSize">Initial (and maximal) size of the memory used on the GPU</param>
        /// <param name="buffer">The buffer for writing data</param>
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

        /// <summary>
        /// Uploads the written data to the GPU. Buffer must be bound.
        /// </summary>
        public void Flush()
        {
            writer.Flush();
            var data = stream.ToArray();
            buffer.ReplaceData(data, IntPtr.Zero);
        }

        /// <summary>
        /// Resets the stream. Does not clear the GPU memory.
        /// </summary>
        public void Clear()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
            written = 0;
        }

        /// <summary>
        /// Writes an integer to the stream.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(int value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Writes a float to the stream.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(float value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Writes a byte to the stream.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(byte value)
        {
            writer.Write(value);
        }

        /// <summary>
        /// Writes a byte buffer to the stream.
        /// </summary>
        /// <param name="values">The byte buffer</param>
        public void Write(byte[] values)
        {
            writer.Write(values);
        }
    }
}
