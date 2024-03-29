﻿using PlainCore.Graphics.Core;
using PlainCore.Graphics.Text;
using System;
using System.Numerics;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Static class for transforming sprites to vertices.
    /// </summary>
    public static class SpriteDrawer
    {
        public static SpriteRenderItem Draw(Sprite sprite, int layer = 0)
        {
            return Draw(sprite.Texture, sprite.Color, sprite.Position.X, sprite.Position.Y, sprite.Size.X, sprite.Size.Y, sprite.Rotation, sprite.Origin.X, sprite.Origin.Y, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation, float originX, float originY, int layer = 0)
        {
            return Draw(texture, color, x, y, width, height, rotation, originX, originY, 0, 0, 1, 1, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, float x, float y, float width, float height, float rotation, int layer = 0)
        {
            return Draw(texture, Color4.White, x, y, width, height, rotation, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation, int layer = 0)
        {
            return Draw(texture, color, x, y, width, height, rotation, 0.5f, 0.5f, 0, 0, 1, 1, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, float x, float y, float width, float height, int layer = 0)
        {
            return Draw(texture, Color4.White, x, y, width, height, 0, 0, 1, 1, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, float x, float y, int layer = 0)
        {
            return Draw(texture, Color4.White, x, y, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, Color4 color, float x, float y, int layer = 0)
        {
            return Draw(texture, color, x, y, texture.Texture.Width, texture.Texture.Height, 0, 0, 1, 1, layer);
        }

        public static SpriteRenderItem Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float texX1, float texY1, float texX2, float texY2, int layer)
        {
            float w = width;
            float h = height;

            float lowerX = texture.Rectangle.Position.X + (texX1 * texture.Rectangle.End.X);
            float upperX = texX2 * texture.Rectangle.End.X;
            float lowerY = texY2 * texture.Rectangle.End.Y; //LowerY and UpperY swapped because of image format
            float upperY = texture.Rectangle.Position.Y + (texY1 * texture.Rectangle.End.Y);

            return new SpriteRenderItem()
            {
                LD = new VertexPositionColorTexture(new Vector2(x, y), color, new Vector2(lowerX, lowerY)),
                LT = new VertexPositionColorTexture(new Vector2(x, y + h), color, new Vector2(lowerX, upperY)),
                RD = new VertexPositionColorTexture(new Vector2(x + w, y), color, new Vector2(upperX, lowerY)),
                RT = new VertexPositionColorTexture(new Vector2(x + w, y + h), color, new Vector2(upperX, upperY)),
                Texture = texture.Texture,
                Layer = layer
            };
        }

        public static SpriteRenderItem Draw(ITexture texture, Color4 color, float x, float y, float width, float height, float rotation, float originX, float originY, float texX1, float texY1, float texX2, float texY2, int layer)
        {
            if (rotation == 0)
            {
                return Draw(texture, color, x, y, width, height, texX1, texY1, texX2, texY2, layer);
            }

            float lowerX = texture.Rectangle.Position.X + (texX1 * texture.Rectangle.End.X);
            float upperX = texX2 * texture.Rectangle.End.X;
            float lowerY = texY2 * texture.Rectangle.End.Y; //LowerY and UpperY swapped because of image format
            float upperY = texture.Rectangle.Position.Y + (texY1 * texture.Rectangle.End.Y);

            float sin = (float)Math.Sin(rotation);
            float cos = (float)Math.Cos(rotation);

            float m11 = cos;
            float m12 = sin;
            float m21 = -sin;
            float m22 = cos;

            float ox = originX * width;
            float oy = originY * height;

            var ldx = (-ox * m11) + (-oy * m12) + (2 * x);
            var ldy = (-ox * m21) + (-oy * m22) + (2 * y);
            var lux = (-ox * m11) + ((height - oy) * m12) + (2 * x);
            var luy = (-ox * m21) + ((height - oy) * m22) + (2 * y);
            var rux = ((width - ox) * m11) + (-oy * m12) + (2 * x);
            var ruy = ((width - ox) * m21) + (-oy * m22) + (2 * y);
            var rdx = ((width - ox) * m11) + ((height - oy) * m12) + (2 * x);
            var rdy = ((width - ox) * m21) + ((height - oy) * m22) + (2 * y);

            return new SpriteRenderItem()
            {
                LD = new VertexPositionColorTexture(new Vector2(ldx, ldy), color, new Vector2(lowerX, lowerY)),
                LT = new VertexPositionColorTexture(new Vector2(lux, luy), color, new Vector2(lowerX, upperY)),
                RT = new VertexPositionColorTexture(new Vector2(rdx, rdy), color, new Vector2(upperX, upperY)),
                RD = new VertexPositionColorTexture(new Vector2(rux, ruy), color, new Vector2(upperX, lowerY)),
                Texture = texture.Texture,
                Layer = layer
            };
        }

        public static SpriteRenderItem[] DrawString(Font font, string text, Color4 color, float x, float y, int layer = 0, float scale = 1f)
        {
            return font.DrawString(text, color, x, y, layer, scale);
        }

        public static SpriteRenderItem[] DrawString(Font font, string text, float x, float y, int layer = 0, float scale = 1f)
        {
            return font.DrawString(text, x, y, layer, scale);
        }
    }
}
