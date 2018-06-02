using System;
using PlainCore.System;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A part of a texture.
    /// </summary>
    public class TextureRegion : ITexture
    {
        public TextureRegion(Texture texture, FloatRectangle rectangle)
        {
            this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
            this.rectangle = rectangle;
        }

        protected readonly Texture texture;
        protected readonly FloatRectangle rectangle;

        public Texture Texture => texture;

        public FloatRectangle Rectangle => rectangle;
    }
}
