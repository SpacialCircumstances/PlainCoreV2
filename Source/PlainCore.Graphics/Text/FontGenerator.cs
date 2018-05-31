using SharpFont;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Text
{
    /// <summary>
    /// Allows generating fonts from font files.
    /// </summary>
    public static class FontGenerator
    {
        private const int MAX_BITMAP_WIDTH = 1024;
        private const int HORIZONTAL_OFFSET = 2; //Reduces artifacts when scaling up

        /// <summary>
        /// Generate a font from a font file.
        /// </summary>
        /// <param name="fontFileName">Name of the font file</param>
        /// <param name="fontSize">Size of the bitmap font</param>
        /// <param name="lowerChar">The lowest character to render</param>
        /// <param name="upperChar">The hightest character</param>
        /// <returns>A description for the font</returns>
        public static FontDescription GenerateFont(string fontFileName, uint fontSize, int lowerChar = 33, int upperChar = 127)
        {
            var font = new FontFace(File.OpenRead(fontFileName));

            var glyphs = new Dictionary<char, GlyphLayout>();

            var currentX = 0;
            var currentY = 0;
            var maxY = 0;

            for (int i = lowerChar; i < upperChar; i++)
            {
                char c = (char)i;
                var (w, h) = GetGlyphSize(font, c, (int)fontSize);

                //Glyph would be to big
                if (currentX + w + HORIZONTAL_OFFSET > MAX_BITMAP_WIDTH)
                {
                    currentY += maxY;
                    maxY = 0;
                    currentX = 0;
                }

                //Glyph is biggest in its line
                if (h > maxY)
                {
                    maxY = h;
                }

                var layout = new GlyphLayout(c, (currentX, currentY), (w, h));
                currentX += w + HORIZONTAL_OFFSET;

                glyphs.Add(c, layout);
            }

            var finalHeight = currentY + maxY;

            var bitmap = new Image<Rgba32>(MAX_BITMAP_WIDTH, finalHeight);

            bitmap.Mutate(ctx =>
            {
                foreach (var glyph in glyphs)
                {
                    var img = RenderGlyph(font, glyph.Key, (int)fontSize);
                    var pos = new Point(glyph.Value.BitmapPosition.X, glyph.Value.BitmapPosition.Y);
                    ctx.DrawImage(img, 1f, pos);
                }
            });

            return new FontDescription(bitmap, fontSize, glyphs);
        }

        private static unsafe Image<Rgba32> RenderGlyph(FontFace face, char character, int size)
        {
            var glyph = face.GetGlyph(character, size);
            var (w, h) = GetGlyphSize(face, character, size);

            var surface = new Surface
            {
                Bits = Marshal.AllocHGlobal(w * h),
                Width = w,
                Height = h,
                Pitch = w
            };

            //Clear the memory
            var stuff = (byte*)surface.Bits;
            for (int i = 0; i < surface.Width * surface.Height; i++)
                *stuff++ = 0;

            glyph.RenderTo(surface);

            int len = w * h;
            var rawData = new byte[len];
            Marshal.Copy(surface.Bits, rawData, 0, len);
            var pixelData = ConvertToPixels(rawData);

            return Image.LoadPixelData<Rgba32>(pixelData, w, h);
        }

        private static (int, int) GetGlyphSize(FontFace face, char character, int size)
        {
            var glyph = face.GetGlyph(character, size);
            return (glyph.RenderWidth, glyph.RenderHeight);
        }

        private static byte[] ConvertToPixels(byte[] rawData)
        {
            var len = rawData.Length;
            var pixelData = new byte[len * 4];
            int index = 0;
            for (int i = 0; i < len; i++)
            {
                byte c = rawData[i];
                pixelData[index++] = 255;
                pixelData[index++] = 255;
                pixelData[index++] = 255;
                pixelData[index++] = c;
            }

            return pixelData;
        }
    }
}