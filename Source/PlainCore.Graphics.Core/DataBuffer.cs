using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    class DataBuffer<T>
    {
        public DataBuffer(uint initialSize, IDeviceBuffer<T> buffer)
        {
            this.buffer = buffer;
            this.buffer.CopyRawData(IntPtr.Zero, initialSize);
        }

        private IDeviceBuffer<T> buffer;
    }
}
