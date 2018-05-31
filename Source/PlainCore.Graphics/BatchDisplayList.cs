using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public class BatchDisplayList<T> : AbstractDisplayList<T>, IChangeableDisplayList<T> where T : struct
    {
        public static BatchDisplayList<T> Create(IRenderPipelineSettings pipelineSettings)
        {
            return new BatchDisplayList<T>(pipelineSettings.VertexSize, 1024, pipelineSettings.Primitive, pipelineSettings.Shader, pipelineSettings.VertexAttributes);
        }

        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;

        public const uint DEFAULT_BUFFER_SIZE = 1024;

        private int elementCount;
        private uint currentVertexBufferSize;

        public BatchDisplayList(uint vertexSize, uint vertexBufferSize = DEFAULT_BUFFER_SIZE, OpenGL.PrimitiveType primitive = OpenGL.PrimitiveType.Triangles, ShaderPipeline pipeline = null, Core.VertexAttributeDescription[] vertexAttributes = null) : base(vertexSize, pipeline, vertexAttributes)
        {
            vertexArrayBuffer = new VertexArrayBuffer<T>(vertexSize, OpenGL.BufferUsage.StreamDraw, primitive);
            indexBuffer = new IndexBuffer<T>(OpenGL.BufferUsage.DynamicDraw);
            vertexArrayObject = new VertexArrayObject<T>(vertexArrayBuffer, this.pipeline, this.vertexAttributes);
            vertexArrayBuffer.Bind();
            vertexArrayBuffer.CopyRawData(IntPtr.Zero, vertexSize * vertexBufferSize);
            vertexArrayBuffer.Unbind();
            currentVertexBufferSize = vertexBufferSize;
        }

        public void ResizeVertexBuffer(uint newSize)
        {
            vertexArrayBuffer.Bind();
            vertexArrayBuffer.CopyRawData(IntPtr.Zero, newSize);
            currentVertexBufferSize = newSize;
            vertexArrayBuffer.Unbind();
        }

        public void SetVertices(T[] vertices)
        {
            if (vertices == null || vertices.Length > currentVertexBufferSize)
            {
                throw new ArgumentException();
            }

            vertexArrayBuffer.Bind();
            vertexArrayBuffer.ReplaceData(vertices, IntPtr.Zero);
            vertexArrayBuffer.Unbind();
        }

        public void SetIndices(int[] indices)
        {
            SetIndices(indices, indices.Length);
        }

        public void SetIndices(int[] indices, int elementCount)
        {
            this.elementCount = elementCount;
            indexBuffer.Bind();
            indexBuffer.CopyData(indices);
            indexBuffer.Unbind();
        }

        public override void Draw(IResourceSet resourceSet)
        {
            Draw(resourceSet, elementCount);
        }

        public override void Draw(IResourceSet resourceSet, int elements)
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
            vertexArrayBuffer.ReplaceData(pointer, (uint)length, IntPtr.Zero);
            vertexArrayBuffer.Unbind();
        }

        public override void Dispose()
        {
            vertexArrayBuffer.Dispose();
            indexBuffer.Dispose();
            vertexArrayObject.Dispose();
        }
    }
}
