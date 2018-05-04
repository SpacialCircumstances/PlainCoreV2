using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public class DynamicDisplayList<T> : AbstractDisplayList<T>, IChangeableDisplayList<T> where T : struct
    {
        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;

        //Current indices count
        private int elements;

        public DynamicDisplayList(uint vertexSize, OpenGL.PrimitiveType primitiveType = OpenGL.PrimitiveType.Triangles, ShaderPipeline pipeline = null, VertexAttributeDescription[] vertexAttributes = null) : base(pipeline, vertexAttributes)
        {
            vertexArrayBuffer = new VertexArrayBuffer<T>(vertexSize, OpenGL.BufferUsage.DynamicDraw, primitiveType);
            indexBuffer = new IndexBuffer<T>(OpenGL.BufferUsage.DynamicDraw);
            vertexArrayObject = new VertexArrayObject<T>(vertexArrayBuffer, this.pipeline, this.vertexAttributes);
        }

        public void SetVertices(T[] vertices)
        {
            vertexArrayBuffer.Bind();
            vertexArrayBuffer.CopyData(vertices);
            vertexArrayBuffer.Unbind();
        }

        public void SetIndices(int[] indices)
        {
            elements = indices.Length;
            indexBuffer.Bind();
            indexBuffer.CopyData(indices);
            indexBuffer.Unbind();
        }

        public override void Draw(IResourceSet resourceSet)
        {
            pipeline.Bind();
            vertexArrayObject.Bind();
            indexBuffer.Bind();
            vertexArrayBuffer.Bind();

            foreach (var uniform in resourceSet.GetUniforms())
            {
                uniform.Set(pipeline);
            }

            indexBuffer.DrawIndexed(vertexArrayBuffer, elements);

            vertexArrayBuffer.Unbind();
            indexBuffer.Unbind();
            vertexArrayObject.Unbind();
            pipeline.Unbind();
        }
    }
}
