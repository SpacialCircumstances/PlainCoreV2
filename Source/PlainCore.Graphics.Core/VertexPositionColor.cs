using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColor
    {
        public Vector2 Position;
        public Color4 Color;

        public VertexPositionColor(Vector2 position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
