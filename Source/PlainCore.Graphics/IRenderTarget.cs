using PlainCore.Graphics.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    /// <summary>
    /// A target for drawing.
    /// </summary>
    public interface IRenderTarget
    {
        Framebuffer Framebuffer { get; }
        Viewport Viewport { get; }
        Matrix4x4 WorldMatrix { get; }
    }
}
