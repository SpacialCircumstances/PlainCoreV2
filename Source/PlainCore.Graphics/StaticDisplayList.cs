using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public class StaticDisplayList<T> : AbstractDisplayList<T> where T: struct
    {
        public static StaticDisplayList<T> FromRenderer(IRenderer<T> renderer)
        {
            return new StaticDisplayList<T>(renderer.Vertices, renderer.Indices, renderer.VertexSize, renderer.Primitive, renderer.Shader, renderer.VertexAttributes);
        }

        private readonly VertexArrayBuffer<T> vertexArrayBuffer;
        private readonly IndexBuffer<T> indexBuffer;
        private readonly VertexArrayObject<T> vertexArrayObject;
        private readonly int elementCount;

        public StaticDisplayList(T[] vertices, int[] indices, uint vertexSize, OpenGL.PrimitiveType primitiveType = OpenGL.PrimitiveType.Triangles, ShaderPipeline pipeline = null, VertexAttributeDescription[] attributes = null): base(vertexSize, pipeline, attributes)
        {
            vertexArrayBuffer = new VertexArrayBuffer<T>(vertexSize, OpenGL.BufferUsage.StaticDraw, primitiveType);
            indexBuffer = new IndexBuffer<T>(OpenGL.BufferUsage.StaticDraw);
            vertexArrayObject = new VertexArrayObject<T>(vertexArrayBuffer, this.pipeline, vertexAttributes);
            vertexArrayBuffer.Bind();
            indexBuffer.Bind();
            vertexArrayBuffer.CopyData(vertices);
            indexBuffer.CopyData(indices);
            indexBuffer.Unbind();
            vertexArrayBuffer.Unbind();
            elementCount = indices.Length;
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

            indexBuffer.DrawIndexed(vertexArrayBuffer, elementCount);

            vertexArrayBuffer.Unbind();
            indexBuffer.Unbind();
            vertexArrayObject.Unbind();
            pipeline.Unbind();
        }
    }
}
