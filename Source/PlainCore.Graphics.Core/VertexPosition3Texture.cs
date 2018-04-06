using System.Numerics;
using System.Runtime.InteropServices;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Vertex containing a 3D position and texture coordinates.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition3Texture
    {
        public Vector3 Position;
        public Vector2 TextureCoordinates;

        public const uint Size = 20;

        /// <summary>
        /// Create a vertex with a 3D position and texture coordinates.
        /// </summary>
        /// <param name="position">Position component</param>
        /// <param name="textureCoordinates">Texture coordinates component</param>
        public VertexPosition3Texture(Vector3 position, Vector2 textureCoordinates)
        {
            Position = position;
            TextureCoordinates = textureCoordinates;
        }
    }
}
