using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics.Shapes
{
    /// <summary>
    /// A triangle shape.
    /// </summary>
    public class Triangle : IShape
    {
        public Triangle(VertexPositionColor vertex1, VertexPositionColor vertex2, VertexPositionColor vertex3)
        {
            if (vertices.Length != 3) throw new ArgumentOutOfRangeException();
            this.vertices = new VertexPositionColor[3] { vertex1, vertex2, vertex3 };
        }

        private readonly VertexPositionColor[] vertices;
        private readonly static int[] INDICES = { 0, 1, 2 };

        public int[] GetIndices()
        {
            return INDICES;
        }

        public VertexPositionColor[] GetVertices()
        {
            return vertices;
        }
    }
}
