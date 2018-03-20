using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition3Texture
    {
        public Vector3 Position;
        public Vector2 TextureCoordinates;

        public const uint Size = 20;

        public VertexPosition3Texture(Vector3 position, Vector2 textureCoordinates)
        {
            Position = position;
            TextureCoordinates = textureCoordinates;
        }
    }
}
