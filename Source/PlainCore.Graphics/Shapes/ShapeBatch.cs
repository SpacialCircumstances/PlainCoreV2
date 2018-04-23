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
        }

        public void End()
        {
            Flush();
            pipeline.Unbind();
            vertexArrayObject.Unbind();
            vertexArrayBuffer.Unbind();
            indexBuffer.Unbind();
        }

        public void PushVertex(float x, float y, Color4 color)
        {
            vertexDataBuffer.Write(x);
            vertexDataBuffer.Write(y);
            vertexDataBuffer.Write(color.R);
            vertexDataBuffer.Write(color.G);
            vertexDataBuffer.Write(color.B);
            vertexDataBuffer.Write(color.A);
            PushIndices();
        }

        public void PushIndices()
        {
            indexDataBuffer.Write(index);
            index++;
        }

        protected void Flush()
        {
            if (index == 0) return;
            indexDataBuffer.Flush();
            vertexDataBuffer.Flush();
            indexBuffer.DrawIndexed(vertexArrayBuffer, index);
            indexDataBuffer.Clear();
            vertexDataBuffer.Clear();
            index = 0;
        }

        protected void CheckFlush()
        {
            if (index >= MAX_BATCH_SIZE)
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
