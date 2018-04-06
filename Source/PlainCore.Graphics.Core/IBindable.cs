using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Describes a bindable GPU resource.
    /// </summary>
    public interface IBindable: IDisposable
    {
        /// <summary>
        /// Bind this resource.
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbind this resource.
        /// </summary>
        void Unbind();
    }
}
