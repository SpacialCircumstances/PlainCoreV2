using PlainCore.Graphics.Core;
using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class SpriteBatch: IDisposable
    {
        private const int MAX_BATCH_SIZE = 1024;

        private readonly List<VertexPositionColorTexture> vertices;

        private readonly VertexArrayBuffer<VertexPositionColorTexture> vertexArrayBuffer;
        private readonly VertexArrayObject<VertexPositionColorTexture> vertexArrayObject;
        private readonly IndexBuffer<VertexPositionColorTexture> indexBuffer;
        private readonly ShaderPipeline pipeline;
        private readonly Matrix4fUniform worldMatrixUniform;

        private Texture currentTexture;
        private int index;

        public SpriteBatch(ShaderPipeline pipeline = null)
        {
            this.pipeline = pipeline ?? new ShaderPipeline(
                    DefaultShader.FromType(typeof(VertexPositionColorTexture), ShaderType.Vertex),
                    DefaultShader.FromType(typeof(VertexPositionColorTexture), ShaderType.Fragment));

            vertexArrayBuffer = new VertexArrayBuffer<VertexPositionColorTexture>(VertexPositionColorTexture.Size, OpenGL.BufferUsage.StreamDraw, OpenGL.PrimitiveType.Triangles);
            vertexArrayObject = new VertexArrayObject<VertexPositionColorTexture>(vertexArrayBuffer, this.pipeline, DefaultVertexDefinition.FromType(typeof(VertexPositionColorTexture)));
            indexBuffer = new IndexBuffer<VertexPositionColorTexture>();
            vertexArrayBuffer.Bind();
            vertexArrayBuffer.CopyRawData(IntPtr.Zero, MAX_BATCH_SIZE * 4 * VertexPositionColorTexture.Size);
            vertexArrayBuffer.Unbind();
            indexBuffer.Bind();
            indexBuffer.CopyRawData(IntPtr.Zero, MAX_BATCH_SIZE * 6 * sizeof(int));
            indexBuffer.Unbind();

            vertices = new List<VertexPositionColorTexture>(MAX_BATCH_SIZE * 4);

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
            vertices.Clear();
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

        public void Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation, float originX, float originY)
        {
            Draw(texture, color, x, y, width, height, rotation, originX, originY, 0, 0, 1, 1);
        }

        public void Draw(ITexture texture, float x, float y, float width, float height, float rotation)
        {
            Draw(texture, Color4.White, x, y, width, height, rotation);
        }

        public void Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation)
        {
            Draw(texture, color, x, y, width, height, rotation, 0.5f, 0.5f, 0, 0, 1, 1);
        }

        public void Draw(ITexture texture, float x, float y, float width, float height)
        {
            Draw(texture, Color4.White, x, y, width, height, 0, 0, 1, 1);
        }

        public void Draw(ITexture texture, float x, float y)
        {
            Draw(texture, Color4.White, x, y);
        }

        public void Draw(ITexture texture, Color4 color, float x, float y)
        {
            Draw(texture, color, x, y, texture.Texture.Width, texture.Texture.Height, 0, 0, 1, 1);
        }

        public void Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float texX1, float texY1, float texX2, float texY2)
        {
            CheckTextureFlush(texture.Texture);

            float lowerX = texture.Rectangle.Position.X + (texX1 * texture.Rectangle.End.X);
            float upperX = texX2 * texture.Rectangle.End.X;
            float upperY = texture.Rectangle.Position.Y + (texY1 * texture.Rectangle.End.Y);
            float lowerY = texY2 * texture.Rectangle.End.Y;

            PushVertex(x, y, color, lowerX, lowerY);
            PushVertex(x + width, y, color, upperX, lowerY);
            PushVertex(x + width, y + height, color, upperX, upperY);
            PushVertex(x, y + height, color, lowerX, upperY);
            index++;
        }

        public void Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation, float originX, float originY, float texX1, float texY1, float texX2, float texY2)
        {
            if(rotation == 0)
            {
                Draw(texture, color, x, y, width, height, texX1, texY1, texX2, texY2);
            }

            CheckTextureFlush(texture.Texture);

            float lowerX = texture.Rectangle.Position.X + (texX1 * texture.Rectangle.End.X);
            float upperX = texX2 * texture.Rectangle.End.X;
            float upperY = texture.Rectangle.Position.Y + (texY1 * texture.Rectangle.End.Y);
            float lowerY = texY2 * texture.Rectangle.End.Y;

            float sin = (float)Math.Sin(rotation);
            float cos = (float)Math.Cos(rotation);

            float m11 = cos;
            float m12 = sin;
            float m21 = -sin;
            float m22 = cos;

            float ox = originX * width;
            float oy = originY * height;

            var ldx = (-ox * m11) + (-oy * m12) + x;
            var ldy = (-ox * m21) + (-oy * m22) + y;
            var lux = (-ox * m11) + ((height - oy) * m12) + x;
            var luy = (-ox * m21) + ((height - oy) * m22) + y;
            var rux = ((width - ox) * m11) + (-oy * m12) + x;
            var ruy = ((width - ox) * m21) + (-oy * m22) + y;
            var rdx = ((width - ox) * m11) + ((height - oy) * m12) + x;
            var rdy = ((width - ox) * m21) + ((height - oy) * m22) + y;

            PushVertex(ldx, ldy, color, lowerX, lowerY);
            PushVertex(rux, ruy, color, upperX, lowerY);
            PushVertex(rdx, rdy, color, upperX, upperY);
            PushVertex(lux, luy, color, lowerX, upperY);
            index++;
        }

        protected void PushVertex(float x, float y, Color4 color, float tx, float ty)
        {
            vertices.Add(new VertexPositionColorTexture(new Vector2(x, y), color, new Vector2(tx, ty)));
        }

        protected void Flush()
        {
            if (currentTexture == null) return;
            vertexArrayBuffer.ReplaceData(vertices.ToArray(), IntPtr.Zero);
            int[] indices = new int[index * 6];

            for (int i = 0; i < index; i++) //Compute indices
            {
                int idx = i * 6;
                int inst = i * 4;
                indices[idx] = (ushort)inst;
                indices[idx + 1] = (ushort)(inst + 1);
                indices[idx + 2] = (ushort)(inst + 2);
                indices[idx + 3] = (ushort)(inst);
                indices[idx + 4] = (ushort)(inst + 2);
                indices[idx + 5] = (ushort)(inst + 3);
            }

            indexBuffer.ReplaceData(indices, IntPtr.Zero);
            currentTexture.Use(pipeline);
            indexBuffer.DrawIndexed(vertexArrayBuffer, index);
            vertices.Clear();
            index = 0;
            currentTexture = null;
        }

        protected void CheckTextureFlush(Texture texture)
        {
            if(currentTexture == null)
            {
                currentTexture = texture;
                return;
            }

            if(texture != currentTexture || index >= MAX_BATCH_SIZE)
            {
                Flush();
            }

            currentTexture = texture;
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
