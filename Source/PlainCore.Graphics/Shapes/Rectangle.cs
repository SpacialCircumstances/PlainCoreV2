using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics.Shapes
{
    /// <summary>
    /// A rectangular shape.
    /// </summary>
    public class Rectangle : IShape
    {
        public Rectangle(Vector2 position, Vector2 size, Color4 color)
        {
            this.position = position;
            this.size = size;
            this.color = color;
            ComputeVertices();
        }

        public Rectangle(Vector2 position, Vector2 size): this(position, size, Color4.White)
        {

        }

        private Vector2 position;
        private Vector2 size;
        private Color4 color;
        private VertexPositionColor[] vertices;
        private int[] indices = { 0, 1, 2, 0, 1, 3 };

        public Vector2 Position
        {
            set
            {
                position = value;
                ComputeVertices();
            }
            get => position;
        }

        public Vector2 Size
        {
            set
            {
                size = value;
                ComputeVertices();
            }
            get => size;
        }

        public Color4 Color
        {
            set
            {
                color = value;
                ComputeVertices();
            }
            get => color;
        }

        private void ComputeVertices()
        {
            vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(position, color);
            vertices[1] = new VertexPositionColor(position + size, color);
            vertices[2] = new VertexPositionColor(new Vector2(position.X, position.Y + size.Y), color);
            vertices[3] = new VertexPositionColor(new Vector2(position.X + size.X, position.Y), color);

        }

        public int[] GetIndices()
        {
            return indices;
        }

        public VertexPositionColor[] GetVertices()
        {
            return vertices;
        }
    }
}
