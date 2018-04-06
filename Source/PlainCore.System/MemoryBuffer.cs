using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.System
{
    /// <summary>
    /// Allows writing primitives to a byte array
    /// </summary>
    public class MemoryBuffer : IDisposable
    {
        /// <summary>
        /// Create a MemoryBuffer with the maximum size of the byte array
        /// </summary>
        /// <param name="size">The maximum size of the byte array backing the buffer</param>
        public MemoryBuffer(int size)
        {
            buffer = new byte[size];
            writer = new BinaryWriter(stream = new MemoryStream(buffer));
        }

        protected byte[] buffer;
        protected int written;
        protected MemoryStream stream;
        protected BinaryWriter writer;

        /// <summary>
        /// The amount of bytes written since creation/calling Clear().
        /// </summary>
        public int BytesWritten => written;

        /// <summary>
        /// The maximum size of the byte array backing the buffer.
        /// </summary>
        public int Size => buffer.Length;

        /// <summary>
        /// The buffer
        /// </summary>
        public byte[] Buffer => buffer;

        /// <summary>
        /// Clears the stream so writing begins from the start.
        /// </summary>
        /// <remarks>
        /// Does not reallocate the buffer</remarks>
        public void Clear()
        {
            written = 0;
            writer.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// Write an Integer to the buffer.
        /// </summary>
        /// <param name="value">Value to write</param>
        public void Write(int value)
        {
            writer.Write(value);
            written += 4;
        }

        /// <summary>
        /// Write an Single to the buffer.
        /// </summary>
        /// <param name="value">Value to write</param>
        public void Write(float value)
        {
            writer.Write(value);
            written += 4;
        }

        /// <summary>
        /// Write an Byte to the buffer.
        /// </summary>
        /// <param name="value">Value to write</param>
        public void Write(byte value)
        {
            writer.Write(value);
            written++;
        }

        /// <summary>
        /// Dispose the resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }
    }
}
