using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    public class StaticDisplayList<T> : AbstractDisplayList<T> where T: struct
    {
        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;
        private readonly int elementCount;

        public StaticDisplayList(T[] vertices, int[] indices, uint vertexSize, OpenGL.PrimitiveType primitiveType = OpenGL.PrimitiveType.Triangles, ShaderPipeline pipeline = null, VertexAttributeDescription[] attributes = null): base(vertexSize, pipeline, attributes)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (indices == null) throw new ArgumentNullException(nameof(indices));

            vertexArrayBuffer = new VertexArrayBuffer<T>(vertexSize, OpenGL.BufferUsage.StaticDraw, primitiveType);
            indexBuffer = new IndexBuffer<T>(OpenGL.BufferUsage.StaticDraw);
            vertexArrayObject = new VertexArrayObject<T>(vertexArrayBuffer, this.pipeline, vertexAttributes);
            vertexArrayBuffer.Bind();
            indexBuffer.Bind();
            vertexArrayBuffer.CopyData(vertices);
            indexBuffer.CopyData(indices);
            indexBuffer.Unbind();
            vertexArrayBuffer.Unbind();
            elementCount = indices.Length;
        }

        public override void Draw(IResourceSet resourceSet)
        {
            Draw(resourceSet, elementCount);
        }

        public override void Draw(IResourceSet resourceSet, int elements)
        {
            if (resourceSet == null) throw new ArgumentNullException(nameof(resourceSet));

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

        public override void Dispose()
        {
            vertexArrayBuffer.Dispose();
            indexBuffer.Dispose();
            vertexArrayObject.Dispose();
        }
    }
}
