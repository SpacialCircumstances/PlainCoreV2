using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IDisplayList: IDisposable
    {
        void Draw(IResourceSet resourceSet);
        void Draw(IResourceSet resourceSet, int elements);
    }
}
