using System;

namespace PlainCore.Graphics
{
    /// <summary>
    /// Interface for a display list with changeable data.
    /// </summary>
    /// <typeparam name="T">Vertex type</typeparam>
    public interface IChangeableDisplayList<T>: IDisplayList
    {
        void SetIndices(int[] indices);
        void SetIndices(int[] indices, int elementCount);
        void SetIndicesFromPointer(IntPtr pointer, int length);
        void SetVertices(T[] vertices);
        void SetVerticesFromPointer(IntPtr pointer, int length);
    }
}
