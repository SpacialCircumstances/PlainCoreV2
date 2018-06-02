using OpenGL;
using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A interface describing the settings for a display list.
    /// </summary>
    public interface IRenderPipelineSettings: IDisposable
    {
        uint VertexSize { get; }

        PrimitiveType Primitive { get; }

        ShaderPipeline Shader { get; }

        VertexAttributeDescription[] VertexAttributes { get; }
    }
}
