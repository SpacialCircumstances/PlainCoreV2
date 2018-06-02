using PlainCore.Graphics.Core;
using System;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A single sprite that can be rendered.
    /// </summary>
    public struct SpriteRenderItem: IComparable<SpriteRenderItem>
    {


        public VertexPositionColorTexture LT;
        public VertexPositionColorTexture RT;
        public VertexPositionColorTexture RD;
        public VertexPositionColorTexture LD;
        public Texture Texture;

        public SpriteRenderItem(VertexPositionColorTexture lT, VertexPositionColorTexture rT, VertexPositionColorTexture rD, VertexPositionColorTexture lD, Texture texture)
        {
            LT = lT;
            RT = rT;
            RD = rD;
            LD = lD;
            Texture = texture;
        }

        public int CompareTo(SpriteRenderItem other)
        {
            return (int)Texture.InternalTexture.Handle - (int)other.Texture.InternalTexture.Handle;
        }
    }
}
