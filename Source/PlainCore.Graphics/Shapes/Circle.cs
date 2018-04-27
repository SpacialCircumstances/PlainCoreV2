using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Shapes
{
    public class Circle: IShape
    {
        private int points;
        private Vector2 position;
        private float radius;
        private Color4 color;

        private int[] indices;
        private VertexPositionColor[] vertices;

        public Circle(int points, Vector2 position, float radius, Color4 color)
        {
            this.points = points;
            this.position = position;
            this.radius = radius;
            this.color = color;
            CalculateElements();
        }

        public int[] GetIndices()
        {
            return indices;
        }

        public VertexPositionColor[] GetVertices()
        {
            return vertices;
        }

        protected void CalculateElements()
        {
            VertexPositionColor center = new VertexPositionColor(position, color);
            vertices = new VertexPositionColor[points + 1];
            indices = new int[points * 3];
            vertices[0] = center;

            double fraction = (2 * Math.PI) / points;

            for (int i = 1; i <= points; i++) //Starts at 1 because 0 is center vertex
            {
                float x = (float)Math.Sin(fraction * (i - 1)) * radius;
                float y = (float)Math.Cos(fraction * (i - 1)) * radius;
                vertices[i] = new VertexPositionColor(position + new Vector2(x, y), color);
                int indexBaseIndex = (i - 1) * 3;
                indices[indexBaseIndex] = 0;
                indices[indexBaseIndex + 1] = i;
                int lastVertexIndex = i + 1;
                if (i == points) lastVertexIndex = 1;
                indices[indexBaseIndex + 2] = lastVertexIndex;
            }
        }
    }
}
