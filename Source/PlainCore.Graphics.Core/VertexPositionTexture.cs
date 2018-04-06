using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Vertex containing a 2D position and texture coordinates.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionTexture
    {
        public Vector2 Position;
        public Vector2 TextureCoordinates;

        public const uint Size = 16;

        /// <summary>
        /// Create a Vertex with position and texture coordinates.
        /// </summary>
        /// <param name="position">Position component</param>
        /// <param name="textureCoordinates">Texture coordinates component</param>
        public VertexPositionTexture(Vector2 position, Vector2 textureCoordinates)
        {
            Position = position;
            TextureCoordinates = textureCoordinates;
        }
    }
}
