using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics.Shapes
{
    public class FreeShape : IShape
    {
        public FreeShape(int[] indices, VertexPositionColor[] vertices)
        {
            Indices = indices;
            Vertices = vertices;
        }

        public int[] Indices { get; set; }
        public VertexPositionColor[] Vertices { get; set; }

        public int[] GetIndices()
        {
            return Indices;
        }

        public VertexPositionColor[] GetVertices()
        {
            return Vertices;
        }
    }
}
