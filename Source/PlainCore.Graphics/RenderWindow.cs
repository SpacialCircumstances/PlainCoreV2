using OpenGL;
using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class RenderWindow : OpenGLWindow, IRenderTarget
    {
        public RenderWindow(uint width = 800, uint height = 600, string title = "PlainCore", bool resizable = false) : base(width, height, title, resizable)
        {
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            view = new View(new Viewport(0, (int)height, (int)width, 0), new Vector2(0, 0), new Vector2(width, height));
        }

        protected Framebuffer framebuffer = Framebuffer.GetDefault();
        protected View view;

        public Framebuffer Framebuffer => framebuffer;
        public Viewport Viewport => view.Viewport;
        public Matrix4x4 WorldMatrix => view.WorldMatrix;

        public View View
        {
            get => view;
            set
            {
                view = value;
                view.Viewport.Set();
            }
        }

        public void Clear(Color4 color)
        {
            view.Viewport.Set();
            framebuffer.Bind();
            framebuffer.Clear(color);
        }
    }
}
