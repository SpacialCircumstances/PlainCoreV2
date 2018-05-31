using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

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
