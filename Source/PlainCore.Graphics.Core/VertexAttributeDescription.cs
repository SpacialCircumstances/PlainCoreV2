using OpenGL;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Describes an attribute in a vertex
    /// </summary>
    public struct VertexAttributeDescription
    {
        /// <summary>
        /// Name of attribute in GLSL shader
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Component count
        /// </summary>
        public readonly int Size;

        /// <summary>
        /// Type of components
        /// </summary>
        public readonly VertexAttribType AttributeType;

        /// <summary>
        /// Data is normalized
        /// </summary>
        public readonly bool Normalized;

        /// <summary>
        /// Size of full vertex
        /// </summary>
        public readonly int VertexSize;

        /// <summary>
        /// Offset from beginning of vertex instance memory
        /// </summary>
        public readonly int Offset;

        /// <summary>
        /// Creates a new attribute description
        /// </summary>
        /// <param name="name">Name of attribute in GLSL shader</param>
        /// <param name="size">Component count</param>
        /// <param name="attributeType">Type of components</param>
        /// <param name="normalized">Data is normalized</param>
        /// <param name="vertexSize">Size of full vertex</param>
        /// <param name="offset">Offset from beginning of vertex instance memory</param>
        public VertexAttributeDescription(string name, int size, VertexAttribType attributeType, bool normalized, int vertexSize, int offset)
        {
            Name = name;
            Size = size;
            AttributeType = attributeType;
            Normalized = normalized;
            VertexSize = vertexSize;
            Offset = offset;
        }
    }
}
