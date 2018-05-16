using OpenGL;
using PlainCore.Graphics.Core;
using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class SpriteRenderer
    {
        

        public uint VertexSize => VertexPositionColorTexture.Size;

        public PrimitiveType Primitive => PrimitiveType.Triangles;

        public ShaderPipeline Shader => null;

        public VertexAttributeDescription[] VertexAttributes => null;
    }
}
