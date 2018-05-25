using PlainCore.Graphics.Core;
using PlainCore.System;
using System;
using System.Numerics;

namespace PlainCore.Graphics
{
    public class SpriteBatcher
    {
        private readonly FastList<SpriteRenderItem> spriteItems = new FastList<SpriteRenderItem>(128);

        private SpriteRenderItem[] cachedRenderItems = null;

        public SpriteRenderItem[] Sprites => cachedRenderItems;

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
                return;
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

        public SpriteRenderItem[] End()
        {
            cachedRenderItems = new SpriteRenderItem[spriteItems.Count];
            Array.Copy(spriteItems.Buffer, cachedRenderItems, spriteItems.Count);
            return cachedRenderItems;
        }
    }
}
