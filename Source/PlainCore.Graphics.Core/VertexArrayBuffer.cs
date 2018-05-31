using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A buffer holding vertex data.
    /// </summary>
    /// <typeparam name="T">Type of the vertex. Must be a struct.</typeparam>
    public class VertexArrayBuffer<T>: IDeviceBuffer<T> where T: struct
    {
        /// <summary>
        /// Create a new buffer for array data.
        /// </summary>
        /// <param name="vertexSize">Size (in bytes) of a single vertex</param>
        /// <param name="usage">Buffer usage. Defaults to StreamDraw</param>
        /// <param name="primitive">Primitive type. Defaults to Triangles</param>
        public VertexArrayBuffer(uint vertexSize, BufferUsage usage = BufferUsage.StreamDraw, PrimitiveType primitive = PrimitiveType.Triangles)
        {
            this.vertexSize = vertexSize;
            this.usage = usage;
            this.primitive = primitive;

            Handle = Gl.GenBuffer();
            Verify.VerifyResourceCreated(Handle);
        }

        protected readonly BufferUsage usage;
        protected readonly PrimitiveType primitive;
        protected uint vertexSize;
        public readonly uint Handle;

        public PrimitiveType Primitive => primitive;

        public void Bind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        }

        public void CopyData(T[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferData(BufferTarget.ArrayBuffer, (this.vertexSize * (uint)data.Length), data, usage);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(Handle);
        }

        public void Unbind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Directly draw the vertices without an index buffer. This buffer must be bound.
        /// </summary>
        /// <param name="elements">Number of elements to draw</param>
        public void DrawDirect(int elements)
        {
            Gl.DrawArrays(primitive, 0, elements);
        }

        public void CopyRawData(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)data.Length, data, usage);
        }

        public void CopyRawData(IntPtr pointer, uint size)
        {
            Gl.BufferData(BufferTarget.ArrayBuffer, size, pointer, usage);
        }

        public void ReplaceData(byte[] data, IntPtr offset)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferSubData(BufferTarget.ArrayBuffer, offset, (uint)data.Length, data);
        }

        public void ReplaceData(IntPtr data, uint size, IntPtr offset)
        {
            Gl.BufferSubData(BufferTarget.ArrayBuffer, offset, size, data);
        }

        public void ReplaceData(T[] data, IntPtr offset)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferSubData(BufferTarget.ArrayBuffer, offset, vertexSize * (uint)data.Length, data);
        }
    }
}
