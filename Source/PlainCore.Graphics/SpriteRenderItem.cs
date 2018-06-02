using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A single sprite that can be rendered.
    /// </summary>
    public struct SpriteRenderItem
    {
        public VertexPositionColorTexture LT;
        public VertexPositionColorTexture RT;
        public VertexPositionColorTexture RD;
        public VertexPositionColorTexture LD;
        public Texture Texture;
        public int Layer;

        public SpriteRenderItem(VertexPositionColorTexture lT, VertexPositionColorTexture rT, VertexPositionColorTexture rD, VertexPositionColorTexture lD, Texture texture, int layer = 0)
        {
            LT = lT;
            RT = rT;
            RD = rD;
            LD = lD;
            Texture = texture;
            Layer = layer;
        }
    }
}
