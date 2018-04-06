using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Vertex containing a 2D position, a color and texture coordinates.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColorTexture
    {
        public Vector2 Position;
        public Color4 Color;
        public Vector2 TextureCoordinates;

        public const uint Size = Color4.Size + 16;

        /// <summary>
        /// Create a vertex with position, color and texture coordinates.
        /// </summary>
        /// <param name="position">Position component</param>
        /// <param name="color">Color component</param>
        /// <param name="textureCoordinates">Texture coordinates component</param>
        public VertexPositionColorTexture(Vector2 position, Color4 color, Vector2 textureCoordinates)
        {
            Position = position;
            Color = color;
            TextureCoordinates = textureCoordinates;
        }
    }
}
