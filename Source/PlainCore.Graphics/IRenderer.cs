using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IRenderer<T>
    {
        int[] Indices { get; }
        T[] Vertices { get; }
        uint VertexSize { get; }
        OpenGL.PrimitiveType Primitive { get; }
        ShaderPipeline Shader { get; }
        VertexAttributeDescription[] VertexAttributes { get; }
    }
}
