using System;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A display list for frequently changing index and vertex data.
    /// </summary>
    /// <typeparam name="T">Vertex type</typeparam>
    public class StreamDisplayList<T> : AbstractDisplayList<T>, IChangeableDisplayList<T> where T: struct
    {
        public static StreamDisplayList<T> Create(IRenderPipelineSettings pipelineSettings)
        {
            return new StreamDisplayList<T>(pipelineSettings.VertexSize, pipelineSettings.Primitive, 1024, 1024, pipelineSettings.Shader, pipelineSettings.VertexAttributes);
        }

        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;

        //Current indices count
        private int elements;

        public StreamDisplayList(uint vertexSize, OpenGL.PrimitiveType primitiveType = OpenGL.PrimitiveType.Triangles, uint vertexBufferSize = 1024, uint indexBufferSize = 1024, ShaderPipeline pipeline = null, Core.VertexAttributeDescription[] vertexAttributes = null) : base(vertexSize, pipeline, vertexAttributes)
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
            SetIndices(indices, indices.Length);
        }

        public void SetIndices(int[] indices, int elementCount)
        {
            if (indices == null) throw new ArgumentNullException(nameof(indices));

            elements = elementCount;
            indexBuffer.Bind();
            indexBuffer.ReplaceData(indices, IntPtr.Zero);
            indexBuffer.Unbind();
        }

        public void SetVertices(T[] vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            vertexArrayBuffer.Bind();
            vertexArrayBuffer.ReplaceData(vertices, IntPtr.Zero);
            vertexArrayBuffer.Unbind();
        }

        public override void Draw(IResourceSet resourceSet)
        {
            Draw(resourceSet, elements);
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

        public void SetIndicesFromPointer(IntPtr pointer, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length));
            }

            indexBuffer.Bind();
            indexBuffer.ReplaceData(pointer, (uint)length, IntPtr.Zero);
            indexBuffer.Unbind();
        }

        public void SetVerticesFromPointer(IntPtr pointer, int length)
        {
            if (length < 0)
            {
                throw new ArgumentException(nameof(length));
            }

            vertexArrayBuffer.Bind();
            vertexArrayBuffer.ReplaceData(pointer, vertexSize * (uint)length, IntPtr.Zero);
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
