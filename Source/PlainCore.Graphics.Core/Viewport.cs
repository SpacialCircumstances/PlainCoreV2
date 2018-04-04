using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    public class Viewport
    {
        protected int left, right, bottom, top;

        public Viewport(int left, int right, int bottom, int top)
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
