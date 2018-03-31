using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public interface IBindable: IDisposable
    {
        void Bind();
        void Unbind();
    }
}
