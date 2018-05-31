using OpenGL;
using PlainCore.Graphics.Core;
using System.Collections.Generic;
using System.Numerics;

namespace PlainCore.Graphics.Shapes
{
    /// <summary>
    /// A renderer for rendering arbitrary shapes.
    /// </summary>
    public class ShapeRenderer: IRenderPipelineSettings
    {
        private int index;
        private int elementCount;
        private readonly List<VertexPositionColor> vertices = new List<VertexPositionColor>();
        private readonly List<int> indices = new List<int>();

        /// <summary>
        /// Begin the batching process.
        /// </summary>
        public void Begin()
        {
            index = 0;
            vertices.Clear();
            indices.Clear();
        }

        /// <summary>
        /// Add the shape to the batch.
        /// </summary>
        /// <param name="shape">The shape to render</param>
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

        /// <summary>
        ///Add the shape to the batch after applying a transform.
        /// </summary>
        /// <param name="shape">The shape to render</param>
        /// <param name="transform">The transform to use</param>
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

        /// <summary>
        /// End the batching and upload the data to a display list.
        /// </summary>
        /// <param name="displayList">The display list that should receive the data</param>
        public void End(IChangeableDisplayList<VertexPositionColor> displayList)
        {
            var (verts, inds) = End();
            displayList.SetIndices(inds);
            displayList.SetVertices(verts);
        }

        /// <summary>
        /// End the batching and get the drawing data.
        /// </summary>
        /// <returns>A tuple containing the vertex and index data.</returns>
        public (VertexPositionColor[], int[]) End()
        {
            return (vertices.ToArray(), indices.ToArray());
        }

        public void Dispose()
        {
            internalShader?.Dispose();
        }

        public uint VertexSize => VertexPositionColor.Size;

        public PrimitiveType Primitive => PrimitiveType.Triangles;

        private ShaderPipeline internalShader;

        public ShaderPipeline Shader
        {
            get
            {
                return internalShader ?? (internalShader = new ShaderPipeline(
                        DefaultShader.FromType(typeof(VertexPositionColor), Core.ShaderType.Vertex),
                        DefaultShader.FromType(typeof(VertexPositionColor), Core.ShaderType.Fragment)));
            }
        }

        private VertexAttributeDescription[] internalVertexAttributes;

        public VertexAttributeDescription[] VertexAttributes
        {
            get
            {
                return internalVertexAttributes ?? (internalVertexAttributes = DefaultVertexDefinition.FromType(typeof(VertexPositionColor)));
            }
        }
    }
}
