using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Window
{
    public struct ContextSettings
    {
        public ContextSettings(int majorGLVersion, int minorGLVersion, bool coreProfile)
        {
            MajorGLVersion = majorGLVersion;
            MinorGLVersion = minorGLVersion;
            CoreProfile = coreProfile;
        }

        public int MajorGLVersion;
        public int MinorGLVersion;
        public bool CoreProfile;
    }
}
