using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Describes a OpenGL Viewport
    /// </summary>
    public class Viewport
    {
        protected int left, right, bottom, top;

        public Viewport(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

        public void Set()
        {
            Gl.Viewport(left, bottom, right - left, top - bottom);
        }
    }
}
