using OpenGL;
using PlainCore.Graphics.Core;
using System.Collections.Generic;
using System.Numerics;

namespace PlainCore.Graphics.Shapes
{
    public class ShapeRenderer: IRenderer<VertexPositionColor>
    {
        private int index;
        private int elementCount;
        private readonly List<VertexPositionColor> vertices = new List<VertexPositionColor>();
        private readonly List<int> indices = new List<int>();

        public void Begin()
        {
            index = 0;
            vertices.Clear();
            indices.Clear();
        }

        public void Render(IShape shape)
        {
            var shapeIndices = shape.GetIndices();
            var shapeVertices = shape.GetVertices();

            vertices.AddRange(shapeVertices);

            foreach (var i in shapeIndices)
            {
                indices.Add(index + i);
            }

            elementCount += shapeIndices.Length;
            index += shapeVertices.Length;
        }

        public void Render(IShape shape, Matrix4x4 transform)
        {
            var shapeIndices = shape.GetIndices();
            var shapeVertices = shape.GetVertices();

            foreach (var vertex in shapeVertices)
            {
                var transformed = Vector2.Transform(vertex.Position, transform);
                vertices.Add(new VertexPositionColor(transformed, vertex.Color));
            }

            foreach (var i in shapeIndices)
            {
                indices.Add(index + i);
            }

            elementCount += shapeIndices.Length;
            index += shapeVertices.Length;
        }

        public void End()
        {
            Indices = indices.ToArray();
            onIndicesChanged?.Invoke(Indices);
            Vertices = vertices.ToArray();
            onVerticesChanged?.Invoke(Vertices);
        }

        public void UseDisplayList(IChangeableDisplayList<VertexPositionColor> displayList)
        {
            if (displayList != null)
            {
                onVerticesChanged = new VerticesUpdate(verts => displayList.SetVertices(verts));
                onIndicesChanged = new IndicesUpdate(indices => displayList.SetIndices(indices));
            }
            else
            {
                onVerticesChanged = null;
                onIndicesChanged = null;
            }
        }

        public void RemoveDisplayList()
        {
            onVerticesChanged = null;
            onIndicesChanged = null;
        }

        protected delegate void VerticesUpdate(VertexPositionColor[] vertices);
        protected delegate void IndicesUpdate(int[] Indices);

        protected VerticesUpdate onVerticesChanged;
        protected IndicesUpdate onIndicesChanged;

        public int[] Indices { get; protected set; }
        public VertexPositionColor[] Vertices { get; protected set; }

        public uint VertexSize => VertexPositionColor.Size;

        public PrimitiveType Primitive => PrimitiveType.Triangles;

        public ShaderPipeline Shader => null; //Use default shader for vertex type

        public VertexAttributeDescription[] VertexAttributes => null; //Use default vertex attributes
    }
}
