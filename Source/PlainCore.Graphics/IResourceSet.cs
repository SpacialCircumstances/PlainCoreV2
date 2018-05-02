using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IResourceSet
    {
        IEnumerable<IUniform> GetUniforms();
    }
}
