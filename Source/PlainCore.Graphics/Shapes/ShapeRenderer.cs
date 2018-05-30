﻿using OpenGL;
using PlainCore.Graphics.Core;
using System.Collections.Generic;
using System.Numerics;

namespace PlainCore.Graphics.Shapes
{
    public class ShapeRenderer: IRenderPipelineSettings
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

        public void End(IChangeableDisplayList<VertexPositionColor> displayList)
        {
            var (verts, inds) = End();
            displayList.SetIndices(inds);
            displayList.SetVertices(verts);
        }

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
