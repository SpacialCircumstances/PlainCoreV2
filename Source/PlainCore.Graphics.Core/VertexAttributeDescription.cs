using OpenGL;

namespace PlainCore.Graphics.Core
{
    public struct VertexAttributeDescription
    {
        public readonly string Name;
        public readonly int Size;
        public readonly VertexAttribType AttributeType;
        public readonly bool Normalized;
        public readonly int Stride;
        public readonly int Offset;

        public VertexAttributeDescription(string name, int size, VertexAttribType attributeType, bool normalized, int stride, int offset)
        {
            Name = name;
            Size = size;
            AttributeType = attributeType;
            Normalized = normalized;
            Stride = stride;
            Offset = offset;
        }
    }
}
