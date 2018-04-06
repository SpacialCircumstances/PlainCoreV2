using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Vertex containing a 3D position, a color and texture coordinates.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition3ColorTexture
    {
        public Vector3 Position;
        public Color4 Color;
        public Vector2 TextureCoordinates;

        public const uint Size = Color4.Size + 20;

        /// <summary>
        /// Create a vertex with a 3D position, a color and texture coordinates.
        /// </summary>
        /// <param name="position">Position component</param>
        /// <param name="color">Color component</param>
        /// <param name="textureCoordinates">Texture coordinates component</param>
        public VertexPosition3ColorTexture(Vector3 position, Color4 color, Vector2 textureCoordinates)
        {
            Position = position;
            Color = color;
            TextureCoordinates = textureCoordinates;
        }
    }
}
