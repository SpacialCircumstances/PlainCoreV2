using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IDisplayList
    {
        void Draw(IResourceSet resourceSet);
    }
}
