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
        private readonly DataBuffer<VertexPositionColor> vertexDataBuffer;

        private readonly VertexArrayBuffer<VertexPositionColor> vertexArrayBuffer;
        private readonly VertexArrayObject<VertexPositionColor> vertexArrayObject;
        private readonly IndexBuffer<VertexPositionColor> indexBuffer;
        private readonly ShaderPipeline pipeline;
        private readonly Matrix4fUniform worldMatrixUniform;

        private int index;
        private int geometryCount;
        private bool isStarted;

        public ShapeBatch(ShaderPipeline pipeline = null)
        {
            this.pipeline = pipeline ?? new ShaderPipeline(
                    DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Vertex),
                    DefaultShader.FromType(typeof(VertexPositionColor), ShaderType.Fragment));

            vertexArrayBuffer = new VertexArrayBuffer<VertexPositionColor>(VertexPositionColor.Size, OpenGL.BufferUsage.StreamDraw, OpenGL.PrimitiveType.Triangles);
            vertexArrayObject = new VertexArrayObject<VertexPositionColor>(vertexArrayBuffer, this.pipeline, DefaultVertexDefinition.FromType(typeof(VertexPositionColor)));
            indexBuffer = new IndexBuffer<VertexPositionColor>();

            indexDataBuffer = new DataBuffer<int>(MAX_BATCH_SIZE * sizeof(int), indexBuffer);
            vertexDataBuffer = new DataBuffer<VertexPositionColor>(MAX_BATCH_SIZE * VertexPositionColor.Size, vertexArrayBuffer);
            worldMatrixUniform = new Matrix4fUniform(DefaultShader.MVP_UNIFORM_NAME);
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
            vertexDataBuffer.Clear();
            index = 0;
            geometryCount = 0;

            if (isStarted)
            {
                throw new InvalidOperationException("Begin may not be called on a running SpriteBatch");
            }

            isStarted = true;
        }

        public void End()
        {
            if (!isStarted)
            {
                throw new InvalidOperationException("SpriteBatch.Begin must be called before End");
            }
            isStarted = false;

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

            foreach (var vertex in vertices)
            {
                vertexDataBuffer.WriteVertex(vertex);
            }

            PushIndices(indices, vertices.Length);
        }

        protected void PushIndices(int[] indices, int vertexCount)
        {
            for (int i = 0; i < indices.Length; i++)
            {
                indexDataBuffer.Write(indices[i] + index);
            }

            geometryCount += indices.Length;
            index += vertexCount;
        }

        protected void Flush()
        {
            if (index == 0 || geometryCount == 0) return;
            indexDataBuffer.Flush();
            vertexDataBuffer.Flush();
            indexBuffer.DrawIndexed(vertexArrayBuffer, geometryCount);
            indexDataBuffer.Clear();
            vertexDataBuffer.Clear();
            geometryCount = 0;
            index = 0;
        }

        protected void CheckFlush(int indexCount)
        {
            if (!isStarted)
            {
                throw new InvalidOperationException("You must call Begin before drawing");
            }

            if (index + indexCount >= MAX_BATCH_SIZE)
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
