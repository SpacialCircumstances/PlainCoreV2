using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    interface IDeviceBuffer<T>: IBindable
    {
        void CopyData(T[] data);
        void CopyRawData(byte[] data);
        void CopyRawData(IntPtr pointer, uint size);
        void ReplaceData(byte[] data, IntPtr offset);
        void ReplaceData(IntPtr data, uint size, IntPtr offset);
    }
}
