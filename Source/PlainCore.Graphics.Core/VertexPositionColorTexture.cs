using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.Graphics.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColorTexture
    {
        public Vector2 Position;
        public Color4 Color;
        public Vector2 TextureCoordinates;

        public const uint Size = Color4.Size + 16;

        public VertexPositionColorTexture(Vector2 position, Color4 color, Vector2 textureCoordinates)
        {
            Position = position;
            Color = color;
            TextureCoordinates = textureCoordinates;
        }
    }
}
