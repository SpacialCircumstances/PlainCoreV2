using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class VertexArrayObject<T> : IBindable where T: struct
    {
        public VertexArrayObject(VertexArrayBuffer<T> buffer, ShaderPipeline pipeline, params VertexAttributeDescription[] attributes)
        {
            this.buffer = buffer;
            Handle = Gl.GenVertexArray();
            Verify.VerifyResourceCreated(Handle);

            this.pipeline = pipeline;

            Bind();
            buffer.Bind();

            foreach (var attr in attributes)
            {
                SetAttribute(attr);
            }

            Unbind();
            buffer.Unbind();
        }

        protected VertexArrayBuffer<T> buffer;
        protected ShaderPipeline pipeline;
        public readonly uint Handle;

        public void Bind()
        {
            Gl.BindVertexArray(Handle);
        }

        public void Dispose()
        {
            Gl.DeleteVertexArrays(Handle);
        }

        public void Unbind()
        {
            Gl.BindVertexArray(0);
        }

        protected void SetAttribute(VertexAttributeDescription attr)
        {
            uint position = pipeline.GetAttributeLocation(attr.Name);

            Gl.EnableVertexAttribArray(position);
            Gl.VertexAttribPointer(position, attr.Size, attr.AttributeType, attr.Normalized, attr.VertexSize, new IntPtr(attr.Offset));
        }
    }
}
