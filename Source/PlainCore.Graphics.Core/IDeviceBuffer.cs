using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    interface IDeviceBuffer<T>: IBindable
    {
        void CopyData(T[] data);
        void CopyRawData(byte[] data);
        void CopyRawData(IntPtr pointer, uint elements);
    }
}
