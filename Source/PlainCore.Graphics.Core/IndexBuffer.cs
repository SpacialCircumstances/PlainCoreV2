using OpenGL;

namespace PlainCore.Graphics.Core
{
    public class IndexBuffer<T> : IDeviceBuffer where T: struct
    {
        public IndexBuffer(BufferUsage usage = BufferUsage.StreamDraw)
        {
            this.usage = usage;
            Handle = Gl.GenBuffer();
            Verify.VerifyResourceCreated(Handle);
        }

        public readonly uint Handle;
        protected int[] indices;
        protected readonly BufferUsage usage;

        public int[] Indices
        {
            get => indices;
            set
            {
                indices = value;
            }
        }

        public void Bind()
        {
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
        }

        public void CopyData()
        {
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)indices.Length * sizeof(int), indices, usage);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(Handle);
        }

        public void Unbind()
        {
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void DrawIndexed(VertexArrayBuffer<T> buffer)
        {
            Gl.DrawElements(buffer.Primitive, indices.Length, DrawElementsType.UnsignedInt, null);
        }
    }
}
