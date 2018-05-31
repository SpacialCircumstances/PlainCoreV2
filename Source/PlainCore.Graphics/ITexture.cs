using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Text;

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
