using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IDrawable
    {
        void Draw(IRenderTarget target, RenderStates states);
    }
}
