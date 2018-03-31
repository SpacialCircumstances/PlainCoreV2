using System;
using OpenGL;

namespace PlainCore.Graphics.Core
{
    public class IndexBuffer<T> : IDeviceBuffer<int> where T: struct
    {
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

        public void DrawIndexed(VertexArrayBuffer<T> buffer, int elements)
        {
            Gl.DrawElements(buffer.Primitive, elements, DrawElementsType.UnsignedInt, null);
        }

        public void CopyRawData(byte[] data)
        {
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)data.Length, data, usage);
        }

        public void CopyRawData(IntPtr pointer, uint size)
        {
            Gl.BufferData(BufferTarget.ElementArrayBuffer, size, pointer, usage);
        }

        public void ReplaceData(byte[] data, IntPtr offset)
        {
            Gl.BufferSubData(BufferTarget.ElementArrayBuffer, offset, (uint)data.Length, data);
        }

        public void ReplaceData(IntPtr data, uint size, IntPtr offset)
        {
            Gl.BufferSubData(BufferTarget.ElementArrayBuffer, offset, size, data);
        }
    }
}
