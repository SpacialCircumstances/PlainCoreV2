using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public class RenderWindow : OpenGLWindow
    {
        public RenderWindow(uint width = 800, uint height = 600, string title = "PlainCore", bool resizable = false) : base(width, height, title, resizable)
        {
        }
    }
}
