using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public abstract class AbstractDisplayList<T> : IDisplayList where T: struct
    {
        protected ShaderPipeline pipeline;
        protected readonly VertexAttributeDescription[] vertexAttributes;
        protected readonly uint vertexSize;


        protected AbstractDisplayList(uint vertexSize, ShaderPipeline pipeline = null, VertexAttributeDescription[] vertexAttributes = null)
        {
            this.pipeline = pipeline ?? new ShaderPipeline(
                DefaultShader.FromType(typeof(T), ShaderType.Vertex),
                DefaultShader.FromType(typeof(T), ShaderType.Fragment));

            this.vertexAttributes = vertexAttributes ?? DefaultVertexDefinition.FromType(typeof(T));
            this.vertexSize = vertexSize;
        }

        public abstract void Draw(IResourceSet resourceSet);

        public virtual void Dispose()
        {
            pipeline.Dispose();
        }
    }
}
