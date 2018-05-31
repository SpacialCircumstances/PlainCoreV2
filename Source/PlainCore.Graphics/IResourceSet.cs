using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Contains resources for a drawing operation.
    /// </summary>
    public interface IResourceSet
    {
        IEnumerable<IUniform> GetUniforms();
    }
}
