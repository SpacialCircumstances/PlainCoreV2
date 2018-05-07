using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public class StreamDisplayList<T> : AbstractDisplayList<T>, IChangeableDisplayList<T> where T: struct
    {
        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;

        public const uint DEFAULT_BUFFER_SIZE = 1024;

        //Current indices count
        private int elements;

        public StreamDisplayList(uint vertexSize, OpenGL.PrimitiveType primitiveType = OpenGL.PrimitiveType.Triangles, uint vertexBufferSize = DEFAULT_BUFFER_SIZE, uint indexBufferSize = 1024, ShaderPipeline pipeline = null, Core.VertexAttributeDescription[] vertexAttributes = null) : base(vertexSize, pipeline, vertexAttributes)
        {
            vertexArrayBuffer = new VertexArrayBuffer<T>(vertexSize, OpenGL.BufferUsage.StreamDraw, primitiveType);
            indexBuffer = new IndexBuffer<T>(OpenGL.BufferUsage.StreamDraw);
            vertexArrayObject = new VertexArrayObject<T>(vertexArrayBuffer, this.pipeline, this.vertexAttributes);
            vertexArrayBuffer.Bind();
            indexBuffer.Bind();
            vertexArrayBuffer.CopyRawData(IntPtr.Zero, vertexSize * vertexBufferSize);
            indexBuffer.CopyRawData(IntPtr.Zero, sizeof(int) * indexBufferSize);
            indexBuffer.Unbind();
            vertexArrayBuffer.Unbind();
        }

        public void SetIndices(int[] indices)
        {
            elements = indices.Length;
            indexBuffer.Bind();
            indexBuffer.ReplaceData(indices, IntPtr.Zero);
            indexBuffer.Unbind();
        }

        public void SetVertices(T[] vertices)
        {
            vertexArrayBuffer.Bind();
            vertexArrayBuffer.ReplaceData(vertices, IntPtr.Zero);
            vertexArrayBuffer.Unbind();
        }

        public override void Draw(IResourceSet resourceSet)
        {
            pipeline.Bind();
            vertexArrayObject.Bind();
            indexBuffer.Bind();
            vertexArrayBuffer.Bind();

            foreach (var uniform in resourceSet.GetUniforms())
            {
                uniform.Set(pipeline);
            }

            indexBuffer.DrawIndexed(vertexArrayBuffer, elements);

            vertexArrayBuffer.Unbind();
            indexBuffer.Unbind();
            vertexArrayObject.Unbind();
            pipeline.Unbind();
        }

        public void ChangeFromRenderer(IRenderer<T> renderer)
        {
            if (renderer.VertexSize != this.vertexSize
                || renderer.Primitive != this.vertexArrayBuffer.Primitive
                || (renderer.Shader != null && renderer.Shader != this.pipeline)
                || (renderer.VertexAttributes != null && renderer.VertexAttributes != this.vertexAttributes))
            {
                SetVertices(renderer.Vertices);
                SetIndices(renderer.Indices);
            }
        }
    }
}
