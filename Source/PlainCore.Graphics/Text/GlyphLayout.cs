﻿namespace PlainCore.Graphics.Text
{
    /// <summary>
    /// Contains data for rendering a glyph.
    /// </summary>
    public struct GlyphLayout
    {
        public GlyphLayout(char character, (int x, int y) bitmapPosition, (int w, int h) size)
        {
            Character = character;
            BitmapPosition = bitmapPosition;
            GlyphSize = size;
        }

        public char Character { get; set; }
        public (int X, int Y) BitmapPosition { get; set; }
        public (int W, int H) GlyphSize { get; set; }
    }
}