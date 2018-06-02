using PlainCore.Graphics.Core;
using System.Collections.Generic;

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
