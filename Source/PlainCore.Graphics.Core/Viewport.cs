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

        /// <summary>
        /// Creates a viewport from the desired points
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="top">Top value</param>
        /// <param name="right">Right value</param>
        /// <param name="bottom">Bottom value</param>
        public Viewport(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

        /// <summary>
        /// Sets this viewport as the current one
        /// </summary>
        public void Set()
        {
            Gl.Viewport(left, bottom, right - left, top - bottom);
        }
    }
}
