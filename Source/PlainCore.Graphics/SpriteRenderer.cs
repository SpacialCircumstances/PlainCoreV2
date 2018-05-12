using OpenGL;
using PlainCore.Graphics.Core;
using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class SpriteRenderer : IRenderer<VertexPositionColorTexture>
    {
        private const int MAX_BATCH_SIZE = 1024;

        private readonly FastList<SpriteRenderItem> spriteItems = new FastList<SpriteRenderItem>(128);

        private int[] indices;

        private readonly VertexPositionColorTexture[] vertexArray = new VertexPositionColorTexture[MAX_BATCH_SIZE * 4];

        public SpriteRenderer()
        {
            WriteIndices();
        }

        protected void WriteIndices()
        {
            indices = new int[MAX_BATCH_SIZE * 6];

            for (int i = 0; i < (MAX_BATCH_SIZE / 4); i++)
            {
                int offset = i * 4;
                int index = i * 6;
                indices[index] = offset;
                indices[index + 1] = offset + 1;
                indices[index + 2] = offset + 2;
                indices[index + 3] = offset + 1;
                indices[index + 4] = offset + 3;
                indices[index + 5] = offset + 2;
            }
        }
        
        public void Begin()
        {
            spriteItems.Clear();
        }

        public void Draw(Sprite sprite)
        {
            Draw(sprite.Texture, sprite.Color, sprite.Position.X, sprite.Position.Y, sprite.Size.X, sprite.Size.Y, sprite.Rotation, sprite.Origin.X, sprite.Origin.Y);
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
            float w = width;
            float h = height;

            float lowerX = texture.Rectangle.Position.X + (texX1 * texture.Rectangle.End.X);
            float upperX = texX2 * texture.Rectangle.End.X;
            float lowerY = texture.Rectangle.Position.Y + (texY1 * texture.Rectangle.End.Y);
            float upperY = texY2 * texture.Rectangle.End.Y;

            spriteItems.Add(new SpriteRenderItem()
            {
                LD = new VertexPositionColorTexture(new Vector2(x, y), color, new Vector2(lowerX, upperY)),
                LT = new VertexPositionColorTexture(new Vector2(x, y + h), color, new Vector2(lowerX, lowerY)),
                RD = new VertexPositionColorTexture(new Vector2(x + w, y), color, new Vector2(upperX, lowerY)),
                RT = new VertexPositionColorTexture(new Vector2(x + w, y + h), color, new Vector2(upperX, upperY)),
                Texture = texture.Texture
            });
        }

        public void Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation, float originX, float originY, float texX1, float texY1, float texX2, float texY2)
        {
            if (rotation == 0)
            {
                Draw(texture, color, x, y, width, height, texX1, texY1, texX2, texY2);
            }

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

            spriteItems.Add(new SpriteRenderItem()
            {
                LT = new VertexPositionColorTexture(new Vector2(ldx, ldy), color, new Vector2(lowerX, lowerY)),
                RT = new VertexPositionColorTexture(new Vector2(rux, ruy), color, new Vector2(upperX, lowerY)),
                LD = new VertexPositionColorTexture(new Vector2(rdx, rdy), color, new Vector2(upperX, upperY)),
                RD = new VertexPositionColorTexture(new Vector2(lux, luy), color, new Vector2(lowerX, upperY)),
                Texture = texture.Texture
            });
        }

        public void End(Action<IntPtr, int, int[], Texture> callback)
        {
            if (spriteItems.Count == 0) return;

            //Sort sprites by texture
            Array.Sort(spriteItems.Buffer, 0, spriteItems.Count);

            var batchItems = spriteItems.Buffer;

            int index = 0;
            int count = spriteItems.Count;
            unsafe
            {
                while (count > 0)
                {
                    int itemsForBatch = (count <= MAX_BATCH_SIZE) ? count : MAX_BATCH_SIZE;
                    Texture texture = null;

                    fixed (VertexPositionColorTexture* varrayPointer = vertexArray)
                    {
                        for (int i = 0; i < itemsForBatch; i++)
                        {
                            var currentItem = batchItems[i];

                            if (currentItem.Texture != texture)
                            {
                                //Flush
                                if (index != 0)
                                {
                                    callback.Invoke(new IntPtr(varrayPointer), index, indices, texture);
                                }

                                index = 0;
                                texture = currentItem.Texture;
                            }

                            *(varrayPointer + index) = currentItem.LT;
                            *(varrayPointer + index + 1) = currentItem.RT;
                            *(varrayPointer + index + 2) = currentItem.LD;
                            *(varrayPointer + index + 3) = currentItem.RD;
                            index += 4;
                        }

                        callback.Invoke(new IntPtr(varrayPointer), index, indices, texture);
                    }

                    count -= itemsForBatch;
                }
            }
        }

        public uint VertexSize => VertexPositionColorTexture.Size;

        public PrimitiveType Primitive => PrimitiveType.Triangles;

        public ShaderPipeline Shader => null;

        public VertexAttributeDescription[] VertexAttributes => null;
    }
}
