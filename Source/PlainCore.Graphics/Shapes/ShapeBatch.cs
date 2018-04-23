using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Shapes
{
    public class ShapeBatch : IDisposable
    {
        private const int MAX_BATCH_SIZE = 1024;

        private readonly DataBuffer<int> indexDataBuffer;

        private readonly VertexArrayBuffer<VertexPositionColor> vertexArrayBuffer;
        private readonly VertexArrayObject<VertexPositionColor> vertexArrayObject;
        private readonly IndexBuffer<VertexPositionColor> indexBuffer;
        private readonly ShaderPipeline pipeline;
        private readonly Matrix4fUniform worldMatrixUniform;
        private readonly List<VertexPositionColor> vertices;

        private int index;

        public ShapeBatch(ShaderPipeline pipeline = null)
        {
            this.pipeline = pipeline ?? new ShaderPipeline(
                    DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Vertex),
                    DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Fragment));

            vertexArrayBuffer = new VertexArrayBuffer<VertexPositionColor>(VertexPositionColor.Size);
            vertexArrayObject = new VertexArrayObject<VertexPositionColor>(vertexArrayBuffer, pipeline, DefaultVertexDefinition.FromType(typeof(VertexPositionColor)));
            indexBuffer = new IndexBuffer<VertexPositionColor>();
            worldMatrixUniform = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
            indexDataBuffer = new DataBuffer<int>(MAX_BATCH_SIZE * sizeof(int), indexBuffer);
            vertices = new List<VertexPositionColor>(MAX_BATCH_SIZE);
        }

        public void Begin(IRenderTarget target)
        {
            pipeline.Bind();
            worldMatrixUniform.Matrix = target.WorldMatrix;
            worldMatrixUniform.Set(pipeline);
            vertexArrayBuffer.Bind();
            vertexArrayObject.Bind();
            indexBuffer.Bind();
            indexDataBuffer.Clear();
            index = 0;
        }

        public void End()
        {
            Flush();
            pipeline.Unbind();
            vertexArrayObject.Unbind();
            vertexArrayBuffer.Unbind();
            indexBuffer.Unbind();
        }

        public void Draw(IShape shape)
        {
            var indices = shape.GetIndices();
            CheckFlush(indices.Length);
            var vertices = shape.GetVertices();
            this.vertices.AddRange(vertices);
            WriteIndices(indices);
        }

        protected void WriteIndices(int[] indices)
        {
            foreach (var i in indices)
            {
                indexDataBuffer.Write(index + i);
            }

            index += indices.Length;
        }

        protected void Flush()
        {
            if (index == 0) return;
            indexDataBuffer.Flush();
            vertexArrayBuffer.ReplaceData(vertices.ToArray(), IntPtr.Zero);
            indexBuffer.DrawIndexed(vertexArrayBuffer, index);
            indexDataBuffer.Clear();
            vertices.Clear();
            index = 0;
        }

        protected void CheckFlush(int numItems)
        {
            if (index + numItems >= MAX_BATCH_SIZE)
            {
                Flush();
            }
        }

        public void Dispose()
        {
            vertexArrayBuffer.Dispose();
            vertexArrayObject.Dispose();
            indexBuffer.Dispose();
            pipeline.Dispose();
        }
    }
}
