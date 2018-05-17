using OpenGL;
using PlainCore.Graphics.Core;

namespace PlainCore.Graphics
{
    public interface IRenderPipelineSettings
    {
        uint VertexSize { get; }

        PrimitiveType Primitive { get; }

        ShaderPipeline Shader { get; }

        VertexAttributeDescription[] VertexAttributes { get; }
    }
}
