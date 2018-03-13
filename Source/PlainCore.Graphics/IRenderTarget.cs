using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IRenderTarget
    {
        void Clear(float r, float g, float b, float a);
        void Draw(IDrawable drawable);
    }
}
