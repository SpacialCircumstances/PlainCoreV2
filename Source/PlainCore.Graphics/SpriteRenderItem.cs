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

        public int CompareTo(SpriteRenderItem other)
        {
            return (int)Texture.InternalTexture.Handle - (int)other.Texture.InternalTexture.Handle;
        }
    }
}
