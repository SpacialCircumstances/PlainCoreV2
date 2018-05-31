using System;
using OpenGL;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A buffer for holding index data.
    /// </summary>
    /// <typeparam name="T">Data type of the vertices associated with the indices</typeparam>
    public class IndexBuffer<T> : IDeviceBuffer<int> where T: struct
    {
        /// <summary>
        /// Create a new index buffer.
        /// </summary>
        /// <param name="usage">Buffer usage flag. Defaults to StreamDraw.</param>
        public IndexBuffer(BufferUsage usage = BufferUsage.StreamDraw)
        {
            this.usage = usage;
            Handle = Gl.GenBuffer();
            Verify.VerifyResourceCreated(Handle);
        }

        public readonly uint Handle;
        protected readonly BufferUsage usage;

        public void Bind()
        {
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
        }

        public void CopyData(int[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)data.Length * sizeof(int), data, usage);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(Handle);
        }

        public void Unbind()
        {
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        /// <summary>
        /// Draw the vertices in the bound buffer, using this buffer for the indices.
        /// </summary>
        /// <param name="buffer">Vertex buffer. Must be bound.</param>
        /// <param name="elements">Number of elements to draw</param>
        public void DrawIndexed(VertexArrayBuffer<T> buffer, int elements)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (elements <= 0) throw new ArgumentOutOfRangeException(nameof(elements));
            Gl.DrawElements(buffer.Primitive, elements, DrawElementsType.UnsignedInt, null);
        }

        public void CopyRawData(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)data.Length, data, usage);
        }

        public void CopyRawData(IntPtr pointer, uint size)
        {
            Gl.BufferData(BufferTarget.ElementArrayBuffer, size, pointer, usage);
        }

        public void ReplaceData(byte[] data, IntPtr offset)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferSubData(BufferTarget.ElementArrayBuffer, offset, (uint)data.Length, data);
        }

        public void ReplaceData(IntPtr data, uint size, IntPtr offset)
        {
            Gl.BufferSubData(BufferTarget.ElementArrayBuffer, offset, size, data);
        }

        public void ReplaceData(int[] data, IntPtr offset)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Gl.BufferSubData(BufferTarget.ElementArrayBuffer, offset, sizeof(int) * (uint)data.Length, data);
        }
    }
}
