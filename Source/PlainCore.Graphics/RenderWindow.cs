using OpenGL;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public class RenderWindow : OpenGLWindow, IRenderTarget
    {
        public RenderWindow(uint width = 800, uint height = 600, string title = "PlainCore", bool resizable = false) : base(width, height, title, resizable)
        {
        }

        public void Clear(float r = 0f, float g = 0f, float b = 0f, float a = 0f)
        {
            Gl.ClearColor(r, g, b, a);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Draw(IDrawable drawable)
        {
            drawable.Draw(this, null);
        }
    }
}
