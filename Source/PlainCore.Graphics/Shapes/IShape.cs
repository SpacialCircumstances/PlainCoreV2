using PlainCore.Graphics.Core;

namespace PlainCore.Graphics.Shapes
{
    /// <summary>
    /// Interface every shape must implement.
    /// </summary>
    public interface IShape
    {
        VertexPositionColor[] GetVertices();
        int[] GetIndices();
    }
}
