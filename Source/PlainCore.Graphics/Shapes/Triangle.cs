using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics.Shapes
{
    public class Triangle : IShape
    {
        public Triangle(VertexPositionColor[] vertices)
        {
            if (vertices.Length != 3) throw new ArgumentOutOfRangeException();
            this.vertices = vertices;
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
