﻿using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    static class Verify
    {
        public static void VerifyResourceCreated(uint handle)
        {
            if (handle == 0)
            {
                var err = Gl.GetError();
                throw new InvalidOperationException($"OpenGL resource creation failed: {err}");
            }
        }

        public static void VerifyShaders(ShaderResource[] shaders)
        {

        }
    }
}
