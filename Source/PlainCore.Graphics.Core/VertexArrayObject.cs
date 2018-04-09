using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Contains metadata for a vertex drawing operation, for example attribute definitions and shaders.
    /// </summary>
    /// <typeparam name="T">Vertex type</typeparam>
    public class VertexArrayObject<T> : IBindable where T: struct
    {
        /// <summary>
        /// Create a new vertex array object
        /// </summary>
        /// <param name="buffer">The buffer the metadata applies to. Does not need to be bound.</param>
        /// <param name="pipeline">The shader pipeline for the drawing operation.</param>
        /// <param name="attributes">Vertex attribute definitions</param>
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
