using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IChangeableDisplayList<T>: IDisplayList
    {
        void SetIndices(int[] indices);
        void SetIndices(int[] indices, int elementCount);
        void SetIndicesFromPointer(IntPtr pointer, int length);
        void SetVertices(T[] vertices);
        void SetVerticesFromPointer(IntPtr pointer, int length);
    }
}
