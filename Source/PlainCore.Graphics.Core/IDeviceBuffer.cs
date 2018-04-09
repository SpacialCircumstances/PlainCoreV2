using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A buffer for GPU data.
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public interface IDeviceBuffer<T>: IBindable
    {
        /// <summary>
        /// Copy an array of primitives. Buffer must be bound.
        /// </summary>
        /// <param name="data">Data array</param>
        void CopyData(T[] data);

        /// <summary>
        /// Copy a raw array of bytes. Buffer must be bound.
        /// </summary>
        /// <param name="data">Byte array</param>
        void CopyRawData(byte[] data);

        /// <summary>
        /// Copy data from a memory location. Can be used to initialize an empty buffer. Buffer must be bound.
        /// </summary>
        /// <param name="pointer">Pointer to the data</param>
        /// <param name="size">Size of the GPU memory to allocate, in bytes</param>
        void CopyRawData(IntPtr pointer, uint size);

        /// <summary>
        /// Replace a part of the buffer data. Buffer must be bound.
        /// </summary>
        /// <param name="data">Data for replacement</param>
        /// <param name="offset">Location of the data that gets replaced</param>
        void ReplaceData(byte[] data, IntPtr offset);

        /// <summary>
        /// Replace a part of the buffer data. Buffer must be bound.
        /// </summary>
        /// <param name="data">Pointer to replacement data</param>
        /// <param name="size">Size of replacement data</param>
        /// <param name="offset">Location of the data that gets replaced</param>
        void ReplaceData(IntPtr data, uint size, IntPtr offset);
    }
}
