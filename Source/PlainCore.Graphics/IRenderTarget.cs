using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IRenderTarget
    {
        void Clear();
        void Draw(IDrawable drawable);
    }
}
