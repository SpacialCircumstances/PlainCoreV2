using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    interface IBindable: IDisposable
    {
        void Bind();
        void Unbind();
    }
}
