using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;

namespace PlainCore.Graphics.Text
{
    public class FontDescription
    {
        private readonly Dictionary<char, GlyphLayout> glyphs;

        public FontDescription(Image<Rgba32> bitmap, Dictionary<char, GlyphLayout> glyphs, uint fontSize)
        {
            this.Bitmap = bitmap;
            this.glyphs = glyphs;
            this.FontSize = fontSize;
        }

        public uint FontSize { get; }
        public Image<Rgba32> Bitmap { get; }

        public GlyphLayout GetGlyph(char c)
        {
            var found = glyphs.TryGetValue(c, out var glyph);

            if (!found)
            {
                throw new ArgumentException($"Character {c} not found in Font");
            }

            return glyph;
        }

        public GlyphLayout this[char c]
        {
            get
            {
                return GetGlyph(c);
            }
        }
    }
}
