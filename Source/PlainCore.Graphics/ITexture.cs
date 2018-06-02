using PlainCore.System;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A drawable texture.
    /// </summary>
    public interface ITexture
    {
        Texture Texture { get; }
        FloatRectangle Rectangle { get; }
    }
}
