using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics.Text
{
    /// <summary>
    /// A class that defines a font that can be used for drawing text.
    /// </summary>
    public class Font: IDisposable
    {
        /// <summary>
        /// Create the font from a file containing a serialized description.
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>The font</returns>
        public static Font FromDescriptionFile(string filename)
        {
            return new Font(FontDescription.FromDescriptionFile(filename));
        }

        /// <summary>
        /// Generate the font from a font file.
        /// </summary>
        /// <param name="fontFileName">Filename</param>
        /// <param name="fontSize">The size of the font in pixels</param>
        /// <returns>The font</returns>
        public static Font GenerateFromFont(string fontFileName, uint fontSize)
        {
            return new Font(FontGenerator.GenerateFont(fontFileName, fontSize));
        }

        /// <summary>
        /// Create a font.
        /// </summary>
        /// <param name="description">Font description</param>
        public Font(FontDescription description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));

            this.texture = Texture.FromImage(description.Bitmap);
            this.description = description;
        }

        private readonly Texture texture;
        private readonly FontDescription description;

        /// <summary>
        /// Generate the sprites for drawing a string.
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="x">Position x</param>
        /// <param name="y">Position y</param>
        /// <param name="scale">Scale of the text (default 1)</param>
        /// <returns>A list of sprites</returns>
        public SpriteRenderItem[] DrawString(string text, float x, float y, int layer = 0, float scale = 1f)
        {
            return DrawString(text, Color4.White, x, y, layer, scale);
        }

        /// <summary>
        /// Generate the sprites for drawing a string.
        /// </summary>
        /// <param name="text">The text to draw</param>
        /// <param name="color">The color of the text</param>
        /// <param name="x">Position x</param>
        /// <param name="y">Position y</param>
        /// <param name="scale">Scale of the text (default 1)</param>
        /// <returns>A list of sprites</returns>
        public SpriteRenderItem[] DrawString(string text, Color4 color, float x, float y, int layer = 0, float scale = 1f)
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
                renderItems[i] = SpriteDrawer.Draw(texture, color, currentX, y, glyph.GlyphSize.W * scale, glyph.GlyphSize.H * scale, x1, y1, x2, y2, layer);
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
