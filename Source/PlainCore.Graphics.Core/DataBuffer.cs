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
        /// Resets the buffer. Does not clear the GPU memory.
        /// </summary>
        public void Clear()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
            written = 0;
        }

        /// <summary>
        /// Writes an integer to the buffer.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(int value)
        {
            writer.Write(value);
            written += 4;
        }

        /// <summary>
        /// Writes a float to the buffer.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(float value)
        {
            writer.Write(value);
            written += 4;
        }

        /// <summary>
        /// Writes a byte to the buffer.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(byte value)
        {
            writer.Write(value);
            written++;
        }

        /// <summary>
        /// Writes a byte buffer to the buffer.
        /// </summary>
        /// <param name="values">The byte buffer</param>
        public void Write(byte[] values)
        {
            writer.Write(values);
            written += values.Length;
        }

        /// <summary>
        /// Writes a double to the buffer.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(double value)
        {
            writer.Write(value);
            written += 8;
        }

        /// <summary>
        /// Writes a long to the buffer.
        /// </summary>
        /// <param name="value">The value</param>
        public void Write(long value)
        {
            writer.Write(value);
            written += 8;
        }

        /// <summary>
        /// Writes a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public void WriteVertex(VertexPositionColor vertex)
        {
            Write(vertex.Position.X);
            Write(vertex.Position.Y);
            Write(vertex.Color.R);
            Write(vertex.Color.G);
            Write(vertex.Color.B);
            Write(vertex.Color.A);
        }

        /// <summary>
        /// Writes a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public void WriteVertex(VertexPosition3Color vertex)
        {
            Write(vertex.Position.X);
            Write(vertex.Position.Y);
            Write(vertex.Position.Z);
            Write(vertex.Color.R);
            Write(vertex.Color.G);
            Write(vertex.Color.B);
            Write(vertex.Color.A);
        }

        /// <summary>
        /// Writes a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public void WriteVertex(VertexPositionTexture vertex)
        {
            Write(vertex.Position.X);
            Write(vertex.Position.Y);
            Write(vertex.TextureCoordinates.X);
            Write(vertex.TextureCoordinates.Y);
        }

        /// <summary>
        /// Writes a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public void WriteVertex(VertexPosition3Texture vertex)
        {
            Write(vertex.Position.X);
            Write(vertex.Position.Y);
            Write(vertex.Position.Z);
            Write(vertex.TextureCoordinates.X);
            Write(vertex.TextureCoordinates.Y);
        }

        /// <summary>
        /// Writes a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public void WriteVertex(VertexPositionColorTexture vertex)
        {
            Write(vertex.Position.X);
            Write(vertex.Position.Y);
            Write(vertex.Color.R);
            Write(vertex.Color.G);
            Write(vertex.Color.B);
            Write(vertex.Color.A);
            Write(vertex.TextureCoordinates.X);
            Write(vertex.TextureCoordinates.Y);
        }

        /// <summary>
        /// Writes a vertex to the buffer.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        public void WriteVertex(VertexPosition3ColorTexture vertex)
        {
            Write(vertex.Position.X);
            Write(vertex.Position.Y);
            Write(vertex.Position.Z);
            Write(vertex.Color.R);
            Write(vertex.Color.G);
            Write(vertex.Color.B);
            Write(vertex.Color.A);
            Write(vertex.TextureCoordinates.X);
            Write(vertex.TextureCoordinates.Y);
        }
    }
}
