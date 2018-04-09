using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Core
{
    /// <summary>
    /// A depth buffer that can be attached to a framebuffer.
    /// </summary>
    public class DepthBuffer : IBindable
    {
        /// <summary>
        /// Creates a new depth buffer with specific size.
        /// </summary>
        /// <param name="width">Width in pixels of the buffer</param>
        /// <param name="height">Height in pixels of the buffer</param>
        public DepthBuffer(int width, int height)
        {
            Handle = Gl.GenRenderbuffer();
            Verify.VerifyResourceCreated(Handle);
            Bind();
            Gl.RenderbufferStorage(RenderbufferTarget.Renderbuffer, InternalFormat.DepthComponent, width, height);
            Unbind();
        }

        /// <summary>
        /// OpenGL handle.
        /// </summary>
        public readonly uint Handle;

        public void Bind()
        {
            Gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
        }

        public void Dispose()
        {
            Gl.DeleteRenderbuffers(Handle);
        }

        public void Unbind()
        {
            Gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }
    }
}
