using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IRenderTarget
    {
        void Clear(Color4 color);
        void Draw(IDrawable drawable);
    }
}
