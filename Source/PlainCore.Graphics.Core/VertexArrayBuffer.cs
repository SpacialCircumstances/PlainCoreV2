using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class VertexArrayBuffer<T>: IDeviceBuffer where T: struct
    {
        public VertexArrayBuffer(uint vertexSize, BufferUsage usage = BufferUsage.StreamDraw, PrimitiveType primitive = PrimitiveType.Triangles, int initialCapacity = 3)
        {
            vertices = new T[initialCapacity];
            this.vertexSize = vertexSize;
            this.usage = usage;
            this.primitive = primitive;

            Handle = Gl.GenBuffer();
            Verify.VerifyResourceCreated(Handle);
        }

        protected readonly BufferUsage usage;
        protected readonly PrimitiveType primitive;
        protected uint vertexSize;
        protected T[] vertices;
        public readonly uint Handle;

        public T[] Vertices
        {
            get => vertices;
            set
            {
                vertices = value;
            }
        }

        public void Bind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        }

        public void CopyData()
        {
            Gl.BufferData(BufferTarget.ArrayBuffer, (this.vertexSize * (uint)vertices.Length), this.vertices, usage);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(Handle);
        }

        public void Unbind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Draw()
        {
            Gl.DrawArrays(primitive, 0, vertices.Length);
        }
    }
}
