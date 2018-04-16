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
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void Clear(Color4 color)
        {
            Gl.ClearColor(color.R, color.G, color.B, color.A);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
