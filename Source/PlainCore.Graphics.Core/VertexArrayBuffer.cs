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
        /// <param name="usage">(Optional) Buffer usage. Defaults to StreamDraw</param>
        /// <param name="primitive">(Optional) Primitive type. Defaults to Triangles</param>
        /// <param name="initialCapacity">(Optional) Initial buffer capacity</param>
        public VertexArrayBuffer(uint vertexSize, BufferUsage usage = BufferUsage.StreamDraw, PrimitiveType primitive = PrimitiveType.Triangles, int initialCapacity = 3)
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

        public void DrawDirect(int elements)
        {
            Gl.DrawArrays(primitive, 0, elements);
        }

        public void CopyRawData(byte[] data)
        {
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)data.Length, data, usage);
        }

        public void CopyRawData(IntPtr pointer, uint size)
        {
            Gl.BufferData(BufferTarget.ArrayBuffer, size, pointer, usage);
        }

        public void ReplaceData(byte[] data, IntPtr offset)
        {
            Gl.BufferSubData(BufferTarget.ArrayBuffer, offset, (uint)data.Length, data);
        }

        public void ReplaceData(IntPtr data, uint size, IntPtr offset)
        {
            Gl.BufferSubData(BufferTarget.ArrayBuffer, offset, size, data);
        }
    }
}
