using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlainCore.Graphics.Test
{
    public class RenderTextureTest
    {
        [Fact]
        public void TestCreate()
        {
            //Dummy window for context initialization
            var window = new OpenGLWindow(200, 200, "Test", false, false);

            var tex = new RenderTexture(800, 600);
          
            window.Close();
            window.Dispose();
        }
    }
}
