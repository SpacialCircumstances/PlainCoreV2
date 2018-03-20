using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionTexture
    {
        public Vector2 Position;
        public Vector2 TextureCoordinates;

        public const uint Size = 16;

        public VertexPositionTexture(Vector2 position, Vector2 textureCoordinates)
        {
            Position = position;
            TextureCoordinates = textureCoordinates;
        }
    }
}
