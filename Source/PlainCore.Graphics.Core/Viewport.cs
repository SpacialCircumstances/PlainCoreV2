﻿using OpenGL;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// Describes a OpenGL Viewport
    /// </summary>
    public class Viewport
    {
        protected int left, right, bottom, top;

        /// <summary>
        /// Left border of the viewport.
        /// </summary>
        public int Left => left;

        /// <summary>
        /// Right border of the viewport.
        /// </summary>
        public int Right => right;

        /// <summary>
        /// Bottom border of the viewport.
        /// </summary>
        public int Bottom => bottom;

        /// <summary>
        /// Top border of the viewport.
        /// </summary>
        public int Top => top;

        /// <summary>
        /// Height of the viewport.
        /// </summary>
        public int Height => top - bottom;

        /// <summary>
        /// Width of the viewport.
        /// </summary>
        public int Width => right - left;

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
