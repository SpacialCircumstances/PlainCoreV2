using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    interface IDeviceBuffer: IBindable
    {
        void CopyData();
        void CopyRawData(byte[] data);
        void CopyRawData(IntPtr pointer);
    }
}
