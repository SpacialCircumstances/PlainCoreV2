using OpenGL;
using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    public interface IRenderPipelineSettings: IDisposable
    {
        uint VertexSize { get; }

        PrimitiveType Primitive { get; }

        ShaderPipeline Shader { get; }

        VertexAttributeDescription[] VertexAttributes { get; }
    }
}
