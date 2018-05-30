using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics.Text
{
    public class Font: IDisposable
    {
        public static Font FromDescriptionFile(string filename)
        {
            return new Font(FontDescription.FromDescriptionFile(filename));
        }

        public static Font GenerateFromFont(string fontFileName, uint fontSize)
        {
            return new Font(FontGenerator.GenerateFont(fontFileName, fontSize));
        }

        public Font(FontDescription description)
        {
            this.texture = Texture.FromImage(description.Bitmap);
            this.description = description;
        }

        private readonly Texture texture;
        private readonly FontDescription description;

        public SpriteRenderItem[] DrawString(string text, float x, float y, float scale = 1f)
        {
            return DrawString(text, Color4.White, x, y, scale);
        }

        public SpriteRenderItem[] DrawString(string text, Color4 color, float x, float y, float scale = 1f)
        {
            SpriteRenderItem[] renderItems = new SpriteRenderItem[text.Length];

            float currentX = x;
            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                var glyph = description.GetGlyph(character);
                var fx = 1f / texture.Width;
                var fy = 1f / texture.Height;
                var x1 = fx * (float)glyph.BitmapPosition.X;
                var y1 = fy * (float)glyph.BitmapPosition.Y;
                var x2 = x1 + (fx * (float)glyph.GlyphSize.W);
                var y2 = y1 + (fy * (float)glyph.GlyphSize.H);
                renderItems[i] = SpriteBatcher.Draw(texture, color, currentX, y, glyph.GlyphSize.W * scale, glyph.GlyphSize.H * scale, x1, y1, x2, y2);
                currentX += glyph.GlyphSize.W * scale;
            }

            return renderItems;
        }

        public void Dispose()
        {
            texture.Dispose();
        }
    }
}
