using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IChangeableDisplayList<T>: IDisplayList
    {
        void SetIndices(int[] indices);
        void SetVertices(T[] vertices);
    }
}
