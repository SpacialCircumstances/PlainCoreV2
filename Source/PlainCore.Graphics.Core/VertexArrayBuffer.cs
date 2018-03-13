using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class VertexArrayBuffer<T>: IDeviceBuffer where T: struct
    {
        public VertexArrayBuffer(uint vertexSize, BufferUsage usage = BufferUsage.StreamDraw, int initialCapacity = 3)
        {
            vertices = new T[initialCapacity];
            this.vertexSize = vertexSize;
            this.usage = usage;

            handle = Gl.GenBuffer();
            Verify.VerifyResourceCreated(handle);
        }

        protected readonly BufferUsage usage;
        protected uint vertexSize;
        protected T[] vertices;
        protected uint handle;

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
            Gl.BindBuffer(BufferTarget.ArrayBuffer, handle);
        }

        public void CopyData()
        {
            Bind();
            Gl.BufferData(BufferTarget.ArrayBuffer, (this.vertexSize * (uint)vertices.Length), this.vertices, usage);
            Unbind();
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(handle);
        }

        public void Unbind()
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
