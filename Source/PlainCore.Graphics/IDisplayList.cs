using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Interface for a display list.
    /// </summary>
    public interface IDisplayList: IDisposable
    {
        /// <summary>
        /// Draw the full display list with the resource set.
        /// </summary>
        /// <param name="resourceSet">Resource set</param>
        void Draw(IResourceSet resourceSet);

        /// <summary>
        /// Draw a certain number of elements in the display list with the resource set.
        /// </summary>
        /// <param name="resourceSet">Resource set</param>
        /// <param name="elements">Elements to draw</param>
        void Draw(IResourceSet resourceSet, int elements);
    }
}
