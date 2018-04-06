using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Vertex containing a 2D position and a color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColor
    {
        public Vector2 Position;
        public Color4 Color;

        public const uint Size = Color4.Size + 8;

        /// <summary>
        /// Create a vertex with a position and a color.
        /// </summary>
        /// <param name="position">Position component</param>
        /// <param name="color">Color component</param>
        public VertexPositionColor(Vector2 position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
