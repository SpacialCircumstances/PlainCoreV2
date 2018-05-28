using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    public class DynamicDisplayList<T> : AbstractDisplayList<T>, IChangeableDisplayList<T> where T : struct
    {
        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;

        //Current indices count
        private int elements;

        public DynamicDisplayList(uint vertexSize, OpenGL.PrimitiveType primitiveType = OpenGL.PrimitiveType.Triangles, ShaderPipeline pipeline = null, VertexAttributeDescription[] vertexAttributes = null) : base(vertexSize, pipeline, vertexAttributes)
        {
            vertexArrayBuffer = new VertexArrayBuffer<T>(vertexSize, OpenGL.BufferUsage.DynamicDraw, primitiveType);
            indexBuffer = new IndexBuffer<T>(OpenGL.BufferUsage.DynamicDraw);
            vertexArrayObject = new VertexArrayObject<T>(vertexArrayBuffer, this.pipeline, this.vertexAttributes);
        }

        public void SetVertices(T[] vertices)
        {
            vertexArrayBuffer.Bind();
            vertexArrayBuffer.CopyData(vertices);
            vertexArrayBuffer.Unbind();
        }

        public void SetIndices(int[] indices)
        {
            SetIndices(indices, indices.Length);
        }

        public void SetIndices(int[] indices, int elementCount)
        {
            elements = elementCount;
            indexBuffer.Bind();
            indexBuffer.CopyData(indices);
            indexBuffer.Unbind();
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

        public void SetIndicesFromPointer(IntPtr pointer, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length));
            }

            indexBuffer.Bind();
            indexBuffer.CopyRawData(pointer, (uint)length);
            indexBuffer.Unbind();
        }

        public void SetVerticesFromPointer(IntPtr pointer, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length)); ;
            }

            vertexArrayBuffer.Bind();
            vertexArrayBuffer.CopyRawData(pointer, (uint)length);
            vertexArrayBuffer.Unbind();
        }
    }
}
